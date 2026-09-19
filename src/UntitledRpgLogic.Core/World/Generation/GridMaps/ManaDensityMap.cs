namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Represents the ambient mana concentration. <c>0f</c> designates no mana.
/// </summary>
public sealed class ManaDensityMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ManaDensityMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the map grid in tiles.</param>
	/// <param name="heightTiles">The total height of the map grid in tiles.</param>
	public ManaDensityMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ManaDensityMap" /> class using an existing flat depth array.
	/// </summary>
	/// <param name="widthTiles">The total width of the map grid in tiles.</param>
	/// <param name="heightTiles">The total height of the map grid in tiles.</param>
	/// <param name="manaDensities">
	///     The flattened floatean array indicating the mana density of the tile.
	/// </param>
	public ManaDensityMap(int widthTiles, int heightTiles, float[] manaDensities)
		: base(widthTiles, heightTiles, manaDensities)
	{
	}


	/// <summary>
	///     Retrieves the mana density at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The mana density at the specified tile.</returns>
	public float GetManaDensity(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the mana density at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="manaDensity">Whether the tile is a river.</param>
	public void SetManaDensity(int tileX, int tileY, float manaDensity) => this.SetValue(tileX, tileY, manaDensity);
}
