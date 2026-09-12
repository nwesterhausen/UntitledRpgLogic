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
	public int ClampToDefinitionBounds(StatDefinition definition, int rawValue);
}
