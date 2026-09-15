namespace UntitledRpgLogic.Core.World.Generation.MapGrids;

/// <summary>
///     Represents the procedural or sampled bedrock elevation grid across a 2D map coordinate space.
/// </summary>
public sealed class TerrainLiquidMaterialMap : NoiseGrid2D<Ulid?>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainLiquidMaterialMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	public TerrainLiquidMaterialMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainLiquidMaterialMap" /> class using an existing flat materials
	///     array.
	/// </summary>
	/// <param name="widthTiles">The total width of the heightmap in tiles.</param>
	/// <param name="heightTiles">The total height of the heightmap in tiles.</param>
	/// <param name="materials">The flattened material ID array of length <c>widthTiles * heightTiles</c>.</param>
	public TerrainLiquidMaterialMap(int widthTiles, int heightTiles, Ulid?[] materials)
		: base(widthTiles, heightTiles, materials)
	{
	}


	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The <see cref="Materials.MaterialDefinition.Id" /> of the liquid, or null if dry or out of bounds.</returns>
	public Ulid? GetLiquidMaterial(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the liquid material at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="material">The liquid material to assign.</param>
	public void SetLiquidMaterial(int tileX, int tileY, Ulid? material) => this.SetValue(tileX, tileY, material);
}
