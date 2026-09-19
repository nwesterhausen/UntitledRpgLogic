using System.Globalization;
using UntitledRpgLogic.Core.Networking;
using UntitledRpgLogic.Services.Domains;
using UntitledRpgLogic.Services.Networking;

namespace UntitledRpgLogic.UnitTests.Networking;

[TestClass]
public sealed class AreaOfInterestServiceTests
{
	private readonly FakePlayerSessionService playerSessionService = new();
	private readonly SpatialMathService spatialMathService = new();

	[TestMethod]
	public void UpdateEntityPosition_PlayerClientMoves_SubscribesToSurroundingChunks()
	{
		var service = new AreaOfInterestService(this.spatialMathService, this.playerSessionService);
		var playerId = Ulid.NewUlid();
		var mapId = Ulid.NewUlid();
		const string ConnectionId = "conn_42";

		this.playerSessionService.Register(playerId, ConnectionId);

		// Position 0,0 falls into chunk (0, 0)
		service.UpdateEntityPosition(playerId, mapId, 0f, 0f);

		// Center chunk (0, 0) must have the client subscribed
		var centerSubscribers = service.GetSubscribedClients(mapId, 0, 0);
		Assert.Contains(ConnectionId, centerSubscribers);

		// Neighbor chunk (1, 1) within 1-chunk radius must also have the client subscribed
		var neighborSubscribers = service.GetSubscribedClients(mapId, 1, 1);
		Assert.Contains(ConnectionId, neighborSubscribers);

		// Far chunk (3, 3) must NOT have the client subscribed
		var farSubscribers = service.GetSubscribedClients(mapId, 3, 3);
		Assert.DoesNotContain(ConnectionId, farSubscribers);
	}

	[TestMethod]
	public void UpdateEntityPosition_PlayerMovesFarAway_UnsubscribesFromDistantChunks()
	{
		var service = new AreaOfInterestService(this.spatialMathService, this.playerSessionService);
		var playerId = Ulid.NewUlid();
		var mapId = Ulid.NewUlid();
		const string connectionId = "conn_99";

		this.playerSessionService.Register(playerId, connectionId);

		// Step 1: Client starts at chunk (0, 0)
		service.UpdateEntityPosition(playerId, mapId, 0f, 0f);
		Assert.Contains(connectionId, service.GetSubscribedClients(mapId, 0, 0));

		// Step 2: Client moves far away to chunk (10, 10) (160 world units)
		service.UpdateEntityPosition(playerId, mapId, 160f, 160f);

		// Old chunk (0, 0) should no longer have the client
		Assert.DoesNotContain(connectionId, service.GetSubscribedClients(mapId, 0, 0));

		// New chunk (10, 10) should have the client
		Assert.Contains(connectionId, service.GetSubscribedClients(mapId, 10, 10));
	}

	[TestMethod]
	public void UpdateEntityPosition_NonPlayerEntity_DoesNotRegisterSubscriptions()
	{
		var service = new AreaOfInterestService(this.spatialMathService, this.playerSessionService);
		var npcId = Ulid.NewUlid(); // Not in player session service
		var mapId = Ulid.NewUlid();

		service.UpdateEntityPosition(npcId, mapId, 5f, 5f);

		var subscribers = service.GetSubscribedClients(mapId, 0, 0);
		Assert.IsEmpty(subscribers);
	}

	private sealed class FakePlayerSessionService : IPlayerSessionService
	{
		private readonly Dictionary<Ulid, string> sessions = new();

		public Task<string?> GetPlayerSessionAsync(Ulid playerId)
		{
			this.sessions.TryGetValue(playerId, out var connectionId);
			return Task.FromResult(connectionId);
		}

		public Task RegisterSessionAsync(Ulid playerId, long connectionId)
		{
			this.sessions[playerId] = connectionId.ToString(CultureInfo.InvariantCulture);
			return Task.CompletedTask;
		}

		public Task RemoveSessionAsync(Ulid playerId)
		{
			this.sessions.Remove(playerId);
			return Task.CompletedTask;
		}

		public void Register(Ulid playerId, string connectionId) => this.sessions[playerId] = connectionId;
	}
}
