namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     Represents the areas of high and low magical energey for a world over a 2D map coordinate space. Is a web of
///     high energy conduits throughout the world.
/// </summary>
public sealed class LeylineMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="LeylineMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the LeylineMap in tiles.</param>
	/// <param name="heightTiles">The total height of the LeylineMap in tiles.</param>
	public LeylineMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="LeylineMap" /> class using an existing magical energy level array.
	/// </summary>
	/// <param name="widthTiles">The total width of the LeylineMap in tiles.</param>
	/// <param name="heightTiles">The total height of the LeylineMap in tiles.</param>
	/// <param name="energyLevels">The flattened magical energy levels array of length <c>widthTiles * heightTiles</c>.</param>
	public LeylineMap(int widthTiles, int heightTiles, ReadOnlySpan<float> energyLevels)
		: base(widthTiles, heightTiles, energyLevels)
	{
	}

	/// <summary>
	///     Retrieves the magical energy level at the specified tile coordinate. Clamps coordinates to valid boundaries.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The magical energy level value at the given coordinate.</returns>
	public float GetLeylineEnergy(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the magical energy level at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="energyLevel">The magical energy level to assign.</param>
	public void SetLeylineEnergy(int tileX, int tileY, float energyLevel) => this.SetValue(tileX, tileY, energyLevel);
}
