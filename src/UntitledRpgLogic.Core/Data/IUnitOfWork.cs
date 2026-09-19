namespace UntitledRpgLogic.Core.Data;

/// <summary>
///     Defines a Unit of Work contract to coordinate multi-repository transactions,
///     state tracking, and atomic persistence across database operations.
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
	/// <summary>
	///     Persists all pending changes tracked within the current unit of work to the underlying database.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>
	///     A task representing the asynchronous operation. The task result contains the number of state entries
	///     written to the database.
	/// </returns>
	/// <exception cref="OperationCanceledException">Thrown if the operation is canceled before completion.</exception>
	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	///     Begins an explicit database transaction boundary across all coordinated repositories.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="InvalidOperationException">Thrown if a transaction is already active on this unit of work.</exception>
	/// <exception cref="OperationCanceledException">Thrown if the operation is canceled before completion.</exception>
	public Task BeginTransactionAsync(CancellationToken cancellationToken = default);

	/// <summary>
	///     Saves pending changes and commits the active database transaction atomically.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="InvalidOperationException">Thrown if no transaction has been started.</exception>
	/// <exception cref="OperationCanceledException">Thrown if the operation is canceled before completion.</exception>
	public Task CommitTransactionAsync(CancellationToken cancellationToken = default);

	/// <summary>
	///     Discards pending changes and rolls back the active database transaction to its initial state.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="InvalidOperationException">Thrown if no transaction is currently active to roll back.</exception>
	/// <exception cref="OperationCanceledException">Thrown if the operation is canceled before completion.</exception>
	public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
