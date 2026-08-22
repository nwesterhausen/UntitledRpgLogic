using Microsoft.Extensions.Logging;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class LoggingExtensions
{
	[LoggerMessage(
		EventId = EventIdValues.LevelablePointsChanged,
		Level = LogLevel.Debug,
		Message = "{name}.{target} {action} {pointChange} ({newValue})")]
	public static partial void LevelablePointsChanged(
		this ILogger logger,
		string name,
		string target,
		int pointChange,
		int newValue,
		string action);

	[LoggerMessage(
		EventId = EventIdValues.LevelablePointsChangedGeneric,
		Level = LogLevel.Debug,
		Message = "{name} {action} {pointChange} ({newValue})")]
	public static partial void LevelablePointsChangedGeneric(
		this ILogger logger,
		string name,
		int pointChange,
		int newValue,
		string action);

	[LoggerMessage(
		EventId = EventIdValues.LevelChanged,
		Level = LogLevel.Information,
		Message = "{name}.{target} {action} {levelChange} level{plural} ({newLevel})")]
	public static partial void LevelChanged(
		this ILogger logger,
		string name,
		string target,
		int levelChange,
		int newLevel,
		string action,
		string plural);

	[LoggerMessage(
		EventId = EventIdValues.LevelChangedGeneric,
		Level = LogLevel.Information,
		Message = "{name} {action} {levelChange} level{plural} ({newLevel})")]
	public static partial void LevelChangedGeneric(
		this ILogger logger,
		string name,
		int levelChange,
		int newLevel,
		string action,
		string plural);
}
