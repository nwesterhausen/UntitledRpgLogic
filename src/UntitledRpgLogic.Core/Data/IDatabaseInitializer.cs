namespace UntitledRpgLogic.Core.Data;

/// <summary>
///     Defines a mechanism to provision, migrate, and verify the underlying database schema.
/// </summary>
public interface IDatabaseInitializer
{
	/// <summary>
	///     Executes pending database migrations or prepares the database schema for the active persistence provider.
	/// </summary>
	/// <param name="cancellationToken">A token to observe while waiting for the initialization task to complete.</param>
	/// <returns>A <see cref="Task" /> representing the asynchronous initialization operation.</returns>
	Task InitializeAsync(CancellationToken cancellationToken = default);
}
