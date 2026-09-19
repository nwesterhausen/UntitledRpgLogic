using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.WorldGen.Noise;
using Random = UntitledRpgLogic.Extensions.Common.Random;

namespace UntitledRpgLogic.WorldGen.OldGenerators;

/// <summary>
///     Simulates oceanic flooding, precipitation runoff, and major river vein consolidation.
/// </summary>
public static class MacroHydrologyGenerator
{
	private static readonly (int dx, int dy)[] CardinalNeighbors =
	[
		(0, -1), // North
		(1, 0), // East
		(0, 1), // South
		(-1, 0) // West
	];

	private static readonly (int dx, int dy)[] AllNeighbors =
	[
		(0, -1), // North
		(1, 0), // East
		(0, 1), // South
		(-1, 0), // West
		(-1, -1), // North-West
		(1, -1), // North-East
		(1, 1), // South-East
		(-1, 1) // South-West
	];

	/// <summary>
	/// </summary>
	/// <param name="context"></param>
	/// <param name="seed"></param>
	/// <param name="waterMaterialId"></param>
	/// <param name="worldConfig"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static HydrologyMaps Generate(
		WorldGenContext context,
		long seed,
		Ulid waterMaterialId,
		WorldMapConfiguration worldConfig)
	{
		ArgumentNullException.ThrowIfNull(context);
		ArgumentNullException.ThrowIfNull(worldConfig);

		var cfg = worldConfig.Hydrology;
		var width = context.MapConfig.WidthTiles;
		var height = context.MapConfig.HeightTiles;
		var hydrology = new HydrologyMaps(width, height);

		// 1. Flood-fill connected perimeter oceans using SeaLevel
		FloodFillConnectedOceans(context, hydrology, waterMaterialId, worldConfig.Terrain.SeaLevel);

		// 2. Pre-generate continuous moisture map to guide river origins
		var moistureMap = NoiseMaker.GenerateNoiseArray(
			new NoiseSettings
			{
				Seed = seed ^ 0x31415926u,
				Scale = 1.0f / 0.01f,
				Octaves = 3,
				Persistence = 0.5f,
				Lacunarity = 2.0f,
				TargetMin = 0.0f,
				TargetMax = 1.0f
			}, width, height);

		// 3. Accumulate flow flux across the terrain using RaindropCycles and MaxDescentSteps
		var fluxMap = new int[width * height];
		var rng = new Random((int)seed);
		var pathBuffer = new (int X, int Y)[cfg.MaxDescentSteps];

		for (var i = 0; i < cfg.RaindropCycles; i++)
		{
			var rx = rng.NextInt(1, width - 1);
			var ry = rng.NextInt(1, height - 1);

			// Rivers start primarily in moist uplands above sea level
			if (context.Terrain.GetElevation(rx, ry) <= worldConfig.Terrain.SeaLevel + 40 ||
			    moistureMap[(ry * width) + rx] < 0.55f)
			{
				continue;
			}

			var pathLength = TraceDescentPath(context, hydrology, rx, ry, worldConfig, pathBuffer);

			// Record flux along the path traced
			for (var p = 0; p < pathLength; p++)
			{
				var (px, py) = pathBuffer[p];
				fluxMap[(py * width) + px]++;
			}
		}

		// 4. Filter by RiverFluxThreshold, size via BaseRiverDepth, and carve using RiverBedErosionMeters
		for (var y = 0; y < height; y++)
		{
			var rowOffset = y * width;
			for (var x = 0; x < width; x++)
			{
				var idx = rowOffset + x;
				var flux = fluxMap[idx];
				var elev = context.Terrain.GetElevation(x, y);

				// Skip tiles already submerged in oceans
				if (hydrology.GetLiquidDepth(x, y) > 0 && elev < worldConfig.Terrain.SeaLevel)
				{
					continue;
				}

				if (flux >= cfg.RiverFluxThreshold)
				{
					// Scale water depth proportional to flux volume above the threshold
					var depthScale = Math.Min(30, flux / cfg.RiverFluxThreshold);
					var depth = (ushort)Math.Min(ushort.MaxValue, cfg.BaseRiverDepth + (depthScale * 2));

					hydrology.SetLiquid(x, y, depth, waterMaterialId, WaterBodyType.River);

					// Erode bedrock so rivers sit inside natural trenches
					if (cfg.RiverBedErosionMeters > 0)
					{
						var erodedElevation = (short)(elev - cfg.RiverBedErosionMeters);
						context.Terrain.SetElevation(x, y, erodedElevation);
					}
				}
			}
		}

		if (cfg.FormInlandLakes)
		{
			FloodInlandTerminalLakes(context, hydrology, waterMaterialId, worldConfig);
		}

		return hydrology;
	}

	private static void FloodFillConnectedOceans(
		WorldGenContext context,
		HydrologyMaps hydrology,
		Ulid oceanWaterId,
		short seaLevel)
	{
		var width = context.MapConfig.WidthTiles;
		var height = context.MapConfig.HeightTiles;
		var visited = new bool[width * height];
		var queue = new Queue<(int X, int Y)>();

		void EnqueueIfOcean(int x, int y)
		{
			var idx = (y * width) + x;
			if (!visited[idx] && context.Terrain.GetElevation(x, y) < seaLevel)
			{
				visited[idx] = true;
				queue.Enqueue((x, y));
			}
		}

		for (var x = 0; x < width; x++)
		{
			EnqueueIfOcean(x, 0);
			EnqueueIfOcean(x, height - 1);
		}

		for (var y = 1; y < height - 1; y++)
		{
			EnqueueIfOcean(0, y);
			EnqueueIfOcean(width - 1, y);
		}

		while (queue.Count > 0)
		{
			var (cx, cy) = queue.Dequeue();
			var elev = context.Terrain.GetElevation(cx, cy);
			var depth = (ushort)Math.Min(ushort.MaxValue, seaLevel - elev);

			hydrology.SetLiquid(cx, cy, depth, oceanWaterId, WaterBodyType.Ocean);

			foreach (var (dx, dy) in CardinalNeighbors)
			{
				var nx = cx + dx;
				var ny = cy + dy;

				if (!hydrology.IsInBounds(nx, ny))
				{
					continue;
				}

				var nIdx = (ny * width) + nx;
				if (!visited[nIdx] && context.Terrain.GetElevation(nx, ny) < seaLevel)
				{
					visited[nIdx] = true;
					queue.Enqueue((nx, ny));
				}
			}
		}
	}

	private static int TraceDescentPath(
		WorldGenContext context,
		HydrologyMaps hydrology,
		int startX,
		int startY,
		WorldMapConfiguration worldConfig,
		(int X, int Y)[] pathBuffer)
	{
		var cx = startX;
		var cy = startY;
		var steps = 0;
		var cfg = worldConfig.Hydrology;

		while (steps < cfg.MaxDescentSteps)
		{
			pathBuffer[steps++] = (cx, cy);

			var curElev = context.Terrain.GetElevation(cx, cy);

			// Terminate once an ocean basin is reached
			if (curElev < worldConfig.Terrain.SeaLevel && hydrology.GetLiquidDepth(cx, cy) > 0)
			{
				break;
			}

			var lowestX = cx;
			var lowestY = cy;
			var lowestElev = curElev;

			foreach (var (dx, dy) in AllNeighbors)
			{
				var nx = cx + dx;
				var ny = cy + dy;

				if (!hydrology.IsInBounds(nx, ny))
				{
					continue;
				}

				var nElev = context.Terrain.GetElevation(nx, ny);
				if (nElev < lowestElev)
				{
					lowestElev = nElev;
					lowestX = nx;
					lowestY = ny;
				}
			}

			// Terminate if caught in an inland depression basin
			if (lowestX == cx && lowestY == cy)
			{
				break;
			}

			cx = lowestX;
			cy = lowestY;
		}

		return steps;
	}

	private static void FloodInlandTerminalLakes(
		WorldGenContext context,
		HydrologyMaps hydrology,
		Ulid waterMaterialId,
		WorldMapConfiguration worldConfig)
	{
		var width = context.MapConfig.WidthTiles;
		var height = context.MapConfig.HeightTiles;
		var cfg = worldConfig.Hydrology;

		for (var y = 0; y < height; y++)
		{
			for (var x = 0; x < width; x++)
			{
				// Only examine active river tiles sitting at or above sea level
				if (!hydrology.IsRiver(x, y) || context.Terrain.GetElevation(x, y) < worldConfig.Terrain.SeaLevel)
				{
					continue;
				}

				// A terminal sink has no strictly lower neighbor to flow to
				if (IsDepressionSink(context, hydrology, x, y))
				{
					FloodFillLake(context, hydrology, x, y, waterMaterialId, cfg);
				}
			}
		}
	}

	private static bool IsDepressionSink(WorldGenContext context, HydrologyMaps hydrology, int cx, int cy)
	{
		var curElev = context.Terrain.GetElevation(cx, cy);

		foreach (var (dx, dy) in AllNeighbors)
		{
			var nx = cx + dx;
			var ny = cy + dy;

			if (!hydrology.IsInBounds(nx, ny))
			{
				continue;
			}

			// If any neighbor is lower, water flows away and is not trapped
			if (context.Terrain.GetElevation(nx, ny) < curElev)
			{
				return false;
			}
		}

		return true;
	}

	private static void FloodFillLake(
		WorldGenContext context,
		HydrologyMaps hydrology,
		int sinkX,
		int sinkY,
		Ulid waterMaterialId,
		HydrologyConfiguration cfg)
	{
		var width = context.MapConfig.WidthTiles;
		var sinkElevation = context.Terrain.GetElevation(sinkX, sinkY);

		// Lake water level fills up to a capped spillway height above the basin bottom
		var lakeWaterLevel = (short)(sinkElevation + cfg.MaxLakeDepthMeters);

		var queue = new Queue<(int X, int Y)>();
		var visited = new HashSet<int>();

		queue.Enqueue((sinkX, sinkY));
		visited.Add((sinkY * width) + sinkX);

		var tilesFilled = 0;

		while (queue.Count > 0 && tilesFilled < cfg.MaxLakeTiles)
		{
			var (cx, cy) = queue.Dequeue();
			var elev = context.Terrain.GetElevation(cx, cy);

			if (elev <= lakeWaterLevel)
			{
				// Compute fluid depth relative to the lake's horizontal surface datum
				var depth = (ushort)Math.Clamp(lakeWaterLevel - elev + cfg.BaseRiverDepth, 1, ushort.MaxValue);

				// Setting isRiverChannel: false causes MacroClimateGenerator to classify this as BiomeType.Lake
				hydrology.SetLiquid(cx, cy, depth, waterMaterialId, WaterBodyType.Lake);
				tilesFilled++;

				foreach (var (dx, dy) in AllNeighbors)
				{
					var nx = cx + dx;
					var ny = cy + dy;

					if (!hydrology.IsInBounds(nx, ny))
					{
						continue;
					}

					var nIdx = (ny * width) + nx;
					if (!visited.Contains(nIdx) && context.Terrain.GetElevation(nx, ny) <= lakeWaterLevel)
					{
						visited.Add(nIdx);
						queue.Enqueue((nx, ny));
					}
				}
			}
		}
	}
}
