using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

public class LeylineMapGenerator : INoisemapGenerator<LeylineMap>
{
	private static readonly NoiseSettings LeylineNoise = new()
	{
		Scale = 60f,
		Octaves = 6,
		Lacunarity = 2,
		Frequency = 1.2f,
		Persistence = 0.4f
	};

	private LeylineMapGenerator() { }

	public static LeylineMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var leylines = new LeylineMap(generationContext.MapConfig.WidthTiles, generationContext.MapConfig.HeightTiles);
		var noiseMap = NoiseMaker.GenerateNoiseMap(
			LeylineNoise with { Seed = generationContext.MapConfig.Seed },
			generationContext.MapConfig.WidthTiles,
			generationContext.MapConfig.HeightTiles);

		for (var x = 0; x < leylines.WidthTiles; x++)
		{
			for (var y = 0; y < leylines.HeightTiles; y++)
			{
				var smoothNoise = (float)SmoothFn(noiseMap.GetNoise(x, y));
				leylines.SetLeylineEnergy(x, y, smoothNoise);
			}
		}

		return leylines;
	}

	private static double SmoothFn(float x) => (5 * Math.Pow(x, 4)) - (4 * Math.Pow(x, 5));
}
