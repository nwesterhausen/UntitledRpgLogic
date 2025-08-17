using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class ModifierLoggerExtensions
{
	/// <summary>
	///     Logs the application of a modifier to a stat, including the modifier name and stat name.
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="modifierName">The name of the modifier being applied.</param>
	/// <param name="statName">The name of the affected stat.</param>
	[LoggerMessage(
		EventId = EventIds.ModifierApplied,
		Level = LogLevel.Debug,
		Message = "Applied modifier {ModifierName} to stat {StatName}.")]
	public static partial void LogModifierApplied(this ILogger logger, string modifierName, string statName);
}
