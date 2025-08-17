using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class LevelableLoggerExtensions
{
	/// <param name="logger"></param>
	/// <param name="itemType"></param>
	/// <param name="itemName"></param>
	/// <param name="oldLevel"></param>
	/// <param name="newLevel"></param>
	[LoggerMessage(
		EventId = EventIds.LevelableLevelChangedOnNamedItem,
		Level = LogLevel.Information,
		Message = "{ItemType} {ItemName} advanced from level {OldLevel} to {NewLevel}.")]
	public static partial void LogLevelableChanged(this ILogger logger, string itemType, string itemName, int oldLevel,
		int newLevel);

	/// <summary>
	///     Logs the level change of a generic levelable item, such as a skill or character, including the item type,
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="itemType"></param>
	/// <param name="oldLevel"></param>
	/// <param name="newLevel"></param>
	[LoggerMessage(
		EventId = EventIds.LevelableLevelChanged,
		Level = LogLevel.Information,
		Message = "{ItemType} advanced from level {OldLevel} to {NewLevel}.")]
	public static partial void LogLevelableChangedGeneric(this ILogger logger, string itemType, int oldLevel,
		int newLevel);

	/// <summary>
	///     Logs the change in points for a levelable item, such as a skill or character, including the item type,
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="itemType"></param>
	/// <param name="itemName"></param>
	/// <param name="pointsGained"></param>
	/// <param name="totalPoints"></param>
	[LoggerMessage(
		EventId = EventIds.LevelablePointsChangedOnNamedItem,
		Level = LogLevel.Debug,
		Message = "{ItemType} {ItemName} gained {PointsGained} points. Total: {TotalPoints}.")]
	public static partial void LogLevelablePointsChanged(this ILogger logger, string itemType, string itemName,
		int pointsGained, int totalPoints);

	/// <summary>
	///     Logs the change in points for a generic levelable item, such as a skill or character, including the item type,
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="itemType"></param>
	/// <param name="pointsGained"></param>
	/// <param name="totalPoints"></param>
	[LoggerMessage(
		EventId = EventIds.LevelablePointsChanged,
		Level = LogLevel.Debug,
		Message = "{ItemType} gained {PointsGained} points. Total: {TotalPoints}.")]
	public static partial void LogLevelablePointsChangedGeneric(this ILogger logger, string itemType, int pointsGained,
		int totalPoints);
}
