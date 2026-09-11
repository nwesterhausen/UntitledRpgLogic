using System.Linq.Expressions;

namespace UntitledRpgLogic.Core.Data;

/// <summary>
///     Generic repository interface providing persistence operations and eager loading for root entities.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TId">The entity identifier type.</typeparam>
public interface IEntityRepository<TEntity, in TId> where TEntity : class, IDbEntity<TId>
{
	/// <summary>
	///     Retrieves an entity by its identifier with optional single-level property includes.
	/// </summary>
	Task<TEntity?> GetByIdAsync(
		TId id,
		CancellationToken cancellationToken = default,
		params Expression<Func<TEntity, object?>>[] includes);

	/// <summary>
	///     Retrieves an entity by its identifier using a composable query builder to support deep navigation includes (ThenInclude).
	/// </summary>
	/// <param name="id">The entity identifier.</param>
	/// <param name="include">A function to configure eager-loading includes and then-includes.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The hydrated entity if found; otherwise, null.</returns>
	Task<TEntity?> GetByIdAsync(
		TId id,
		Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Retrieves multiple entities by their unique identifiers.
	/// </summary>
	Task<IReadOnlyList<TEntity>> GetByIdsAsync(
		IEnumerable<TId> ids,
		CancellationToken cancellationToken = default,
		params Expression<Func<TEntity, object?>>[] includes);

	/// <summary>
	///     Adds a new entity to the repository.
	/// </summary>
	Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

	/// <summary>
	///     Updates an existing entity in the repository.
	/// </summary>
	void Update(TEntity entity);

	/// <summary>
	///     Removes an entity from the repository.
	/// </summary>
	void Remove(TEntity entity);
}
