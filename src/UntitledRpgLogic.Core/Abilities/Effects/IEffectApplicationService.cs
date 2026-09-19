using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     Defines the contract for a service responsible for applying the logic of game effects.
/// </summary>
public interface IEffectApplicationService
{
	/// <summary>
	///     Applies a given effect, processing all its components.
	/// </summary>
	/// <param name="effect">The effect to apply.</param>
	/// <param name="caster">The entity casting or initiating the effect (optional).</param>
	/// <param name="targets">The collection of target entities (optional).</param>
	public void ApplyEffect(Effect effect, Entity? caster = null, IEnumerable<Entity>? targets = null);
}
