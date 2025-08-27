using Microsoft.Extensions.Logging;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
/// High-performance logging extensions for game events (server or client).
/// </summary>
/// <remarks>
/// This class is a partial class. Additional logging methods are defined in other files
/// named LoggingExtensions.*.cs.
/// </remarks>
public static partial class LoggerExtensions
{
	[LoggerMessage(
		EventId = EventIdValues.None,
		Level = LogLevel.Debug,
		Message = "This has been a test of the logging system.")]
	public static partial void TestLog(this ILogger logger);
}
