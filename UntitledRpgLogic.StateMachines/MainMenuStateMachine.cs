using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Stateless;

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

    private readonly ILogger<MainMenuStateMachine> _logger;
    private readonly StateMachine<State, Trigger> _machine;

    private State _state = State.MainMenu;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainMenuStateMachine" /> class.
    /// </summary>
    /// <param name="logger">A logger instance for logging state machine transitions and information.</param>
    public MainMenuStateMachine(ILogger<MainMenuStateMachine>? logger = null)
    {
        _logger = logger ?? NullLogger<MainMenuStateMachine>.Instance;
        _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

        _ = _machine.Configure(State.MainMenu)
            .Permit(Trigger.SelectNewGame, State.NewGame)
            .Permit(Trigger.SelectLoadGame, State.LoadGame)
            .Permit(Trigger.SelectSettings, State.Settings)
            .Permit(Trigger.SelectQuit, State.ShutDown);

        _machine.OnTransitioned(Transition);

        _logger.LogDebug("MainMenu state machine initialized with initial state: {InitialState}", _state);
    }

    /// <summary>
    ///     Publicly exposed handler for triggering state transitions in the main menu state machine.
    /// </summary>
    public Action<Trigger> TriggerAction => _machine.Fire;

    /// <summary>
    ///     Logs the transition and invokes the StateChanged event.
    /// </summary>
    /// <param name="t">The transition that occurred.</param>
    private void Transition(StateMachine<State, Trigger>.Transition t)
    {
        _logger.LogDebug(
            "MainMenu transition {source} -> {destination} via {trigger}",
            t.Source, t.Destination, t.Trigger);
        StateChanged?.Invoke(t.Destination);
    }

    /// <summary>
    ///     Event that is triggered when the state of the main menu changes.
    /// </summary>
    public event Action<State>? StateChanged;
}
