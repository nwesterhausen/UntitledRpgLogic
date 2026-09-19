namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Contract for a coordinator service to handle interacting with the world map.
/// </summary>
public interface IWorldCoordinatorService
{
	/// <summary>
	///     Moves an entity to a target map coordinate, updating spatial positioning
	///     and verifying chunk/map boundaries.
	/// </summary>
	public Task<bool> MoveEntityAsync(
		Ulid entityId,
		Ulid targetMapId,
		float targetX,
		float targetY,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Loads, decompresses, and returns a chunk's tile grid, generating it if absent.
	/// </summary>
	public Task<WorldChunk> GetOrLoadChunkAsync(
		Ulid mapId,
		int chunkX,
		int chunkY,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Executes map transition portals or doors between source and target locations.
	/// </summary>
	public Task<bool> TriggerTransitionAsync(
		Ulid entityId,
		Ulid transitionId,
		CancellationToken cancellationToken = default);
}
