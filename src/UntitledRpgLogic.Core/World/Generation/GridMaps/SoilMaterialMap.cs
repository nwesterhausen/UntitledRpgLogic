using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Stores the <see cref="MaterialDefinition.Id" /> for the soil layer on a tile.
/// </summary>
public sealed class SoilMaterialMap : NoiseGrid2D<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="SoilMaterialMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public SoilMaterialMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="SoilMaterialMap" /> class using an existing flat materials
	///     array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="materials">The flattened material ID array of length <c>widthTiles * heightTiles</c>.</param>
	public SoilMaterialMap(int widthTiles, int heightTiles, Ulid[] materials)
		: base(widthTiles, heightTiles, materials)
	{
	}


	/// <summary>
	///     Retrieves the material identifier for the soil at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The <see cref="Materials.MaterialDefinition.Id" /> of the soil.</returns>
	public Ulid GetSoilMaterial(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the soil material at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="material">The soil material to assign.</param>
	public void SetSoilMaterial(int tileX, int tileY, Ulid material) => this.SetValue(tileX, tileY, material);
}
