using UntitledRpgLogic.WorldGen.Models;

namespace UntitledRpgLogic.WorldGen.Generators;

/// <summary>
///     Configurable parameters for water accumulation, river routing, and sea flooding.
/// </summary>
public record HydrologySettings
{
    public short SeaLevel { get; init; }
    public int RaindropCycles { get; init; } = 2500;
    public ushort BaseRiverDepth { get; init; } = 4;
    public int MaxDescentSteps { get; init; } = 600;
}

/// <summary>
///     Simulates oceanic flooding, precipitation runoff, and river path carving across terrain.
/// </summary>
public static class MacroHydrologyGenerator
{
    private static readonly (int dx, int dy)[] Neighbors =
    [
        (0, -1),  // North
        (1, 0),   // East
        (0, 1),   // South
        (-1, 0),  // West
        (-1, -1), // North-West
        (1, -1),  // North-East
        (1, 1),   // South-East
        (-1, 1)   // South-West
    ];

    /// <summary>
    ///     Generates ocean basins, lake depressions, and river paths over an existing heightmap.
    /// </summary>
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
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var elev = heightmap.GetElevation(x, y);
                if (elev < cfg.SeaLevel)
                {
                    var depth = (ushort)Math.Min((int)ushort.MaxValue, cfg.SeaLevel - elev);
                    hydrology.SetLiquid(x, y, depth, waterMaterialId, isRiverChannel: false);
                }
            }
        }

        // 2. Simulate surface river runoffs via particle descent
        var rng = new Random((int)seed);
        for (var i = 0; i < cfg.RaindropCycles; i++)
        {
            var curX = rng.Next(0, width);
            var curY = rng.Next(0, height);

            // Rivers start above sea level in mountains or hills
            if (heightmap.GetElevation(curX, curY) <= cfg.SeaLevel + 50)
            {
                continue;
            }

            SimulateFlow(heightmap, hydrology, curX, curY, waterMaterialId, cfg);
        }

        return hydrology;
    }

    private static void SimulateFlow(
        TerrainHeightmap heightmap,
        TerrainHydrology hydrology,
        int startX,
        int startY,
        Ulid waterMaterialId,
        HydrologySettings cfg)
    {
        var cx = startX;
        var cy = startY;

        for (var step = 0; step < cfg.MaxDescentSteps; step++)
        {
            var curElev = heightmap.GetElevation(cx, cy);

            // Reached an existing water body / ocean
            if (curElev <= cfg.SeaLevel || hydrology.GetLiquidDepth(cx, cy) > cfg.BaseRiverDepth)
            {
                break;
            }

            // Mark cell as an active river tile
            var existingDepth = hydrology.GetLiquidDepth(cx, cy);
            var newDepth = (ushort)Math.Min((int)ushort.MaxValue, existingDepth + cfg.BaseRiverDepth);
            hydrology.SetLiquid(cx, cy, newDepth, waterMaterialId, isRiverChannel: true);

            // Find lowest neighbor (steepest descent)
            var lowestX = cx;
            var lowestY = cy;
            var lowestElev = curElev;

            foreach (var (dx, dy) in Neighbors)
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

            // Caught in a local minimum depression (lake pool)
            if (lowestX == cx && lowestY == cy)
            {
                break;
            }

            cx = lowestX;
            cy = lowestY;
        }
    }
}
