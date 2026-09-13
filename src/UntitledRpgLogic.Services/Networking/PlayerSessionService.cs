using System.Collections.Concurrent;
using System.Globalization;
using UntitledRpgLogic.Core.Networking;

namespace UntitledRpgLogic.Services;

/// <summary>
///     In-memory player session service maintaining thread-safe bidirectional mappings
///     between player entity identifiers and network connection identifiers.
/// </summary>
public sealed class PlayerSessionService : IPlayerSessionService
{
	private readonly ConcurrentDictionary<string, Ulid> connectionToPlayer = new();
	private readonly ConcurrentDictionary<Ulid, string> playerToConnection = new();

	/// <inheritdoc />
	public Task<string?> GetPlayerSessionAsync(Ulid playerId)
	{
		this.playerToConnection.TryGetValue(playerId, out var connectionId);
		return Task.FromResult(connectionId);
	}

	/// <inheritdoc />
	public Task RegisterSessionAsync(Ulid playerId, long connectionId)
	{
		var connStr = connectionId.ToString(CultureInfo.InvariantCulture);

		// If this player had a previous connection, clean up reverse lookup
		if (this.playerToConnection.TryGetValue(playerId, out var oldConn))
		{
			this.connectionToPlayer.TryRemove(oldConn, out _);
		}

		// If this connection ID was bound to another player, clean up forward lookup
		if (this.connectionToPlayer.TryGetValue(connStr, out var oldPlayer))
		{
			this.playerToConnection.TryRemove(oldPlayer, out _);
		}

		this.playerToConnection[playerId] = connStr;
		this.connectionToPlayer[connStr] = playerId;

		return Task.CompletedTask;
	}

	/// <inheritdoc />
	public Task RemoveSessionAsync(Ulid playerId)
	{
		if (this.playerToConnection.TryRemove(playerId, out var connectionId))
		{
			this.connectionToPlayer.TryRemove(connectionId, out _);
		}

		return Task.CompletedTask;
	}
}
