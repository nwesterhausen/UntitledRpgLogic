namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Pure domain service for calculating stat apparent values, min-max bounds, and linked stat propagation.
/// </summary>
public interface IStatCalculationService
{
	/// <summary>
	///     Recomputes the apparent value of a stat based on its baseline, linked dependencies, and bounds.
	/// </summary>
	public int CalculateApparentValue(Stat targetStat, IEnumerable<Stat> dependencyStats);

	/// <summary>
	///     Clamps a value within the boundaries defined by the stat's template definition.
	/// </summary>
	public int ClampToStatBounds(Stat stat, int rawValue);

	/// <summary>
	///     Calculates the raw damage value to subtract from a health or armor stat.
	/// </summary>
	/// <param name="options">The incoming damage parameters and scaling.</param>
	/// <param name="targetStat">The concrete stat instance being damaged.</param>
	/// <returns>The total point damage to inflict.</returns>
	public int CalculatePointChange(ChangeOptions options, Stat targetStat);

	/// <summary>
	///     Calculates effective damage after applying resistance percentage and flat reduction.
	/// </summary>
	/// <param name="rawDamage">The raw point damage that is being applied.</param>
	/// <param name="resistancePercent">The percentage mitigation to apply to the incoming damage.</param>
	/// <param name="flatMitigation">A flat damage mitigation to apply.</param>
	public int CalculateMitigatedPointChange(int rawDamage, float resistancePercent, int flatMitigation);
}
