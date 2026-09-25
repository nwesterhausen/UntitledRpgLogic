namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     Represents the mid-scale ambient temperature simulation layer in degrees Celsius across a 2D map space.
///     Influenced by <see cref="HeightMap" />, desired pole direction(s), and desired equatorial and polar temperatures.
/// </summary>
public sealed class TemperatureMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="TemperatureMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public TemperatureMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="TemperatureMap" /> class using an existing flat materials
	///     array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="temperatures">The flattened temperature array of length <c>widthTiles * heightTiles</c>.</param>
	public TemperatureMap(int widthTiles, int heightTiles, ReadOnlySpan<float> temperatures)
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
