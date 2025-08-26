using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class LoggingExtensions
{
	[LoggerMessage(
		EventId = EventIdValues.ClientAuthenticationFailed,
		Level = LogLevel.Warning,
		Message = "Authentication failed for client {ClientId}. Reason: {Reason}")]
	public static partial void AuthenticationFailed(this ILogger logger, Ulid clientId, string reason);

	[LoggerMessage(
		EventId = EventIdValues.ClientAuthenticationSucceeded,
		Level = LogLevel.Information,
		Message = "Client {ClientId} successfully authenticated.")]
	public static partial void AuthenticationSuccess(this ILogger logger, Ulid clientId);
}
