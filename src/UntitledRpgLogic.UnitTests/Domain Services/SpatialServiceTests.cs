using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Services.Domains;

namespace UntitledRpgLogic.UnitTests.Domain_Services;

[TestClass]
public sealed class SpatialAndRespirationServiceTests
{
	private readonly SpatialMathService spatialMathService = new();

	[TestMethod]
	[DataRow(0f, 0f, 0, 0)]
	[DataRow(15.9f, 15.9f, 0, 0)]
	[DataRow(16f, 16f, 1, 1)]
	[DataRow(-0.1f, -0.1f, -1, -1)]
	[DataRow(-16f, -16f, -1, -1)]
	[DataRow(-16.1f, -16.1f, -2, -2)]
	public void WorldToChunkCoordinates_TranslatesBothPositiveAndNegativeGrids(
		float worldX, float worldY, int expectedChunkX, int expectedChunkY)
	{
		var (chunkX, chunkY) = this.spatialMathService.WorldToChunkCoordinates(worldX, worldY);

		Assert.AreEqual(expectedChunkX, chunkX);
		Assert.AreEqual(expectedChunkY, chunkY);
	}

	[TestMethod]
	[DataRow(5f, 7f, 5, 7)]
	[DataRow(16f, 17f, 0, 1)]
	[DataRow(-1f, -1f, 15, 15)]
	[DataRow(-16f, -16f, 0, 0)]
	public void WorldToLocalTileIndex_WrapsCorrectlyWithinSixteenTileBounds(
		float worldX, float worldY, int expectedTileX, int expectedTileY)
	{
		var (tileX, tileY) = this.spatialMathService.WorldToLocalTileIndex(worldX, worldY);

		Assert.AreEqual(expectedTileX, tileX);
		Assert.AreEqual(expectedTileY, tileY);
	}

	[TestMethod]
	public void CalculateDistance_MismatchedMapIds_ReturnsInfinity()
	{
		var posA = new WorldPosition { MapId = Ulid.NewUlid(), X = 0f, Y = 0f };
		var posB = new WorldPosition { MapId = Ulid.NewUlid(), X = 5f, Y = 5f };

		var dist = this.spatialMathService.CalculateDistance(posA, posB);

		Assert.IsTrue(float.IsPositiveInfinity(dist));
	}
}
