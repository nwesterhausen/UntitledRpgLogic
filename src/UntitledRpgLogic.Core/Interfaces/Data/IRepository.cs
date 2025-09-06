using System.Linq.Expressions;

namespace UntitledRpgLogic.Core.Interfaces.Data;

/// <summary>
///		The generic repository interface for performing basic data access operations on entities of type TEntity.
/// </summary>
/// <typeparam name="TEntity">The class or object that is stored in this table</typeparam>
/// <typeparam name="TId">The type of the entity's primary key.</typeparam>
public interface IRepository<TEntity, in TId>
	where TEntity : class, IDbEntity<TId>
	where TId : notnull
{
	/// <summary>
	///		Gets an entity of type TEntity by its unique identifier asynchronously.
	/// </summary>
	/// <param name="id">The identifier of the entity.</param>
	/// <returns>matching entity or nothing</returns>
	public ValueTask<TEntity?> GetByIdAsync(TId id);

	/// <summary>
	///		Gets all entities of type TEntity from the data source asynchronously.
	/// </summary>
	/// <returns>A list of all the entities of type TEntity</returns>
	public Task<IEnumerable<TEntity>> GetAllAsync();

	/// <summary>
	/// Finds entities of type TEntity that match the specified predicate asynchronously.
	/// </summary>
	/// <param name="predicate">The condition to filter the entities.</param>
	/// <returns>A collection of entities that satisfy the predicate.</returns>
	public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Retrieves a single entity of type TEntity that matches the specified predicate asynchronously.
	/// </summary>
	/// <param name="predicate">The condition to filter the entity.</param>
	/// <returns>The single matching entity, or null if no match is found.</returns>
	public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Checks if any entities of type TEntity match the specified predicate asynchronously.
	/// </summary>
	/// <param name="predicate">The condition to evaluate.</param>
	/// <returns>True if any entities match the predicate; otherwise, false.</returns>
	public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Adds a new entity of type TEntity to the data source asynchronously.
	/// </summary>
	/// <param name="entity">The entity to add.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public Task AddAsync(TEntity entity);

	/// <summary>
	/// Adds multiple entities of type TEntity to the data source asynchronously.
	/// </summary>
	/// <param name="entities">The collection of entities to add.</param>
	public Task AddRangeAsync(IEnumerable<TEntity> entities);

	/// <summary>
	/// Removes an entity of type TEntity from the data source.
	/// </summary>
	/// <param name="entity">The entity to remove.</param>
	public void Remove(TEntity entity);

	/// <summary>
	/// Removes multiple entities of type TEntity from the data source.
	/// </summary>
	/// <param name="entities">The collection of entities to remove.</param>
	public void RemoveRange(IEnumerable<TEntity> entities);
}
