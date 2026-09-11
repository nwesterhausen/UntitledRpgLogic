using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Infrastructure.Data.Repositories;

/// <inheritdoc />
public class EntityRepository<TEntity, TId> : Repository<TEntity>, IEntityRepository<TEntity, TId>
	where TEntity : class, IDbEntity<TId>
	where TId : notnull
{
	/// <inheritdoc />
	public EntityRepository(RpgDbContext context) : base(context)
	{
	}

	/// <inheritdoc />
	public virtual async Task<TEntity?> GetByIdAsync(
		TId id,
		CancellationToken cancellationToken = default,
		params Expression<Func<TEntity, object?>>[] includes)
	{
		var query = ApplyIncludes(this.DbSet, includes);

		return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public virtual async Task<TEntity?> GetByIdAsync(
		TId id,
		Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(include);

		var query = include(this.DbSet.AsQueryable());

		return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public virtual async Task<IReadOnlyList<TEntity>> GetByIdsAsync(
		IEnumerable<TId> ids,
		CancellationToken cancellationToken = default,
		params Expression<Func<TEntity, object?>>[] includes)
	{
		ArgumentNullException.ThrowIfNull(ids);

		var idList = ids as IReadOnlyCollection<TId> ?? ids.ToList();
		if (idList.Count == 0)
		{
			return Array.Empty<TEntity>();
		}

		var query = ApplyIncludes(this.DbSet, includes);

		return await query
			.Where(e => idList.Contains(e.Id))
			.ToListAsync(cancellationToken)
			.ConfigureAwait(false);
	}
}
