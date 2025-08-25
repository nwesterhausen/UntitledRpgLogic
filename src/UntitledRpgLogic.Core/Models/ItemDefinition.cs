using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Definition data for an item type. Instances reference this by ULID.
///     Keep values EF-friendly and focused on definition-time attributes.
/// </summary>
public record ItemDefinition
{
	/// <summary>
	///     Primary key (ULID) for the item definition.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     Display name for the item.
	/// </summary>
	public required string Name { get; init; }

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
