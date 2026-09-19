using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Pure calculation service converting continuous world coordinates into chunk indices and local tile coordinates.
/// </summary>
public interface ISpatialMathService
{
	/// <summary>
	///     Translates a floating-point world coordinate into a chunk grid index.
	/// </summary>
	public (int ChunkX, int ChunkY) WorldToChunkCoordinates(float worldX, float worldY);

	/// <summary>
	///     Translates a floating-point world coordinate into a 0..15 local tile index within its chunk.
	/// </summary>
	public (int TileX, int TileY) WorldToLocalTileIndex(float worldX, float worldY);

	/// <summary>
	///     Calculates the Chebyshev or Euclidean distance between two world positions on the same map.
	/// </summary>
	public float CalculateDistance(WorldPosition posA, WorldPosition posB);
}
