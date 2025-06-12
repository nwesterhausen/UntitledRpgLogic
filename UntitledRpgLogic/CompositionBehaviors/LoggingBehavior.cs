using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <summary>
///     Provides logging capabilities via composition.
/// </summary>
public partial class LoggingBehavior : IHasLogging
{
    /// <summary>
    ///     Defines the mapping of event IDs to their expected argument types.
    /// </summary>
    private static readonly Dictionary<int, Type[]> EventArgumentMap = new()
    {
        { EventIds.STAT_CREATED_INT_VALUE, [typeof(IStat)] },
        { EventIds.STAT_VALUE_CHANGED_INT_VALUE, [typeof(int), typeof(string), typeof(string)] },
        { EventIds.STAT_ILLEGAL_CHANGE_INT_VALUE, [typeof(string), typeof(string)] },
        { EventIds.SKILL_POINTS_CHANGED_INT_VALUE, [typeof(string), typeof(string)] },
        { EventIds.STAT_LINKED_INT_VALUE, [typeof(string), typeof(string), typeof(double)] },
        { EventIds.STAT_UNLINKED_INT_VALUE, [typeof(string), typeof(string)] },
        { EventIds.SKILL_CREATED_INT_VALUE, [typeof(string)] },
        { EventIds.SKILL_LEVEL_CHANGED_INT_VALUE, [typeof(string), typeof(int)] }
    };

    /// <summary>
    ///     Reference to the logger in order to use the [LoggerMessage] attribute for logging methods.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    ///     Creates a new instance of the <see cref="LoggingBehavior" /> class with the specified logger.
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public LoggingBehavior(ILogger? logger = null)
    {
        Logger = logger ?? NullLogger<LoggingBehavior>.Instance;
        _logger = Logger;
    }

    /// <inheritdoc />
    public void LogEvent(EventId eventId, params object?[] args)
    {
        LogErrorEvent(null, eventId, args);
    }

    /// <inheritdoc />
    public void LogErrorEvent(Exception? exception, EventId eventId, params object?[] args)
    {
        // Validate the event ID and arguments
        if (!AreValidArguments(eventId.Id, args))
            return;

        // If an exception is provided, log an error and then let the event details be logged.
        if (exception != null) LogError(exception, eventId);

        // Warning-level events
        if (_logger.IsEnabled(LogLevel.Warning))
            switch (eventId.Id)
            {
                case EventIds.STAT_ILLEGAL_CHANGE_INT_VALUE:
                    LogIllegalStatChangeAttempt((string)args[0]!, (string)args[1]!);
                    return;
                // Add other warning-level events here
            }
#if RELEASE
        else
            return;
#endif

        // Info-level events
        if (_logger.IsEnabled(LogLevel.Information))
            switch (eventId.Id)
            {
                case EventIds.SKILL_POINTS_CHANGED_INT_VALUE:
                    LogSkillPointsChanged((string)args[0]!, (string)args[1]!, (string)args[2]!);
                    return;
                case EventIds.STAT_CREATED_INT_VALUE:
                    LogStatCreated((IStat)args[0]!);
                    return;
                case EventIds.SKILL_CREATED_INT_VALUE:
                    _logger.LogInformation(eventId, "Created skill: {skillName}", args[0]);
                    return;
                // Add other Info-level events here
            }
#if RELEASE
        else
            return;
#endif

        // Handle Debug-level events
        if (!_logger.IsEnabled(LogLevel.Debug))
        {
#if DEBUG
    throw new ArgumentOutOfRangeException(nameof(eventId), $"Invalid event ID: {eventId}");
#endif
            return;
        }

        switch (eventId.Id)
        {
            case EventIds.STAT_VALUE_CHANGED_INT_VALUE:
                LogStatChanged((int)args[0]!, (string)args[1]!, (string)args[2]!);
                return;
            case EventIds.STAT_LINKED_INT_VALUE:
                LogStatLinked((string)args[0]!, (string)args[1]!, (double)(args[2] ?? 0));
                return;
            case EventIds.STAT_UNLINKED_INT_VALUE:
                LogStatUnlinked((string)args[0]!, (string)args[1]!);
                return;
            case EventIds.SKILL_LEVEL_CHANGED_INT_VALUE:
                _logger.LogDebug(eventId, "Skill {skillName} level changed to {level}", args[0], args[1]);
                return;
            // Add other Debug-level events here
        }
    }

