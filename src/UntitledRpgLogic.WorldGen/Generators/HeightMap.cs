using UntitledRpgLogic.WorldGen.Models;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

/// <summary>
///     Configurable parameters for generating large-scale terrain elevation grids.
/// </summary>
public record HeightmapSettings
{
    public float Frequency { get; init; } = 0.005f;
    public int Octaves { get; init; } = 6;
    public float Persistence { get; init; } = 0.5f;
    public float Lacunarity { get; init; } = 2.0f;
    public short MinElevation { get; init; } = -8000;
    public short MaxElevation { get; init; } = 8000;
    public short SeaLevel { get; init; }
}

/// <summary>
///     Generates continuous bedrock elevation heightmaps using multi-octave gradient noise.
/// </summary>
public static class MacroHeightmapGenerator
{
    /// <summary>
    ///     Populates a <see cref="TerrainHeightmap"/> with procedural bedrock elevations.
    /// </summary>
    public static TerrainHeightmap Generate(
        int widthTiles,
        int heightTiles,
        uint seed,
        HeightmapSettings? settings = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(widthTiles);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heightTiles);

        var cfg = settings ?? new HeightmapSettings();
        var heightmap = new TerrainHeightmap(widthTiles, heightTiles);
        var elevationSpan = cfg.MaxElevation - cfg.MinElevation;

        for (var y = 0; y < heightTiles; y++)
        {
            for (var x = 0; x < widthTiles; x++)
            {
                // Sample normalized fBm noise in range [-1.0, 1.0]
                var noise = SimplexNoise.SampleFbm(
                    x * cfg.Frequency,
                    y * cfg.Frequency,
                    seed,
                    cfg.Octaves,
                    cfg.Persistence,
                    cfg.Lacunarity);

                // Map [-1.0, 1.0] -> [0.0, 1.0]
                var normalized = Math.Clamp((noise + 1.0f) * 0.5f, 0.0f, 1.0f);

                // Shape terrain: raise power for sharper peaks and broader valleys
                var shaped = MathF.Pow(normalized, 1.25f);
                var elevation = (short)MathF.Round(cfg.MinElevation + (shaped * elevationSpan));

                heightmap.SetElevation(x, y, elevation);
            }
        }

        return heightmap;
    }
}
