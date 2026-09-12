namespace UntitledRpgLogic.Core.Abilities;

using UntitledRpgLogic.Core.Entities;

/// <summary>
///     Pure domain service managing stack counts, duration renewal, and stat adjustments for active modifiers.
/// </summary>
public interface IModifierApplicationService
{
	/// <summary>
	///     Applies a modifier definition to an entity in memory, handling stack stacking limits and refreshing durations.
	/// </summary>
	public bool TryApplyModifier(Entity entity, ModifierDefinition definition, DateTimeOffset currentTime);

	/// <summary>
	///     Prunes expired modifiers and evaluates whether stacks decrease individually or all at once.
	/// </summary>
	public void TickModifiers(Entity entity, DateTimeOffset currentTime);
}
