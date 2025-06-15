using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Stateless;

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
        ShutDown,
        SettingsAudio,
        SettingsGraphics,
        SettingsControls,
        SettingsGameplay,
        NewGameSetup,
        LoadGameSelection
    }

    public enum Trigger
    {
        SelectNewGame,
        SelectLoadGame,
        SelectSettings,
        SelectQuit,
        SelectSettingsAudio,
        SelectSettingsGraphics,
        SelectSettingsControls,
        SelectSettingsGameplay,
        Continue,
        Back
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
        _machine.Configure(State.NewGame)
            .Permit(Trigger.Continue, State.NewGameSetup)
            .Permit(Trigger.Back, State.MainMenu);
        _machine.Configure(State.LoadGame)
            .Permit(Trigger.Continue, State.LoadGameSelection)
            .Permit(Trigger.Back, State.MainMenu);
        _machine.Configure(State.Settings)
            .Permit(Trigger.SelectSettingsAudio, State.SettingsAudio)
            .Permit(Trigger.SelectSettingsGraphics, State.SettingsGraphics)
            .Permit(Trigger.SelectSettingsControls, State.SettingsControls)
            .Permit(Trigger.SelectSettingsGameplay, State.SettingsGameplay)
            .Permit(Trigger.Back, State.MainMenu);
        _machine.Configure(State.SettingsAudio)
            .SubstateOf(State.Settings);
        _machine.Configure(State.SettingsGraphics)
            .SubstateOf(State.Settings);
        _machine.Configure(State.SettingsControls)
            .SubstateOf(State.Settings);
        _machine.Configure(State.SettingsGameplay)
            .SubstateOf(State.Settings);

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
