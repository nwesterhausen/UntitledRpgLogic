namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     A normal distribution of noise in a 2D grid.
/// </summary>
public sealed class NoiseMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="NoiseMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public NoiseMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="NoiseMap" /> class using an existing flat materials array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="noises">The flattened noise array of length <c>widthTiles * heightTiles</c>.</param>
	public NoiseMap(int widthTiles, int heightTiles, ReadOnlySpan<float> noises)
		: base(widthTiles, heightTiles, noises)
	{
	}

	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The noise of the tile, or null if out of bounds.</returns>
	public float GetNoise(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the noise at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="noise">The noise to assign.</param>
	public void SetNoise(int tileX, int tileY, float noise) => this.SetValue(tileX, tileY, noise);
}
