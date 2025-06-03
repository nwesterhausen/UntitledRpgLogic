using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.BaseImplementations;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Models a stat for storage in a database. These are the base definitions of stats that can be used by entities.
/// </summary>
/// <remarks>
///     These do not store values or anything because they are not attached to an entity, these simply define a stat
///     which can exist.
/// </remarks>
public class StatModel
{
    /// <summary>
    ///     The unique identifier in the database for this stat.
    /// </summary>
    [Key]
    public int Id { get; init; }

    /// <summary>
    ///     The name of the stat (e.g., "Strength", "Agility").
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    ///     The variation type of the stat, which may define how the stat behaves or changes.
    /// </summary>
    public StatVariation Variation { get; init; } = StatVariation.Pseudo;

    /// <summary>
    ///     The maximum allowed value for the stat.
    /// </summary>
    public int MaxValue { get; init; } = StatBase.STAT_DEFAULT_MAX_VALUE;

    /// <summary>
    ///     The minimum allowed value for the stat.
    /// </summary>
    public int MinValue { get; init; } = StatBase.STAT_DEFAULT_MIN_VALUE;
}