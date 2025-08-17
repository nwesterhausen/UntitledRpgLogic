using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class SkillLoggerExtensions
{
	/// <summary>
	///     Logs an attempt to add points to a skill that is already at its maximum level.
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="points">The number of points attempted to add.</param>
	/// <param name="skillName">The name of the skill.</param>
	[LoggerMessage(
		EventId = EventIds.SkillAddPointsMaxLevel,
		Level = LogLevel.Warning,
		Message = "Attempted to add {Points} points to a max-level skill: {SkillName}.")]
	public static partial void LogAddPointsToMaxLevelSkill(this ILogger logger, int points, string skillName);
}
