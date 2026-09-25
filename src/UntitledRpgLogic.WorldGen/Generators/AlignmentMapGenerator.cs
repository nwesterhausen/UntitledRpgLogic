using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

public class AlignmentMapGenerator : INoisemapGenerator<AlignmentMap>
{
	private static readonly NoiseSettings AlignmentNoise = new()
	{
		Scale = 90f,
		Octaves = 6,
		Lacunarity = 2,
		Frequency = 1f,
		Persistence = 0.4f
	};

	private static readonly float FirstSep = 0.2f;
	private static readonly float SecondSep = 0.8f;
	private AlignmentMapGenerator() { }

	public static AlignmentMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var alignment =
			new AlignmentMap(generationContext.MapConfig.WidthTiles, generationContext.MapConfig.HeightTiles);
		var noiseMap = NoiseMaker.GenerateNoiseMap(
			AlignmentNoise with { Seed = generationContext.MapConfig.Seed },
			generationContext.MapConfig.WidthTiles,
			generationContext.MapConfig.HeightTiles);

		for (var x = 0; x < alignment.WidthTiles; x++)
		{
			for (var y = 0; y < alignment.HeightTiles; y++)
			{
				var smoothNoise = 0f;
				if (noiseMap.GetNoise(x, y) < FirstSep)
				{
					smoothNoise = FirstSmoothFn(x);
				}
				else if (noiseMap.GetNoise(x, y) > SecondSep)
				{
					smoothNoise = SecondSmoothFn(x);
				}

				alignment.SetAlignment(x, y, smoothNoise);
			}
		}

		return alignment;
	}

	private static float FirstSmoothFn(float x) => (75 * x * x) - (250 * x) - 1;
	private static float SecondSmoothFn(float x) => (-250 * x * x * x) + (675 * x * x) - (600 * x) + 176;
}
