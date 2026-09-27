using System.Numerics;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;

namespace UntitledRpgLogic.WorldGen;

public static class Calculations
{
	private static readonly float VolcanismSuctionThreshold = 0.5f;
	private static readonly float VolcanicHeatMultiplier = 2f;

	/// <summary>
	///     Get a normalized latitude based on distance from the equator (may result in a <c>0</c> return if there is
	///     no equator (no poles).
	/// </summary>
	/// <param name="yCoord"></param>
	/// <param name="height"></param>
	/// <param name="hasNorthPole"></param>
	/// <param name="hasSouthPole"></param>
	/// <returns></returns>
	public static float CalculateNormalizedLatitude(float yCoord, float height, bool hasNorthPole, bool hasSouthPole)
	{
		// Uniform world: no poles, treated as equatorial calm/monsoon
		if (!hasNorthPole && !hasSouthPole)
		{
			return 0f;
		}

		if (hasNorthPole && hasSouthPole)
		{
			var halfHeight = height / 2f;
			// 0.0 at equator (y = halfHeight), 1.0 at top/bottom edges
			return Math.Clamp(Math.Abs(halfHeight - yCoord) / halfHeight, 0f, 1f);
		}

		if (hasNorthPole)
		{
			// North Pole is y = 0 (t = 1.0), Equator is y = height (t = 0.0)
			return Math.Clamp((height - yCoord) / height, 0f, 1f);
		}

		// South Pole is y = height (t = 1.0), Equator is y = 0 (t = 0.0)
		return Math.Clamp(yCoord / height, 0f, 1f);
	}

	/// <summary>
	///     Check if given y coordinate is considered to be Northern Hemisphere based on poles and map height.
	/// </summary>
	/// <param name="yCoord"></param>
	/// <param name="height"></param>
	/// <param name="hasNorthPole"></param>
	/// <param name="hasSouthPole"></param>
	/// <returns></returns>
	public static bool IsNortherHemisphere(float yCoord, float height, bool hasNorthPole, bool hasSouthPole)
	{
		var equatorY = !hasNorthPole && hasSouthPole ? 0f :
			hasNorthPole && !hasSouthPole ? height :
			height / 2f;

		var isNorthernHemisphere = yCoord < equatorY;
		return isNorthernHemisphere;
	}

	/// <summary>
	///     Calculate a baseline wind vector based on latitude and any poles.
	/// </summary>
	/// <param name="y"></param>
	/// <param name="height"></param>
	/// <param name="hasNorthPole"></param>
	/// <param name="hasSouthPole"></param>
	/// <returns></returns>
	public static Vector2 CalculateBaselinePlanetaryWind(int y, float height, bool hasNorthPole, bool hasSouthPole)
	{
		// First check if no poles, and quick exit with just some light breeze
		if (!hasNorthPole && !hasSouthPole)
		{
			return new Vector2(-1.5f, 0f);
		}

		var normalizedLatitude = CalculateNormalizedLatitude(
			y, height, hasNorthPole, hasSouthPole);
		var isNorthernHemisphere = IsNortherHemisphere(
			y, height, hasNorthPole, hasSouthPole);
		var towardEquatorY = isNorthernHemisphere ? 1f : -1f;
		var towardPoleY = -towardEquatorY;

		if (normalizedLatitude <= 0.05)
		{
			// equatorial area, calm
			// near zero velocity, calm winds
			return new Vector2(-0.5f, towardEquatorY * 0.2f);
		}

		if (normalizedLatitude <= 0.35)
		{
			// Easterlies
			// wind blowing west, angle toward equator
			var bandInfluence = (normalizedLatitude - 0.05f) / 0.3f;
			var speed = (MathF.Sin(bandInfluence * MathF.PI) * 5f) + 1f;
			return new Vector2(-speed, towardEquatorY * (speed * 0.35f));
		}

		if (normalizedLatitude <= 0.40)
		{
			// High-pressure Calm
			// light winds
			return new Vector2(0.5f, 0f);
		}

		if (normalizedLatitude <= 0.70)
		{
			// Westerlies
			// wind blowing east, angle toward pole
			var bandInfluence = (normalizedLatitude - 0.40f) / 0.30f;
			var speed = (MathF.Sin(bandInfluence * MathF.PI) * 7.0f) + 2.0f;
			return new Vector2(speed, towardPoleY * (speed * 0.4f));
		}

		if (normalizedLatitude <= 0.75)
		{
			// Polar Front Convergence
			// string shear and variable direction
			return new Vector2(1f, towardEquatorY * 1f);
		}

		{
			// Polar Easterlies
			// cold wind blowing west, spiral away from pole
			var bandInfluence = (normalizedLatitude - 0.75f) / 0.25f;
			var speed = ((1.0f - bandInfluence) * 4.0f) + 1.0f;
			return new Vector2(-speed, towardEquatorY * (speed * 0.5f));
		}
	}

