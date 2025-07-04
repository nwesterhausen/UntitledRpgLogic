using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Database model for a definition of a stat. This is used to define properties of a stat like name, value range
///     and (if its dependent) which stat it depends on. This is not used when a stat belongs to an entity, but rather
///     defines
///     how those stats would behave.
/// </summary>
public record StatDefinition
{
    /// <summary>
    ///     The GUID for the stat. Any instances of this stat refer to this definition via this ID.
    /// </summary>
    [Key]
    public Guid Id { get; init; }

    /// <summary>
    ///     The name of the stat. This is used to identify the stat in the game and is used in the UI.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    ///     Whether the stat is able to be directly changed or not.
    /// </summary>
    public bool HasChangeableValue { get; init; }

    /// <summary>
    ///     The minimum value for this stat. This is the lowest value the stat can have.
    /// </summary>
    public int MinValue { get; init; }

    /// <summary>
    ///     The maximum value for this stat. This is the highest value the stat can have.
    /// </summary>
    public int MaxValue { get; init; }

    /// <summary>
    ///     The type of stat this is. This is used to determine how the stat behaves in the game.
    /// </summary>
    public required StatVariation Variation { get; init; }

    /// <summary>
    ///     Stats that this stat depends on (if any).
    /// </summary>
    public ICollection<LinkedStats> LinkedStats { get; init; } = new List<LinkedStats>();
}
