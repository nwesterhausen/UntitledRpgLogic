using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

public class SavageryMapGenerator : INoisemapGenerator<SavageryMap>
{
	private static readonly NoiseSettings SavageryNoise = new()
	{
		Scale = 90f,
		Octaves = 6,
		Lacunarity = 2,
		Frequency = 1f,
		Persistence = 0.4f
	};

	private static readonly float FirstSep = 0.2f;
	private static readonly float SecondSep = 0.8f;
	private SavageryMapGenerator() { }

	public static SavageryMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var savagery = new SavageryMap(generationContext.MapConfig.WidthTiles, generationContext.MapConfig.HeightTiles);
		var noiseMap = NoiseMaker.GenerateNoiseMap(
			SavageryNoise with { Seed = generationContext.MapConfig.Seed },
			generationContext.MapConfig.WidthTiles,
			generationContext.MapConfig.HeightTiles);
		var chanceMap = NoiseMaker.GenerateNoiseMap(
			SavageryNoise with { Seed = generationContext.MapConfig.Seed ^ 0x100abc },
			generationContext.MapConfig.WidthTiles,
			generationContext.MapConfig.HeightTiles);

		var chanceBenign = generationContext.MapConfig.Arcana.SavageryInfluence == 0f
			? 0f
			: generationContext.MapConfig.Arcana.BenignSavageryWeight /
			  (float)Math.Max(1,
				  generationContext.MapConfig.Arcana.BenignSavageryWeight +
				  generationContext.MapConfig.Arcana.ViciousSavageryWeight) *
			  generationContext.MapConfig.Arcana.SavageryInfluence;
		var chanceVicious = generationContext.MapConfig.Arcana.SavageryInfluence == 0f
			? 0f
			: generationContext.MapConfig.Arcana.ViciousSavageryWeight /
			  (float)Math.Max(1,
				  generationContext.MapConfig.Arcana.BenignSavageryWeight +
				  generationContext.MapConfig.Arcana.ViciousSavageryWeight) *
			  generationContext.MapConfig.Arcana.SavageryInfluence;

		for (var x = 0; x < savagery.WidthTiles; x++)
		{
			for (var y = 0; y < savagery.HeightTiles; y++)
			{
				var smoothNoise = 0f;
				var noise = noiseMap.GetNoise(x, y);
				var chance = chanceMap.GetNoise(x, y);

				if (chance < chanceBenign && noise < FirstSep)
				{
					// If we were within the first segment of the split function
					smoothNoise = FirstSmoothFn(x);
				}
				else if (chance < chanceVicious && noise > SecondSep)
				{
					// If we were within the last segment of the split function
					smoothNoise = SecondSmoothFn(x);
				}

				savagery.SetSavagery(x, y, smoothNoise);
			}
		}

		return savagery;
	}

	private static float FirstSmoothFn(float x) => (75 * x * x) - (250 * x) - 1;
	private static float SecondSmoothFn(float x) => (-250 * x * x * x) + (675 * x * x) - (600 * x) + 176;
}
