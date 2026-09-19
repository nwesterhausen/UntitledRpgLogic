namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Marks the tile in the map grid as a <see cref="WaterBodyType" />.
/// </summary>
public sealed class BasinMap : NoiseGrid2D<WaterBodyType>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="BasinMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public BasinMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="BasinMap" /> class using an existing flat materials array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="waterBody">The flattened body type array of length <c>widthTiles * heightTiles</c>.</param>
	public BasinMap(int widthTiles, int heightTiles, WaterBodyType[] waterBody)
		: base(widthTiles, heightTiles, waterBody)
	{
	}

	/// <summary>
	///     Retrieves the body of water at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The body of water at the tile, or null if out of bounds.</returns>
	public WaterBodyType GetWaterBody(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the body of water at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="waterBody">The body of water to assign.</param>
	public void SetWaterBody(int tileX, int tileY, WaterBodyType waterBody) => this.SetValue(tileX, tileY, waterBody);
}
