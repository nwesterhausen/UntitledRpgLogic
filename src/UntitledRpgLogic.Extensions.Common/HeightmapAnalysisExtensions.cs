using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Extensions.Common;

public readonly record struct LandmassStatistics(
	int TotalTiles,
	int LandTiles,
	int WaterTiles,
	float LandFraction,
	float LandPercentage);

public static class HeightmapAnalysisExtensions
{
	/// <summary>
	///     Calculates total land surface area and percentage relative to a given sea level.
	/// </summary>
	/// <param name="heightmap">The terrain elevation heightmap.</param>
	/// <param name="seaLevel">The vertical datum defining sea level in meters.</param>
	public static LandmassStatistics CalculateLandmass(
		this Heightmap heightmap,
		short seaLevel = 0)
	{
		ArgumentNullException.ThrowIfNull(heightmap);

		var totalTiles = heightmap.WidthTiles * heightmap.HeightTiles;
		var landTiles = 0;

		// Iterate directly through the 1D span without bounds-check overhead
		var span = heightmap.AsReadOnlySpan();
		for (var i = 0; i < span.Length; i++)
		{
			if (span[i] >= seaLevel)
			{
				landTiles++;
			}
		}

		var waterTiles = totalTiles - landTiles;
		var fraction = totalTiles > 0 ? (float)landTiles / totalTiles : 0.0f;

		return new LandmassStatistics(
			TotalTiles: totalTiles,
			LandTiles: landTiles,
			WaterTiles: waterTiles,
			LandFraction: fraction,
			LandPercentage: fraction * 100.0f);
	}
}
