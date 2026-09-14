using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.WorldGen.Generators;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.UnitTests.WorldGen;

[TestClass]
public sealed class MacroGenerationTests
{
	[TestMethod]
	public void SimplexNoise_IdenticalInputsAndSeeds_ProduceDeterministicValues()
	{
		var sample1 = SimplexNoise.Sample(12.34f, 56.78f, 42u);
		var sample2 = SimplexNoise.Sample(12.34f, 56.78f, 42u);
		var sampleOther = SimplexNoise.Sample(12.34f, 56.78f, 99u);

		Assert.AreEqual(sample1, sample2);
		Assert.AreNotEqual(sample1, sampleOther);
	}

	[TestMethod]
	public void MacroHeightmapGenerator_ValidDimensions_GeneratesWithinSpecifiedBounds()
	{
		var settings = new HeightmapSettings();
		var mapConfig = new WorldMapConfiguration
		{
			HeightTiles = 64,
			WidthTiles = 64,
			MinElevation = -500,
			MaxElevation = 1500,
			Heightmap = settings
		};

		var map = MacroHeightmapGenerator.Generate(12345u, mapConfig);

		Assert.AreEqual(64, map.WidthTiles);
		Assert.AreEqual(64, map.HeightTiles);

		for (var y = 0; y < map.HeightTiles; y++)
		{
			for (var x = 0; x < map.WidthTiles; x++)
			{
				var elev = map.GetElevation(x, y);
				Assert.IsGreaterThanOrEqualTo(mapConfig.MinElevation, elev);
				Assert.IsLessThanOrEqualTo(mapConfig.MaxElevation, elev);
			}
		}
	}

	[TestMethod]
	public void MacroHydrologyGenerator_SubSeaBasins_PopulatesWaterDepthAndMaterial()
	{
		var mapConfig = new WorldMapConfiguration
		{
			HeightTiles = 32,
			WidthTiles = 32,
			MinElevation = -1000,
			MaxElevation = 1000,
			Heightmap = new HeightmapSettings { SeaLevel = 0 }
		};
		var heightmap = MacroHeightmapGenerator.Generate(777u, mapConfig);

		var waterId = Ulid.NewUlid();
		var hydrology = MacroHydrologyGenerator.Generate(heightmap, 777u, mapConfig, waterId);

		for (var y = 0; y < 32; y++)
		{
			for (var x = 0; x < 32; x++)
			{
				var elev = heightmap.GetElevation(x, y);
				if (elev < 0)
				{
					Assert.IsGreaterThan(0, hydrology.GetLiquidDepth(x, y));
					Assert.AreEqual(waterId, hydrology.GetLiquidMaterial(x, y));
				}
			}
		}
	}

	[TestMethod]
	public void FloodFillConnectedOceans_IsolatedInlandBasin_RemainsDry()
	{
		var heightmap = new TerrainHeightmap(5, 5);

		// Create a 5x5 bowl: edges are at elevation 100m, center (2,2) is at -500m
		for (var y = 0; y < 5; y++)
		{
			for (var x = 0; x < 5; x++)
			{
				heightmap.SetElevation(x, y, 100);
			}
		}

		heightmap.SetElevation(2, 2, -500);

		var waterId = Ulid.NewUlid();
		var hydrology = MacroHydrologyGenerator.Generate(
			heightmap,
			42u,
			new WorldMapConfiguration
			{
				HeightTiles = 5,
				WidthTiles = 5,
				Hydrology = new HydrologySettings { SeaLevel = 0, RaindropCycles = 0 }
			}, waterId);

		// The center tile is below sea level, but because the perimeter is above sea level,
		// the ocean cannot breach it.
		Assert.AreEqual(0, hydrology.GetLiquidDepth(2, 2), "Inland basin below sea level should remain dry.");
		Assert.IsNull(hydrology.GetLiquidMaterial(2, 2));
	}
}
