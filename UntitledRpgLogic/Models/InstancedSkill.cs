using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Represents an instanced skill in the RPG logic. This class specifically for storing skill instances in a database.
/// </summary>
public class InstancedSkill
{
    /// <summary>
    ///     The unique identifier for the instanced skill. This is used to identify the skill instance in the game.
    /// </summary>
    [Key]
    public Guid Id { get; init; }

    /// <summary>
    ///     The unique identifier for the skill definition that this instanced skill is based on. This links the instanced
    ///     skill to its definition.
    /// </summary>
    public required Guid SkillDefinitionId { get; init; }

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
}