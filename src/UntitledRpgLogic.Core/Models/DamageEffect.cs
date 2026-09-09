using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Concrete effect subtype that deals immediate or periodic health/armor damage.
/// </summary>
public record DamageEffect : Effect
{
	/// <summary>
	/// 	Initializes default base values.
	/// </summary>
	public DamageEffect()
	{
		this.EffectType = EffectType.Damage;
	}

	/// <summary>
	/// 	Initializes a new <see cref="Effect" /> with <see cref="EffectType.Damage" />.
	/// </summary>
	public DamageEffect(Name name) : base(name, EffectType.Damage)
	{
	}

	/// <summary>
	/// 	The base amount of damage dealt by this effect.
	/// </summary>
	public float BaseDamage { get; init; }

	/// <summary>
	/// 	The type of damage dealt by this effect.
	/// </summary>
	public DamageType DamageType { get; init; }

	/// <summary>
	/// 	Whether this damage ignores any armor or defense.
	/// </summary>
	public bool IgnoresArmor { get; init; }

	/// <summary>
	/// 	The delay before the damage is applied.
	/// </summary>
	public TimeSpan? Delay { get; init; }
}
