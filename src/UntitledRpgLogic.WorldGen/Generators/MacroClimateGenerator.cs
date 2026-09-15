using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.MapGrids;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;
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
		WorldMapConfiguration worldConfig)
	{
		ArgumentNullException.ThrowIfNull(heightmap);
		ArgumentNullException.ThrowIfNull(hydrology);
		ArgumentNullException.ThrowIfNull(worldConfig);

		var cfg = worldConfig.Climate ?? new ClimateSettings();
		var width = heightmap.WidthTiles;
		var height = heightmap.HeightTiles;

		// 1. Generate multi-octave temperature variations [-8.0°C to +8.0°C]
		var tempTurbulence = NoiseMaker.GenerateNoiseArray(
			new NoiseSettings
			{
				Seed = seed ^ 0x9e3779b9u,
				Scale = 32.0f,
				Octaves = 3,
				Persistence = 0.5f,
				Lacunarity = 2.0f,
				TargetMin = -8.0f,
				TargetMax = 8.0f
			}, width, height);

		// 2. Generate multi-octave base rainfall [0.0 to 1.0]
		var rawRainfall = NoiseMaker.GenerateNoiseArray(
			new NoiseSettings
			{
				Seed = seed ^ 0x85ebca6bu,
				Scale = 28.0f,
				Octaves = 4,
				Persistence = 0.5f,
				Lacunarity = 2.0f,
				TargetMin = 0.0f,
				TargetMax = 1.0f
			}, width, height);

		var climate = new TerrainClimate(width, height);
		var halfHeight = height / 2.0f;
		var warpSeed = seed ^ 0x517cc1b7u;

		for (var y = 0; y < height; y++)
		{
			var rowOffset = y * width;

			for (var x = 0; x < width; x++)
			{
				var idx = rowOffset + x;
				var elevation = heightmap.GetElevation(x, y);
				var liquidDepth = hydrology.GetLiquidDepth(x, y);

				// --- 1. DOMAIN WARPING FOR LATITUDE ---
				// Sample low-frequency noise to bend the horizontal isotherm lines
				var warpOffset = NoiseMaker.GenerateSimpleNoise(x * 0.015f, y * 0.015f, warpSeed) * (height * 0.22f);
				var warpedY = Math.Clamp(y + warpOffset, 0, height);

				var latDist = Math.Abs(warpedY - halfHeight) / halfHeight;
				var latFactor = 1.0f - latDist;

				// Base regional temperature curve
				var baseLatTemp = MathF.Min(cfg.EquatorTemperature, cfg.PoleTemperature) +
				                  (latFactor * MathF.Abs(cfg.EquatorTemperature - cfg.PoleTemperature));

				// --- 2. TEMPERATURE EVALUATION ---
				// High mountains cool off via lapse rate (e.g., 6.5°C per 1,000m)
				var lapseCooling = Math.Max((short)0, elevation) / 1000.0f * cfg.LapseRatePer1000M;
				var temperature = baseLatTemp - lapseCooling + tempTurbulence[idx];

				// --- 3. PRECIPITATION & MARITIME BUFFERS ---
				// Coastlines and ocean air supply humidity buffers
				var isOcean = liquidDepth > 0 && elevation < worldConfig.Heightmap.SeaLevel;
				var riverBonus = hydrology.IsRiver(x, y) ? 0.25f : 0.0f;
				var marineBonus = isOcean ? 0.15f : 0.0f;

				var rainfall = Math.Clamp(rawRainfall[idx] + riverBonus + marineBonus, 0.0f, 1.0f);

				// --- 4. WHITTAKER BIOME CLASSIFICATION ---
				var biome = ClassifyBiome(
					elevation,
					liquidDepth,
					temperature,
					rainfall,
					cfg.MountainThreshold,
					hydrology.IsRiver(x, y));

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
