using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class StateSpecificPropertiesDto
{
	[JsonPropertyName("state")] public StateOfMatter State { get; init; } = StateOfMatter.None;

	[JsonPropertyName("color")] public string Color { get; init; } = "#FFFFFF";

	[JsonPropertyName("mechanical")] public MechanicalPropertiesDto? MechanicalProperties { get; init; }

	[JsonPropertyName("thermal")] public ThermalPropertiesDto? ThermalProperties { get; init; }

	[JsonPropertyName("electrical")] public ElectricalPropertiesDto? ElectricalProperties { get; init; }

	[JsonPropertyName("fantastical")] public FantasticalPropertiesDto? FantasticalProperties { get; init; }

	public StateSpecificProperties ToModel() => new()
	{
		State = this.State,
		Color = this.Color,
		MechanicalProperties = this.MechanicalProperties?.ToModel(),
		ThermalProperties = this.ThermalProperties?.ToModel(),
		ElectricalProperties = this.ElectricalProperties?.ToModel(),
		FantasticalProperties = this.FantasticalProperties?.ToModel()
	};

	public static StateSpecificPropertiesDto FromModel(StateSpecificProperties model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new StateSpecificPropertiesDto
		{
			State = model.State,
			Color = model.Color,
			MechanicalProperties =
				model.MechanicalProperties is not null
					? MechanicalPropertiesDto.FromModel(model.MechanicalProperties)
					: null,
			ThermalProperties =
				model.ThermalProperties is not null
					? ThermalPropertiesDto.FromModel(model.ThermalProperties)
					: null,
			ElectricalProperties =
				model.ElectricalProperties is not null
					? ElectricalPropertiesDto.FromModel(model.ElectricalProperties)
					: null,
			FantasticalProperties = model.FantasticalProperties is not null
				? FantasticalPropertiesDto.FromModel(model.FantasticalProperties)
				: null
		};
	}
}
