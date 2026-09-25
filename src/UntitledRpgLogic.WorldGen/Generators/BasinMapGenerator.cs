using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.GridMaps;

namespace UntitledRpgLogic.WorldGen.Generators;

public class BasinMapGenerator : INoisemapGenerator<BasinMap>
{
	private BasinMapGenerator() { }

	public static BasinMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var basin = new BasinMap(generationContext.MapConfig.WidthTiles, generationContext.MapConfig.HeightTiles);

		for (var x = 0; x < basin.WidthTiles; x++)
		{
			for (var y = 0; y < basin.HeightTiles; y++)
			{
				// Tier 1.5, we can only determine Ocean / Not-ocean
				if (generationContext.Terrain.GetElevation(x, y) < generationContext.MapConfig.Terrain.SeaLevel)
				{
					basin.SetWaterBody(x, y, WaterBodyType.Ocean);
				}
			}
		}

		return basin;
	}
}
