using Microsoft.Extensions.Logging;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class LoggingExtensions
{
	[LoggerMessage(
		EventId = EventIdValues.MainMenuStateMachineInitialized,
		Level = LogLevel.Debug,
		Message = "MainMenu state machine initialized with {initialState}")]
	public static partial void MainMenuStateMachineInitialized(
		this ILogger logger,
		string initialState
	);

	[LoggerMessage(
		EventId = EventIdValues.MainMenuStateMachineTransitioned,
		Level = LogLevel.Debug,
		Message = "MainMenu transition {source} -> {destination} via {trigger}")]
	public static partial void MainMenuStateMachineTransitioned(
		this ILogger logger,
		string source,
		string destination,
		string trigger
	);
}
