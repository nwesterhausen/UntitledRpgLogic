namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for classes that have a guid.
/// </summary>
public interface IHasGuid
{
    /// <summary>
    ///     The unique identifier for the object, represented as a Guid.
    /// </summary>
    Guid Guid { get; }

    /// <summary>
    ///     A base-64 encoded string representation of the Guid.
    ///     This value is derived from the Guid property.
    /// </summary>
    string Id { get; }

    /// <summary>
    ///     The first 8 characters of the guid, used as a short identifier.
    ///     This value is derived from the Guid property.
    /// </summary>
    string ShortGuid { get; }
}
