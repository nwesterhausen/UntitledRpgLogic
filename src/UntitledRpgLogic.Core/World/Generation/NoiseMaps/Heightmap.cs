namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     Represents the procedural or sampled bedrock elevation grid across a 2D map coordinate space.
/// </summary>
public sealed class HeightMap : NoiseGrid2D<short>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="HeightMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public HeightMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="HeightMap" /> class using an existing flat elevations array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="elevations">The flattened bedrock elevations array of length <c>widthTiles * heightTiles</c>.</param>
	public HeightMap(int widthTiles, int heightTiles, short[] elevations)
		: base(widthTiles, heightTiles, elevations)
	{
	}

	/// <summary>
	///     Retrieves the bedrock elevation at the specified tile coordinate. Clamps coordinates to valid boundaries.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The bedrock elevation value at the given coordinate.</returns>
	public short GetElevation(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the bedrock elevation at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="elevation">The bedrock elevation to assign.</param>
	public void SetElevation(int tileX, int tileY, short elevation) => this.SetValue(tileX, tileY, elevation);
}
