using Microsoft.Extensions.Logging;
using ZLogger;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class SessionLoggingExtensions
{
	[ZLoggerMessage(
		EventId = EventIdValues.ClientAuthenticationFailed,
		Level = LogLevel.Warning,
		Message = "Authentication failed for client {ClientId}. Reason: {Reason}")]
	public static partial void AuthenticationFailed(this ILogger logger, Ulid clientId, string reason);

	[ZLoggerMessage(
		EventId = EventIdValues.ClientAuthenticationSucceeded,
		Level = LogLevel.Information,
		Message = "Client {ClientId} successfully authenticated.")]
	public static partial void AuthenticationSuccess(this ILogger logger, Ulid clientId);
}
