namespace UntitledRpgLogic.Core.Options;

/// <summary>
///     Options available when applying damage to a stat.
/// </summary>
public class DamageOptions
{
	/// <summary>
	///     A flat amount of damage to apply to the stat.
	/// </summary>
	public int? FlatDamage { get; init; }

	/// <summary>
	///     A percentage of the stat's current value to apply as damage.
	/// </summary>
	public float? PercentageDamage { get; init; }

	/// <summary>
	///     A percentage of the stat's maximum value to apply as damage.
	/// </summary>
	public float? PercentageDamageOfMax { get; init; }

	/// <summary>
	///     The identifier for the source of the damage, used for tracking and logging purposes.
	/// </summary>
	public Guid? SourceId { get; init; }
}