    /// <inheritdoc />
    public void LogError(Exception exception, EventId eventId)
    {
        const string defaultErrorMessage = "An error occurred. Please reference the EventID for more details.";

        _logger.LogError(eventId, exception, defaultErrorMessage);
    }

    /// <inheritdoc />
    public ILogger Logger { get; }

    /// <summary>
    ///     Log an error with a custom message and exception.
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="message"></param>
    public void LogError(Exception exception, string message)
    {
        _logger.LogError(exception, message);
    }

    /// <summary>
    ///     Validate that the provided arguments match the expected types for the given event ID.
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="args"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentException"></exception>
    private static bool AreValidArguments(int eventId, object?[] args)
    {
#if DEBUG
    if (!EventArgumentMap.TryGetValue(eventId, out var expectedTypes))
        throw new ArgumentOutOfRangeException(nameof(eventId), $"Invalid event ID: {eventId}");

    if (args.Length != expectedTypes.Length)
        throw new ArgumentException(
            $"Event ID {eventId} expects {expectedTypes.Length} arguments, but {args.Length} were provided.");

    for (int i = 0; i < expectedTypes.Length; i++)
    {
        if (args[i] != null && args[i]?.GetType() != expectedTypes[i])
            throw new ArgumentException(
                $"Argument {i} for event ID {eventId} must be of type {expectedTypes[i].Name}, but was {args[i]?.GetType().Name}.");
    }
    return true;
#else
        if (!EventArgumentMap.TryGetValue(eventId, out var expectedTypes))
            return false;
        if (args.Length != expectedTypes.Length)
            return false;
        for (var i = 0; i < expectedTypes.Length; i++)
            if (args[i] != null && args[i]?.GetType() != expectedTypes[i])
                return false;

        return true;
#endif
    }


    /// <summary>
    ///     Logs a stat value change event.
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="statName"></param>
    /// <param name="modifier"></param>
    [LoggerMessage(
        EventId = EventIds.STAT_VALUE_CHANGED_INT_VALUE,
        Level = LogLevel.Debug,
        Message = "{modifier}{delta} {statName}")]
    protected partial void LogStatChanged(int delta, string statName, string modifier = "");

    /// <summary>
    ///     Logs when a stat is created.
    /// </summary>
    /// <param name="stat"></param>
    [LoggerMessage(
        EventId = EventIds.STAT_CREATED_INT_VALUE,
        Level = LogLevel.Debug,
        Message = "Created {stat}")]
    protected partial void LogStatCreated(IStat stat);

    /// <summary>
    ///     Logs an illegal stat change attempt.
    /// </summary>
    /// <param name="statName">The name of the stat.</param>
    /// <param name="attemptedAction">The attempted action.</param>
    [LoggerMessage(
        EventId = EventIds.STAT_ILLEGAL_CHANGE_INT_VALUE,
        Level = LogLevel.Warning,
        Message = "Minor Stat {statName} had an attempt to {attemptedAction} which was canceled.")]
    protected partial void LogIllegalStatChangeAttempt(string statName, string attemptedAction);

    /// <summary>
    ///     Logs when a stat is linked.
    /// </summary>
    /// <param name="majorName">The name of the major stat.</param>
    /// <param name="minorName">The name of the minor stat.</param>
    /// <param name="ratio">The ratio of contribution.</param>
    [LoggerMessage(
        EventId = EventIds.STAT_LINKED_INT_VALUE,
        Level = LogLevel.Debug,
        Message = "Linked {minorName} to {majorName} with ratio {ratio}")]
    protected partial void LogStatLinked(string majorName, string minorName, double ratio);

    /// <summary>
    ///     Logs when a stat is unlinked.
    /// </summary>
    /// <param name="majorName">The name of the major stat.</param>
    /// <param name="minorName">The name of the minor stat.</param>
    [LoggerMessage(
        EventId = EventIds.STAT_UNLINKED_INT_VALUE,
        Level = LogLevel.Debug,
        Message = "Unlinked {minorName} from {majorName}")]
    protected partial void LogStatUnlinked(string majorName, string minorName);

    /// <summary>
    ///     Log when points are added or removed from a skill.
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="pointsChange"></param>
    /// <param name="progress"></param>
    [LoggerMessage(
        EventId = EventIds.SKILL_POINTS_CHANGED_INT_VALUE,
        Level = LogLevel.Debug,
        Message = "{skillName} {pointsChange}: {progress} to next level")]
    protected partial void LogSkillPointsChanged(string skillName, string pointsChange, string progress);
}