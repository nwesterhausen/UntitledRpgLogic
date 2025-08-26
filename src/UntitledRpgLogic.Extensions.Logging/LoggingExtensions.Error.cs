using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Extensions.Logging;

public static partial class LoggingExtensions
{
	[LoggerMessage(
		EventId = EventIdValues.UnexpectedError,
		Level = LogLevel.Error,
		Message = "An unexpected error occurred while processing message of type {MessageType} for client {ClientId}.")]
	public static partial void UnexpectedErrorProcessingMessage(this ILogger logger, Exception ex, MessageType messageType, Ulid clientId);
}
