using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Stat model for stats which are in use by an entity. These are stats that should have a stored value.
/// </summary>
[Keyless]
public sealed class InstancedStatModel
{
    /// <summary>
    ///     The unique identifier for the entity this stat is associated with.
    /// </summary>
    public Guid EntityId { get; init; }

    /// <summary>
    ///     The entity which has instanced this stat.
    /// </summary>
    [ForeignKey(nameof(EntityId))]
    public EntityModel? Entity { get; init; }

    /// <summary>
    ///     The ID of the stat this instance is based on.
    /// </summary>
    public int StatId { get; init; }

    /// <summary>
    ///     The stat this instance is based on.
    /// </summary>
    [ForeignKey(nameof(StatId))]
    public StatModel? Stat { get; init; }

    /// <summary>
    ///     The value of the stat instance, which can be modified independently of the base stat.
    /// </summary>
    public int Value { get; init; }
}