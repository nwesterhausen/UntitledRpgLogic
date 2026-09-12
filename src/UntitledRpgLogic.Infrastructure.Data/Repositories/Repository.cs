using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Infrastructure.Data.Repositories;

/// <summary>
///     Implementation of <see cref="IRepository{T}" />. Interacts with the <see cref="DbSet" /> declared on the
///     <see cref="RpgDbContext" />.
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
	/// <summary>
	///     Creates a repository for the supplied database context.
	/// </summary>
	/// <param name="context">Database context to create a respository within</param>
	public Repository(RpgDbContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		this.Context = context;
		this.DbSet = context.Set<T>();
	}

	/// <summary>
	///     Gets the underlying <see cref="RpgDbContext" />.
	/// </summary>
	protected RpgDbContext Context { get; }

	/// <summary>
	///     Gets the entity <see cref="Microsoft.EntityFrameworkCore.DbSet{T}" />.
	/// </summary>
	protected DbSet<T> DbSet { get; }

	/// <inheritdoc />
	public virtual async Task<T?> FirstOrDefaultAsync(
		Expression<Func<T, bool>> predicate,
		CancellationToken cancellationToken = default,
		params Expression<Func<T, object?>>[] includes)
	{
		ArgumentNullException.ThrowIfNull(predicate);

		var query = ApplyIncludes(this.DbSet, includes);
		return await query.FirstOrDefaultAsync(predicate, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public virtual async Task<IReadOnlyList<T>> GetAsync(
		Expression<Func<T, bool>>? predicate = null,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		int? skip = null,
		int? take = null,
		CancellationToken cancellationToken = default,
		params Expression<Func<T, object?>>[] includes)
	{
		var query = ApplyIncludes(this.DbSet, includes);

		if (predicate is not null)
		{
			query = query.Where(predicate);
		}

		if (orderBy is not null)
		{
			query = orderBy(query);
		}

		if (skip.HasValue)
		{
			query = query.Skip(skip.Value);
		}

		if (take.HasValue)
		{
			query = query.Take(take.Value);
		}

		return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public virtual async Task<bool> AnyAsync(
		Expression<Func<T, bool>> predicate,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(predicate);
		return await this.DbSet.AnyAsync(predicate, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public virtual async Task<int> CountAsync(
		Expression<Func<T, bool>>? predicate = null,
		CancellationToken cancellationToken = default) =>
		predicate is null
			? await this.DbSet.CountAsync(cancellationToken).ConfigureAwait(false)
			: await this.DbSet.CountAsync(predicate, cancellationToken).ConfigureAwait(false);

	/// <inheritdoc />
	public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(entity);
		await this.DbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(entities);
		await this.DbSet.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public virtual void Update(T entity)
	{
		ArgumentNullException.ThrowIfNull(entity);
		this.DbSet.Update(entity);
	}

	/// <inheritdoc />
	public virtual void Remove(T entity)
	{
		ArgumentNullException.ThrowIfNull(entity);
		this.DbSet.Remove(entity);
	}

	/// <inheritdoc />
	public virtual void RemoveRange(IEnumerable<T> entities)
	{
		ArgumentNullException.ThrowIfNull(entities);
		this.DbSet.RemoveRange(entities);
	}

	/// <summary>
	///     Safely applies optional include expressions to an EF Core query.
	/// </summary>
	protected static IQueryable<T> ApplyIncludes(
		IQueryable<T> query,
		IEnumerable<Expression<Func<T, object?>>>? includes)
	{
		if (includes is null)
		{
			return query;
		}

		return includes.Aggregate(query, (current, include) => current.Include(include));
	}
}
