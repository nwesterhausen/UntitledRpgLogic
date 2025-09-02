using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Infrastructure.Logging.Database;

/// <summary>
/// A provider that creates instances of the DatabaseLogger.
/// </summary>
[ProviderAlias("Database")]
public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<string, DatabaseLogger> _loggers = new();

    public DatabaseLoggerProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new DatabaseLogger(name, _serviceProvider));
    }

    public void Dispose()
    {
        foreach (var logger in _loggers.Values)
        {
            logger.Dispose();
        }
        _loggers.Clear();
    }
}
