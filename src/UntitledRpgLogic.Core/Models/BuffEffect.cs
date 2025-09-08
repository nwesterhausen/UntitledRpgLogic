using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     An effect that applies a positive or neutral impact to the target, enhancing their attributes or abilities.
/// </summary>
public class BuffEffect : Effect
{
	/// <summary>
	///     An effect that applies a positive or neutral impact to the target, enhancing their attributes or abilities.
	/// </summary>
	public BuffEffect() => this.EffectType = EffectType.Buff;
}
