using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents a concrete item instance in the world or inventory.
///     Holds its own ULID and current state, and references the item definition by ULID.
/// </summary>
public record ItemInstance
{
	/// <summary>
	///     Primary key (ULID) for the item instance.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     FK to the item definition for this instance.
	/// </summary>
	public required Ulid ItemDefinitionId { get; init; }

	/// <summary>
	///     Current quantity in this instance's stack. 1 for non-stackable items.
	/// </summary>
	public int Quantity { get; init; } = 1;

	/// <summary>
	///     Current durability. 0 means not applicable.
	/// </summary>
	public int Durability { get; init; }

	/// <summary>
	///     Optional material reference for primary material of this instance.
	/// </summary>
	public Ulid? PrimaryMaterialId { get; init; }

	/// <summary>
	///     Optional identifier for who/what crafted this item.
	/// </summary>
	public Ulid? CraftedBy { get; init; }

	/// <summary>
	///     The item definition that this instance references.
	/// </summary>
	[ForeignKey(nameof(ItemDefinitionId))]
	public ItemDefinition? ItemDefinition { get; init; }

	/// <summary>
	///     The primary material definition (if any) associated with this item instance.
	/// </summary>
	[ForeignKey(nameof(PrimaryMaterialId))]
	public MaterialDefinition? PrimaryMaterial { get; init; }
}
