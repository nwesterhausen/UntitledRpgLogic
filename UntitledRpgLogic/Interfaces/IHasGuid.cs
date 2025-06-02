namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that have a guid.
/// </summary>
public interface IHasGuid
{
    /// <summary>
    ///     Property to get the unique identifier of the object.
    /// </summary>
    public Guid Id { get; }
}