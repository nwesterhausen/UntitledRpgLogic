using UntitledRpgLogic.Core.World.Generation;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extensions to analyze heightmaps.
/// </summary>
public static class TerrainMapsAnalysisExtensions
{
	/// <summary>
	///     Calculates total land surface area and percentage relative to a given sea level.
	/// </summary>
	/// <param name="terrain">The collection of terrain maps.</param>
	/// <param name="seaLevel">The sea level elevation</param>
	public static LandmassStatistics CalculateLandmass(
		this TerrainMaps terrain,
		short seaLevel = 0)
	{
		ArgumentNullException.ThrowIfNull(terrain);

		var landTiles = 0;

		// Iterate directly through the 1D span without bounds-check overhead
		var span = terrain.HeightMapCells;
		foreach (var t in span)
		{
			if (t >= seaLevel)
			{
				landTiles++;
			}
		}

		var waterTiles = terrain.TotalTiles - landTiles;
		var fraction = terrain.TotalTiles > 0 ? (float)landTiles / terrain.TotalTiles : 0.0f;

		return new LandmassStatistics(
			terrain.TotalTiles,
			landTiles,
			waterTiles,
			fraction,
			fraction * 100.0f);
	}
}
