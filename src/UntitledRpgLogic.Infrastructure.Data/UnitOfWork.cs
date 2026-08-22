using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Infrastructure.Data;

/// <summary>
///     An implementation of the Unit of Work pattern using Entity Framework Core.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="UnitOfWork" /> class.
/// </remarks>
/// <param name="context">The database context to be used for this unit of work.</param>
public class UnitOfWork(RpgDbContext context) : IUnitOfWork
{
	private readonly RpgDbContext context = context ?? throw new ArgumentNullException(nameof(context));
	private bool disposed;

	/// <inheritdoc />
	public int Commit() => this.context.SaveChanges();

	/// <inheritdoc />
	public Task<int> CommitAsync(CancellationToken cancellationToken = default) => this.context.SaveChangesAsync(cancellationToken);

	/// <inheritdoc />
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	///     Disposes the underlying DbContext.
	/// </summary>
	/// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!this.disposed)
		{
			if (disposing)
			{
				this.context.Dispose();
			}
		}

		this.disposed = true;
	}
}
