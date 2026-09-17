using UntitledRpgLogic.Core.World;
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
	public static HeightMap Generate(WorldMapConfiguration worldConfig, WorldGenContext? generationContext = null)
	{
		ArgumentNullException.ThrowIfNull(worldConfig);

		var heightMap = new HeightMap(worldConfig.WidthTiles, worldConfig.HeightTiles);

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
		// We would also want a "normalized" target distance for the falloff to start at. Choose something like 15% of the width or
		// height before the edge?
		var normalFalloffDistance = 1 - 0.45f;
		var finalFalloffDistance = 1 - 0.15f;
		// Force the maxelevation to lower faster and faster outside of the falloff distance
		// We can get a value that's distance from our point to the target by (abs(dist) - normalFalloffDist)
		var currEdgeDist = 0f;
		// Finally influence the degree of max elevation shrinkage, 100% -> % where elevation is 100 < sealevel.
		// maxElevation + 100 = span for -100 -> maxElevation. If we want maxElevation at overhang 0f and nothing at dist = edge distance
		// But we actually know x,y when we are working on this, so we can simply bound -100 hard at x < 5, x > width - 5 AND y < 5, y > height - 5
		// or find distance of point 5,5 from middle of the grid and then use that as a measuring stick.
		var edgeX = ((finalFalloffDistance * worldConfig.WidthTiles) - midX) / midX;
		var edgeY = ((finalFalloffDistance * worldConfig.WidthTiles) - midY) / midY;
		var finalEdge = (float)Math.Sqrt((edgeX * edgeX) + (edgeY * edgeY));
		// The ratio between targetEdge and finalEdge |((finalEdge - currEdge) - targetEdge) / targetEdge| plots on 0 - 1, multiply by span -100, maxElevation
		// WHEN currEdge > targetEdge and currEdge < finalEdge. IF currEdge > finalEdge, then clamp at -100. IF currEdge < targetEdge, we don't modify it.
		var ratioEdgeDist = 0f;
		// Then we adjust the max elevation to be a ratio based on the span(-100, maxelevation)
		var modifiedMaxElevation = (short)0;
		var falloffElevationSpan = worldConfig.Terrain.MaxElevation - worldConfig.Terrain.SeaLevel + 100;

		// modified seeds for distorting our distance calculation
		var modifiedSeed = worldConfig.Seed ^ 0x1030fedu;
		var distortionRatio = 0.05f;

		var rowOffset = 0;
		for (var y = 0; y < worldConfig.HeightTiles; y++)
		{
			rowOffset = y * worldConfig.WidthTiles;
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

					// if the abs(dist) is more than normalFalloffDistance we need to falloff
					if (dist >= normalFalloffDistance)
					{
						currEdgeDist = dist - normalFalloffDistance;
						if (dist > finalEdge)
						{
							// max elevation capped at -100
							modifiedMaxElevation = -100;
						}
						else
						{
							// max elevation is ratio * falloffElevSpan
							ratioEdgeDist =
								Math.Clamp((dist - normalFalloffDistance) / (finalEdge - normalFalloffDistance), 0f,
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
				heightMap.SetElevation(x, y,
					(short)Math.Clamp(adjustedElevation, short.MinValue, short.MaxValue));
			}
		}

		return heightMap;
	}
}
