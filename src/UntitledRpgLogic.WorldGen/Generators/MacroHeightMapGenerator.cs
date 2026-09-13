using UntitledRpgLogic.WorldGen.Models;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

public static class MacroHeightmapGenerator
{
	/// <summary>
	///     Populates a <see cref="TerrainHeightmap" /> with bedrock elevations, applying an ocean falloff mask if requested.
	/// </summary>
	public static TerrainHeightmap Generate(
		uint seed,
		WorldMapConfiguration config,
		HeightmapSettings? noiseSettings = null)
	{
		ArgumentNullException.ThrowIfNull(config);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(config.WidthTiles);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(config.HeightTiles);

		var noise = noiseSettings ?? new HeightmapSettings();
		var heightmap = new TerrainHeightmap(config.WidthTiles, config.HeightTiles);
		var elevationSpan = config.MaxElevation - config.MinElevation;

		var halfW = config.WidthTiles / 2.0f;
		var halfH = config.HeightTiles / 2.0f;

		for (var y = 0; y < config.HeightTiles; y++)
		{
			// Normalized Y distance from center in range [-1.0, 1.0]
			var dy = (y - halfH) / halfH;

			for (var x = 0; x < config.WidthTiles; x++)
			{
				// Normalized X distance from center in range [-1.0, 1.0]
				var dx = (x - halfW) / halfW;

				// 1. Sample raw fBm terrain noise in range [-1.0, 1.0]
				var rawNoise = SimplexNoise.SampleFbm(
					x * noise.Frequency,
					y * noise.Frequency,
					seed,
					noise.Octaves,
					noise.Persistence,
					noise.Lacunarity);

				var normalizedNoise = Math.Clamp((rawNoise + 1.0f) * 0.5f, 0.0f, 1.0f);

				// 2. Apply island perimeter mask if configured
				if (config.SurroundWithOcean)
				{
					// Square-circle distance to the map edge
					var distFromCenter = MathF.Pow((dx * dx) + (dy * dy), 0.5f);

					// Remap: 0.0 at center, scaling up towards 1.0 at border
					var borderStart = 1.0f - config.OceanBorderThickness;
					var falloff = 0.0f;

					if (distFromCenter > borderStart)
					{
						var t = (distFromCenter - borderStart) / config.OceanBorderThickness;
						falloff = MathF.Pow(Math.Clamp(t, 0.0f, 1.0f), config.IslandFalloffSteepness);
					}

					// Subtract the falloff from the noise elevation
					normalizedNoise = Math.Clamp(normalizedNoise - falloff, 0.0f, 1.0f);
				}

				// 3. Shape mountains and valleys
				var shaped = MathF.Pow(normalizedNoise, 1.25f);
				var elevation = (short)MathF.Round(config.MinElevation + (shaped * elevationSpan));

				// Force edge tiles strictly below sea level
				if (config.SurroundWithOcean &&
				    (x == 0 || x == config.WidthTiles - 1 || y == 0 || y == config.HeightTiles - 1))
				{
					elevation = (short)Math.Min(elevation, config.SeaLevel - 500);
				}

				heightmap.SetElevation(x, y, elevation);
			}
		}

		return heightmap;
	}
}
