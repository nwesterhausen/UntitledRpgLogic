using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Database model and container representing an inventory owned by an <see cref="Entity" />.
/// </summary>
[Table("inventories")]
public record Inventory : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="Inventory" /> record for EF Core materialization.
	/// </summary>
	public Inventory()
	{
		this.Id = Ulid.NewUlid();
		this.Capacity = 20;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Inventory" /> record with an explicit identifier.
	/// </summary>
	/// <param name="id">The unique primary key identifier for the inventory.</param>
	public Inventory(Ulid id) : this() => this.Id = id;

	/// <summary>
	///     The unique primary key for this inventory instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Foreign key linking this inventory back to its owning entity.
	/// </summary>
	public Ulid EntityId { get; init; }

	/// <summary>
	///     Navigation property to the owning entity.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public virtual Entity? Entity { get; init; }

	/// <summary>
	///     The maximum number of item slots this inventory can hold.
	/// </summary>
	public int Capacity { get; init; }

	/// <summary>
	///     Optional filter restricting which items can be placed in this inventory.
	///     A null filter permits all item types.
	/// </summary>
	public InventoryFilter? Filter { get; init; }

	/// <summary>
	///     The collection of item instances held within this inventory.
	/// </summary>
	public virtual ICollection<Item> Items { get; init; } = [];
}
