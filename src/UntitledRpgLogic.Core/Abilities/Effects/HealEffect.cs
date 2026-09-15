using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Stats;

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
	public HealEffect() : base(EffectType.Heal){}

	/// <summary>
	///     Initializes a new <see cref="Effect" /> with <see cref="EffectType.Heal" />.
	/// </summary>
	[SetsRequiredMembers]
	public HealEffect(Name name,
		Ulid affectedStatId,
		StatChangeOptions options,
		bool canOverheal = false) : base(name, EffectType.Heal)
	{
		ArgumentNullException.ThrowIfNull(options);

		this.AddAffectedStat(affectedStatId, options with { IsPositive = true });
		this.CanOverheal = canOverheal;
	}

	/// <summary>
	///     Whether this healing effect can go beyond the max value of the affected stats.
	/// </summary>
	public bool CanOverheal { get; init; }
}
