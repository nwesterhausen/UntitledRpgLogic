namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for getting identity information.
/// </summary>
public interface IHasIdentifier
{
	/// <summary>
	///     The unique identifier for the object, represented as a Ulid.
	/// </summary>
	public Ulid Identifier { get; }
}
