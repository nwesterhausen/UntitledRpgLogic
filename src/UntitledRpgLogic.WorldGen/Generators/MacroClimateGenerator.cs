using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.WorldGen.Models;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

/// <summary>
///     Configurable parameters for temperature gradients, environmental lapse rates, and moisture distribution.
/// </summary>
public record ClimateSettings
{
    public float EquatorTemperature { get; init; } = 32.0f; // °C at equator (middle latitude)
    public float PoleTemperature { get; init; } = -15.0f;    // °C at poles (top/bottom latitude)
    public float LapseRatePer1000M { get; init; } = 6.5f;   // Environmental cooling rate per 1,000 units elevation
    public float TemperatureNoiseFrequency { get; init; } = 0.008f;
    public float RainfallNoiseFrequency { get; init; } = 0.006f;
    public short MountainThreshold { get; init; } = 1800;
}

/// <summary>
///     Generates temperature gradients, precipitation simulations, and Whittaker-style biome classifications.
/// </summary>
public static class MacroClimateGenerator
{
    /// <summary>
    ///     Generates temperature, rainfall, and derived biomes across the map based on heightmap and hydrology inputs.
    /// </summary>
    public static TerrainClimate Generate(
        TerrainHeightmap heightmap,
        TerrainHydrology hydrology,
        uint seed,
        ClimateSettings? settings = null)
    {
        ArgumentNullException.ThrowIfNull(heightmap);
        ArgumentNullException.ThrowIfNull(hydrology);

        var cfg = settings ?? new ClimateSettings();
        var width = heightmap.WidthTiles;
        var height = heightmap.HeightTiles;
        var climate = new TerrainClimate(width, height);

        var tempSeed = seed ^ 0x9e3779b9u;
        var rainSeed = seed ^ 0x85ebca6bu;
        var halfHeight = height / 2.0f;

        for (var y = 0; y < height; y++)
        {
            // Latitudinal factor: 1.0 at equator (center), 0.0 at poles (top/bottom edges)
            var latDist = Math.Abs(y - halfHeight) / halfHeight;
            var latFactor = 1.0f - latDist;
            var baseLatTemp = MathF.Min(cfg.EquatorTemperature, cfg.PoleTemperature) +
                              (latFactor * MathF.Abs(cfg.EquatorTemperature - cfg.PoleTemperature));

            for (var x = 0; x < width; x++)
            {
                var elevation = heightmap.GetElevation(x, y);
                var liquidDepth = hydrology.GetLiquidDepth(x, y);

                // 1. Calculate temperature (latitude baseline + lapse rate cooling + simplex noise turbulence)
                var lapseCooling = (Math.Max((short)0, elevation) / 1000.0f) * cfg.LapseRatePer1000M;
                var tempNoise = SimplexNoise.Sample(x * cfg.TemperatureNoiseFrequency, y * cfg.TemperatureNoiseFrequency, tempSeed) * 5.0f;
                var temperature = baseLatTemp - lapseCooling + tempNoise;

                // 2. Calculate rainfall (fBm noise + proximity to water bodies)
                var rainNoise = (SimplexNoise.SampleFbm(x * cfg.RainfallNoiseFrequency, y * cfg.RainfallNoiseFrequency, rainSeed, octaves: 4) + 1.0f) * 0.5f;
                var riverBonus = hydrology.IsRiverWithinDistance(x, y, maxDistanceTiles: 2) ? 0.25f : 0.0f;
                var rainfall = Math.Clamp(rainNoise + riverBonus, 0.0f, 1.0f);

                // 3. Classify Whittaker biome
                var biome = ClassifyBiome(elevation, liquidDepth, temperature, rainfall, cfg.MountainThreshold);

                climate.SetCell(x, y, temperature, rainfall, biome);
            }
        }

        return climate;
    }

    /// <summary>
    ///     Classifies a tile coordinate into a <see cref="BiomeType"/> using elevation, temperature, and moisture.
    /// </summary>
    public static BiomeType ClassifyBiome(
        short elevation,
        ushort liquidDepth,
        float temperatureCelsius,
        float rainfall,
        short mountainThreshold)
    {
        // Hydrology overrides
        if (liquidDepth > 0 && elevation < 0)
        {
            return BiomeType.Ocean;
        }

        if (liquidDepth > 0 && elevation >= 0)
        {
            return BiomeType.Beach;
        }

        // High-altitude mountain / alpine peaks
        if (elevation >= mountainThreshold)
        {
            return temperatureCelsius <= 0.0f ? BiomeType.Glacial : BiomeType.Mountain;
        }

        // Polar / Sub-polar cold regimes
        if (temperatureCelsius < -5.0f)
        {
            return BiomeType.Glacial;
        }

        if (temperatureCelsius < 3.0f)
        {
            return rainfall < 0.35f ? BiomeType.Tundra : BiomeType.Taiga;
        }

        // Temperate regimes
        if (temperatureCelsius < 18.0f)
        {
            if (rainfall < 0.25f)
            {
                return BiomeType.Desert;
            }

            if (rainfall < 0.60f)
            {
                return BiomeType.Grassland;
            }

            return BiomeType.TemperateForest;
        }

        // Warm / Tropical regimes
        if (rainfall < 0.20f)
        {
            return BiomeType.Desert;
        }

        if (rainfall < 0.50f)
        {
            return BiomeType.Savanna;
        }

        return BiomeType.TropicalRainforest;
    }
}
