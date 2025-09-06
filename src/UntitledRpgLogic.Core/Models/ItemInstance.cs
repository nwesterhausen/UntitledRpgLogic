using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents a concrete item instance in the world or inventory.
///     Holds its own ULID and current state, and references the item definition by ULID.
/// </summary>
public record ItemInstance: IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ItemInstance" /> class with default values.
	///     This constructor sets the item instance ID to a new ULID and the item definition ID to an empty ULID.
	/// </summary>
	public ItemInstance()
	{
		this.Id = Ulid.NewUlid();
		this.ItemDefinitionId = Ulid.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ItemInstance" /> class with the specified item definition ID.
	///     This constructor sets the item instance ID to a new ULID and links the instance to the provided item definition.
	/// </summary>
	/// <param name="itemDefinitionId">The unique identifier of the item definition this instance is based on.</param>
	public ItemInstance(Ulid itemDefinitionId)
	{
		this.Id = Ulid.NewUlid();
		this.ItemDefinitionId = itemDefinitionId;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ItemInstance" /> class with the specified item definition ID
	///     and crafted-by ID.
	///     This constructor sets the item instance ID to a new ULID, links the instance to the provided item definition,
	///     and associates it with the entity or process that crafted it.
	/// </summary>
	/// <param name="itemDefinitionId">The unique identifier of the item definition this instance is based on.</param>
	/// <param name="craftedById">The unique identifier of the entity or process that crafted this item.</param>
	public ItemInstance(Ulid itemDefinitionId, Ulid craftedById)
	{
		this.Id = Ulid.NewUlid();
		this.ItemDefinitionId = itemDefinitionId;
		this.CraftedById = craftedById;
	}

	/// <summary>
	///     Primary key for the item instance.
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
	public Ulid? CraftedById { get; init; }

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
