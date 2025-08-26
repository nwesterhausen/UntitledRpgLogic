using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
/// High-performance logging extensions for Session-related events.
/// </summary>
public static partial class LoggerExtensions
{
	[LoggerMessage(
		EventId = EventIdValues.None,
		Level = LogLevel.Debug,
		Message = "This has been a test of the logging system.")]
	public static partial void TestLog(this ILogger logger);
}
