namespace UntitledRpgLogic.Core.Environment;

/// <summary>
///     Defines the baseline or localized atmospheric composition and pressure for a map or chunk.
/// </summary>
public record AtmosphereProfile
{
	/// <summary>
	///     Standard baseline clear atmosphere at 1.0 atmosphere total pressure.
	/// </summary>
	public static readonly AtmosphereProfile StandardDefault = new();

	/// <summary>
	///     Total atmospheric pressure measured in atmospheres (1.0 = standard sea level, 0.0 = total vacuum).
	/// </summary>
	public float TotalPressureAtm { get; init; } = 1.0f;

	/// <summary>
	///     Relative opacity or particulate suspension reducing line-of-sight visibility (0.0 = clear, 1.0 = blinding).
	/// </summary>
	public float ParticulateDensity { get; init; }

	/// <summary>
	///     Breakdown of gaseous materials comprising this atmosphere.
	/// </summary>
	public ICollection<AtmosphericGasFraction> GasFractions { get; init; } = [];

	/// <summary>
	///     Calculates the partial pressure of a specific gas material in atmospheres:
	///     $$P_{partial} = P_{total} \times \text{Ratio}$$
	/// </summary>
	public float GetPartialPressure(Ulid gasMaterialId)
	{
		var fraction = this.GasFractions.FirstOrDefault(g => g.MaterialId == gasMaterialId);
		return fraction is null ? 0.0f : this.TotalPressureAtm * fraction.Ratio;
	}
}
