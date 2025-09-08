using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// An effect that inflicts damage.
/// </summary>
public class DamageEffect : Effect
{
	/// <summary>
	/// An effect that inflicts damage.
	/// </summary>
	public DamageEffect() => this.EffectType = EffectType.Damage;

	/// <summary>
	/// Gets or sets the specific type of damage (e.g., Fire, Frost, Shadow). Referenced by ULID.
	/// </summary>
	public Ulid DamageTypeId { get; set; }

	/// <summary>
	/// Gets or sets the base amount of damage dealt.
	/// </summary>
	public float BaseAmount { get; set; }
}