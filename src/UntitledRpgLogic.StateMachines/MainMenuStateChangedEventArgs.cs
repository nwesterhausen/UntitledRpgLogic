using static UntitledRpgLogic.StateMachines.MainMenuStateMachine;

namespace UntitledRpgLogic.StateMachines;

/// <summary>
/// Event when MainMenu changes state
/// </summary>
public class MainMenuStateChangedEventArgs(State newState) : EventArgs
{

	/// <summary>
	/// The new state of the main menu
	/// </summary>
	public State NewState { get; } = newState;
}
