using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Extensions.Common;

public static class HydrologySpatialExtensions
{
	/// <summary>
	///     Scans whether a designated river cell exists within a Chebyshev distance radius around an origin coordinate[cite:
	///     2].
	/// </summary>
	public static bool IsRiverWithinDistance(this TerrainHydrology hydrology, int originX, int originY,
		int maxDistanceTiles)
	{
		ArgumentNullException.ThrowIfNull(hydrology);

		var startX = Math.Max(0, originX - maxDistanceTiles);
		var endX = Math.Min(hydrology.WidthTiles - 1, originX + maxDistanceTiles);
		var startY = Math.Max(0, originY - maxDistanceTiles);
		var endY = Math.Min(hydrology.HeightTiles - 1, originY + maxDistanceTiles);

		for (var y = startY; y <= endY; y++)
		{
			for (var x = startX; x <= endX; x++)
			{
				if (hydrology.IsRiver(x, y))
				{
					return true;
				}
			}
		}

		return false;
	}
}
