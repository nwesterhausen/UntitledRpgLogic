namespace UntitledRpgLogic.Core.Interfaces.Data;

/// <summary>
///     Interface for a unit of work pattern which is designed for handling the interfaces defined in this project.
/// </summary>
public interface IUnitOfWork : IDisposable
{
	/// <summary>
	///     Commits all changes made in the unit of work to the database.
	/// </summary>
	/// <returns>number of state entries written to the database</returns>
	public int Commit();

	/// <summary>
	///     Commits all changes made in the unit of work to the database asynchronously.
	/// </summary>
	/// <param name="cancellationToken">A token to observe for cancellation requests.</param>
	/// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
	public Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
