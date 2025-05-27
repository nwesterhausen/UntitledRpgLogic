using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Stat;

public abstract partial class StatBase
{
    /// <summary>
    ///     Event ID for stat creation.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public const int STAT_CREATED = 1000;

    /// <summary>
    ///     Event ID for when a stat is linked to another stat.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public const int STAT_LINKED = 1001;

    /// <summary>
    ///     Event ID for when a stat is unlinked from another stat.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public const int STAT_UNLINKED = 1002;

    /// <summary>
    ///     Event ID for when a stat value changes.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public const int STAT_VALUE_CHANGED = 1005;

    /// <summary>
    ///     Event ID for when a stat is attempted to be changed illegally.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public const int STAT_ILLEGAL_CHANGE = 1099;

    protected internal ILogger _logger;

    [LoggerMessage(
        EventId = STAT_VALUE_CHANGED,
        Level = LogLevel.Debug,
        Message = "{modifier}{delta} {statName}")]
    private partial void LogStatChanged(int delta, string statName, string modifier = "");

    [LoggerMessage(
        EventId = STAT_CREATED,
        Level = LogLevel.Debug,
        Message = "Created {stat}")]
    private partial void LogStatCreated(StatBase stat);

    /// <summary>
    ///     Logs an illegal stat change attempt.
    /// </summary>
    /// <param name="statName">The name of the stat.</param>
    /// <param name="attemptedAction">The attempted action.</param>
    [LoggerMessage(
        EventId = STAT_ILLEGAL_CHANGE,
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
        EventId = STAT_LINKED,
        Level = LogLevel.Debug,
        Message = "Linked {minorName} to {majorName} with ratio {ratio}")]
    protected partial void LogStatLinked(string majorName, string minorName, double ratio);

    /// <summary>
    ///     Logs when a stat is unlinked.
    /// </summary>
    /// <param name="majorName">The name of the major stat.</param>
    /// <param name="minorName">The name of the minor stat.</param>
    [LoggerMessage(
        EventId = STAT_UNLINKED,
        Level = LogLevel.Debug,
        Message = "Unlinked {minorName} from {majorName}")]
    protected partial void LogStatUnlinked(string majorName, string minorName);
}