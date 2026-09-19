namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Represents the depth of the soil at the specific tiles.
/// </summary>
public sealed class SoilDepthMap : NoiseGrid2D<ushort>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="SoilDepthMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public SoilDepthMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="SoilDepthMap" /> class using an existing flat depth array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="depths">The flattened soil depth array of length <c>widthTiles * heightTiles</c>.</param>
	public SoilDepthMap(int widthTiles, int heightTiles, ushort[] depths)
		: base(widthTiles, heightTiles, depths)
	{
	}

	/// <summary>
	///     Retrieves the depth of soil at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The depth of the soil.</returns>
	public ushort GetSoilDepth(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the soil depth at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="depth">The soil depth to assign.</param>
	public void SetSoilDepth(int tileX, int tileY, ushort depth) => this.SetValue(tileX, tileY, depth);
}
