using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Join record linking an <see cref="Entity" /> to its owned collection of <see cref="InstancedStat" /> records.
/// </summary>
[Table("entity_stats")]
public record EntityStats
{
	/// <summary>
	///     Initializes a new instance of the <see cref="EntityStats" /> record for EF Core.
	/// </summary>
	public EntityStats()
	{
		this.EntityId = Ulid.Empty;
		this.InstancedStatId = Ulid.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="EntityStats" /> record with explicit keys.
	/// </summary>
	public EntityStats(Ulid entityId, Ulid instancedStatId)
	{
		this.EntityId = entityId;
		this.InstancedStatId = instancedStatId;
	}

	/// <summary>
	///     Foreign key of the owning entity.
	/// </summary>
	public Ulid EntityId { get; init; }

	/// <summary>
	///     Navigation property to the owning entity.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public Entity? Entity { get; init; }

	/// <summary>
	///     Foreign key of the associated instanced stat.
	/// </summary>
	public Ulid InstancedStatId { get; init; }

	/// <summary>
	///     Navigation property to the instanced stat.
	/// </summary>
	[ForeignKey(nameof(InstancedStatId))]
	public InstancedStat? InstancedStat { get; init; }
}
