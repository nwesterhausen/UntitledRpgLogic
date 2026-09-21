using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class OreYieldDto
{
	[JsonPropertyName("material_id")] public Ulid MaterialId { get; init; }

	[JsonPropertyName("efficiency")] public float Efficiency { get; init; } = 1.0f;

	[JsonPropertyName("chance")] public float Chance { get; init; } = 1.0f;

	[JsonPropertyName("minimum_smelt_temperature")]
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
