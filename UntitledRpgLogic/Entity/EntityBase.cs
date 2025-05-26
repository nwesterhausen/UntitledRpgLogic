namespace UntitledRpgLogic.Entity;

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

    public Guid Id { get; }

    public string Name { get; internal set; } = string.Empty;
}