	public static float GetSlopeAlongheading(int x, int y, Vector2 currentWind, ReadOnlyWorldGenContext context)
	{
		// check for dead air
		if (currentWind.LengthSquared() < 0.001f)
		{
			return 0f;
		}

		// normalize as direction (to disregard strength and make direction easier)
		var direction = Vector2.Normalize(currentWind);

		// force direction into our nice neightbor coordinates
		var dirX = (int)MathF.Round(direction.X);
		var dirY = (int)MathF.Round(direction.Y);

		// Forward (wind into)
		var fx = x + dirX;
		var fy = y + dirY;
		// Backward (wind from)
		var bx = x - dirX;
		var by = y - dirY;

		// Get elevations
		var center = context.Terrain.GetElevation(x, y);
		var forward = context.Terrain.GetElevationOrDefault(fx, fy, center);
		var forwardIsSea = context.Hydrology.GetWaterBodyType(fx, fy) == WaterBodyType.Ocean;
		var backward = context.Terrain.GetElevationOrDefault(bx, by, center);
		var backwardIsSea = context.Hydrology.GetWaterBodyType(bx, by) == WaterBodyType.Ocean;
		var seaLevel = context.MapConfig.Terrain.SeaLevel;

		// wind isn't influenced by values below sealevel IF it's a sea
		var slopeSteepness =
			(forwardIsSea ? Math.Clamp(forward, seaLevel, short.MaxValue) : forward) -
			(backwardIsSea ? Math.Clamp(backward, seaLevel, short.MaxValue) : backward);
		return slopeSteepness;
	}

	public static Vector2 CalculateVolcanicInfluence(int x, int y, ReadOnlyWorldGenContext context)
	{
		// Surface wind ignores deep underwater magma vents
		if (context.Hydrology.GetWaterBodyType(x, y) == WaterBodyType.Ocean)
		{
			return Vector2.Zero;
		}

		var strongestVolcanicInfluence = 0f;
		var (dirVX, dirVY) = NeighborHelper.East;

		foreach (var (vx, vy) in NeighborHelper.AllNeighbors)
		{
			// Use context.Terrain instead of the old 'terrain' parameter
			var checkVolcanism = context.Terrain.GetVolcanismOrDefault(vx + x, vy + y, strongestVolcanicInfluence);
			if (checkVolcanism > strongestVolcanicInfluence)
			{
				strongestVolcanicInfluence = checkVolcanism;
				dirVX = vx;
				dirVY = vy;
			}
		}

		if (strongestVolcanicInfluence < VolcanismSuctionThreshold)
		{
			return Vector2.Zero;
		}

		var nearestVolcano = new Vector2(dirVX, dirVY);
		var normalizedVolcanismOverThreshold = float.Lerp(VolcanismSuctionThreshold, 1f, strongestVolcanicInfluence);
		var volcanicSuction = normalizedVolcanismOverThreshold * VolcanicHeatMultiplier;

		return nearestVolcano * volcanicSuction;
	}

	public static Vector2 CalculateThermalWind(int x, int y, ReadOnlyClimateMaps climate)
	{
		var centerTemp = climate.GetTemperature(x, y);
		var coldestNeighborOffset = (0, 0);
		var coldestTemp = centerTemp; // Start at center temp so we only care if a neighbor is ACTUALLY colder

		foreach (var (dx, dy) in NeighborHelper.AllNeighbors)
		{
			var nx = x + dx;
			var ny = y + dy;

			var neighborTemp = climate.GetTemperature(nx, ny);
			if (neighborTemp < coldestTemp)
			{
				coldestTemp = neighborTemp;
				coldestNeighborOffset = (dx, dy); // Keep as offset for the directional push
			}
		}

// Only apply thermal wind if we actually found a colder neighbor
		if (!(coldestTemp < centerTemp))
		{
			return Vector2.Zero;
		}

		var pushDirection = new Vector2(-coldestNeighborOffset.Item1, -coldestNeighborOffset.Item2);
		pushDirection = SafeNormalize(pushDirection);

		var tempDifference = centerTemp - coldestTemp;
		return pushDirection * tempDifference;
	}

	/// <summary>
	///     Normalize helper that avoids <c>NaN</c> returns (and instead returns <c>Vector2.Zero</c>)
	/// </summary>
	/// <param name="v"></param>
	/// <returns></returns>
	public static Vector2 SafeNormalize(Vector2 v) => v.LengthSquared() > 0 ? Vector2.Normalize(v) : Vector2.Zero;
}
