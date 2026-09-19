namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     A map describing the Savagery between peaceful and wild for tiles in a map grid. Savagery is then used to
///     influence generated flora, fauna, dungeons, or whatever else is created in the world.
/// </summary>
public sealed class SavageryMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="SavageryMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the SavageryMap in tiles.</param>
	/// <param name="heightTiles">The total height of the SavageryMap in tiles.</param>
	public SavageryMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="SavageryMap" /> class using an existing flat elevations array.
	/// </summary>
	/// <param name="widthTiles">The total width of the SavageryMap in tiles.</param>
	/// <param name="heightTiles">The total height of the SavageryMap in tiles.</param>
	/// <param name="savageryList">The flattened Savagerys array of length <c>widthTiles * heightTiles</c>.</param>
	public SavageryMap(int widthTiles, int heightTiles, float[] savageryList)
		: base(widthTiles, heightTiles, savageryList)
	{
	}

	/// <summary>
	///     Retrieves the Savagery at the specified tile coordinate. Clamps coordinates to valid boundaries.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The Savagery value at the given coordinate.</returns>
	public float GetSavagery(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the Savagery at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="savagery">The Savagery to assign.</param>
	public void SetSavagery(int tileX, int tileY, float savagery) => this.SetValue(tileX, tileY, savagery);
}
