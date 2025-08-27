namespace UntitledRpgLogic.Core.Interfaces.Common;

/// <summary>
///     Interface for objects that can be crafted, indicating they have a crafter associated with them.
/// </summary>
public interface IIsCrafted
{
	/// <summary>
	///     The reference to the crafter that created this item. This is a ULID that points to an entity or a reserved ULID
	///     that indicates a system crafter (like the game system for pre-generated things that have no creator).
	/// </summary>
	public Ulid CraftedBy { get; }
}
