namespace UntitledRpgLogic.Core.Interfaces.Data;

/// <summary>
///     Interface for a unit of work pattern which is designed for handling the interfaces defined in this project.
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	Task BeginTransactionAsync(CancellationToken cancellationToken = default);
	Task CommitTransactionAsync(CancellationToken cancellationToken = default);
	Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
