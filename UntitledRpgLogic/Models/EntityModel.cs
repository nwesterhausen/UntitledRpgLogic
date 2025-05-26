using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Models;

public class EntityModel
{
    /// <summary>
    ///     The unique identifier for the entity.
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///     The name of the entity.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}