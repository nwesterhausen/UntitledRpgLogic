using Microsoft.Extensions.Logging;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class LoggingExtensions
{
	[LoggerMessage(
		EventId = EventIdValues.PlayerQuestAccepted,
		Level = LogLevel.Information,
		Message = "Player {PlayerId} accepted quest {QuestId}.")]
	public static partial void QuestAccepted(this ILogger logger, Ulid playerId, string questId);

	[LoggerMessage(
		EventId = EventIdValues.PlayerQuestCompleted,
		Level = LogLevel.Information,
		Message = "Player {PlayerId} completed quest {QuestId}.")]
	public static partial void QuestCompleted(this ILogger logger, Ulid playerId, string questId);
}
