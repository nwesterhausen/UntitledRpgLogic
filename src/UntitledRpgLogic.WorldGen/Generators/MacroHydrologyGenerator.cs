using UntitledRpgLogic.WorldGen.Models;
using Random = UntitledRpgLogic.Extensions.Common.Random;

namespace UntitledRpgLogic.WorldGen.Generators;

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

	public static TerrainHydrology Generate(
		TerrainHeightmap heightmap,
		Ulid waterMaterialId,
		uint seed,
		HydrologySettings? settings = null)
	{
		ArgumentNullException.ThrowIfNull(heightmap);

		var cfg = settings ?? new HydrologySettings();
		var width = heightmap.WidthTiles;
		var height = heightmap.HeightTiles;
		var hydrology = new TerrainHydrology(width, height);

		// 1. Fill ocean basins below SeaLevel
		FloodFillConnectedOceans(heightmap, hydrology, waterMaterialId, cfg.SeaLevel);

		// 2. Accumulate flow flux across the terrain
		var fluxMap = new int[width * height];
		var rng = new Random((int)seed);

		// Path buffer to store steps of a single drop descent
		var pathBuffer = new (int x, int y)[cfg.MaxDescentSteps];

		for (var i = 0; i < cfg.RaindropCycles; i++)
		{
			var startX = rng.NextInt(0, width);
			var startY = rng.NextInt(0, height);

			// Rivers start well above sea level in mountains or uplands
			if (heightmap.GetElevation(startX, startY) <= cfg.SeaLevel + 60)
			{
				continue;
			}

			var pathLength = TraceDescentPath(heightmap, hydrology, startX, startY, cfg, pathBuffer);

			// Increment flux along the entire path
			for (var p = 0; p < pathLength; p++)
			{
				var (px, py) = pathBuffer[p];
				fluxMap[(py * width) + px]++;
			}
		}

		// 3. Consolidate: Only promote paths that exceed the flux threshold to active rivers
		for (var y = 0; y < height; y++)
		{
			for (var x = 0; x < width; x++)
			{
				var idx = (y * width) + x;
				var flux = fluxMap[idx];
				var elev = heightmap.GetElevation(x, y);

				// Ignore tiles already submerged in oceans
				if (elev < cfg.SeaLevel)
				{
					continue;
				}

				if (flux >= cfg.RiverFluxThreshold)
				{
					// Scale depth proportionally to the accumulated water volume
					var depthScale = Math.Min(30, flux / cfg.RiverFluxThreshold);
					var depth = (ushort)Math.Min(ushort.MaxValue, cfg.BaseRiverDepth + (depthScale * 2));

					hydrology.SetLiquid(x, y, depth, waterMaterialId, true);

					// Erode bedrock slightly so rivers sit in natural trenches
					heightmap.SetElevation(x, y, (short)(elev - cfg.RiverBedErosionMeters));
				}
			}

			// 4. Fill terminal depression sinks into Lakes
			for (var y2 = 0; y2 < height; y2++)
			{
				for (var x = 0; x < width; x++)
				{
					var idx = (y2 * width) + x;
					var flux = fluxMap[idx];
					var elev = heightmap.GetElevation(x, y2);

					// A terminal sink is an active river tile surrounded by higher or equal terrain
					if (flux >= cfg.RiverFluxThreshold && !hydrology.IsRiver(x, y2) && elev >= cfg.SeaLevel)
					{
						continue;
					}

					if (hydrology.IsRiver(x, y2) && IsLocalDepression(heightmap, x, y2))
					{
						FloodFillLakeBasin(heightmap, hydrology, x, y2, waterMaterialId, cfg);
					}
				}
			}
		}

		return hydrology;
	}

	/// <summary>
	///     Floods all sub-sea level basins connected to the outer world perimeter.
	///     Isolated inland basins below SeaLevel are ignored and remain dry.
	/// </summary>
	private static void FloodFillConnectedOceans(
		TerrainHeightmap heightmap,
		TerrainHydrology hydrology,
		Ulid oceanWaterId,
		short seaLevel)
	{
		var width = heightmap.WidthTiles;
		var height = heightmap.HeightTiles;
		var visited = new bool[width * height];
		var queue = new Queue<(int x, int y)>();

		// Seed BFS with border tiles that sit below sea level
		for (var x = 0; x < width; x++)
		{
			EnqueIfSubmerged(x, 0);
			EnqueIfSubmerged(x, height - 1);
		}

		for (var y = 1; y < height - 1; y++)
		{
			EnqueIfSubmerged(0, y);
			EnqueIfSubmerged(width - 1, y);
		}

		void EnqueIfSubmerged(int bx, int by)
		{
			var idx = (by * width) + bx;
			if (heightmap.GetElevation(bx, by) < seaLevel && !visited[idx])
			{
				visited[idx] = true;
				queue.Enqueue((bx, by));
			}
		}

		// Expand ocean across contiguous sub-sea terrain
		while (queue.Count > 0)
		{
			var (cx, cy) = queue.Dequeue();
			var elev = heightmap.GetElevation(cx, cy);
			var depth = (ushort)Math.Min(ushort.MaxValue, seaLevel - elev);

			hydrology.SetLiquid(cx, cy, depth, oceanWaterId);

			foreach (var (dx, dy) in CardinalNeighbors)
			{
				var nx = cx + dx;
				var ny = cy + dy;

				if (nx < 0 || nx >= width || ny < 0 || ny >= height)
				{
					continue;
				}

				var nIdx = (ny * width) + nx;
				if (!visited[nIdx] && heightmap.GetElevation(nx, ny) < seaLevel)
				{
					visited[nIdx] = true;
					queue.Enqueue((nx, ny));
				}
			}
		}
	}

	private static int TraceDescentPath(TerrainHeightmap heightmap,
		TerrainHydrology hydrology,
		int startX,
		int startY,
		HydrologySettings cfg,
		(int x, int y)[] pathBuffer)
	{
		var cx = startX;
		var cy = startY;
		var steps = 0;

		while (steps < cfg.MaxDescentSteps)
		{
			pathBuffer[steps++] = (cx, cy);

			var curElev = heightmap.GetElevation(cx, cy);

			// Terminate if we reached an active ocean basin
			if (curElev < cfg.SeaLevel && hydrology.GetLiquidDepth(cx, cy) > 0)
			{
				break;
			}

			// Find lowest neighbor (steepest descent)
			var lowestX = cx;
			var lowestY = cy;
			var lowestElev = curElev;

			foreach (var (dx, dy) in AllNeighbors)
			{
				var nx = cx + dx;
				var ny = cy + dy;

				if (nx < 0 || nx >= heightmap.WidthTiles || ny < 0 || ny >= heightmap.HeightTiles)
				{
					continue;
				}

				var nElev = heightmap.GetElevation(nx, ny);
				if (nElev < lowestElev)
				{
					lowestElev = nElev;
					lowestX = nx;
					lowestY = ny;
				}
			}

			// Local minimum reached (inland lake pool or valley floor)
			if (lowestX == cx && lowestY == cy)
			{
				break;
			}

			cx = lowestX;
			cy = lowestY;
		}

		return steps;
	}

	private static bool IsLocalDepression(TerrainHeightmap heightmap, int cx, int cy)
	{
		var curElev = heightmap.GetElevation(cx, cy);

		foreach (var (dx, dy) in AllNeighbors)
		{
			var nx = cx + dx;
			var ny = cy + dy;

			if (nx < 0 || nx >= heightmap.WidthTiles || ny < 0 || ny >= heightmap.HeightTiles)
			{
				continue;
			}

			// If any neighbor is lower, water continues descending and has not pooled
			if (heightmap.GetElevation(nx, ny) < curElev)
			{
				return false;
			}
		}

		return true;
	}

	private static void FloodFillLakeBasin(
		TerrainHeightmap heightmap,
		TerrainHydrology hydrology,
		int sinkX,
		int sinkY,
		Ulid waterMaterialId,
		HydrologySettings cfg)
	{
		var width = heightmap.WidthTiles;
		var height = heightmap.HeightTiles;
		var baseElev = heightmap.GetElevation(sinkX, sinkY);

		// Fill up to a small spillway depth (e.g., 20-30 meters above the sink floor)
		var targetLakeLevel = baseElev + 25;
		var visited = new HashSet<int>();
		var queue = new Queue<(int x, int y)>();

		queue.Enqueue((sinkX, sinkY));
		visited.Add((sinkY * width) + sinkX);

		var lakeTileCount = 0;
		const int maxLakeTiles = 120; // Caps lake expansion to prevent flooding entire continents

		while (queue.Count > 0 && lakeTileCount < maxLakeTiles)
		{
			var (cx, cy) = queue.Dequeue();
			var elev = heightmap.GetElevation(cx, cy);

			if (elev <= targetLakeLevel)
			{
				var depth = (ushort)Math.Clamp(targetLakeLevel - elev + cfg.BaseRiverDepth, 1, ushort.MaxValue);

				// Lakes are marked with isRiverChannel: false
				hydrology.SetLiquid(cx, cy, depth, waterMaterialId);
				lakeTileCount++;

				foreach (var (dx, dy) in AllNeighbors)
				{
					var nx = cx + dx;
					var ny = cy + dy;

					if (nx < 0 || nx >= width || ny < 0 || ny >= height)
					{
						continue;
					}

					var nIdx = (ny * width) + nx;
					if (!visited.Contains(nIdx) && heightmap.GetElevation(nx, ny) <= targetLakeLevel)
					{
						visited.Add(nIdx);
						queue.Enqueue((nx, ny));
					}
				}
			}
		}
	}
}
