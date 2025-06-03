using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Models;

/// <summary>
///     Models an entity for storage in a database.
/// </summary>
public class EntityModel
{
    /// <summary>
    ///     The unique identifier for the entity.
    /// </summary>
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    ///     The name of the entity.
    /// </summary>
    public string Name { get; init; } = string.Empty;
}