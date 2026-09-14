namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Represents the mid-scale ambient temperature simulation layer in degrees Celsius across a 2D map space.
/// </summary>
public sealed class TerrainTemperatureMap : TerrainGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainTemperatureMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public TerrainTemperatureMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainTemperatureMap" /> class using an existing flat materials
	///     array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="temperatures">The flattened temperature array of length <c>widthTiles * heightTiles</c>.</param>
	public TerrainTemperatureMap(int widthTiles, int heightTiles, float[] temperatures)
		: base(widthTiles, heightTiles, temperatures)
	{
	}


	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The temperature of the tile, or null if out of bounds.</returns>
	public float GetTemperature(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the temperature at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="temperature">The temperature to assign.</param>
	public void SetTemperature(int tileX, int tileY, float temperature) => this.SetValue(tileX, tileY, temperature);
}
