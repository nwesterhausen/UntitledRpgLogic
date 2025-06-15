using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Used to track the skills of an entity, such as a player or NPC.
/// </summary>
public class EntitySkills
{
    /// <summary>
    ///     An entity's unique identifier. This is used to reference the entity in the game and in the database.
    /// </summary>
    public required Guid EntityId { get; init; }

    /// <summary>
    ///     A unique identifier for a skill that has been instanced for an entity.
    /// </summary>
    public required Guid InstancedSkillId { get; init; }

    /// <summary>
    ///     The entity that this skill belongs to. This is used to link the skill to the entity it belongs to, such as a player
    ///     or NPC.
    /// </summary>
    [ForeignKey(nameof(EntityId))]
    public Entity? Entity { get; init; }

    /// <summary>
    ///     The instanced skill that this entity has.
    /// </summary>
    [ForeignKey(nameof(InstancedSkillId))]
    public InstancedSkill? InstancedSkill { get; init; }
}
