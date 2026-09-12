using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Core.Entities;

/// <summary>
///     Root database catalog model defining an entity blueprint archetype (e.g., Goblin Raider, Fire Wisp, Guard).
///     Provides baseline attributes, creature classification, and spawning templates for active world entities.
/// </summary>
[Table("entity_definitions")]
public record EntityDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="EntityDefinition" /> record with default values for EF Core.
	/// </summary>
	public EntityDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Description = string.Empty;
		this.Classification = EntityClassification.None;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="EntityDefinition" /> record with a designated name.
	/// </summary>
	/// <param name="name">The display name of the entity archetype.</param>
	public EntityDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     The display name of the entity template.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     Descriptive overview or lore notes for this entity archetype.
	/// </summary>
	[MaxLength(1024)]
	public string Description { get; init; }

	/// <summary>
	///     Bitwise classification flags categorizing the entity archetype.
	/// </summary>
	public EntityClassification Classification { get; init; }

	/// <summary>
	///     Respiratory traits and gas vulnerabilities for this archetype (owned value object serialized as JSON).
	/// </summary>
	public RespiratoryProfile? RespiratoryProfile { get; init; }

	/// <summary>
	///     Initial baseline stat modifiers/allocations granted to this archetype upon spawning (owned JSON collection).
	/// </summary>
	public ICollection<AffectedStat> StartingStats { get; init; } = [];

	/// <summary>
	///     Skill discipline templates granted to this archetype upon spawning (owned JSON collection).
	/// </summary>
	public ICollection<Ulid> InnateSkillDefinitionIds { get; init; } = [];

	/// <summary>
	///     Item templates automatically granted or equipped when instances of this archetype spawn.
	/// </summary>
	public ICollection<Ulid> StartingItemDefinitionIds { get; init; } = [];

	/// <summary>
	///     Navigation property to all active world entities spawned from this definition.
	/// </summary>
	public virtual ICollection<Entity> Instances { get; } = new List<Entity>();

	/// <summary>
	///     The unique catalog identifier for the entity template. Can be loaded from external config definitions.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }
}
