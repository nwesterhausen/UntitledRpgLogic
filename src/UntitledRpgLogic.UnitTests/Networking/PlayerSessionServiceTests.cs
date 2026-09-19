using UntitledRpgLogic.Services.Networking;

namespace UntitledRpgLogic.UnitTests.Networking;

[TestClass]
public sealed class PlayerSessionServiceTests
{
	private readonly PlayerSessionService service = new();

	[TestMethod]
	public async Task GetPlayerSessionAsync_UnregisteredPlayer_ReturnsNull()
	{
		var session = await this.service.GetPlayerSessionAsync(Ulid.NewUlid()).ConfigureAwait(false);
		Assert.IsNull(session);
	}

	[TestMethod]
	public async Task RegisterSessionAsync_ValidRegistration_BindsConnectionId()
	{
		var playerId = Ulid.NewUlid();
		const long connectionId = 12345;

		await this.service.RegisterSessionAsync(playerId, connectionId).ConfigureAwait(false);

		var retrieved = await this.service.GetPlayerSessionAsync(playerId).ConfigureAwait(false);
		Assert.AreEqual("12345", retrieved);
	}

	[TestMethod]
	public async Task RegisterSessionAsync_PlayerReconnectsWithNewConnection_OverwritesAndCleansUp()
	{
		var playerId = Ulid.NewUlid();

		await this.service.RegisterSessionAsync(playerId, 101).ConfigureAwait(false);
		await this.service.RegisterSessionAsync(playerId, 202).ConfigureAwait(false);

		var current = await this.service.GetPlayerSessionAsync(playerId).ConfigureAwait(false);
		Assert.AreEqual("202", current);
	}

	[TestMethod]
	public async Task RemoveSessionAsync_ExistingPlayer_RemovesMapping()
	{
		var playerId = Ulid.NewUlid();
		await this.service.RegisterSessionAsync(playerId, 555).ConfigureAwait(false);

		await this.service.RemoveSessionAsync(playerId).ConfigureAwait(false);

		var result = await this.service.GetPlayerSessionAsync(playerId).ConfigureAwait(false);
		Assert.IsNull(result);
	}
}
