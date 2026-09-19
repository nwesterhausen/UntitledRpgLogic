namespace UntitledRpgLogic.Core.Networking;

/// <summary>
///     Tracks entity spatial locations and calculates network visibility subscriptions across chunk boundaries.
/// </summary>
public interface IAreaOfInterestService
{
	/// <summary>
	///     Updates the tracked spatial position for an entity across maps and coordinates.
	/// </summary>
	/// <param name="entityId">The unique identifier of the entity.</param>
	/// <param name="mapId">The unique identifier of the map.</param>
	/// <param name="x">The horizontal coordinate.</param>
	/// <param name="y">The vertical coordinate.</param>
	public void UpdateEntityPosition(Ulid entityId, Ulid mapId, float x, float y);

	/// <summary>
	///     Retrieves the collection of client session identifiers subscribed to updates in the target chunk.
	/// </summary>
	/// <param name="mapId">The unique identifier of the map.</param>
	/// <param name="chunkX">The horizontal chunk coordinate.</param>
	/// <param name="chunkY">The vertical chunk coordinate.</param>
	/// <returns>A collection of subscribed client connection identifiers.</returns>
	public IReadOnlyCollection<string> GetSubscribedClients(Ulid mapId, int chunkX, int chunkY);
}
