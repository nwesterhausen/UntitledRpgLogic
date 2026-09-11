using System.Linq.Expressions;

namespace UntitledRpgLogic.Core.Data;

/// <summary>
///     Generic repository interface supporting LINQ expression-based queries and change tracking.
/// </summary>
/// <typeparam name="T">The entity type managed by this repository.</typeparam>
public interface IRepository<T> where T : class
{
	/// <summary>
	///     Retrieves an entity matching the given predicate, or null if not found.
	/// </summary>
	Task<T?> FirstOrDefaultAsync(
		Expression<Func<T, bool>> predicate,
		CancellationToken cancellationToken = default,
		params Expression<Func<T, object?>>[] includes);

	/// <summary>
	///     Retrieves all entities matching the given predicate.
	/// </summary>
	Task<IReadOnlyList<T>> GetAsync(
		Expression<Func<T, bool>>? predicate = null,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		int? skip = null,
		int? take = null,
		CancellationToken cancellationToken = default,
		params Expression<Func<T, object?>>[] includes);

	/// <summary>
	///     Checks if any entity satisfies the specified predicate.
	/// </summary>
	Task<bool> AnyAsync(
		Expression<Func<T, bool>> predicate,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Returns the count of entities satisfying the specified predicate.
	/// </summary>
	Task<int> CountAsync(
		Expression<Func<T, bool>>? predicate = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Marks a new entity for insertion.
	/// </summary>
	Task AddAsync(T entity, CancellationToken cancellationToken = default);

	/// <summary>
	///     Marks a collection of entities for insertion.
	/// </summary>
	Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

	/// <summary>
	///     Marks an existing entity as modified.
	/// </summary>
	void Update(T entity);

	/// <summary>
	///     Marks an entity for deletion.
	/// </summary>
	void Remove(T entity);

	/// <summary>
	///     Marks a collection of entities for deletion.
	/// </summary>
	void RemoveRange(IEnumerable<T> entities);
}
