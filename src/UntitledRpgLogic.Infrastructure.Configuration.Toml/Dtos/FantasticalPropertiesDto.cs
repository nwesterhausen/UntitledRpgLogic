using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class FantasticalPropertiesDto
{
	[JsonPropertyName("aetherial_conductivity")]
	public float AetherialConductivity { get; init; }

	[JsonPropertyName("elemental_attunement")]
	public Dictionary<Ulid, float> ElementalAttunement { get; init; } = [];

	[JsonPropertyName("mana_capacity")] public float ManaCapacity { get; init; }

	[JsonPropertyName("purity")] public float Purity { get; init; }

	[JsonPropertyName("luminosity")] public float Luminosity { get; init; }

	public FantasticalProperties ToModel() => new()
	{
		AetherialConductivity = this.AetherialConductivity,
		ElementalAttunement = new Dictionary<Ulid, float>(this.ElementalAttunement),
		ManaCapacity = this.ManaCapacity,
		Purity = this.Purity,
		Luminosity = this.Luminosity
	};

	public static FantasticalPropertiesDto FromModel(FantasticalProperties model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new FantasticalPropertiesDto
		{
			AetherialConductivity = model.AetherialConductivity,
			ElementalAttunement = new Dictionary<Ulid, float>(model.ElementalAttunement),
			ManaCapacity = model.ManaCapacity,
			Purity = model.Purity,
			Luminosity = model.Luminosity
		};
	}
}
