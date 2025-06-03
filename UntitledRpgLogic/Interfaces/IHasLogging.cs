using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     For classes that have logging capabilities.
/// </summary>
public interface IHasLogging
{
    /// <summary>
    ///     The logger instance used for logging events and errors.
    /// </summary>
    public ILogger Logger { get; }

    /// <summary>
    ///     Add a log entry with the specified event ID.
    /// </summary>
    /// <remarks>This should not log invalid event ids for your class</remarks>
    /// <param name="eventId">see <see cref="LoggingEventIds" /> for options</param>
    /// <param name="args">additional details to include with the log entry, based on the EventID</param>
    public void LogEvent(EventId eventId, params object?[] args);

    /// <summary>
    ///     Add a log entry with the specified exception and event ID.
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="eventId"></param>
    public void LogError(Exception exception, EventId eventId);
}