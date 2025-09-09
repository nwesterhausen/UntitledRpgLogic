using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Used to track the stats of an entity, such as a player or NPC.
/// </summary>
public class EntityStats
{
	/// <summary>
	///     Initializes a new instance of the <see cref="EntityStats" /> class.
	///     This parameterless constructor is required by Entity Framework Core for materialization.
	/// </summary>
	public EntityStats()
	{
		this.EntityId = Ulid.Empty;
		this.InstancedStatId = Ulid.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="EntityStats" /> class with the specified entity ID
	///     and item instance ID.
	/// </summary>
	/// <param name="entityId">The unique identifier of the entity owning the item instance.</param>
	/// <param name="instancedStatId">The unique identifier of the stat belonging by the entity.</param>
	public EntityStats(Ulid entityId, Ulid instancedStatId)
	{
		this.EntityId = entityId;
		this.InstancedStatId = instancedStatId;
	}

	/// <summary>
	///     An entity's unique identifier. This is used to reference the entity in the game and in the database.
	/// </summary>
	public required Ulid EntityId { get; set; }

	/// <summary>
	///     A unique identifier for a stat that has been instanced for an entity.
	/// </summary>
	public required Ulid InstancedStatId { get; set; }

	/// <summary>
	///     The entity that this stat belongs to. This is used to link the stat the entity it belongs to, such as a player or
	///     NPC.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public required Entity Entity { get; set; }

	/// <summary>
	///     The instanced stat that this entity has.
	/// </summary>
	[ForeignKey(nameof(InstancedStatId))]
	public required InstancedStat InstancedStat { get; set; }
}
