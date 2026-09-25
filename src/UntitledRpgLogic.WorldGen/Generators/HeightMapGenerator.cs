using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;
using UntitledRpgLogic.WorldGen.Noise;

namespace UntitledRpgLogic.WorldGen.Generators;

/// <summary>
///     A generator for terrain height maps.
/// </summary>
public class HeightMapGenerator : INoisemapGenerator<HeightMap>
{
	private HeightMapGenerator() { }

	/// <inheritdoc />
	public static HeightMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var worldConfig = generationContext.MapConfig;
		var terrainHeight = new HeightMap(worldConfig.HeightTiles, worldConfig.WidthTiles);

		var normalizedMap = NoiseMaker.GenerateNoiseMap(
			worldConfig.Terrain.NoiseGeneration with { Seed = worldConfig.Seed }, worldConfig.WidthTiles,
			worldConfig.HeightTiles);

		var midX = worldConfig.WidthTiles / 2f;
		var midY = worldConfig.HeightTiles / 2f;

		// "raw" elevation is the elevation span * noise
		// "adjusted" elevation is the raw moved within the Min - Max range
		var rawElevation = 0f;
		var adjustedElevation = 0f;
		var elevationSpan = worldConfig.Terrain.MaxElevation - worldConfig.Terrain.MinElevation;

		// Can assign every point in the grid a specific point in range [-1.0, 1.0]
		// This then lets use do circle math for island falloff on a known range, no matter the dimensions of the grid.
		var normalX = 0f;
		var normalY = 0f;
		// distance from "origin" (middle) beyond falloffFinishDistance is capped at -100 elevation.
		var falloffStartDistance = 1 - 0.45f;
		var falloffFinishDistance = 1 - 0.15f;

		// pre-calculate the x,y values for the falloff finish, for quick calculations later
		var edgeX = ((falloffFinishDistance * worldConfig.WidthTiles) - midX) / midX;
		var edgeY = ((falloffFinishDistance * worldConfig.WidthTiles) - midY) / midY;

		// final edge is the distance from the "origin" (middle) to the falloff finish.
		var finalEdge = (float)Math.Sqrt((edgeX * edgeX) + (edgeY * edgeY));

		// The ratio between targetEdge and finalEdge |((finalEdge - currEdge) - targetEdge) / targetEdge| plots on 0 - 1, multiply by span -100, maxElevation
		// WHEN currEdge > targetEdge and currEdge < finalEdge. IF currEdge > finalEdge, then clamp at -100. IF currEdge < targetEdge, we don't modify it.
		var ratioEdgeDist = 0f;
		// Then we adjust the max elevation to be a ratio based on the span(-100, maxelevation)
		var modifiedMaxElevation = (short)0;
		var falloffElevationSpan = worldConfig.Terrain.MaxElevation - worldConfig.Terrain.SeaLevel + 100;

		// modified seeds for distorting our distance calculation
		var modifiedSeed = worldConfig.Seed ^ 0x1030fedu;
		var warpNoiseSettings = new NoiseSettings { Seed = modifiedSeed, Frequency = 0.5f, Scale = 20f, Octaves = 2 };
		var distortionRatio = 0.05f;

		for (var y = 0; y < worldConfig.HeightTiles; y++)
		{
			for (var x = 0; x < worldConfig.WidthTiles; x++)
			{
				var noise = normalizedMap.GetNoise(x, y);
				modifiedMaxElevation = worldConfig.Terrain.MaxElevation;

				// Can do island falloff calculation in here.
				if (worldConfig.Terrain.SurroundWithOcean)
				{
					// translate our point in [width, height] to a point in [-1.0, 1.0]
					normalX = (x - midX) / midX;
					normalY = (y - midY) / midY;

					// calculate distance from center of the grid (normalized)
					var dist = (float)Math.Sqrt((normalX * normalX) + (normalY * normalY));
					// warp noise is added to dist to create bays and other inland-pointing coastal features
					// helps break up the island from being all round to much more irregular
					var warpNoise = NoiseMaker.GenerateNoise(warpNoiseSettings, x, y);
					var modWarp = Math.Pow((warpNoise + 1) / 2, 2) * distortionRatio;
					dist += (float)modWarp;

					// if the abs(dist) is more than normalFalloffDistance we need to falloff
					if (dist >= falloffStartDistance)
					{
						if (dist > finalEdge)
						{
							// max elevation capped at -100
							modifiedMaxElevation = -100;
						}
						else
						{
							// max elevation is ratio * falloffElevSpan
							ratioEdgeDist =
								Math.Clamp((dist - falloffStartDistance) / (finalEdge - falloffStartDistance), 0f,
									1f);
							modifiedMaxElevation = (short)Math.Round(worldConfig.Terrain.MaxElevation -
							                                         (ratioEdgeDist * falloffElevationSpan));
						}
					}

					rawElevation = (modifiedMaxElevation - worldConfig.Terrain.MinElevation) * noise;
				}
				else
				{
					// To meet "base" requirements, just map noise into the range for elevation
					//
					// elevationSpan = MaxElevation - MinElevation. Simply transform noise into a value in 0 - elevationSpan.
					// Then, make sure we do not go out of bounds for allowed value.
					//
					rawElevation = elevationSpan * noise;
				}

				adjustedElevation = worldConfig.Terrain.MinElevation + MathF.Round(rawElevation);
				terrainHeight.SetElevation(x, y,
					(short)Math.Clamp(adjustedElevation, short.MinValue, short.MaxValue));
			}
		}

		return terrainHeight;
	}
}
