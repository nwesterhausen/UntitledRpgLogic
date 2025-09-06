using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
///     The definition for the configuration of an Item. This is what is parsed from a TOML file to then create an actual
///     item in the game.
/// </summary>
public record ItemDataConfig : ITomlConfig
{
	/// <summary>
	///     Items will always have a name. This is required.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	///     An optional plural name for the item. If not provided, the game will attempt to guess a plural form of the Name.
	/// </summary>
	public string? PluralName { get; set; }

	/// <summary>
	///     An optional name for the item that can be used as an adjective. E.g. "Sword" Soup vs "Swordy" Soup.
	///     <remarks>If not provided, the Name will be used as the adjective.</remarks>
	/// </summary>
	public string? NameAsAdjective { get; set; }

	/// <summary>
	///     An optional quality for the item. This can be used to define the rarity or quality of the item. If not supplied,
	///     the default will be <see cref="Quality.None" />
	/// </summary>
	public Quality? ItemQuality { get; set; }

	/// <summary>
	///     The type of the item. This is required and defines the general category of the item, such as Weapon, Armor, etc.
	/// </summary>
	public required ItemType ItemType { get; set; }

	/// <summary>
	///     A subtype for the item. This can be used to further categorize the item within its type. Defaults to
	///     <see cref="ItemSubtype.None" />.
	/// </summary>
	public ItemSubtype? ItemSubtype { get; set; }

	/// <summary>
	///     A short description of the item. This is optional and can be used to provide additional context or flavor text
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	///     An optional GUID representing the creator of the item. This can be used to track who crafted the item.
	/// </summary>
	public Ulid CraftedBy { get; set; } = WellKnownIdentifiers.GameSystem;

	/// <summary>
	///     Optionally specify a specific dimension scale for the item's width, height, and depth. If not provided, they
	///     will be interpreted as being in the default scale of <see cref="DimensionScale.Cm" />.
	/// </summary>
	public DimensionScale? DimensionScale { get; set; }

	/// <summary>
	///     Optional. This can be inferred based on the <see cref="ItemType" /> and <see cref="ItemSubtype" />, but can be
	///     specified explicitly if needed. This is used for calculating the volume and other spatial properties of the item.
	/// </summary>
	public ShapeType? ShapeType { get; set; }

	/// <summary>
	///     The width of the item in the specified dimension scale. This is required and must be a positive value.
	/// </summary>
	public float Width { get; set; }

	/// <summary>
	///     The height of the item in the specified dimension scale. This is required and must be a positive value.
	/// </summary>
	public float Height { get; set; }

	/// <summary>
	///     Optional. The depth of the item in the specified dimension scale. If not provided, it will default to 1 unit.
	/// </summary>
	public float? Depth { get; set; }

	/// <summary>
	///     A reference to the material that this item is made of. This is a ULID that points to a material entity,
	///     specified in that material's configuration under <see cref="Id" />.
	/// </summary>
	public Ulid MaterialId { get; set; }

	/// <summary>
	///     The maximum number of this item that can be stacked in a single inventory slot. If this is greater than 1, the item is considered
	///     stackable.
	///     If not provided, defaults to 1 (not stackable).
	/// </summary>
	public int MaxStack { get; set; } = 1;

	/// <summary>
	///     The base durability of the item. This represents how much wear and tear the item can take before it breaks or becomes unusable.
	///     If the item does not have durability, this should be set to 0. (e.g., for consumables or quest items)
	/// </summary>
	public int BaseDurability { get; set; }

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
	public ConfigType ConfigType => ConfigType.Item;
}
