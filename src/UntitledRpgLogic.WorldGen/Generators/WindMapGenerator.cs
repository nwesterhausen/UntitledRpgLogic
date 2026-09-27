using System.Numerics;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.WorldGen.Generators;

public class WindMapGenerator : INoisemapGenerator<WindMap>
{
	public static WindMap Generate(ReadOnlyWorldGenContext generationContext)
	{
		var width = generationContext.MapConfig.WidthTiles;
		var height = generationContext.MapConfig.HeightTiles;
		var hasNorthPole = generationContext.MapConfig.Climate.NorthPole;
		var hasSouthPole = generationContext.MapConfig.Climate.SouthPole;

		var wind = new WindMap(width, height);

		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				var baselineWind = Calculations.CalculateBaselinePlanetaryWind(
					y, height, hasNorthPole, hasSouthPole);

				// here we do adjustments based on
				// - temperature gradients
				var thermalWind = Calculations.CalculateThermalWind(x, y, generationContext.Climate);
				baselineWind += thermalWind;

				// - mountain blocking
				var slopeSteepness = Calculations.GetSlopeAlongheading(x, y, baselineWind, generationContext);

				if (slopeSteepness > 0)
				{
					var direction = Calculations.SafeNormalize(baselineWind);
					var pushback = -direction;
					// adjust by some amount to avoid crazy wind
					var deflection = slopeSteepness / 500f;
					baselineWind += pushback * deflection;
				}

				// - volcanic influence
				var volcanicInfluence = Calculations.CalculateVolcanicInfluence(x, y, generationContext);
				baselineWind += volcanicInfluence;

				// - leyline influence (maybe make wind flow predominantly down the leylines)

				// Get the heading and velocity from our updated baseline
				var heading = MathF.Atan2(baselineWind.Y, baselineWind.X);
				var velocity = baselineWind.Length();

				wind.SetWindVector(x, y, new Vector2(heading, velocity));
			}
		}

		// Run an averaging pass using neighbors and averaging
		var relaxedWind = new WindMap(width, height);

		for (var x2 = 0; x2 < width; x2++)
		{
			for (var y2 = 0; y2 < height; y2++)
			{
				var directionSum = Vector2.Zero;
				var speedSum = 0f;

				foreach (var (dx, dy) in NeighborHelper.AllNeighbors)
				{
					var neighborWind = wind.GetWindVector(x2 + dx, y2 + dy);

					// Only extract direction if the wind is actually blowing
					if (neighborWind.LengthSquared() > 0)
					{
						directionSum += Vector2.Normalize(neighborWind);
					}

					speedSum += neighborWind.Length();
				}

				var averageDirection = Calculations.SafeNormalize(directionSum);
				var averageSpeed = speedSum / 9f;

				relaxedWind.SetWindVector(x2, y2, averageDirection * averageSpeed);
			}
		}

		return relaxedWind;
	}
}
