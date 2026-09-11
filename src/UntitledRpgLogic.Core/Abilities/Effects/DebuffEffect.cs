using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     An effect that applies a negative impact to the target, enhancing their attributes or abilities.
/// </summary>
public record DebuffEffect : Effect
{

	/// <summary>
	/// 	Initializes default base values.
	/// </summary>
	public DebuffEffect()
	{
		this.EffectType = EffectType.Debuff;
	}

	/// <summary>
	/// 	Initializes a new <see cref="Effect" /> with <see cref="EffectType.Debuff" />.
	/// </summary>
	public DebuffEffect(Name name) : base(name, EffectType.Debuff)
	{
	}
}
