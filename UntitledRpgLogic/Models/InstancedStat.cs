using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Represents an entity's stat, defined by a StatDefinition, with a base value and an apparent value.
/// </summary>
public class InstancedStat
{
    /// <summary>
    ///     The unique identifier for the instanced stat. This is used to identify the stat in the database and in the game.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    ///     The unique identifier for the stat definition that this instanced stat is based on. This links the instanced stat
    ///     to its definition.
    /// </summary>
    public Guid StatDefinitionId { get; set; }

    /// <summary>
    ///     The unique identifier for the entity that owns this stat. This links the stat to the entity it belongs to, such as
    ///     a player or NPC.
    /// </summary>
    public Guid OwnerEntityId { get; set; }

    /// <summary>
    ///     The base value of the stat, which is the raw value before any modifications or effects are applied.
    /// </summary>
    public int BaseValue { get; set; }

    /// <summary>
    ///     The apparent value of the stat, which may differ from the base value due to modifications, buffs, or debuffs.
    /// </summary>
    public int ApparentValue { get; set; }

    /// <summary>
    ///     The stat definition that this instanced stat is based on. This provides the properties and behavior of the stat.
    /// </summary>
    [ForeignKey(nameof(StatDefinitionId))]
    public required StatDefinition StatDefinition { get; set; }
}