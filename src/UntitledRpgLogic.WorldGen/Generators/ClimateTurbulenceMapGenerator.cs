using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.WorldGen.Generators;

public class ClimateTurbulenceMapGenerator : INoisemapGenerator<ClimateTurbulenceMap>
{
	private static readonly float EnergyThreshold = 0.9f;
	private static readonly float VolcanismWeight = 0.4f;
	private static readonly float LeylineWeight = 1f - VolcanismWeight;
	private static readonly float ResonanceMultiplier = 1.25f;
	private ClimateTurbulenceMapGenerator() { }

	public static ClimateTurbulenceMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var turbulence = new ClimateTurbulenceMap(generationContext.MapConfig.WidthTiles,
			generationContext.MapConfig.HeightTiles);


		for (var x = 0; x < turbulence.WidthTiles; x++)
		{
			for (var y = 0; y < turbulence.HeightTiles; y++)
			{
				var volcanism = generationContext.Terrain.GetVolcanism(x, y);
				var leyline = generationContext.Arcana.GetLeylineEnergy(x, y);

				if (volcanism < EnergyThreshold || leyline < EnergyThreshold)
				{
					continue;
				}

				var energyValue = (volcanism * VolcanismWeight) + (leyline * LeylineWeight);
				var resonancevalue = volcanism * leyline * ResonanceMultiplier;

				var turbulenceValue = Math.Clamp(
					energyValue + resonancevalue,
					0f, 1f);
				turbulence.SetTurbulence(x, y, turbulenceValue);
			}
		}

		return turbulence;
	}
}
