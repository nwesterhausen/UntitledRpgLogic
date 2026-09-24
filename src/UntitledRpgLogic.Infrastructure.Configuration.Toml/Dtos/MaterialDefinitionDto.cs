using Tomlyn.Serialization;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class MaterialDefinitionDto : IConfigDto<MaterialDefinitionDto, MaterialDefinition>
{
	/// <summary>
	///     The unique identifier for the material definition.
	/// </summary>
	public Ulid Id { get; init; } = Ulid.Empty;

	/// <summary>
	///     The display name of the material.
	/// </summary>
	public Name Name { get; init; } = Name.Empty;

	/// <summary>
	///     A descriptive overview of the material's appearance, lore, and traits.
	/// </summary>
	public string Description { get; init; } = string.Empty;

	/// <summary>
	///     Bitwise classification flags (e.g., NaturalOre, Combustible, Fluid).
	/// </summary>
	public MaterialTraits Flags { get; init; } = MaterialTraits.None;

	/// <summary>
	///     The default phase of matter for this material under standard room temperature conditions.
	/// </summary>
	public StateOfMatter DefaultState { get; init; } = StateOfMatter.Solid;


	/// <summary>
	///     Compositional breakdown and metal extraction yields produced when smelting this material.
	///     Empty for pure elements or non-ores.
	/// </summary>
	[TomlSingleOrArray]
	public List<OreYieldDto> SmeltYields { get; init; } = [];

	/// <summary>
	///     Baseline mechanical characteristics (density, hardness, elasticity).
	/// </summary>
	public MechanicalPropertiesDto? MechanicalProperties { get; init; }

	/// <summary>
	///     Baseline thermal properties (melting point, boiling point, specific heat).
	/// </summary>
	public ThermalPropertiesDto? ThermalProperties { get; init; }

	/// <summary>
	///     Baseline electrical and magnetic characteristics.
	/// </summary>
	public ElectricalPropertiesDto? ElectricalProperties { get; init; }

	/// <summary>
	///     Baseline mystical traits, mana conductivity, and planar elemental attunements.
	/// </summary>
	public FantasticalPropertiesDto? FantasticalProperties { get; init; }

	/// <summary>
	///     Phase transition deviations when the material shifts state (Solid, Liquid, Gas).
	/// </summary>
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
