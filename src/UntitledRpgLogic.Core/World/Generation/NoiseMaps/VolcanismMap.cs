namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     Represents the mantle forces beneath the crust. More volcanic activity can result in volcanoes, calderas, islands
///     and hotsprings.
/// </summary>
public sealed class VolcanismMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="VolcanismMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public VolcanismMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="VolcanismMap" /> class using an existing volcanism level array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="volcanism">The flattened volcanism levels array of length <c>widthTiles * heightTiles</c>.</param>
	public VolcanismMap(int widthTiles, int heightTiles, float[] volcanism)
		: base(widthTiles, heightTiles, volcanism)
	{
	}

	/// <summary>
	///     Retrieves the volcanism level at the specified tile coordinate. Clamps coordinates to valid boundaries.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The volcanism level value at the given coordinate.</returns>
	public float GetVolcanism(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the volcanism level at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="volcanism">The volcanism level to assign.</param>
	public void SetVolcanism(int tileX, int tileY, float volcanism) => this.SetValue(tileX, tileY, volcanism);
}
