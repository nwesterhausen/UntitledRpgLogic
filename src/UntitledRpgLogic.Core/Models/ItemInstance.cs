using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents a concrete item instance in the world or inventory.
///     Holds its own ULID and current state, and references the item definition by ULID.
/// </summary>
[Table("item_instances")]
public record ItemInstance : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ItemInstance" /> record with default values for EF Core.
	/// </summary>
	public ItemInstance()
	{
		this.Id = Ulid.NewUlid();
		this.ItemDefinitionId = Ulid.Empty;
		this.Quantity = 1;
		this.Durability = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ItemInstance" /> record referencing an item definition.
	/// </summary>
	/// <param name="itemDefinitionId">The unique identifier of the item definition this instance is based on.</param>
	public ItemInstance(Ulid itemDefinitionId) : this() => this.ItemDefinitionId = itemDefinitionId;

	/// <summary>
	///     Initializes a new instance of the <see cref="ItemInstance" /> record with definition and crafter details.
	/// </summary>
	/// <param name="itemDefinitionId">The unique identifier of the item definition this instance is based on.</param>
	/// <param name="craftedById">The unique identifier of the entity or process that crafted this item.</param>
	public ItemInstance(Ulid itemDefinitionId, Ulid craftedById) : this(itemDefinitionId) => this.CraftedById = craftedById;

	/// <summary>
	///     The unique primary key for the item instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     Foreign key referencing the parent <see cref="ItemDefinition" /> template.
	/// </summary>
	public required Ulid ItemDefinitionId { get; init; }

	/// <summary>
	///     Navigation property to the item definition.
	/// </summary>
	[ForeignKey(nameof(ItemDefinitionId))]
	public ItemDefinition? ItemDefinition { get; init; }

	/// <summary>
	///     Current quantity in this item stack (1 for non-stackable items).
	/// </summary>
	[Range(1, int.MaxValue)]
	public int Quantity { get; set; }

	/// <summary>
	///     Current durability remaining (0 indicates not applicable or indestructible).
	/// </summary>
	public int Durability { get; set; }

	/// <summary>
	///     Optional material reference for the primary material of this specific instance.
	/// </summary>
	public Ulid? PrimaryMaterialId { get; init; }

	/// <summary>
	///     Navigation property to the primary material definition.
	/// </summary>
	[ForeignKey(nameof(PrimaryMaterialId))]
	public MaterialDefinition? PrimaryMaterial { get; init; }

	/// <summary>
	///     Optional identifier for the entity or system that crafted this item.
	/// </summary>
	public Ulid? CraftedById { get; init; }
}
