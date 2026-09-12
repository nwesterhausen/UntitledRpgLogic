using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Progression;
using LevelingDefinition = UntitledRpgLogic.Core.Progression.LevelingDefinition;

namespace UntitledRpgLogic.Core.Skills;

/// <summary>
///     A skill definition in the RPG logic, for usage with a database.
/// </summary>
[Table("skill_definitions")]
public record SkillDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes an empty instance of the <see cref="SkillDefinition" /> class (for EF use).
	/// </summary>
	public SkillDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="SkillDefinition" /> class with the specified name.
	/// </summary>
	/// <param name="name">The name of the skill.</param>
	public SkillDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     The unique identifier for the skill definition. This is used to identify the skill in the database.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The name of the skill. This is used to identify the skill in the game and should be unique.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	/// 	The id of the leveling definition used by this skill.
	/// </summary>
	public Ulid? LevelingDefinitionId { get; init; }

	/// <summary>
	/// 	The definition of how leveling is calculated for this skill.
	/// </summary>
	[ForeignKey(nameof(LevelingDefinitionId))]
	public LevelingDefinition? LevelingDefinition { get; init; }

	/// <summary>
	///     Navigation property for all abilities that belong to this skill discipline.
	/// </summary>
	public virtual ICollection<AbilityDefinition> Abilities { get; } = new List<AbilityDefinition>();
}
