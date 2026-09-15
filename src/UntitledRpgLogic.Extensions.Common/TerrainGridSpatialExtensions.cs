using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;

namespace UntitledRpgLogic.Extensions.Common;

public static class TerrainGridSpatialExtensions
{
	/// <summary>
	///     Evaluates whether a sub-rectangle fits entirely within the map boundary.
	/// </summary>
	public static bool ContainsRegion(this NoiseGridDimensions grid, int startX, int startY, int width, int height)
	{
		ArgumentNullException.ThrowIfNull(grid);

		return grid.IsInBounds(startX, startY) && grid.IsInBounds(startX + width - 1, startY + height - 1);
	}
}
