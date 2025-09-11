using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Definition data for an item type. Instances reference this by ULID.
///     Keep values EF-friendly and focused on definition-time attributes.
/// </summary>
public record ItemDefinition : IDbEntity<Ulid>
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

	/// <summary>
	///     Primary key for the item definition.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }
}
