namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Represents the derived ecological biome classification grid across a 2D map space.
/// </summary>
public sealed class TerrainBiomeMap : TerrainGrid2D<BiomeType>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainBiomeMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public TerrainBiomeMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainBiomeMap" /> class using an existing flat materials array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="biomes">The flattened biome array of length <c>widthTiles * heightTiles</c>.</param>
	public TerrainBiomeMap(int widthTiles, int heightTiles, BiomeType[] biomes)
		: base(widthTiles, heightTiles, biomes)
	{
	}

	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The biome of the tile, or null if out of bounds.</returns>
	public BiomeType GetBiome(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the biome at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="biome">The biome to assign.</param>
	public void SetBiome(int tileX, int tileY, BiomeType biome) => this.SetValue(tileX, tileY, biome);
}
