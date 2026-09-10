using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Core.Interfaces.Data.Repositories;

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
		IQueryable<TEntity> query = this.DbSet;
		query = ApplyIncludes(query, includes);

		return await query.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public virtual async Task<IReadOnlyList<TEntity>> GetByIdsAsync(
		IEnumerable<TId> ids,
		CancellationToken cancellationToken = default,
		params Expression<Func<TEntity, object?>>[] includes)
	{
		ArgumentNullException.ThrowIfNull(ids);
		var idList = ids.ToList();

		IQueryable<TEntity> query = this.DbSet;
		query = ApplyIncludes(query, includes);

		return await query
			.Where(e => idList.Contains(e.Id))
			.ToListAsync(cancellationToken)
			.ConfigureAwait(false);
	}
}
