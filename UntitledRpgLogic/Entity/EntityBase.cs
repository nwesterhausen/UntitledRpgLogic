namespace UntitledRpgLogic.Entity;

/// <summary>
///     Represents the base class for all entities in the RPG system.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    ///     Creates a new instance of <see cref="EntityBase" /> with a new unique identifier.
    /// </summary>
    /// <param name="id">optinally provide a unique identifier to use</param>
    protected EntityBase(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }

    /// <summary>
    ///     The unique identifier for this entity.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     The name of the entity.
    /// </summary>
    public string Name { get; internal set; } = string.Empty;
}