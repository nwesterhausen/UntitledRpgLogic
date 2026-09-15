namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     Represents the mid-scale normalized precipitation/rainfall simulation layer (0.0 to 1.0) across a 2D map
///     space.
/// </summary>
public sealed class TerrainRainfallMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainRainfallMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public TerrainRainfallMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainRainfallMap" /> class using an existing flat materials array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="rainfall">The flattened rainfall array of length <c>widthTiles * heightTiles</c>.</param>
	public TerrainRainfallMap(int widthTiles, int heightTiles, float[] rainfall)
		: base(widthTiles, heightTiles, rainfall)
	{
	}

	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The rainfall of the tile, or null if out of bounds.</returns>
	public float GetRainfall(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the rainfall at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="rainfall">The rainfall to assign.</param>
	public void SetRainfall(int tileX, int tileY, float rainfall) =>
		this.SetValue(tileX, tileY, Math.Clamp(rainfall, 0.0f, 1.0f));
}
