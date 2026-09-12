using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.Core.Entities;

/// <summary>
///     Root database model representing a living actor, player, NPC, or creature in the game world.
/// </summary>
[Table("entities")]
public record Entity : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="Entity" /> record for EF Core materialization.
	/// </summary>
	public Entity()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Entity" /> record with an explicit identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the entity.</param>
	public Entity(Ulid id) : this() => this.Id = id;

	/// <summary>
	///     Initializes a new instance of the <see cref="Entity" /> record with a designated name.
	/// </summary>
	/// <param name="name">The display name of the entity.</param>
	public Entity(Name name) : this() => this.Name = name;

	/// <summary>
	///     The display name of the entity.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     Optional foreign key to the governing template archetype.
	///     Null for unique players or bespoke procedural actors without a base template.
	/// </summary>
	public Ulid? DefinitionId { get; init; }

	/// <summary>
	///     Navigation property to the archetype definition.
	/// </summary>
	[ForeignKey(nameof(DefinitionId))]
	public virtual EntityDefinition? Definition { get; init; }

	/// <summary>
	///     The map and world coordinates where this entity is actively spawned.
	///     Null indicates the entity is currently despawned or not placed in the world.
	/// </summary>
	public WorldPosition? Position { get; set; }

	/// <summary>
	///     Navigation property to the map where the entity is located.
	/// </summary>
	[ForeignKey("Position_MapId")]
	public virtual MapDefinition? CurrentMap { get; init; }

	/// <summary>
	///     Navigation property to the entity's inventory container.
	/// </summary>
	public virtual Inventory? Inventory { get; set; }

	/// <summary>
	///     Join navigations linking the entity to its learned skill instances.
	/// </summary>
	public virtual ICollection<EntitySkills> Skills { get; init; } = [];

	/// <summary>
	///     Join navigations linking the entity to its active stat instances.
	/// </summary>
	public virtual ICollection<EntityStats> Stats { get; init; } = [];

	/// <summary>
	///     Collection of ongoing status modifiers actively attached to this entity.
	/// </summary>
	public virtual ICollection<AppliedModifier> AppliedModifiers { get; init; } = [];

	/// <summary>
	///     Owned collection of stat adjustments applied to this entity.
	/// </summary>
	public virtual ICollection<AffectedStat> AffectedStats { get; init; } = [];

	/// <summary>
	///     The unique primary key for the entity. Can be loaded statically from config archives or generated.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }
}
