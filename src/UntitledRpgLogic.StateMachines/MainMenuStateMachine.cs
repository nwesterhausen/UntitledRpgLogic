using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Stateless;
using UntitledRpgLogic.Extensions.Logging;

namespace UntitledRpgLogic.StateMachines;

/// <summary>
///     A state machine for the main menu of the game. Provides rigorous control over the main menu state and transitions.
/// </summary>
public class MainMenuStateMachine
{
	/// <summary>
	///     Represents the various states that the main menu can be in.
	/// </summary>
	public enum State
	{
		/// <summary>
		///     The main menu is at the root level.
		/// </summary>
		MainMenu,

		/// <summary>
		///     The user is in the new game creation screen.
		/// </summary>
		NewGame,

		/// <summary>
		///     The user is in the load game screen.
		/// </summary>
		LoadGame,

		/// <summary>
		///     The user is in the settings menu.
		/// </summary>
		Settings,

		/// <summary>
		///     The game is shutting down.
		/// </summary>
		ShutDown
	}

	/// <summary>
	///     Represents the triggers that can cause a state transition in the main menu.
	/// </summary>
	public enum Trigger
	{
		/// <summary>
		///     The user has selected the new game option.
		/// </summary>
		SelectNewGame,

		/// <summary>
		///     The user has selected the load game option.
		/// </summary>
		SelectLoadGame,

		/// <summary>
		///     The user has selected the settings option.
		/// </summary>
		SelectSettings,

		/// <summary>
		///     The user has selected the quit option.
		/// </summary>
		SelectQuit
	}

	private readonly ILogger<MainMenuStateMachine> logger;
	private readonly StateMachine<State, Trigger> machine;

	private State state = State.MainMenu;

#pragma warning disable CA1873 // We check for logger being null before expensive operations.

	/// <summary>
	///     Initializes a new instance of the <see cref="MainMenuStateMachine" /> class.
	/// </summary>
	/// <param name="logger">A logger instance for logging state machine transitions and information.</param>
	public MainMenuStateMachine(ILogger<MainMenuStateMachine>? logger = null)
	{
		this.logger = logger ?? NullLogger<MainMenuStateMachine>.Instance;
		this.machine = new StateMachine<State, Trigger>(() => this.state, s => this.state = s);

		_ = this.machine.Configure(State.MainMenu)
			.Permit(Trigger.SelectNewGame, State.NewGame)
			.Permit(Trigger.SelectLoadGame, State.LoadGame)
			.Permit(Trigger.SelectSettings, State.Settings)
			.Permit(Trigger.SelectQuit, State.ShutDown);

		this.machine.OnTransitioned(this.Transition);

		if (this.logger is not NullLogger<MainMenuStateMachine>)
		{
			this.logger.MainMenuStateMachineInitialized(this.state.ToString());
		}
	}

	/// <summary>
	///     Publicly exposed handler for triggering state transitions in the main menu state machine.
	/// </summary>
	public Action<Trigger> TriggerAction => this.machine.Fire;

	/// <summary>
	///     Logs the transition and invokes the StateChanged event.
	/// </summary>
	/// <param name="t">The transition that occurred.</param>
	private void Transition(StateMachine<State, Trigger>.Transition t)
	{
		if (this.logger is not NullLogger<MainMenuStateMachine>)
		{
			this.logger.MainMenuStateMachineTransitioned(
				t.Source.ToString(), t.Destination.ToString(), t.Trigger.ToString());
		}

		this.StateChanged?.Invoke(this, new MainMenuStateChangedEventArgs(t.Destination));
	}

	/// <summary>
	///     Event that is triggered when the state of the main menu changes.
	/// </summary>
	public event EventHandler<MainMenuStateChangedEventArgs>? StateChanged;
#pragma warning restore CA1873 // Allow check for expensive operations again
}
