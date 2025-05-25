namespace UntitledRpgLogic.Stat;

using Microsoft.Extensions.Logging;

public abstract partial class StatBase {
  protected internal ILogger _logger;
  
  [LoggerMessage(
    EventId = LoggingEvents.STAT_VALUE_CHANGED,
    Level = LogLevel.Debug,
    Message = "{modifier}{delta} {statName}")]
  private partial void LogStatChanged(int delta, string statName, string modifier = "");

  [LoggerMessage(
    EventId = LoggingEvents.STAT_CREATED,
    Level = LogLevel.Debug,
    Message = "Created {stat}")]
  private partial void LogStatCreated(StatBase stat);

  /// <summary>
  ///   Logs an illegal stat change attempt.
  /// </summary>
  /// <param name="statName">The name of the stat.</param>
  /// <param name="attemptedAction">The attempted action.</param>
  [LoggerMessage(
    EventId = LoggingEvents.STAT_ILLEGAL_CHANGE,
    Level = LogLevel.Warning,
    Message = "Minor Stat {statName} had an attempt to {attemptedAction} which was canceled.")]
  protected partial void LogIllegalStatChangeAttempt(string statName, string attemptedAction);

  /// <summary>
  ///   Logs when a stat is linked.
  /// </summary>
  /// <param name="majorName">The name of the major stat.</param>
  /// <param name="minorName">The name of the minor stat.</param>
  /// <param name="ratio">The ratio of contribution.</param>
  [LoggerMessage(
    EventId = LoggingEvents.STAT_LINKED,
    Level = LogLevel.Debug,
    Message = "Linked {minorName} to {majorName} with ratio {ratio}")]
  protected partial void LogStatLinked(string majorName, string minorName, double ratio);

  /// <summary>
  ///   Logs when a stat is unlinked.
  /// </summary>
  /// <param name="majorName">The name of the major stat.</param>
  /// <param name="minorName">The name of the minor stat.</param>
  [LoggerMessage(
    EventId = LoggingEvents.STAT_UNLINKED,
    Level = LogLevel.Debug,
    Message = "Unlinked {minorName} from {majorName}")]
  protected partial void LogStatUnlinked(string majorName, string minorName);
}
