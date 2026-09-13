using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Extensions.Common;
using UntitledRpgLogic.WorldGen.Generators;
using UntitledRpgLogic.WorldGen.Models;
using UntitledRpgLogic.WorldGen.Services;

namespace UntitledRpgLogic.UnitTests.WorldGen;

[TestClass]
public sealed class ChunkGeneratorServiceTests
{
	[TestMethod]
	public void GenerateChunk_ValidCoordinates_BuildsValidTileBlobAndPalette()
	{
		const uint seed = 42u;
		var waterId = Ulid.NewUlid();
		var mapping = new BiomeMaterialMapping { WaterMaterialId = waterId };

		var heightmap = MacroHeightmapGenerator.Generate(32, 32, seed);
		var hydrology = MacroHydrologyGenerator.Generate(heightmap, waterId, seed);
		var climate = MacroClimateGenerator.Generate(heightmap, hydrology, seed);
		var context = new WorldGenContext(heightmap, hydrology, climate, mapping, seed);

		var service = new ChunkGeneratorService();
		var mapId = Ulid.NewUlid();

		var chunk = service.GenerateChunk(mapId, chunkX: 1, chunkY: 1, context);

		Assert.AreEqual(mapId, chunk.MapId);
		Assert.AreEqual(1, chunk.ChunkX);
		Assert.AreEqual(1, chunk.ChunkY);
		Assert.IsNotEmpty(chunk.MaterialPalette);
		Assert.IsNotEmpty(chunk.CompressedTileBlob);

		// Verify decompressed tiles match palette bounds
		var tiles = new Tile2D[ChunkBlobExtensions.TileCount];
		chunk.CompressedTileBlob.DecompressTilesInto(tiles);

		foreach (var tile in tiles)
		{
			Assert.IsLessThan(chunk.MaterialPalette.Count, tile.GroundPaletteIndex);
			if (tile.LiquidDepth > 0)
			{
				Assert.IsLessThan(chunk.MaterialPalette.Count, tile.LiquidPaletteIndex);
			}
		}
	}
}
