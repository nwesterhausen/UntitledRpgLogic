using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.GridMaps;

namespace UntitledRpgLogic.WorldGen.Generators;

public class BasinMapGenerator : INoisemapGenerator<BasinMap>
{
	private BasinMapGenerator() { }

	public static BasinMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var width = generationContext.MapConfig.WidthTiles;
		var height = generationContext.MapConfig.HeightTiles;
		var seaLevel = generationContext.MapConfig.Terrain.SeaLevel;

		var basin = new BasinMap(width, height);
		var visited = new BooleanMap(width, height); // track which tiles were visited to avoid infinite loops
		var bfsQueue = new Queue<(int X, int Y)>(); // Queue for breadth-first search

		// Seed the perimeter with ocean
		for (var x = 0; x < width; x++)
		{
			// With one loop, we can check both the top and bottom line at once
			TryEnqueue(x, 0);
			TryEnqueue(x, height - 1);
		}

		for (var y = 0; y < height; y++)
		{
			// Uses one loop to check first and last vertical bar
			TryEnqueue(0, y);
			TryEnqueue(width - 1, y);
		}

		// Breadth-First Search (Flood-fill) for filling ocean tiles
		while (bfsQueue.Count > 0)
		{
			var (checkX, checkY) = bfsQueue.Dequeue();
			// If it's in the queue, it's an ocean.
			basin.SetWaterBody(checkX, checkY, WaterBodyType.Ocean);

			// For each cardinal direction, check each neighbor and add to queue if needed.
			foreach (var (directionX, directionY) in NeighborHelper.CardinalNeighbors)
			{
				var nextX = checkX + directionX;
				var nextY = checkY + directionY;

				TryEnqueue(nextX, nextY);
			}
		}

		return basin;

		// Helper for checking points if they have elevation below sea level, and if so, adding the point to the queue
		void TryEnqueue(int x, int y)
		{
			if (basin.IsInBounds(x, y) && // If (x,y) is a legal position
			    !visited.IsTrue(x, y) && // If we haven't already visited (x,y)
			    generationContext.Terrain.GetElevation(x, y) < seaLevel) // If elevation at (x,y) is above sea level
			{
				visited.SetTrue(x, y);
				bfsQueue.Enqueue((x, y));
			}
		}
	}
}
