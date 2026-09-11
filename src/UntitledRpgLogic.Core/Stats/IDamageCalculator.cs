namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Calculates final mitigated damage applied to target stats.
/// </summary>
public interface IDamageCalculator
{
	/// <summary>
	///     Calculates the raw damage value to subtract from a health or armor stat.
	/// </summary>
	/// <param name="options">The incoming damage parameters and scaling.</param>
	/// <param name="targetStat">The concrete stat instance being damaged.</param>
	/// <returns>The total point damage to inflict.</returns>
	public int CalculatePointDamage(DamageOptions options, Stat targetStat);

	/// <summary>
	///     Calculates effective damage after applying resistance percentage and flat reduction.
	/// </summary>
	/// <param name="rawDamage">The raw point damage that is being applied.</param>
	/// <param name="resistancePercent">The percentage mitigation to apply to the incoming damage.</param>
	/// <param name="flatMitigation">A flat damage mitigation to apply.</param>
	public int CalculateMitigatedDamage(int rawDamage, float resistancePercent, int flatMitigation);
}
