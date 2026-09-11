using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Represents a concrete item instance in the world or inventory.
///     Holds its own ULID and current state, and references the item definition by ULID.
/// </summary>
[Table("item_instances")]
public record Item : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="Item" /> record with default values for EF Core.
	/// </summary>
	public Item()
	{
		this.Id = Ulid.NewUlid();
		this.DefinitionId = Ulid.Empty;
		this.Quantity = 1;
		this.Durability = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Item" /> record referencing an item definition.
	/// </summary>
	/// <param name="definitionId">The unique identifier of the item definition this instance is based on.</param>
	public Item(Ulid definitionId) : this() => this.DefinitionId = definitionId;

	/// <summary>
	///     Initializes a new instance of the <see cref="Item" /> record with definition and crafter details.
	/// </summary>
	/// <param name="definitionId">The unique identifier of the item definition this instance is based on.</param>
	/// <param name="craftedById">The unique identifier of the entity or process that crafted this item.</param>
	public Item(Ulid definitionId, Ulid craftedById) : this(definitionId) => this.CraftedById = craftedById;

	/// <summary>
	///     The unique primary key for the item instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     Foreign key referencing the parent <see cref="Definition" /> template.
	/// </summary>
	public required Ulid DefinitionId { get; init; }

	/// <summary>
	///     Navigation property to the item definition.
	/// </summary>
	[ForeignKey(nameof(DefinitionId))]
	public ItemDefinition? Definition { get; init; }

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
