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
		var chanceMap = NoiseMaker.GenerateNoiseMap(
			AlignmentNoise with { Seed = generationContext.MapConfig.Seed ^ 0x200abc },
			generationContext.MapConfig.WidthTiles,
			generationContext.MapConfig.HeightTiles);

		var chanceGood = generationContext.MapConfig.Arcana.AlignmentInfluence == 0f
			? 0f
			: generationContext.MapConfig.Arcana.GoodAlignmentWeight /
			  (float)Math.Max(1,
				  generationContext.MapConfig.Arcana.GoodAlignmentWeight +
				  generationContext.MapConfig.Arcana.EvilAlignmentWeight) *
			  generationContext.MapConfig.Arcana.AlignmentInfluence;
		var chanceEvil = generationContext.MapConfig.Arcana.AlignmentInfluence == 0f
			? 0f
			: generationContext.MapConfig.Arcana.EvilAlignmentWeight /
			  (float)Math.Max(1,
				  generationContext.MapConfig.Arcana.GoodAlignmentWeight +
				  generationContext.MapConfig.Arcana.EvilAlignmentWeight) *
			  generationContext.MapConfig.Arcana.AlignmentInfluence;

		for (var x = 0; x < alignment.WidthTiles; x++)
		{
			for (var y = 0; y < alignment.HeightTiles; y++)
			{
				var smoothNoise = 0f;
				var noise = noiseMap.GetNoise(x, y);
				var chance = chanceMap.GetNoise(x, y);

				if (chance < chanceEvil && noise < FirstSep)
				{
					// If we were within the first segment of the split function
					smoothNoise = FirstSmoothFn(x);
				}
				else if (chance < chanceGood && noise > SecondSep)
				{
					// If we were within the last segment of the split function
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
