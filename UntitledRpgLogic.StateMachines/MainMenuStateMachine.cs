namespace UntitledRpgLogic.StateMachines;

/// <summary>
///     A state machine for the main menu of the game. Provides rigorous control over the main menu state and transitions.
/// </summary>
public class MainMenuStateMachine
{
    public enum State
    {
        MainMenu,
        NewGame,
        LoadGame,
        Settings,
        ShutDown
    }

    public enum Trigger
    {
        SelectNewGame,
        SelectLoadGame,
        SelectSettings,
        SelectQuit
    }

    private readonly ILogger<MainMenuStateMachine> _logger;
    private readonly StateMachine<State, Trigger> _machine;

    private State _state = State.MainMenu;

    public MainMenuStateMachine(ILogger<MainMenuStateMachine>? logger = null)
    {
        _logger = logger ?? NullLogger<MainMenuStateMachine>.Instance;
        _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

        _machine.Configure(State.MainMenu)
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
