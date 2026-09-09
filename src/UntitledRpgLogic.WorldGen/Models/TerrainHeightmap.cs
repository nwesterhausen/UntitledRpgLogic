namespace UntitledRpgLogic.WorldGen.Models;

/// <summary>
///     Represents the procedural or sampled bedrock elevation grid across a 2D map coordinate space.
/// </summary>
public class TerrainHeightmap
{
	private readonly short[] _elevations;

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainHeightmap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public TerrainHeightmap(int widthTiles, int heightTiles)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(widthTiles);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heightTiles);

		this.WidthTiles = widthTiles;
		this.HeightTiles = heightTiles;
		this._elevations = new short[widthTiles * heightTiles];
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainHeightmap" /> class using an existing flat elevations array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="elevations">The flattened bedrock elevations array of length <c>widthTiles * heightTiles</c>.</param>
	public TerrainHeightmap(int widthTiles, int heightTiles, short[] elevations)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(widthTiles);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heightTiles);
		ArgumentNullException.ThrowIfNull(elevations);

		if (elevations.Length != widthTiles * heightTiles)
		{
			throw new ArgumentException(
				$"Elevations array length ({elevations.Length}) must equal width * height ({widthTiles * heightTiles}).",
				nameof(elevations));
		}

		this.WidthTiles = widthTiles;
		this.HeightTiles = heightTiles;
		this._elevations = elevations;
	}

	/// <summary>
	///     Gets the total horizontal dimension of the heightmap in tiles.
	/// </summary>
	public int WidthTiles { get; }

	/// <summary>
	///     Gets the total vertical dimension of the heightmap in tiles.
	/// </summary>
	public int HeightTiles { get; }

	/// <summary>
	///     Retrieves the bedrock elevation at the specified tile coordinate. Clamps coordinates to valid boundaries.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The bedrock elevation value at the given coordinate.</returns>
	public short GetElevation(int tileX, int tileY)
	{
		var clampedX = Math.Clamp(tileX, 0, this.WidthTiles - 1);
		var clampedY = Math.Clamp(tileY, 0, this.HeightTiles - 1);
		return this._elevations[(clampedY * this.WidthTiles) + clampedX];
	}

	/// <summary>
	///     Sets the bedrock elevation at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="elevation">The bedrock elevation to assign.</param>
	public void SetElevation(int tileX, int tileY, short elevation)
	{
		if (tileX >= 0 && tileX < this.WidthTiles && tileY >= 0 && tileY < this.HeightTiles)
		{
			this._elevations[(tileY * this.WidthTiles) + tileX] = elevation;
		}
	}
}
