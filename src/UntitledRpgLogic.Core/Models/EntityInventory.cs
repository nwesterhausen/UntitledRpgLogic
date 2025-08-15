using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Link table relating an Entity to the ItemInstances it owns/holds.
/// </summary>
public class EntityInventory
{
	/// <summary>
	///     FK to the owning entity.
	/// </summary>
	public required Ulid EntityId { get; init; }

	/// <summary>
	///     FK to the item instance held by the entity.
	/// </summary>
	public required Ulid ItemInstanceId { get; init; }

	/// <summary>
	///     The entity that owns/holds the item instance.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public Entity? Entity { get; init; }

	/// <summary>
	///     The item instance held by the entity.
	/// </summary>
	[ForeignKey(nameof(ItemInstanceId))]
	public ItemInstance? ItemInstance { get; init; }
}
