namespace UntitledRpgLogic.Core.World.Generation.MapGrids;

/// <summary>
///     Represents the procedural or sampled bedrock elevation grid across a 2D map coordinate space.
/// </summary>
public sealed class IsRiverGrid : NoiseGrid2D<bool>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="IsRiverGrid" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public IsRiverGrid(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="IsRiverGrid" /> class using an existing flat depth array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="riverStatuses">
	///     The flattened boolean array indicating if the tile is a river of length
	///     <c>widthTiles * heightTiles</c>.
	/// </param>
	public IsRiverGrid(int widthTiles, int heightTiles, bool[] riverStatuses)
		: base(widthTiles, heightTiles, riverStatuses)
	{
	}


	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The depth of the liquid.</returns>
	public bool GetIsRiver(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the liquid depth at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="isRiver">Whether the tile is a river.</param>
	public void SetIsRiver(int tileX, int tileY, bool isRiver) => this.SetValue(tileX, tileY, isRiver);
}
