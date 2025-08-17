using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class SkillLoggerExtensions
{
	/// <summary>
	///     Logs the creation of a skill, including its name, level, and maximum level.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="points"></param>
	/// <param name="skillName"></param>
	[LoggerMessage(
		EventId = EventIds.SkillAddPointsMaxLevel,
		Level = LogLevel.Warning,
		Message = "Attempted to add {Points} points to a max-level skill: {SkillName}.")]
	public static partial void LogAddPointsToMaxLevelSkill(this ILogger logger, int points, string skillName);
}
