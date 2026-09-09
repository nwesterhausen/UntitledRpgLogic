using Microsoft.EntityFrameworkCore.Storage;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Infrastructure.Data;

public sealed class UnitOfWork : IUnitOfWork
{
	private readonly RpgDbContext _context;
	private IDbContextTransaction? _currentTransaction;

	public UnitOfWork(RpgDbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
	}

	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		if (_currentTransaction is not null)
		{
			return;
		}

		_currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
	}

	public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

			if (_currentTransaction is not null)
			{
				await _currentTransaction.CommitAsync(cancellationToken).ConfigureAwait(false);
			}
		}
		catch
		{
			await RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
			throw;
		}
		finally
		{
			if (_currentTransaction is not null)
			{
				await _currentTransaction.DisposeAsync().ConfigureAwait(false);
				_currentTransaction = null;
			}
		}
	}

	public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			if (_currentTransaction is not null)
			{
				await _currentTransaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
			}
		}
		finally
		{
			if (_currentTransaction is not null)
			{
				await _currentTransaction.DisposeAsync().ConfigureAwait(false);
				_currentTransaction = null;
			}
		}
	}

	public void Dispose()
	{
		_currentTransaction?.Dispose();
		_context.Dispose();

		GC.SuppressFinalize(this);
	}

	public async ValueTask DisposeAsync()
	{
		if (_currentTransaction is not null)
		{
			await _currentTransaction.DisposeAsync().ConfigureAwait(false);
		}

		await _context.DisposeAsync().ConfigureAwait(false);

		GC.SuppressFinalize(this);
	}
}
