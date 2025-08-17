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
	/// <param name="logger">The logger instance.</param>
	/// <param name="statName">The name of the stat.</param>
	/// <param name="statValue">The current value of the stat.</param>
	/// <param name="statMaxValue">The maximum value of the stat.</param>
	[LoggerMessage(
		EventId = EventIds.StatCreated,
		Level = LogLevel.Information,
		Message = "Created stat {StatName} with value {StatValue}/{StatMaxValue}")]
	public static partial void LogStatCreated(this ILogger logger, string statName, int statValue, int statMaxValue);

	/// <summary>
	///     Logs the damage taken by a stat, including the final damage, percentage of final damage, and incoming damage.
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="statName">The name of the stat.</param>
	/// <param name="finalDamage">The final damage applied after calculations.</param>
	/// <param name="finalDamagePercentage">The final damage as a percentage of max or current value.</param>
	/// <param name="incomingDamage">The raw incoming damage before calculations.</param>
	/// <param name="sourceId">The ID of the source that dealt the damage.</param>
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
	/// <param name="logger">The logger instance.</param>
	/// <param name="statName">The name of the stat.</param>
	/// <param name="healAmount">The amount healed.</param>
	/// <param name="healPercentage">The heal amount as a percentage.</param>
	/// <param name="sourceId">The ID of the source that healed the stat.</param>
	[LoggerMessage(
		EventId = EventIds.StatHealed,
		Level = LogLevel.Information,
		Message = "Stat {StatName} healed for {HealAmount} points ({HealPercentage}%). Source: {SourceId}")]
	public static partial void LogStatHealed(this ILogger logger, string statName, int healAmount, float healPercentage,
		Guid sourceId);

	/// <summary>
	///     Logs an illegal attempt to modify a stat, including the stat name and reason for the illegal change.
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="statName">The name of the stat.</param>
	/// <param name="reason">A description of why the change is illegal.</param>
	[LoggerMessage(
		EventId = EventIds.StatIllegalChange,
		Level = LogLevel.Warning,
		Message = "Illegal attempt to modify stat {StatName}. Reason: {Reason}")]
	public static partial void LogIllegalStatChange(this ILogger logger, string statName, string reason);

}
