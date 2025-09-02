using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Link table relating an Entity to the ItemInstances it owns/holds.
/// </summary>
public class EntityInventory
{
	/// <summary>
	///     Initializes a new instance of the <see cref="EntityInventory" /> class.
	///     This parameterless constructor is required by Entity Framework Core for materialization.
	/// </summary>
	public EntityInventory()
	{
		this.EntityId = Ulid.Empty;
		this.ItemInstanceId = Ulid.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="EntityInventory" /> class with the specified entity ID
	///     and item instance ID.
	/// </summary>
	/// <param name="entityId">The unique identifier of the entity owning the item instance.</param>
	/// <param name="itemInstanceId">The unique identifier of the item instance held by the entity.</param>
	public EntityInventory(Ulid entityId, Ulid itemInstanceId)
	{
		this.EntityId = entityId;
		this.ItemInstanceId = itemInstanceId;
	}

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
