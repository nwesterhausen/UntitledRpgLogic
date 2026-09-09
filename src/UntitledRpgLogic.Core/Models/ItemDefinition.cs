using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Database catalog model defining an item template (e.g., Iron Sword, Health Potion, Wood Log).
///     Provides baseline attributes, classification, and constraints for all instantiated game items.
/// </summary>
[Table("item_definitions")]
public record ItemDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ItemDefinition" /> record with default values (for EF Core).
	/// </summary>
	public ItemDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Description = string.Empty;
		this.ItemType = ItemType.Miscellaneous;
		this.ItemSubtype = ItemSubtype.None;
		this.BaseQuality = Quality.Common;
		this.MaxStackSize = 1;
		this.BaseDurability = 100f;
		this.Weight = 1.0f;
		this.BaseValue = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ItemDefinition" /> record with a designated name.
	/// </summary>
	/// <param name="name">The display name of the item template.</param>
	public ItemDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     The unique catalog identifier for the item template. Can be loaded from external config definitions.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The name of the item template.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     A descriptive overview or flavor text for the item.
	/// </summary>
	[MaxLength(1024)]
	public string Description { get; init; }

	/// <summary>
	///     The high-level category of the item (e.g., Weapon, Armor, Consumable, Material).
	/// </summary>
	public required ItemType ItemType { get; init; }

	/// <summary>
	///     The specialized subtype classification (e.g., OneHandedSword, HeavyChestplate, Potion).
	/// </summary>
	public required ItemSubtype ItemSubtype { get; init; }

	/// <summary>
	///     The baseline craft/spawn quality tier of the item.
	/// </summary>
	public Quality BaseQuality { get; init; }

	/// <summary>
	///     The maximum number of units of this item that can occupy a single inventory slot.
	/// </summary>
	[Range(1, int.MaxValue)]
	public int MaxStackSize { get; init; }

	/// <summary>
	///     The default durability pool for instanced items created from this template (0 for non-degradable items).
	/// </summary>
	public float BaseDurability { get; init; }

	/// <summary>
	///     The physical weight of a single unit of this item.
	/// </summary>
	public float Weight { get; init; }

	/// <summary>
	///     The base currency value or merchant trading cost.
	/// </summary>
	public int BaseValue { get; init; }

	/// <summary>
	///     Optional ID of the player/entity that originally designed or crafted this definition.
	///     Null for standard base game items.
	/// </summary>
	public Ulid? CreatorEntityId { get; init; }

	/// <summary>
	///     Constituent materials that make up this item template (primary metal, hilt wrap, pommel gem, etc.).
	/// </summary>
	public ICollection<ItemMaterialComponent> Materials { get; init; } = [];

	/// <summary>
	///     All instances of this item actively existing in player inventories, containers, or the world.
	/// </summary>
	public virtual ICollection<ItemInstance> Instances { get; } = new List<ItemInstance>();
}
