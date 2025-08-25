namespace UntitledRpgLogic.Core.Interfaces.Effects;

/// <summary>
///     Represents an effect that is continuously active, or triggered by specific game events.
/// </summary>
public interface IPassiveEffect : IEffect
{
	// Passive effects might have methods like:
	// void OnApplied(IEntity target);
	// void OnRemoved(IEntity target);
	// void OnGameTick(float deltaTime);
	// void OnStatChanged(IEntity target, IStat stat, float oldValue, float newValue);
	// For now, it's a marker interface, but it's ready for future expansion.
}
