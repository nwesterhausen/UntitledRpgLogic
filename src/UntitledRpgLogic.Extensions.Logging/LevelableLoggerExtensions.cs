using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class LevelableLoggerExtensions
{
	/// <summary>
	/// Logs a level change for a named levelable item (e.g., a specific skill or character).
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="itemType">The type/category of the item (e.g., "Skill", "Character").</param>
	/// <param name="itemName">The name of the item.</param>
	/// <param name="oldLevel">The level before the change.</param>
	/// <param name="newLevel">The level after the change.</param>
	[LoggerMessage(
		EventId = EventIds.LevelableLevelChangedOnNamedItem,
		Level = LogLevel.Information,
		Message = "{ItemType} {ItemName} advanced from level {OldLevel} to {NewLevel}.")]
	public static partial void LogLevelableChanged(this ILogger logger, string itemType, string itemName, int oldLevel,
		int newLevel);

	/// <summary>
	/// Logs the level change for a generic levelable item (without a specific name).
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="itemType">The type/category of the item (e.g., "Skill", "Character").</param>
	/// <param name="oldLevel">The level before the change.</param>
	/// <param name="newLevel">The level after the change.</param>
	[LoggerMessage(
		EventId = EventIds.LevelableLevelChanged,
		Level = LogLevel.Information,
		Message = "{ItemType} advanced from level {OldLevel} to {NewLevel}.")]
	public static partial void LogLevelableChangedGeneric(this ILogger logger, string itemType, int oldLevel,
		int newLevel);

	/// <summary>
	/// Logs a points change for a named levelable item (e.g., a specific skill or character).
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="itemType">The type/category of the item.</param>
	/// <param name="itemName">The name of the item.</param>
	/// <param name="pointsGained">The number of points gained (or lost if negative).</param>
	/// <param name="totalPoints">The resulting total points after the change.</param>
	[LoggerMessage(
		EventId = EventIds.LevelablePointsChangedOnNamedItem,
		Level = LogLevel.Debug,
		Message = "{ItemType} {ItemName} gained {PointsGained} points. Total: {TotalPoints}.")]
	public static partial void LogLevelablePointsChanged(this ILogger logger, string itemType, string itemName,
		int pointsGained, int totalPoints);

	/// <summary>
	/// Logs a points change for a generic levelable item (without a specific name).
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="itemType">The type/category of the item.</param>
	/// <param name="pointsGained">The number of points gained (or lost if negative).</param>
	/// <param name="totalPoints">The resulting total points after the change.</param>
	[LoggerMessage(
		EventId = EventIds.LevelablePointsChanged,
		Level = LogLevel.Debug,
		Message = "{ItemType} gained {PointsGained} points. Total: {TotalPoints}.")]
	public static partial void LogLevelablePointsChangedGeneric(this ILogger logger, string itemType, int pointsGained,
		int totalPoints);
}
