using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Pure spatial math service converting continuous floating-point coordinates
///     into 16x16 chunk grid coordinates and local tile offsets.
/// </summary>
public sealed class SpatialMathService : ISpatialMathService
{
	private const int ChunkSize = 16;

	/// <inheritdoc />
	public (int ChunkX, int ChunkY) WorldToChunkCoordinates(float worldX, float worldY)
	{
		var chunkX = (int)MathF.Floor(worldX / ChunkSize);
		var chunkY = (int)MathF.Floor(worldY / ChunkSize);
		return (chunkX, chunkY);
	}

	/// <inheritdoc />
	public (int TileX, int TileY) WorldToLocalTileIndex(float worldX, float worldY)
	{
		var tileX = (int)MathF.Floor(worldX) % ChunkSize;
		var tileY = (int)MathF.Floor(worldY) % ChunkSize;

		// Ensure negative world coordinates wrap correctly into [0..15]
		if (tileX < 0)
		{
			tileX += ChunkSize;
		}

		if (tileY < 0)
		{
			tileY += ChunkSize;
		}

		return (tileX, tileY);
	}

	/// <inheritdoc />
	public float CalculateDistance(WorldPosition posA, WorldPosition posB)
	{
		ArgumentNullException.ThrowIfNull(posA);
		ArgumentNullException.ThrowIfNull(posB);

		if (posA.MapId != posB.MapId)
		{
			return float.PositiveInfinity;
		}

		var dx = posA.X - posB.X;
		var dy = posA.Y - posB.Y;
		var dz = posA.Elevation - posB.Elevation;

		return MathF.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
	}
}
