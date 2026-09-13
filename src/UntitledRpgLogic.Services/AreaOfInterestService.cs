using System.Collections.Concurrent;
using UntitledRpgLogic.Core.Networking;
using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.Services;

/// <summary>
///     In-memory spatial interest management service tracking entity chunk locations
///     and computing client subscription sets for localized network broadcasting.
/// </summary>
public sealed class AreaOfInterestService : IAreaOfInterestService
{
	// View radius in chunks (1 = 3x3 chunk grid surrounding the player: -1, 0, +1)
	private const int ViewRadiusInChunks = 1;

	// (MapId, ChunkX, ChunkY) -> Set of Client Connection Ids
	private readonly ConcurrentDictionary<ChunkKey, HashSet<string>> chunkSubscriptions = new();

	// ClientId -> Set of ChunkKeys currently observed
	private readonly ConcurrentDictionary<string, HashSet<ChunkKey>> clientObservedChunks = new();

	// EntityId -> (MapId, ChunkX, ChunkY)
	private readonly ConcurrentDictionary<Ulid, EntityLocation> entityLocations = new();
	private readonly IPlayerSessionService playerSessionService;
	private readonly ISpatialMathService spatialMathService;

	private readonly object syncLock = new();

	/// <summary>
	///     Initializes a new instance of the <see cref="AreaOfInterestService" /> class.
	/// </summary>
	/// <param name="spatialMathService">Pure domain service for world-to-chunk coordinate math.</param>
	/// <param name="playerSessionService">Service providing active player session mappings.</param>
	public AreaOfInterestService(
		ISpatialMathService spatialMathService,
		IPlayerSessionService playerSessionService)
	{
		this.spatialMathService = spatialMathService ?? throw new ArgumentNullException(nameof(spatialMathService));
		this.playerSessionService =
			playerSessionService ?? throw new ArgumentNullException(nameof(playerSessionService));
	}

	/// <inheritdoc />
	public void UpdateEntityPosition(Ulid entityId, Ulid mapId, float x, float y)
	{
		var (chunkX, chunkY) = this.spatialMathService.WorldToChunkCoordinates(x, y);
		var newLocation = new EntityLocation(mapId, chunkX, chunkY);

		this.entityLocations.AddOrUpdate(entityId, newLocation, (_, _) => newLocation);

		// Check if this entity corresponds to an active connected client
		var clientId = this.playerSessionService.GetPlayerSessionAsync(entityId).GetAwaiter().GetResult();
		if (clientId is null)
		{
			return;
		}

		this.UpdateClientSubscriptions(clientId, mapId, chunkX, chunkY);
	}

	/// <inheritdoc />
	public IReadOnlyCollection<string> GetSubscribedClients(Ulid mapId, int chunkX, int chunkY)
	{
		var key = new ChunkKey(mapId, chunkX, chunkY);

		lock (this.syncLock)
		{
			return this.chunkSubscriptions.TryGetValue(key, out var clients)
				? clients.ToArray()
				: Array.Empty<string>();
		}
	}

	private void UpdateClientSubscriptions(string clientId, Ulid mapId, int centerChunkX, int centerChunkY)
	{
		var newVisibleChunks = new HashSet<ChunkKey>();

		for (var dx = -ViewRadiusInChunks; dx <= ViewRadiusInChunks; dx++)
		{
			for (var dy = -ViewRadiusInChunks; dy <= ViewRadiusInChunks; dy++)
			{
				newVisibleChunks.Add(new ChunkKey(mapId, centerChunkX + dx, centerChunkY + dy));
			}
		}

		lock (this.syncLock)
		{
			var oldVisibleChunks = this.clientObservedChunks.TryGetValue(clientId, out var existing)
				? existing
				: [];

			// Chunks the client left
			foreach (var chunk in oldVisibleChunks)
			{
				if (!newVisibleChunks.Contains(chunk) &&
					this.chunkSubscriptions.TryGetValue(chunk, out var subscribers))
				{
					subscribers.Remove(clientId);
					if (subscribers.Count == 0)
					{
						this.chunkSubscriptions.TryRemove(chunk, out _);
					}
				}
			}

			// Chunks the client newly entered
			foreach (var chunk in newVisibleChunks)
			{
				if (!this.chunkSubscriptions.TryGetValue(chunk, out var subscribers))
				{
					subscribers = [];
					this.chunkSubscriptions[chunk] = subscribers;
				}

				subscribers.Add(clientId);
			}

			this.clientObservedChunks[clientId] = newVisibleChunks;
		}
	}

	private readonly record struct EntityLocation(Ulid MapId, int ChunkX, int ChunkY);

	private readonly record struct ChunkKey(Ulid MapId, int ChunkX, int ChunkY);
}
