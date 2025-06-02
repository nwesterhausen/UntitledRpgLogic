using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Base implementation for classes that have a unique identifier (GUID).
/// </summary>
public abstract class HasGuidBase : IHasGuid
{
    /// <summary>
    ///     The unique identifier for this object.
    /// </summary>
    public Guid Id { get; protected internal set; } = Guid.NewGuid();

    /// <summary>
    ///     Overwrite the unique identifier for this object.
    /// </summary>
    /// <param name="id">guid to set for this object</param>
    protected internal void SetId(Guid id)
    {
        Id = id;
    }
}