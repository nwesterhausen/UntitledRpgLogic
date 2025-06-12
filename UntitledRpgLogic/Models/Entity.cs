using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Represents a base entity in the game. This class is used for storing data in the database.
/// </summary>
public class Entity
{
    /// <summary>
    ///     The unique identifier for the entity. This is used to reference the entity in the game and in the database.
    /// </summary>
    [Key]
    public Guid Id { get; init; }

    /// <summary>
    ///     The skills that this entity has.
    /// </summary>
    public ICollection<EntitySkills> Skills { get; init; } = [];

    /// <summary>
    ///     The stats that this entity has.
    /// </summary>
    public ICollection<EntityStats> Stats { get; init; } = [];
}