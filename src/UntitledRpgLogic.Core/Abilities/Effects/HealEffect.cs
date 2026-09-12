using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     Concrete effect subtype that restores character health, mana, or stamina pools.
/// </summary>
public record HealEffect : Effect
{
	/// <summary>
	///     Initializes default base values.
	/// </summary>
	[SetsRequiredMembers]
	public HealEffect() => this.EffectType = EffectType.Heal;

	/// <summary>
	///     Initializes a new <see cref="Effect" /> with <see cref="EffectType.Heal" />.
	/// </summary>
	[SetsRequiredMembers]
	public HealEffect(Name name) : base(name, EffectType.Heal)
	{
	}

	/// <summary>
	///     The amount of healing to apply.
	/// </summary>
	public float BaseHealAmount { get; init; }

	/// <summary>
	///     Whether this healing effect can go beyond the max value of the affected stats.
	/// </summary>
	public bool CanOverheal { get; init; }
}
