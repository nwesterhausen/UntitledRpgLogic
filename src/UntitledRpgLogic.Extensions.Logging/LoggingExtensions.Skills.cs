using Microsoft.Extensions.Logging;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class LoggingExtensions
{
	[LoggerMessage(
		EventId = EventIdValues.SkillValueSet,
		Level = LogLevel.Debug,
		Message = "Setting points for skill {skillName} to {points}.")]
	public static partial void SkillValueSet(this ILogger logger, string skillName, int points);

	[LoggerMessage(
		EventId = EventIdValues.SkillValueDecrease,
		Level = LogLevel.Debug,
		Message = "Removing {points} points to skill {skillName}.")]
	public static partial void SkillValueDecrease(this ILogger logger, string skillName, int points);

	[LoggerMessage(
		EventId = EventIdValues.SkillValueIncrease,
		Level = LogLevel.Debug,
		Message = "Adding {points} points to skill {skillName}.")]
	public static partial void SkillValueIncrease(this ILogger logger, string skillName, int points);

	[LoggerMessage(
		EventId = EventIdValues.AttemptedIncreaseSkillAtMaxLevel,
		Level = LogLevel.Warning,
		Message = "Attempted to add {points} points to max-level skill {skillName}.")]
	public static partial void AttemptedIncreaseSkillAtMaxLevel(this ILogger logger, string skillName, int points);
}
