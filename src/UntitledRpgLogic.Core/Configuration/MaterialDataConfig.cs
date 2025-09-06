using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
///     The configuration for a material in the game. This class defines the properties of a material, including its name,
///     color, temperature and density at different states of matter.
/// </summary>
public record MaterialDataConfig : ITomlConfig
{
	/// <summary>
	///     Materials will always have a name. This is required.
	/// </summary>
	[Required]
	public required string Name { get; init; }

	/// <summary>
	///     An optional plural name for the material. If not provided, the game will attempt to guess a plural form of the
	///     Name.
	/// </summary>
	public string? PluralName { get; init; }

	/// <summary>
	///     An optional name for the material that can be used as an adjective. E.g. "Copper" Soup vs "Coppery" Soup.
	///     <remarks>If not provided, the Name will be used as the adjective.</remarks>
	/// </summary>
	public string? NameAsAdjective { get; init; }

	/// <summary>
	///     Mechanical properties of the material.
	/// </summary>
	public MechanicalPropertiesConfig Mechanical { get; init; } = new();

	/// <summary>
	///     Thermal properties of the material.
	/// </summary>
	public ThermalPropertiesConfig Thermal { get; init; } = new();

	/// <summary>
	///     Electrical properties of the material.
	/// </summary>
	public ElectricalPropertiesConfig Electrical { get; init; } = new();

	/// <summary>
	///     Fantastical properties of the material.
	/// </summary>
	public FantasticalPropertiesConfig Fantastical { get; init; } = new();

	/// <summary>
	///     Properties specific to the material's state of matter.
	/// </summary>
	public Dictionary<StateOfMatter, StateSpecificPropertiesConfig> States { get; init; } = [];

	/// <summary>
	///     If this material extends another material, the Guid of the parent material. This allows for inheritance of properties.
	/// </summary>
	public Guid? Extends { get; init; }

	/// <summary>
	///     Optional. If provided in TOML, this specific Ulid will be used for the item; otherwise, a new one will be generated.
	/// </summary>
	/// <remarks>
	///     This allows for consistent referencing of stats across different configurations or systems.
	///     <br />
	///     If not provided, a new Ulid will be generated when the configuration is loaded. If this happens when
	///     validating a configuration pack, the Ulid will be persisted back to the source file before bundling.
	///     This ensures that every stat has a stable and unique identifier.
	/// </remarks>
	public Ulid Id { get; set; } = Ulid.NewUlid();

	/// <inheritdoc />
	public ConfigType ConfigType => ConfigType.Material;
}
