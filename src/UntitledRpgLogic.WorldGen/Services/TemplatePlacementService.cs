using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.WorldGen.Models;

namespace UntitledRpgLogic.WorldGen.Services;

/// <summary>
///     Evaluates prospective locations on procedural terrain grids to find valid sites for handcrafted stamps.
/// </summary>
public static class TemplatePlacementService
{
	/// <summary>
	///     Standard side length of a chunk in tiles.
	/// </summary>
	public const int ChunkDimension = 16;

	/// <summary>
	///     Evaluates how well a location on the heightmap and hydrology grids matches a template's criteria.
	/// </summary>
	/// <param name="heightmap">The terrain heightmap grid.</param>
	/// <param name="hydrology">The terrain hydrology grid.</param>
	/// <param name="targetChunkX">The prospective chunk X index.</param>
	/// <param name="targetChunkY">The prospective chunk Y index.</param>
	/// <param name="templateWidthChunks">The width of the stamp in chunks.</param>
	/// <param name="templateHeightChunks">The height of the stamp in chunks.</param>
	/// <param name="criteria">The terrain and moisture constraints required by the stamp.</param>
	/// <returns>A positive fitness score where higher values represent flatter/better matches, or 0.0f if rejected.</returns>
	public static float EvaluateCandidateScore(
		TerrainHeightmap heightmap,
		TerrainHydrology hydrology,
		int targetChunkX,
		int targetChunkY,
		int templateWidthChunks,
		int templateHeightChunks,
		PlacementCriteria criteria)
	{
		ArgumentNullException.ThrowIfNull(heightmap);
		ArgumentNullException.ThrowIfNull(hydrology);
		ArgumentNullException.ThrowIfNull(criteria);

		var tileStartX = targetChunkX * ChunkDimension;
		var tileStartY = targetChunkY * ChunkDimension;
		var tileEndX = tileStartX + (templateWidthChunks * ChunkDimension);
		var tileEndY = tileStartY + (templateHeightChunks * ChunkDimension);

		if (tileStartX < 0 || tileStartY < 0 || tileEndX > heightmap.WidthTiles || tileEndY > heightmap.HeightTiles)
		{
			return 0.0f;
		}

		var minFoundElevation = short.MaxValue;
		var maxFoundElevation = short.MinValue;
		var hasRiverAdjacent = false;

		for (var x = tileStartX; x < tileEndX; x++)
		{
			for (var y = tileStartY; y < tileEndY; y++)
			{
				var elev = heightmap.GetElevation(x, y);
				if (elev < minFoundElevation)
				{
					minFoundElevation = elev;
				}

				if (elev > maxFoundElevation)
				{
					maxFoundElevation = elev;
				}

				if (criteria.RequiresRiverProximity && !hasRiverAdjacent)
				{
					if (hydrology.IsRiverWithinDistance(x, y, criteria.MaxDistanceToRiverTiles))
					{
						hasRiverAdjacent = true;
					}
				}
			}
		}

		if (minFoundElevation < criteria.MinElevation || maxFoundElevation > criteria.MaxElevation)
		{
			return 0.0f;
		}

		if (criteria.RequiresRiverProximity && !hasRiverAdjacent)
		{
			return 0.0f;
		}

		var slopeVariance = maxFoundElevation - minFoundElevation;
		if (slopeVariance > criteria.MaxSlopeVariance)
		{
			return 0.0f;
		}

		return 100.0f - slopeVariance;
	}
}
