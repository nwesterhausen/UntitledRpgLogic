namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for objects that can be crafted, indicating they have a crafter associated with them.
/// </summary>
public interface IIsCrafted
{
    /// <summary>
    ///     The reference to the crafter that created this item. This is a GUID that points to an entity or a special case
    ///     (like GM-crafted items).
    /// </summary>
    Guid CraftedBy { get; }
}
