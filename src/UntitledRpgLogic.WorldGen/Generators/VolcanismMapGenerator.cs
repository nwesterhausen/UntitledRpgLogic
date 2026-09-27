using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

public class VolcanismMapGenerator : INoisemapGenerator<VolcanismMap>
{
	private static readonly NoiseSettings VolcanismNoise = new()
	{
		Scale = 60f,
		Octaves = 6,
		Lacunarity = 2,
		Frequency = 1.2f,
		Persistence = 0.4f
	};

	private static readonly uint VolcanismSeedMod = 0x456abc;

	private VolcanismMapGenerator() { }

	public static VolcanismMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var volcanisms =
			new VolcanismMap(generationContext.MapConfig.WidthTiles, generationContext.MapConfig.HeightTiles);
		var noiseMap = NoiseMaker.GenerateNormalNoiseMap(
			VolcanismNoise with { Seed = generationContext.MapConfig.Seed ^ VolcanismSeedMod },
			generationContext.MapConfig.WidthTiles,
			generationContext.MapConfig.HeightTiles);

		for (var x = 0; x < volcanisms.WidthTiles; x++)
		{
			for (var y = 0; y < volcanisms.HeightTiles; y++)
			{
				var smoothNoise = (float)SmoothFn(noiseMap.GetNoise(x, y));
				// Squash the activity to create rare, isolated peaks (high exponent > 1) or multiply activity (exponent < 1)
				var dampenedVolcanism = MathF.Pow(smoothNoise, generationContext.MapConfig.Terrain.VolcanismExponent);

				volcanisms.SetVolcanism(x, y, dampenedVolcanism);
			}
		}

		return volcanisms;
	}

	private static double SmoothFn(float x) => (5 * Math.Pow(x, 4)) - (4 * Math.Pow(x, 5));
}
