using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Represents a link between two stats, where one stat is dependent on another. Provides a one-to-many relationship
/// </summary>
public class LinkedStats
{
    /// <summary>
    ///     The unique identifier for the dependent stat. This is used to identify the stat that depends on another stat.
    /// </summary>
    public Guid DependentStatId { get; set; }

    /// <summary>
    ///     The unique identifier for the linked stat. This is used to identify the stat that is being depended on.
    /// </summary>
    public Guid LinkedStatId { get; set; }

    /// <summary>
    ///     The dependent stat that this link refers to. This is the stat that depends on another stat for its value.
    /// </summary>
    [ForeignKey(nameof(DependentStatId))]
    public required StatDefinition DependentStat { get; set; }

    /// <summary>
    ///     The linked stat that this link refers to. This is the stat that is being depended on by another stat.
    /// </summary>
    [ForeignKey(nameof(LinkedStatId))]
    public required StatDefinition LinkedStat { get; set; }
}