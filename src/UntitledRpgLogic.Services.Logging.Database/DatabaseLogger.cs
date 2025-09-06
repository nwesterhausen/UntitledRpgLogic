using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Infrastructure.Data;

namespace UntitledRpgLogic.Services.Logging.Database;

/// <summary>
///     An implementation of ILogger that writes log entries to the database.
///     It uses a background task and a queue to batch log writes for performance.
/// </summary>
public sealed class DatabaseLogger : ILogger, IDisposable
{
	/// <summary>
	/// A `CancellationTokenSource` used to signal cancellation to the background processing task.
	/// </summary>
	private readonly CancellationTokenSource cancellationTokenSource = new();

	/// <summary>
	/// The category name associated with this logger instance, typically used to group log messages.
	/// </summary>
	private readonly string categoryName;

	/// <summary>
	/// A thread-safe queue for storing log entries before they are processed and written to the database.
	/// </summary>
	private readonly BlockingCollection<LogEntry> logQueue = new(new ConcurrentQueue<LogEntry>());

	/// <summary>
	/// The background task responsible for processing log entries from the queue and writing them to the database.
	/// </summary>
	private readonly Task processingTask;

	/// <summary>
	/// The `IServiceProvider` used to resolve dependencies, such as the `RpgDbContext`, for database operations.
	/// </summary>
	private readonly IServiceProvider serviceProvider;

	/// <summary>
	///	The provider that created this logger, used to access the scope provider for BeginScope.
	/// </summary>
	private readonly IExternalScopeProvider scopeProvider;

	///  <summary>
	/// 		Constructor for the DatabaseLogger. This will start the background processing task.
	///  </summary>
	///  <param name="categoryName"></param>
	///  <param name="serviceProvider"></param>
	///  <param name="scopeProvider"></param>
	public DatabaseLogger(string categoryName, IServiceProvider serviceProvider,IExternalScopeProvider scopeProvider)
	{
		this.categoryName = categoryName;
		this.serviceProvider = serviceProvider;
		this.scopeProvider = scopeProvider;
		this.processingTask = Task.Run(this.ProcessLogQueue, this.cancellationTokenSource.Token);
	}

	/// <inheritdoc />
	public void Dispose()
	{
		this.cancellationTokenSource.Cancel();
		this.logQueue.CompleteAdding();
		try
		{
			// Wait for the processing task to finish handling any remaining items.
			this.processingTask.Wait(TimeSpan.FromSeconds(5));
		}
		catch (OperationCanceledException) { }

		this.cancellationTokenSource.Dispose();
		this.logQueue.Dispose();
	}

	/// <inheritdoc />
	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
	{
		if (!this.IsEnabled(logLevel))
		{
			return;
		}

		// Extract parameters from the state. TState is typically a collection of KeyValuePair<string, object>.
		var parameters = new Dictionary<string, object?>();
		if (state is IReadOnlyCollection<KeyValuePair<string, object?>> statePairs)
		{
			foreach (var pair in statePairs)
			{
				parameters[pair.Key] = pair.Value;
			}
		}
		Ulid? entityId = null;

		// Extract EntityId from the current scope, if available.
		this.scopeProvider.ForEachScope((scopeObject, _) =>
		{
			if (scopeObject is IReadOnlyCollection<KeyValuePair<string, object?>> scopePairs)
			{
				foreach (var pair in scopePairs)
				{
					// If we find an EntityId in the scope, we extract it.
					if (pair.Key.Equals("EntityId", StringComparison.OrdinalIgnoreCase) && pair.Value is Ulid id)
					{
						entityId = id;
					}
					// We can also merge scope parameters into our log parameters for a richer JSON blob.
					parameters[pair.Key] = pair.Value;
				}
			}
		}, state);

		ArgumentNullException.ThrowIfNull(formatter, nameof(formatter));
		var message = formatter(state, exception);
		var parametersJson = parameters.Count > 0 ? JsonSerializer.Serialize(parameters) : null;

		var logEntry = new LogEntry(
			(int)logLevel,
			eventId.Id,
			message,
			this.categoryName,
			entityId,
			parametersJson
		);

		// Add to queue for background processing. This is a non-blocking call.
		this.logQueue.Add(logEntry);
	}

	/// <inheritdoc />
	public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

	/// <inheritdoc />
	public IDisposable? BeginScope<TState>(TState state) where TState : notnull => this.scopeProvider.Push(state);

	private async Task ProcessLogQueue()
	{
		while (!this.cancellationTokenSource.Token.IsCancellationRequested)
		{
			try
			{
				// Take all available items from the queue, or wait for one to arrive.
				var logEntries = new List<LogEntry> { this.logQueue.Take(this.cancellationTokenSource.Token) };

				// Drain any remaining items to process them in a batch.
				while (this.logQueue.TryTake(out var additionalEntry))
				{
					logEntries.Add(additionalEntry);
				}

				if (logEntries.Count != 0)
				{
					// Create a new scope to resolve a DbContext instance.
					// This is crucial for thread safety.
					using var scope = this.serviceProvider.CreateScope();
					var dbContext = scope.ServiceProvider.GetRequiredService<RpgDbContext>();

					await dbContext.LogEntries.AddRangeAsync(logEntries, this.cancellationTokenSource.Token).ConfigureAwait(false);
					await dbContext.SaveChangesAsync(this.cancellationTokenSource.Token).ConfigureAwait(false);
				}
			}
			catch (OperationCanceledException)
			{
				// Graceful shutdown
				break;
			}
			// Catch all exceptions to prevent the background task from crashing.
#pragma warning disable CA1031
			catch (Exception ex)
#pragma warning restore CA1031
			{
				// Log to console as a fallback if the database logger itself fails.
				Console.WriteLine($"Error writing logs to database: {ex.Message}");
				// Avoid fast-spinning on repeated errors
				await Task.Delay(5000, this.cancellationTokenSource.Token).ConfigureAwait(false);
			}
		}
	}
}
