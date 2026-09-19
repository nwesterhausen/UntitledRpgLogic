namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Represents the absolute hydraulic head elevation in meters (relative to
///     <see cref="TerrainConfiguration.SeaLevel" />).
/// </summary>
/// <remarks>
///     If the value stored is greater than the ground level, a spring or swamp should form.
/// </remarks>
public sealed class AquiferMap : NoiseGrid2D<short>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="AquiferMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public AquiferMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="AquiferMap" /> class using an existing flat depth array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="depths">The flattened hydraulic head array of length <c>widthTiles * heightTiles</c>.</param>
	public AquiferMap(int widthTiles, int heightTiles, short[] depths)
		: base(widthTiles, heightTiles, depths)
	{
	}

	/// <summary>
	///     Retrieves the aquifer hydraulic head at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The hydraulic head elevation at the tile.</returns>
	public short GetAquifer(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the hydraulic head at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="depth">The hydraulic head to assign.</param>
	public void SetAquifer(int tileX, int tileY, short depth) => this.SetValue(tileX, tileY, depth);
}
