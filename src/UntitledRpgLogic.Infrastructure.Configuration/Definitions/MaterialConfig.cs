using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Describes properties of a material.
/// </summary>
public record MaterialConfig
{
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
	///     If this material extends another material, the id of the parent material. This allows for inheritance of properties.
	/// </summary>
	public Ulid Extends { get; init; } = Ulid.Empty;

	/// <summary>
	///     The unique identifier for the material. If not provided, a new one will be generated.
	/// </summary>
	public Ulid Id { get; init; } = Ulid.NewUlid();
}
