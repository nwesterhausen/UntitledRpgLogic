using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an instanced skill in the RPG logic. This class specifically for storing skill instances in a database.
/// </summary>
public class InstancedSkill : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedSkill" /> class with default values.
	///     This constructor sets the skill instance ID to a new ULID, the skill definition ID to an empty ULID,
	///     and initializes experience points and level to 0.
	/// </summary>
	public InstancedSkill()
	{
		this.Id = Ulid.NewUlid();
		this.SkillDefinitionId = Ulid.Empty;
		this.ExperiencePoints = 0;
		this.Level = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedSkill" /> class with the specified skill definition ID.
	///     This constructor sets the skill instance ID to a new ULID, initializes experience points and level to 0,
	///     and links the skill instance to the provided skill definition.
	/// </summary>
	/// <param name="skillDefinitionId">The unique identifier of the skill definition this instance is based on.</param>
	public InstancedSkill(Ulid skillDefinitionId)
	{
		this.Id = Ulid.NewUlid();
		this.SkillDefinitionId = skillDefinitionId;
		this.ExperiencePoints = 0;
		this.Level = 0;
	}

	/// <summary>
	///     The unique identifier for the skill definition that this instanced skill is based on. This links the instanced
	///     skill to its definition.
	/// </summary>
	public required Ulid SkillDefinitionId { get; init; }

	/// <summary>
	///     The total amount of experience points accumulated for this skill instance. This is used to track progress towards
	///     leveling up the skill.
	/// </summary>
	public int ExperiencePoints { get; init; }

	/// <summary>
	///     The current level of the skill instance. This represents the skill's proficiency and is used to determine its
	///     effectiveness in the game.
	/// </summary>
	public int Level { get; init; }

	/// <summary>
	/// </summary>
	[ForeignKey(nameof(SkillDefinitionId))]
	public SkillDefinition? SkillDefinition { get; init; }

	/// <summary>
	///     The unique identifier for the instanced skill. This is used to identify the skill instance in the game.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }
}
