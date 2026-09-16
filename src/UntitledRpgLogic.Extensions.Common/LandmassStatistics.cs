namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Describes the statistics for a generated heightmap.
/// </summary>
/// <param name="TotalTiles">Total count of tiles in the map</param>
/// <param name="LandTiles">Total count of land (i.e. above sea level) tiles in the map.</param>
/// <param name="WaterTiles">Total count of water tiles in the map.</param>
/// <param name="LandFraction">Ratio of land to water.</param>
/// <param name="LandPercentage">Ratio of land to water, but multiplied by <c>100f</c> to be a percentage.</param>
public readonly record struct LandmassStatistics(
	int TotalTiles,
	int LandTiles,
	int WaterTiles,
	float LandFraction,
	float LandPercentage);
