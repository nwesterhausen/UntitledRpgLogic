using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.WorldGen.Generators;

namespace UntitledRpgLogic.UnitTests.WorldGen;

[TestClass]
public sealed class MacroClimateTests
{
	[TestMethod]
	[DataRow(25.0f, 0.10f, BiomeType.Desert)]
	[DataRow(28.0f, 0.85f, BiomeType.TropicalRainforest)]
	[DataRow(12.0f, 0.40f, BiomeType.Grassland)]
	[DataRow(12.0f, 0.80f, BiomeType.TemperateForest)]
	[DataRow(0.0f, 0.50f, BiomeType.Taiga)]
	[DataRow(-10.0f, 0.20f, BiomeType.Glacial)]
	public void ClassifyBiome_StandardTerrains_MatchesWhittakerDiagram(
		float temperature,
		float rainfall,
		BiomeType expectedBiome)
	{
		var biome = MacroClimateGenerator.ClassifyBiome(
			100,
			0,
			temperature,
			rainfall,
			2000);

		Assert.AreEqual(expectedBiome, biome);
	}

	[TestMethod]
	public void ClassifyBiome_HighElevation_DerivesMountainOrGlacier()
	{
		var alpine = MacroClimateGenerator.ClassifyBiome(
			2500,
			0,
			10.0f,
			0.5f,
			2000);

		var frozenPeak = MacroClimateGenerator.ClassifyBiome(
			2500,
			0,
			-5.0f,
			0.5f,
			2000);

		Assert.AreEqual(BiomeType.Mountain, alpine);
		Assert.AreEqual(BiomeType.Glacial, frozenPeak);
	}

	[TestMethod]
	public void MacroClimateGenerator_ElevationLapseRate_CoolerAtHighAltitudes()
	{
		var heightmap = new TerrainHeightmap(10, 10);
		var hydrology = new TerrainHydrology(10, 10);

		// Cell (0, 5) low valley at equator; Cell (1, 5) high mountain at equator
		heightmap.SetElevation(0, 5, 0);
		heightmap.SetElevation(1, 5, 3000);

		var climate = MacroClimateGenerator.Generate(heightmap, hydrology, 12345u, new WorldMapConfiguration());

		var lowValleyTemp = climate.GetTemperature(0, 5);
		var mountainTemp = climate.GetTemperature(1, 5);

		Assert.IsLessThan(lowValleyTemp, mountainTemp, "High elevation should reduce temperature due to lapse rate.");
	}
}
