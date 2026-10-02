using Microsoft.Extensions.Logging;
using ZLogger;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class StateMachineLoggingExtensions
{
	[ZLoggerMessage(
		EventId = EventIdValues.MainMenuStateMachineInitialized,
		Level = LogLevel.Debug,
		Message = "MainMenu state machine initialized with {initialState}")]
	public static partial void MainMenuStateMachineInitialized(
		this ILogger logger,
		string initialState
	);

	[ZLoggerMessage(
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
