using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     Concrete effect subtype that deals immediate or periodic health/armor damage.
/// </summary>
public record DamageEffect : Effect
{
	/// <summary>
	///     Creates an empty damage effect record.
	/// </summary>
	[SetsRequiredMembers]
	public DamageEffect()
	{
		this.EffectType = EffectType.Damage;
		this.DamageType = DamageType.None;
	}

	/// <summary>
	///     Initializes a new <see cref="Effect" /> with <see cref="EffectType.Damage" />.
	/// </summary>
	/// <param name="name">The name of the effect.</param>
	/// <param name="affectedStatId">The ID of the affected stat.</param>
	/// <param name="options">The damage options</param>
	/// <param name="ignoresArmor">Whether the damage ignores armor</param>
	[SetsRequiredMembers]
	public DamageEffect(Name name,
		Ulid affectedStatId,
		StatChangeOptions options,
		bool ignoresArmor = false) : base(name, EffectType.Damage)
	{
		this.AddAffectedStat(affectedStatId, options);
		this.IgnoresArmor = ignoresArmor;
	}

	/// <summary>
	///     The type of damage dealt by this effect.
	/// </summary>
	public DamageType DamageType { get; init; }

	/// <summary>
	///     Whether this damage ignores any armor or defense.
	/// </summary>
	public bool IgnoresArmor { get; init; }

	/// <summary>
	///     The delay before the damage is applied.
	/// </summary>
	public TimeSpan? Delay { get; init; }
}
