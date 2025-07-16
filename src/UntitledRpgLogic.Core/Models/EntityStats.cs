using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Used to track the stats of an entity, such as a player or NPC.
/// </summary>
public class EntityStats
{
    /// <summary>
    ///     An entity's unique identifier. This is used to reference the entity in the game and in the database.
    /// </summary>
    public required Guid EntityId { get; set; }

    /// <summary>
    ///     A unique identifier for a stat that has been instanced for an entity.
    /// </summary>
    public required Guid InstancedStatId { get; set; }

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
