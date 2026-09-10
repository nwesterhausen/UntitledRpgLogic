using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Services.Logging.Database;

/// <summary>
///     A provider that creates instances of the DatabaseLogger.
/// </summary>
/// <remarks>
///     Constructor for the DatabaseLoggerProvider.
/// </remarks>
/// <param name="serviceProvider"></param>
[ProviderAlias("Database")]
public class DatabaseLoggerProvider(IServiceProvider serviceProvider) : ILoggerProvider
{
	private readonly ConcurrentDictionary<string, DatabaseLogger> loggers = new();
	private readonly IServiceProvider serviceProvider = serviceProvider;
	private bool disposed;

	internal IExternalScopeProvider ScopeProvider { get; } = new LoggerExternalScopeProvider();

	/// <inheritdoc />
	public ILogger CreateLogger(string categoryName) =>
		this.loggers.GetOrAdd(categoryName, name => new DatabaseLogger(name, this.serviceProvider, this.ScopeProvider));

	/// <inheritdoc />
	public void Dispose()
	{
		// Dispose of unmanaged resources.
		this.Dispose(true);
		// Suppress finalization.
		GC.SuppressFinalize(this);
	}

	/// <summary>
	///     Disposes the provider and its underlying loggers.
	/// </summary>
	/// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (this.disposed)
		{
			return;
		}

		if (disposing)
		{
			// Dispose managed state (managed objects).
			foreach (var logger in this.loggers.Values)
			{
				logger.Dispose();
			}

			this.loggers.Clear();
		}

		this.disposed = true;
	}
}
