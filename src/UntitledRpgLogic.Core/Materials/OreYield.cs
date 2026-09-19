namespace UntitledRpgLogic.Core.Materials;

/// <summary>
///     Represents a refined material produced from smelting/processing an ore.
/// </summary>
public record OreYield
{
	/// <summary>
	///     The ULID of the refined target material (e.g., Iron, Lead, Silver).
	/// </summary>
	public Ulid MaterialId { get; init; }

	/// <summary>
	///     Base ratio or units produced per unit of ore (e.g., 1.0 for full yield).
	/// </summary>
	public float Efficiency { get; init; } = 1.0f;

	/// <summary>
	///     Probability of obtaining this yield during processing (0.0 to 1.0).
	///     (e.g., Galena always yields Lead (1.0), with a 0.5 chance of Silver).
	/// </summary>
	public float Chance { get; init; } = 1.0f;

	/// <summary>
	///     Minimum furnace temperature required to extract this yield.
	/// </summary>
	public float MinimumSmeltTemperature { get; init; }
}
