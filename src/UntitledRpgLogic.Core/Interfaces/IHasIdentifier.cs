namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for classes that have a guid identifier.
///     This is used to ensure that objects can be uniquely identified by a Guid.
/// </summary>
public interface IHasIdentifier
{
	/// <summary>
	///     An identifier for the object.
	/// </summary>
	public Guid Identifier { get; init; }
}
