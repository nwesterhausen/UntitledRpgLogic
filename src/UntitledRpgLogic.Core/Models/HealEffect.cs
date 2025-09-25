using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     An effect that heals the target.
/// </summary>
public class HealEffect : Effect
{
	/// <summary>
	///     An effect that heals the target.
	/// </summary>
	public HealEffect() => this.EffectType = EffectType.Heal;
}
