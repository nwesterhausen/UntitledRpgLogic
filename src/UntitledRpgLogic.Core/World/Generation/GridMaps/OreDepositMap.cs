namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Corresponds to a specific <see cref="OreDepositDefinition" /> to define what ores may be found at this tile.
/// </summary>
public sealed class OreDepositMap : NoiseGrid2D<ushort>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="OreDepositMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public OreDepositMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="OreDepositMap" /> class using an existing flat depositId array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="depositIds">The flattened depositId array of length <c>widthTiles * heightTiles</c>.</param>
	public OreDepositMap(int widthTiles, int heightTiles, ushort[] depositIds)
		: base(widthTiles, heightTiles, depositIds)
	{
	}


	/// <summary>
	///     Retrieves the DepositId for the ore deposit defintion at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The Id of the ore deposit definition.</returns>
	public ushort GetGeologyOres(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the ore deposit Id at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="depositId">The liquid depositId to assign.</param>
	public void SetGeologyOres(int tileX, int tileY, ushort depositId) => this.SetValue(tileX, tileY, depositId);
}
