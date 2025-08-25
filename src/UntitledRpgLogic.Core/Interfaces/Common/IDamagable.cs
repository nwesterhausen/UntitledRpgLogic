namespace UntitledRpgLogic.Core.Interfaces.Common;

/// <summary>
///     Defines the contract for an object that can take damage.
///     The logic for applying damage is handled by a service.
/// </summary>
public interface IDamageable
{
	/// <summary>
	///     The stat that is being damaged.
	/// </summary>
	public IStat Stat { get; }

	/// <summary>
	///     The current damage applied to the stat, in points.
	/// </summary>
	public int CurrentDamage { get; set; }

	/// <summary>
	///     The current damage as a percentage of the stat's maximum value.
	/// </summary>
	public float CurrentPercentageDamage { get; set; }
}
