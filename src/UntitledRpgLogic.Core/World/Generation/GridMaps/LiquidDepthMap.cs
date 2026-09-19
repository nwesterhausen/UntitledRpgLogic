namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Represents the depth of a body of water at the specific tiles.
/// </summary>
public sealed class LiquidDepthMap : NoiseGrid2D<ushort>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="LiquidDepthMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public LiquidDepthMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="LiquidDepthMap" /> class using an existing flat depth array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="depths">The flattened liquid depth array of length <c>widthTiles * heightTiles</c>.</param>
	public LiquidDepthMap(int widthTiles, int heightTiles, ushort[] depths)
		: base(widthTiles, heightTiles, depths)
	{
	}


	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The depth of the liquid.</returns>
	public ushort GetLiquidDepth(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the liquid depth at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="depth">The liquid depth to assign.</param>
	public void SetLiquidDepth(int tileX, int tileY, ushort depth) => this.SetValue(tileX, tileY, depth);
}
