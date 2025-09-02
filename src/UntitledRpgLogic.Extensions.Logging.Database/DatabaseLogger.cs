using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Infrastructure.Data;

namespace UntitledRpgLogic.Infrastructure.Logging.Database;

/// <summary>
/// An implementation of ILogger that writes log entries to the database.
/// It uses a background task and a queue to batch log writes for performance.
/// </summary>
public sealed class DatabaseLogger : ILogger, IDisposable
{
    private readonly string _categoryName;
    private readonly IServiceProvider _serviceProvider;
    private readonly BlockingCollection<LogEntry> _logQueue = new(new ConcurrentQueue<LogEntry>());
    private readonly Task _processingTask;
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public DatabaseLogger(string categoryName, IServiceProvider serviceProvider)
    {
        _categoryName = categoryName;
        _serviceProvider = serviceProvider;
        _processingTask = Task.Run(ProcessLogQueue, _cancellationTokenSource.Token);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
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

        var message = formatter(state, exception);
        var parametersJson = parameters.Count > 0 ? JsonSerializer.Serialize(parameters) : null;

        // Ulid? entityId = TryGetEntityIdFromScope(); // Scope support can be added later

        var logEntry = new LogEntry(
            (int)logLevel,
            eventId.Id,
            message,
            _categoryName,
            null, // entityId
            parametersJson
        );

        // Add to queue for background processing. This is a non-blocking call.
        _logQueue.Add(logEntry);
    }

    private async Task ProcessLogQueue()
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            try
            {
                // Take all available items from the queue, or wait for one to arrive.
                var logEntries = new List<LogEntry> { _logQueue.Take(_cancellationTokenSource.Token) };

                // Drain any remaining items to process them in a batch.
                while (_logQueue.TryTake(out var additionalEntry))
                {
                    logEntries.Add(additionalEntry);
                }

                if (logEntries.Any())
                {
                    // Create a new scope to resolve a DbContext instance.
                    // This is crucial for thread safety.
                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<RpgDbContext>();

                    await dbContext.LogEntries.AddRangeAsync(logEntries, _cancellationTokenSource.Token);
                    await dbContext.SaveChangesAsync(_cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown
                break;
            }
            catch (Exception ex)
            {
                // Log to console as a fallback if the database logger itself fails.
                Console.WriteLine($"Error writing logs to database: {ex.Message}");
                // Avoid fast-spinning on repeated errors
                await Task.Delay(5000, _cancellationTokenSource.Token);
            }
        }
    }

    // By default, we log everything from Information level and above. This can be made configurable.
    public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

    // Scope handling can be implemented here if needed. For now, it's not supported.
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _logQueue.CompleteAdding();
        try
        {
            // Wait for the processing task to finish handling any remaining items.
            _processingTask.Wait(TimeSpan.FromSeconds(5));
        }
        catch (OperationCanceledException) { }

        _cancellationTokenSource.Dispose();
        _logQueue.Dispose();
    }
}
