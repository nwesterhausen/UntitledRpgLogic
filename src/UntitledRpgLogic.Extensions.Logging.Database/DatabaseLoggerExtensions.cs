using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Infrastructure.Logging.Database;

/// <summary>
/// Extension methods for setting up the database logger.
/// </summary>
public static class DatabaseLoggerExtensions
{
    /// <summary>
    /// Adds a database logger named 'Database' to the factory.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
    public static ILoggingBuilder AddDatabase(this ILoggingBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DatabaseLoggerProvider>());
        return builder;
    }
}
