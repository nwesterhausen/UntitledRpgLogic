namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that have a guid.
/// </summary>
public interface IHasGuid
{
    /// <summary>
    ///    The unique identifier for the object, represented as a Guid.
    /// </summary>
    public Guid Guid { get; }

    /// <summary>
    ///     A base-64 encoded string representation of the Guid.
    /// </summary>
    public string Id { get; }

    /// <summary>
    ///     The first 8 characters of the base-64 encoded Guid, used as a short identifier.
    /// </summary>
    public string ShortGuid { get; }
}