using System.Text.Json.Serialization;
using Tomlyn.Serialization;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class MaterialDefinitionDto : IConfigDto<MaterialDefinitionDto, MaterialDefinition>
{
	[JsonPropertyName("id")] public Ulid Id { get; init; } = Ulid.Empty;

	[JsonPropertyName("name")] public Name Name { get; init; } = Name.Empty;

	[JsonPropertyName("description")] public string Description { get; init; } = string.Empty;

	[JsonPropertyName("flags")] public MaterialTraits Flags { get; init; } = MaterialTraits.None;

	[JsonPropertyName("default_state")] public StateOfMatter DefaultState { get; init; } = StateOfMatter.Solid;

	[JsonPropertyName("smelt_yields")]
	[TomlSingleOrArray]
	public List<OreYieldDto> SmeltYields { get; init; } = [];

	[JsonPropertyName("mechanical")] public MechanicalPropertiesDto? MechanicalProperties { get; init; }

	[JsonPropertyName("thermal")] public ThermalPropertiesDto? ThermalProperties { get; init; }

	[JsonPropertyName("electrical")] public ElectricalPropertiesDto? ElectricalProperties { get; init; }

	[JsonPropertyName("fantastical")] public FantasticalPropertiesDto? FantasticalProperties { get; init; }

	[JsonPropertyName("state_properties")]
	[TomlSingleOrArray]
	public List<StateSpecificPropertiesDto> StateProperties { get; init; } = [];

	public MaterialDefinition ToModel() =>
		new()
		{
			Id = this.Id,
			Name = this.Name,
			Description = this.Description,
			Flags = this.Flags,
			DefaultState = this.DefaultState,
			SmeltYields = this.SmeltYields.ConvertAll(y => y.ToModel()),
			MechanicalProperties = this.MechanicalProperties?.ToModel(),
			ThermalProperties = this.ThermalProperties?.ToModel(),
			ElectricalProperties = this.ElectricalProperties?.ToModel(),
			FantasticalProperties = this.FantasticalProperties?.ToModel(),
			StateProperties = this.StateProperties.ConvertAll(s => s.ToModel())
		};

	public static MaterialDefinitionDto FromModel(MaterialDefinition model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new MaterialDefinitionDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			Flags = model.Flags,
			DefaultState = model.DefaultState,
			SmeltYields = model.SmeltYields.Select(OreYieldDto.FromModel).ToList(),
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
			FantasticalProperties =
				model.FantasticalProperties is not null
					? FantasticalPropertiesDto.FromModel(model.FantasticalProperties)
					: null,
			StateProperties = model.StateProperties.Select(StateSpecificPropertiesDto.FromModel).ToList()
		};
	}
}
