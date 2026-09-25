using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

public class TemperatureMapGenerator : INoisemapGenerator<TemperatureMap>
{
	private static readonly NoiseSettings TemperatureNoise = new()
	{
		Scale = 48f,
		Frequency = 0.008f,
		Octaves = 3,
		Persistence = 0.5f,
		Lacunarity = 2.0f,
		TargetMin = -1f,
		TargetMax = 1f
	};

	private TemperatureMapGenerator() { }

	public static TemperatureMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var width = generationContext.MapConfig.WidthTiles;
		var height = generationContext.MapConfig.HeightTiles;
		var seaLevel = generationContext.MapConfig.Terrain.SeaLevel;

		var equatorTemp = generationContext.MapConfig.Climate.EquatorTemperature;
		var polarTemp = generationContext.MapConfig.Climate.PoleTemperature;
		var hasNorthPole = generationContext.MapConfig.Climate.NorthPole;
		var hasSouthPole = generationContext.MapConfig.Climate.SouthPole;
		var tempChangePer1km = generationContext.MapConfig.Climate.LapseRatePer1000M;
		var oceanEquatorialTemp = equatorTemp - 2;
		var oceanPolarTemp = oceanEquatorialTemp - 32;
		// generic noise to help breakup large static bands
		var temperatureNoise = NoiseMaker.GenerateNoiseMap(
			TemperatureNoise with
			{
				Seed = generationContext.MapConfig.Seed,
				Frequency = generationContext.MapConfig.Climate.TemperatureNoiseFrequency
			}, width, height);

		var temperature = new TemperatureMap(width, height);

		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				var isOcean = generationContext.Hydrology.GetWaterBodyType(x, y) == WaterBodyType.Ocean;

				// Elevation temperature calculation (altitude cooling)
				var elevation = generationContext.Terrain.GetElevation(x, y);
				var elevationAboveSea = Math.Max(0f, elevation - seaLevel);
				var deltaTempAltitude = -(elevationAboveSea / 1000f) * tempChangePer1km;

				// Sample low-frequency noise to bend latitude lines
				var noise = temperatureNoise.GetNoise(x, y);
				var yDistortion = noise * (height * 0.06f); // 6% height wobble

				// Base thermal calculation
				var baseTemp = CalculateLatitudeTemp(y + yDistortion, isOcean);
				var intendedTemp = baseTemp + deltaTempAltitude;

				// Volcanic heat injection
				var volcanism = generationContext.Terrain.GetVolcanism(x, y);
				// Exponential volcanic floor & additive offset
				if (volcanism > 0.75f)
				{
					var intensity = (volcanism - 0.65f) / 0.35f; // 0.0 to 1.0
					var volcanicCurve = 0.5f * intensity * intensity * intensity;

					// Minimum thermal floor: an active volcanic crater won't sit below freezing (e.g. 10°C to 45°C)
					var thermalFloor = float.Lerp(5f, 50f, volcanicCurve);
					intendedTemp = Math.Max(intendedTemp, thermalFloor);

					// Also add a direct convective boost to warm the immediate surroundings
					intendedTemp += volcanicCurve * 8f;
				}

				// Slight micro variance (smooth ±1.5°C jitter)
				var finalTemp = intendedTemp + (noise * 1.5f);

				temperature.SetTemperature(x, y, finalTemp);
			}
		}


		return temperature;

		float CalculateLatitudeTemp(float yCoord, bool isOcean)
		{
			var halfHeight = height / 2f;
			// 0.0 at equator (y = halfHeight), 1.0 at top/bottom poles
			var distFromEquator = Math.Clamp(Math.Abs(halfHeight - yCoord) / halfHeight, 0f, 1f);

			var baseEquator = isOcean ? oceanEquatorialTemp : equatorTemp;
			var basePole = isOcean ? oceanPolarTemp : polarTemp;

			// Check hemisphere pole configuration
			var isNorthern = yCoord < halfHeight;
			if ((isNorthern && !hasNorthPole) || (!isNorthern && !hasSouthPole))
			{
				// If pole is disabled, maintain warm/temperate climate instead of freezing
				return baseEquator;
			}

			// Smooth cosine transition across latitudes (gives wider tropical bands)
			var t = (float)(1.0 - Math.Cos(distFromEquator * Math.PI * 0.5));
			return float.Lerp(baseEquator, basePole, t);
		}
	}
}
