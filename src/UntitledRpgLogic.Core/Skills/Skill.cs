using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Core.Skills;

/// <summary>
///     Represents an active, trained skill instance bound to an entity, tracking progression and proficiency.
/// </summary>
[Table("instanced_skills")]
public record InstancedSkill : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedSkill" /> record for EF Core.
	/// </summary>
	public InstancedSkill()
	{
		this.Id = Ulid.NewUlid();
		this.SkillDefinitionId = Ulid.Empty;
		this.ExperiencePoints = 0;
		this.Level = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedSkill" /> record based on a skill discipline.
	/// </summary>
	/// <param name="skillDefinitionId">The identifier of the template skill definition.</param>
	public InstancedSkill(Ulid skillDefinitionId) : this() => this.SkillDefinitionId = skillDefinitionId;

	/// <summary>
	///     The unique identifier for this active skill instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     Foreign key referencing the template <see cref="SkillDefinition" />.
	/// </summary>
	public required Ulid SkillDefinitionId { get; init; }

	/// <summary>
	///     Navigation property to the governing skill template.
	/// </summary>
	[ForeignKey(nameof(SkillDefinitionId))]
	public SkillDefinition? SkillDefinition { get; init; }

	/// <summary>
	///     The total accumulated experience points within this skill discipline.
	/// </summary>
	public int ExperiencePoints { get; set; }

	/// <summary>
	///     The active proficiency level of this skill instance.
	/// </summary>
	public int Level { get; set; }
}
