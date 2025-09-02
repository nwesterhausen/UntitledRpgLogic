using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Definition data for an item type. Instances reference this by ULID.
///     Keep values EF-friendly and focused on definition-time attributes.
/// </summary>
public record ItemDefinition
{
	/// <summary>
	///     Constructs a new <see cref="ItemDefinition" /> with default values. (mainly for EF use)
	/// </summary>
	public ItemDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Quality = Quality.Common;
		this.ItemType = ItemType.Junk;
		this.ItemSubtype = ItemSubtype.None;
		this.Stackable = false;
		this.MaxStack = 1;
		this.MaxDurability = 0;
	}

	/// <summary>
	///     Constructs a new <see cref="ItemDefinition" /> from the provided config data.
	/// </summary>
	/// <param name="config"></param>
	public ItemDefinition(ItemDataConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		this.Id = config.Identifier;
		this.Name = new Name(config.Name, config.PluralName, config.NameAsAdjective);
		this.Quality = config.ItemQuality ?? Quality.Common;
		this.ItemType = config.ItemType;
		this.ItemSubtype = config.ItemSubtype ?? ItemSubtype.None;
		this.Stackable = config.MaxStack > 1;
		this.MaxStack = config.MaxStack;
		this.MaxDurability = config.BaseDurability;
	}

	/// <summary>
	///     Primary key for the item definition.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     Display name for the item.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     Quality tier of the item.
	/// </summary>
	public Quality Quality { get; init; }

	/// <summary>
	///     High-level item type classification.
	/// </summary>
	public ItemType ItemType { get; init; }

	/// <summary>
	///     Subtype classification for the item.
	/// </summary>
	public ItemSubtype ItemSubtype { get; init; }

	/// <summary>
	///     Whether this item can stack in inventory.
	/// </summary>
	public bool Stackable { get; init; }

	/// <summary>
	///     The maximum stack size if stackable.
	/// </summary>
	public int MaxStack { get; init; }

	/// <summary>
	///     Optional maximum durability for items that wear down. 0 means not applicable.
	/// </summary>
	public int MaxDurability { get; init; }
}
