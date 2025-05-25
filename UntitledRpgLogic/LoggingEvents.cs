namespace UntitledRpgLogic;

/// <summary>
///   Defines the logging events for the RPG logic.
/// </summary>
/// <remarks>
///   <list type="bullets">
///     <item>
///       <description>1000-1099 Stat-Related Events</description>
///     </item>
///   </list>
/// </remarks>
public static class LoggingEvents {
  /// <summary>
  ///   Event ID for stat creation.
  /// </summary>
  public const int STAT_CREATED = 1000;

  /// <summary>
  ///   Event ID for when a stat is linked to another stat.
  /// </summary>
  public const int STAT_LINKED = 1001;

  /// <summary>
  ///   Event ID for when a stat is unlinked from another stat.
  /// </summary>
  public const int STAT_UNLINKED = 1002;

  /// <summary>
  ///   Event ID for when a stat value changes.
  /// </summary>
  public const int STAT_VALUE_CHANGED = 1005;

  /// <summary>
  ///   Event ID for when a stat is attempted to be changed illegally.
  /// </summary>
  public const int STAT_ILLEGAL_CHANGE = 1099;
}
