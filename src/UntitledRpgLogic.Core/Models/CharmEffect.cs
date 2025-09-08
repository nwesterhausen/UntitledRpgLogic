using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// An effect that applies a status-effect to an entity.
/// </summary>
public class CharmEffect : Effect
{
	/// <summary>
	/// An effect that applies a status-effect to an entity.
	/// </summary>
	public CharmEffect() => this.EffectType = EffectType.Enchant;
}
