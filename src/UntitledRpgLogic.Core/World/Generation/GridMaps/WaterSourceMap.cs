namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Marks the tile in the map grid as a <see cref="WaterSource" />.
/// </summary>
public sealed class WaterSourceMap : NoiseGrid2D<WaterSource>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="WaterSourceMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public WaterSourceMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="WaterSourceMap" /> class using an existing array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="waterBody">The flattened body type array of length <c>widthTiles * heightTiles</c>.</param>
	public WaterSourceMap(int widthTiles, int heightTiles, WaterSource[] waterBody)
		: base(widthTiles, heightTiles, waterBody)
	{
	}

	/// <summary>
	///     Retrieves the water source type at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The water source type at the tile, or null if out of bounds.</returns>
	public WaterSource GetWaterSource(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the water source type at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="waterSource">The water source type to assign.</param>
	public void SetWaterSource(int tileX, int tileY, WaterSource waterSource) =>
		this.SetValue(tileX, tileY, waterSource);
}
