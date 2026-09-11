using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Database model and container representing an inventory owned by an <see cref="Entity" />.
/// </summary>
[Table("instanced_inventories")]
public record Inventory : IDbEntity<Ulid>
{
	/// <summary>
	///     The unique primary key for this inventory instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

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
	///     The collection of item instances held within this inventory.
	/// </summary>
	public virtual ICollection<Item> Items { get; init; } = [];
}
