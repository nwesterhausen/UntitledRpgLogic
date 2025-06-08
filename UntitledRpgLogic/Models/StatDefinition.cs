using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Enums;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Database model for a definition of a stat. This is used to define properties of a stat like name, value range
///     and (if its dependent) which stat it depends on. This is not used when a stat belongs to an entity, but rather
///     defines
///     how those stats would behave.
/// </summary>
public class StatDefinition
{
    /// <summary>
    ///     The GUID for the stat. Any instances of this stat refer to this definition via this ID.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    ///     The name of the stat. This is used to identify the stat in the game and is used in the UI.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Whether the stat is able to be directly changed or not.
    /// </summary>
    public bool HasChangeableValue { get; set; }

    /// <summary>
    ///     The minimum value for this stat. This is the lowest value the stat can have.
    /// </summary>
    public int MinValue { get; set; }

    /// <summary>
    ///     The maximum value for this stat. This is the highest value the stat can have.
    /// </summary>
    public int MaxValue { get; set; }

    /// <summary>
    ///     The type of stat this is. This is used to determine how the stat behaves in the game.
    /// </summary>
    public required StatVariation Variation { get; set; }

    /// <summary>
    ///     Stats that this stat depends on (if any).
    /// </summary>
    public ICollection<LinkedStats> LinkedStats { get; set; } = new List<LinkedStats>();
}