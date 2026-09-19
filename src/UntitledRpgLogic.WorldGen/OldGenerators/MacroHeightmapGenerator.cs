using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.OldGenerators;

/// <summary>
///     Generates a heightmap based on settings.
/// </summary>
public sealed class MacroHeightmapGenerator : INoisemapGenerator<HeightMap>
{
	private MacroHeightmapGenerator() { }

	/// <summary>
	///     Populates a <see cref="HeightMap" /> with bedrock elevations, applying an ocean falloff mask if requested.
	/// </summary>
	public static HeightMap Generate(
		WorldGenContext generationContext)
	{
		ArgumentNullException.ThrowIfNull(generationContext);

		var worldConfig = generationContext.MapConfig;

		var seed = worldConfig.Seed;
		var noiseSettings = new NoiseSettings
		{
			Seed = worldConfig.Seed,
			Scale = worldConfig.Terrain.NoiseGeneration.Frequency,
			Octaves = worldConfig.Terrain.NoiseGeneration.Octaves,
			Lacunarity = worldConfig.Terrain.NoiseGeneration.Lacunarity,
			Persistence = worldConfig.Terrain.NoiseGeneration.Persistence
		};

		var rawNoise = NoiseMaker.GenerateNoiseArray(noiseSettings, worldConfig.WidthTiles, worldConfig.HeightTiles);
		var heightmap = new HeightMap(worldConfig.WidthTiles, worldConfig.HeightTiles);
		var height = worldConfig.HeightTiles;
		var width = worldConfig.WidthTiles;

		var halfW = width / 2.0f;
		var halfH = height / 2.0f;
		var elevationSpan = worldConfig.Terrain.MaxElevation - worldConfig.Terrain.MinElevation;

		var modifiedSeed1 = new NoiseSettings { Seed = seed ^ 0x1030fedu };
		var modifiedSeed2 = new NoiseSettings { Seed = seed ^ 0xab20cd44u };

		// Secondary buffer width: 25% of the configured ocean border thickness
		var secondaryBorderThickness =
			Math.Clamp(worldConfig.Terrain.OceanBorderThickness * 0.25f, 0.03f, 0.10f);
		var secondaryBorderStart = 1.0f - secondaryBorderThickness;

		for (var y = 0; y < height; y++)
		{
			// dy and dx range from -1.0 (edges) to 0.0 (center) to +1.0 (opposite edge)
			var dy = (y - halfH) / halfH;
			var rowOffset = y * width;

			for (var x = 0; x < width; x++)
			{
				var dx = (x - halfW) / halfW;
				var normalized = rawNoise[rowOffset + x]; // Must be [0.0, 1.0]

				if (worldConfig.Terrain.SurroundWithOcean)
				{
					// Sample low-frequency noise to distort the distance field
					var warpX = NoiseMaker.GenerateNoise(modifiedSeed1, x * 0.015f, y * 0.015f);
					var warpY = NoiseMaker.GenerateNoise(modifiedSeed2, x * 0.015f, y * 0.015f);

					var warpedNx = dx + warpX;
					var warpedNy = dy + warpY;
					// 2. Continuous distance from center using warped coordinates
					var dist = MathF.Sqrt((warpedNx * warpedNx) + (warpedNy * warpedNy));

					// 3. Smooth power curve falloff across the outer half of the map
					// Center (dist <= 0.35) remains unpenalized; edges (dist >= 1.0) drop completely
					const float InnerLandRadius = 0.35f;

					if (dist > InnerLandRadius)
					{
						var t = (dist - InnerLandRadius) / (1.15f - InnerLandRadius);
						var falloff = MathF.Pow(Math.Clamp(t, 0.0f, 1.0f),
							worldConfig.Terrain.IslandFalloffSteepness);
						// Subtracting the falloff pulls elevations toward 0.0 (< 0.09 is ocean)
						normalized = Math.Clamp(normalized - falloff, 0.0f, 1.0f);
					}

					// Guarantees outer boundaries submerge smoothly without a sharp 1-pixel clamp
					var absX = Math.Abs(dx);
					var absY = Math.Abs(dy);

					// Corner curvature radius (smooths corner transitions)
					const float cornerRadius = 0.25f;
					var straightEdgeDist = MathF.Max(absX, absY);
					var cornerExcessX = MathF.Max(0.0f, absX - (1.0f - cornerRadius));
					var cornerExcessY = MathF.Max(0.0f, absY - (1.0f - cornerRadius));
					var cornerDist = 1.0f - cornerRadius +
					                 MathF.Sqrt((cornerExcessX * cornerExcessX) + (cornerExcessY * cornerExcessY));

					// Blend straight edges into rounded corners
					var roundedBoxDist = absX > 1.0f - cornerRadius && absY > 1.0f - cornerRadius
						? cornerDist
						: straightEdgeDist;

					if (roundedBoxDist > secondaryBorderStart)
					{
						var tSecondary = (roundedBoxDist - secondaryBorderStart) / secondaryBorderThickness;
						// Quadratic ramp down to ocean bed
						var secondaryFalloff = MathF.Pow(Math.Clamp(tSecondary, 0.0f, 1.0f), 2.0f);
						normalized = Math.Clamp(normalized - secondaryFalloff, 0.0f, 1.0f);
					}
				}

				// Low values (< 0.45) produce flat ocean floors; high values produce mountains
				float shapedElevation;
				if (normalized < 0.35f)
				{
					shapedElevation = normalized * 0.6f;
				}
				else if (normalized < 0.70f)
				{
					var t = (normalized - 0.35f) / 0.35f;
					shapedElevation = 0.21f + (t * 0.35f);
				}
				else
				{
					var t = (normalized - 0.70f) / 0.30f;
					shapedElevation = 0.56f + (MathF.Pow(t, 2.0f) * 0.44f);
				}

				var targetElev =
					(short)MathF.Round(worldConfig.Terrain.MinElevation + (shapedElevation * elevationSpan));


				heightmap.SetElevation(x, y, Math.Clamp(targetElev, short.MinValue, short.MaxValue));
			}
		}

		return heightmap;
	}
}
