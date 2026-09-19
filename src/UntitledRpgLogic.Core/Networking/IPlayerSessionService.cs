namespace UntitledRpgLogic.Core.Networking;

/// <summary>
///     Manages active player network sessions and client connection associations.
/// </summary>
public interface IPlayerSessionService
{
	/// <summary>
	///     Retrieves the connection identifier associated with an active player session.
	/// </summary>
	/// <param name="playerId">The unique identifier of the player entity.</param>
	/// <returns>A task returning the connection identifier, or <see langword="null" /> if not connected.</returns>
	public Task<string?> GetPlayerSessionAsync(Ulid playerId);

	/// <summary>
	///     Registers an active session mapping a player entity to a network connection identifier.
	/// </summary>
	/// <param name="playerId">The unique identifier of the player entity.</param>
	/// <param name="connectionId">The low-level network connection identifier.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public Task RegisterSessionAsync(Ulid playerId, long connectionId);

	/// <summary>
	///     Removes an active session mapping when a client disconnects.
	/// </summary>
	/// <param name="playerId">The unique identifier of the player entity to deregister.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public Task RemoveSessionAsync(Ulid playerId);
}
