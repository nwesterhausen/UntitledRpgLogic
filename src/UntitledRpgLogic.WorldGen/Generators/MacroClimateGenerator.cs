using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Extensions.Common;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

/// <summary>
///     Generates temperature gradients, precipitation simulations, and Whittaker-style biome classifications.
/// </summary>
public static class MacroClimateGenerator
{
	/// <summary>
	///     Generates temperature, rainfall, and derived biomes across the map based on heightmap and hydrology inputs.
	/// </summary>
	public static TerrainClimate Generate(
		TerrainHeightmap heightmap,
		TerrainHydrology hydrology,
		uint seed,
		ClimateSettings? settings = null)
	{
		ArgumentNullException.ThrowIfNull(heightmap);
		ArgumentNullException.ThrowIfNull(hydrology);

		var cfg = settings ?? new ClimateSettings();
		var width = heightmap.WidthTiles;
		var height = heightmap.HeightTiles;
		var climate = new TerrainClimate(width, height);

		var tempSeed = seed ^ 0x9e3779b9u;
		var rainSeed = seed ^ 0x85ebca6bu;
		var halfHeight = height / 2.0f;

		for (var y = 0; y < height; y++)
		{
			// Latitudinal factor: 1.0 at equator (center), 0.0 at poles (top/bottom edges)
			var latDist = Math.Abs(y - halfHeight) / halfHeight;
			var latFactor = 1.0f - latDist;
			var baseLatTemp = MathF.Min(cfg.EquatorTemperature, cfg.PoleTemperature) +
			                  (latFactor * MathF.Abs(cfg.EquatorTemperature - cfg.PoleTemperature));

			for (var x = 0; x < width; x++)
			{
				var elevation = heightmap.GetElevation(x, y);
				var liquidDepth = hydrology.GetLiquidDepth(x, y);

				// 1. Calculate temperature (latitude baseline + lapse rate cooling + simplex noise turbulence)
				var lapseCooling = Math.Max((short)0, elevation) / 1000.0f * cfg.LapseRatePer1000M;
				var tempNoise = SimplexNoise.Sample(x * cfg.TemperatureNoiseFrequency,
					y * cfg.TemperatureNoiseFrequency, tempSeed) * 5.0f;
				var temperature = baseLatTemp - lapseCooling + tempNoise;

				// 2. Calculate rainfall (fBm noise + proximity to water bodies)
				var rainNoise =
					(SimplexNoise.SampleFbm(x * cfg.RainfallNoiseFrequency, y * cfg.RainfallNoiseFrequency, rainSeed,
						4) + 1.0f) * 0.5f;
				var riverBonus = hydrology.IsRiverWithinDistance(x, y, 2) ? 0.25f : 0.0f;
				var rainfall = Math.Clamp(rainNoise + riverBonus, 0.0f, 1.0f);
				var isRiver = hydrology.IsRiver(x, y);

				// 3. Classify Whittaker biome
				var biome = ClassifyBiome(elevation, liquidDepth, temperature, rainfall, cfg.MountainThreshold,
					isRiver);

				climate.SetCell(x, y, temperature, rainfall, biome);
			}
		}

		return climate;
	}

	/// <summary>
	///     Classifies a tile coordinate into a <see cref="BiomeType" /> using elevation, temperature, and moisture.
	/// </summary>
	public static BiomeType ClassifyBiome(
		short elevation,
		ushort liquidDepth,
		float temperatureCelsius,
		float rainfall,
		short mountainThreshold,
		bool isRiver = false)
	{
// 1. Connected ocean basins (MUST have liquidDepth > 0)
		if (liquidDepth > 0 && elevation < 0 && !isRiver)
		{
			return BiomeType.Ocean;
		}

		// 2. Flowing inland rivers
		if (liquidDepth > 0 && isRiver)
		{
			return BiomeType.River;
		}

		// 3. Standing inland lakes (can be above sea level OR in sub-sea depressions)
		if (liquidDepth > 0)
		{
			return temperatureCelsius <= 0.0f ? BiomeType.Glacial : BiomeType.Lake;
		}

		// 4. Dry land (handles elevations above 0, as well as DRY depressions below 0)
		if (elevation >= mountainThreshold)
		{
			return temperatureCelsius <= 0.0f ? BiomeType.Glacial : BiomeType.Mountain;
		}

		if (temperatureCelsius < -5.0f)
		{
			return BiomeType.Glacial;
		}

		if (temperatureCelsius < 3.0f)
		{
			return rainfall < 0.35f ? BiomeType.Tundra : BiomeType.Taiga;
		}

		if (temperatureCelsius < 18.0f)
		{
			if (rainfall < 0.25f)
			{
				return BiomeType.Desert;
			}

			if (rainfall < 0.60f)
			{
				return BiomeType.Grassland;
			}

			return BiomeType.TemperateForest;
		}

		// Tropical / Warm regimes
		if (rainfall < 0.20f)
		{
			return BiomeType.Desert;
		}

		if (rainfall < 0.50f)
		{
			return BiomeType.Savanna;
		}

		return BiomeType.TropicalRainforest;
	}
}
