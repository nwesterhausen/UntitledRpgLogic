using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class NetworkLoggingExtensions
{
	/// <summary>
	///     Logs that the network service has started.
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	[LoggerMessage(
		EventId = EventIds.NetworkServiceStarted,
		Level = LogLevel.Information,
		Message = "Network service started.")]
	public static partial void LogNetworkServiceStarted(this ILogger logger);
}
