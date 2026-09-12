using Microsoft.EntityFrameworkCore.Storage;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Infrastructure.Data;

/// <summary>
///     The implementation of <see cref="IUnitOfWork" /> that allows for modifying/connecting with a database provider.
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
	private readonly RpgDbContext _context;
	private IDbContextTransaction? _currentTransaction;

	/// <summary> Create the unit of work machine for supplied context </summary>
	/// <param name="context">the database context</param>
	public UnitOfWork(RpgDbContext context) =>
		this._context = context ?? throw new ArgumentNullException(nameof(context));

	/// <inheritdoc />
	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
		await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

	/// <inheritdoc />
	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		if (this._currentTransaction is not null)
		{
			return;
		}

		this._currentTransaction =
			await this._context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

			if (this._currentTransaction is not null)
			{
				await this._currentTransaction.CommitAsync(cancellationToken).ConfigureAwait(false);
			}
		}
		catch
		{
			await this.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
			throw;
		}
		finally
		{
			if (this._currentTransaction is not null)
			{
				await this._currentTransaction.DisposeAsync().ConfigureAwait(false);
				this._currentTransaction = null;
			}
		}
	}

	/// <inheritdoc />
	public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			if (this._currentTransaction is not null)
			{
				await this._currentTransaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
			}
		}
		finally
		{
			if (this._currentTransaction is not null)
			{
				await this._currentTransaction.DisposeAsync().ConfigureAwait(false);
				this._currentTransaction = null;
			}
		}
	}

	/// <inheritdoc />
	public void Dispose()
	{
		this._currentTransaction?.Dispose();
		this._context.Dispose();

		GC.SuppressFinalize(this);
	}

	/// <inheritdoc />
	public async ValueTask DisposeAsync()
	{
		if (this._currentTransaction is not null)
		{
			await this._currentTransaction.DisposeAsync().ConfigureAwait(false);
		}

		await this._context.DisposeAsync().ConfigureAwait(false);

		GC.SuppressFinalize(this);
	}
}
