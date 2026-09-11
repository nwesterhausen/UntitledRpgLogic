using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Core.Skills;

/// <summary>
///     Represents an active, trained skill instance bound to an entity, tracking progression and proficiency.
/// </summary>
[Table("skills")]
public record Skill : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="Skill" /> record for EF Core.
	/// </summary>
	public Skill()
	{
		this.Id = Ulid.NewUlid();
		this.DefinitionId = Ulid.Empty;
		this.ExperiencePoints = 0;
		this.Level = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Skill" /> record based on a skill discipline.
	/// </summary>
	/// <param name="definitionId">The identifier of the template skill definition.</param>
	public Skill(Ulid definitionId) : this() => this.DefinitionId = definitionId;

	/// <summary>
	///     The unique identifier for this active skill instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     Foreign key referencing the template <see cref="Definition" />.
	/// </summary>
	public required Ulid DefinitionId { get; init; }

	/// <summary>
	///     Navigation property to the governing skill template.
	/// </summary>
	[ForeignKey(nameof(DefinitionId))]
	public SkillDefinition? Definition { get; init; }

	/// <summary>
	///     The total accumulated experience points within this skill discipline.
	/// </summary>
	public int ExperiencePoints { get; set; }

	/// <summary>
	///     The active proficiency level of this skill instance.
	/// </summary>
	public int Level { get; set; }
}
