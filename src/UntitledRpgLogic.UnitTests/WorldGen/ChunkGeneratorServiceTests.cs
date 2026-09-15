using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Extensions.Common;
using UntitledRpgLogic.WorldGen;
using UntitledRpgLogic.WorldGen.Generators;

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
		var mapConfig = new WorldMapConfiguration { HeightTiles = 32, WidthTiles = 32 };

		var heightmap = MacroHeightmapGenerator.Generate(seed, mapConfig);
		var hydrology = MacroHydrologyGenerator.Generate(heightmap, seed, waterId, mapConfig);
		var climate = MacroClimateGenerator.Generate(heightmap, hydrology, seed, mapConfig);
		var context = new WorldGenContext(heightmap, hydrology, climate, mapping, seed);

		var service = new ChunkGeneratorService();
		var mapId = Ulid.NewUlid();

		var chunk = service.GenerateChunk(mapId, 1, 1, context);

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
