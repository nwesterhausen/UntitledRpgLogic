namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines the fractional concentration of a gaseous material within an atmospheric blend.
/// </summary>
public record AtmosphericGasFraction
{
	/// <summary>
	///     The foreign identifier of the gaseous <see cref="MaterialDefinition" /> (e.g., Oxygen, CO2, Methane, Nick's Death Gas v2).
	/// </summary>
	public Ulid MaterialId { get; init; }

	/// <summary>
	///     The fractional ratio of this gas in the total volume (0.0 to 1.0, where 1.0 represents 100% of the air volume).
	/// </summary>
	public float Ratio { get; init; }
}
