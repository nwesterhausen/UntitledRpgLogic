using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.WorldGen.Generators;

public class BlendedHeightMapGenerator : INoisemapGenerator<HeightMap>
{
	private static readonly float CalderaVolcanismThreshold = 0.85f;

	/// <summary>
	///     Minimum elevation for a volcanic caldera on land (in % of overall land height)
	/// </summary>
	private static readonly float CalderaMinElevationRatio = 0.4f;

	/// <summary>
	///     Percentage of land height to carve out for a caldera.
	/// </summary>
	private static readonly float CalderaCarveRatio = 0.35f;

	private static readonly float CalderaRimSharpness = 3.0f;

	private static readonly float HotspotVolcanismThreshold = 0.8f;
	private static readonly float HotspotUpliftRatio = 0.3f;

	private static readonly float UnderwaterCalderaCenterVolcanismThreshold = 0.9f;
	private static readonly float UnderwaterCarveRatio = 0.4f;
	private static readonly float UnderwaterRimRatio = 0.25f;
	private BlendedHeightMapGenerator() { }


	public static HeightMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var blendedHeight =
			new HeightMap(generationContext.MapConfig.WidthTiles, generationContext.MapConfig.HeightTiles);

		for (var x = 0; x < generationContext.MapConfig.WidthTiles; x++)
		{
			for (var y = 0; y < generationContext.MapConfig.HeightTiles; y++)
			{
				var elevation = generationContext.Terrain.GetElevation(x, y);
				var volcanism = generationContext.Terrain.GetVolcanism(x, y);

				// Trigger a volcano caldera only at high volcanism AND high height
				var calderaElevation = ApplyCaldera(generationContext.MapConfig, elevation, volcanism);
				if (calderaElevation != elevation)
				{
					blendedHeight.SetElevation(x, y, calderaElevation);
					continue;
				}

				var hotspotElevation = ApplyHotspotIsland(generationContext.MapConfig, elevation, volcanism);
				if (hotspotElevation != elevation)
				{
					blendedHeight.SetElevation(x, y, hotspotElevation);
					continue;
				}

				var underwaterCalderaElevation =
					ApplyUnderwaterCaldera(generationContext.MapConfig, elevation, volcanism);
				if (underwaterCalderaElevation != elevation)
				{
					blendedHeight.SetElevation(x, y, underwaterCalderaElevation);
				}
			}
		}

		return blendedHeight;
	}

	private static short ClampElevation(WorldMapConfiguration config, short elevation) =>
		Math.Clamp(elevation, config.Terrain.MinElevation, config.Terrain.MaxElevation);

	private static short ApplyCaldera(
		WorldMapConfiguration config,
		short height,
		float volcanism)
	{
		var targetMinElevation = config.Terrain.SeaLevel + (config.Terrain.LandSpan * CalderaMinElevationRatio);

		if (volcanism < CalderaVolcanismThreshold || height < targetMinElevation)
		{
			return height;
		}

		// Scale to [0,1] for strength of volcanism within our targets
		var calderaValue = (volcanism - CalderaVolcanismThreshold) / (1f - CalderaVolcanismThreshold);

		// Determine elevation changes
		var maxCarve = config.Terrain.LandSpan * CalderaCarveRatio;
		var depression = (short)(MathF.Pow(calderaValue, CalderaRimSharpness) * maxCarve);

		return ClampElevation(config, depression);
	}

	/// <remarks>
	///     Uplifts ocean floor into islands when volcanism is high.
	/// </remarks>
	private static short ApplyHotspotIsland(
		WorldMapConfiguration config,
		short height,
		float volcanism)
	{
		if (height >= config.Terrain.SeaLevel || volcanism < HotspotVolcanismThreshold)
		{
			return height;
		}

		// Scale to [0,1] for strength
		var hotspotValue = (volcanism - HotspotVolcanismThreshold) / (1f - HotspotVolcanismThreshold);

		// Smoothstep for island shape
		var coneFactor = hotspotValue * hotspotValue * (3f - (2f * hotspotValue));

		// Amount of change scaled to sea level to guarentee an island
		var upliftMax = config.Terrain.WaterSpan * (1f + HotspotUpliftRatio);
		var upliftedHeight = (short)(height + (coneFactor * upliftMax));

		return ClampElevation(config, upliftedHeight);
	}

	/// <remarks>
	///     Carves a collapse trench ringed by a shallow rim below sea level.
	/// </remarks>
	private static short ApplyUnderwaterCaldera(
		WorldMapConfiguration config,
		short height,
		float volcanism)
	{
		if (height >= config.Terrain.SeaLevel || volcanism < CalderaVolcanismThreshold)
		{
			return height;
		}

		var centerDiff = Math.Abs(volcanism - UnderwaterCalderaCenterVolcanismThreshold);

		// Center crater collapse
		var centerThreshold = 0.05f;
		if (centerDiff < centerThreshold)
		{
			var pitIntensity = 1f - (centerDiff / centerThreshold);
			var drop = (short)(pitIntensity * (config.Terrain.WaterSpan * UnderwaterCarveRatio));
			return drop;
		}

		// Surrounding rim
		var rimThreshold = 0.12f;
		var rimSlope1 = 0.085f;
		var rimSlope2 = 0.035f;
		var rimMaxHeight = config.Terrain.SeaLevel - 20;
		if (centerDiff >= centerThreshold && centerDiff <= rimThreshold)
		{
			var rimIntensity = 1f - (Math.Abs(centerDiff - rimSlope1) / rimSlope2);
			var lift = rimIntensity * (config.Terrain.WaterSpan * UnderwaterRimRatio);

			// Must be below sea level to be "underwater"
			var rim = (short)Math.Min(rimMaxHeight, height + lift);
			return rim;
		}

		return height;
	}
}
