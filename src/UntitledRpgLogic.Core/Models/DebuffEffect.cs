using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     An effect that applies a negative impact to the target, reducing their attributes or abilities.
/// </summary>
public class DebuffEffect : Effect
{
	/// <summary>
	///     An effect that applies a negative impact to the target, reducing their attributes or abilities.
	/// </summary>
	public DebuffEffect() => this.EffectType = EffectType.Debuff;
}
