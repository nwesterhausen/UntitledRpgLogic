using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Stat model for stats which are in use by an entity.
/// </summary>
[Keyless]
public class InstancedStatModel
{
    /// <summary>
    ///     The unique identifier for the entity this stat is associated with.
    /// </summary>
    public Guid EntityId { get; set; }

    /// <summary>
    ///     The entity which has instanced this stat.
    /// </summary>
    [ForeignKey(nameof(EntityId))]
    public virtual EntityModel? Entity { get; set; }

    /// <summary>
    ///     The ID of the stat this instance is based on.
    /// </summary>
    public int StatId { get; set; }

    /// <summary>
    ///     The stat this instance is based on.
    /// </summary>
    [ForeignKey(nameof(StatId))]
    public virtual StatModel? Stat { get; set; }

    /// <summary>
    ///     The value of the stat instance, which can be modified independently of the base stat.
    /// </summary>
    public int Value { get; set; }
}