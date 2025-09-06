namespace UntitledRpgLogic.Core.Interfaces.Data;

/// <summary>
///     Defines the base contract for a database entity, providing a typed identifier.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public interface IDbEntity<out TId>
{
	/// <summary>
	///     Gets the unique identifier for the entity.
	/// </summary>
	public TId Id { get; }
}
