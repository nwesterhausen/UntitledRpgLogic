using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class StatLoggerExtensions
{
	/// <summary>
	///     Logs the creation of a stat with its name, value, and maximum value.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="statName"></param>
	/// <param name="statValue"></param>
	/// <param name="statMaxValue"></param>
	[LoggerMessage(
		EventId = EventIds.StatCreated,
		Level = LogLevel.Information,
		Message = "Created stat {StatName} with value {StatValue}/{StatMaxValue}")]
	public static partial void LogStatCreated(this ILogger logger, string statName, int statValue, int statMaxValue);

	/// <summary>
	///     Logs the damage taken by a stat, including the final damage, percentage of final damage, incoming damage,
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="statName"></param>
	/// <param name="finalDamage"></param>
	/// <param name="finalDamagePercentage"></param>
	/// <param name="incomingDamage"></param>
	/// <param name="sourceId"></param>
	[LoggerMessage(
		EventId = EventIds.StatDamageTaken,
		Level = LogLevel.Information,
		Message =
			"Stat {StatName} took {FinalDamage} damage ({FinalDamagePercentage}%). Incoming: {IncomingDamage}. Source: {SourceId}")]
	public static partial void LogStatDamageTaken(this ILogger logger, string statName, int finalDamage,
		float finalDamagePercentage, int incomingDamage, Guid sourceId);

	/// <summary>
	///     Logs the healing of a stat, including the heal amount, percentage of heal, and source ID.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="statName"></param>
	/// <param name="healAmount"></param>
	/// <param name="healPercentage"></param>
	/// <param name="sourceId"></param>
	[LoggerMessage(
		EventId = EventIds.StatHealed,
		Level = LogLevel.Information,
		Message = "Stat {StatName} healed for {HealAmount} points ({HealPercentage}%). Source: {SourceId}")]
	public static partial void LogStatHealed(this ILogger logger, string statName, int healAmount, float healPercentage,
		Guid sourceId);

	/// <summary>
	///     Logs an illegal attempt to modify a stat, including the stat name and reason for the illegal change.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="statName"></param>
	/// <param name="reason"></param>
	[LoggerMessage(
		EventId = EventIds.StatIllegalChange,
		Level = LogLevel.Warning,
		Message = "Illegal attempt to modify stat {StatName}. Reason: {Reason}")]
	public static partial void LogIllegalStatChange(this ILogger logger, string statName, string reason);

}
