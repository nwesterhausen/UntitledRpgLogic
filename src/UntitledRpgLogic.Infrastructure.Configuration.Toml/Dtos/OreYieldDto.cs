using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class OreYieldDto
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

	public OreYield ToModel() => new()
	{
		MaterialId = this.MaterialId,
		Efficiency = this.Efficiency,
		Chance = this.Chance,
		MinimumSmeltTemperature = this.MinimumSmeltTemperature
	};

	public static OreYieldDto FromModel(OreYield model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new OreYieldDto
		{
			MaterialId = model.MaterialId,
			Efficiency = model.Efficiency,
			Chance = model.Chance,
			MinimumSmeltTemperature = model.MinimumSmeltTemperature
		};
	}
}
