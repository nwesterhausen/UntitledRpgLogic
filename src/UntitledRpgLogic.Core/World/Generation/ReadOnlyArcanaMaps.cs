using UntitledRpgLogic.Core.Elements;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Provides a read-only view over an <see cref="ArcanaMaps" /> instance.
/// </summary>
public readonly record struct ReadOnlyArcanaMaps(ArcanaMaps inner)
{
	private readonly ArcanaMaps inner = inner ?? throw new ArgumentNullException(nameof(inner));

	/// <summary>
	///     Width of the maps in tiles.
	/// </summary>
	public int WidthTiles => this.inner.WidthTiles;

	/// <summary>
	///     Height of the maps in tiles.
	/// </summary>
	public int HeightTiles => this.inner.HeightTiles;

	/// <summary>
	///     Get the alignment value at the specified tile.
	/// </summary>
	/// <param name="tileX">Tile x pos in the grid</param>
	/// <param name="tileY">Tile y pos in the grid</param>
	/// <returns>The alignment value</returns>
	public float GetAlignment(int tileX, int tileY) => this.inner.GetAlignment(tileX, tileY);


	/// <summary>
	///     Get the savagery value at the specified tile.
	/// </summary>
	/// <param name="tileX">Tile x pos in the grid</param>
	/// <param name="tileY">Tile y pos in the grid</param>
	/// <returns>The savagery value</returns>
	public float GetSavagery(int tileX, int tileY) => this.inner.GetSavagery(tileX, tileY);


	/// <summary>
	///     Get the leyline energy value at the specified tile.
	/// </summary>
	/// <param name="tileX">Tile x pos in the grid</param>
	/// <param name="tileY">Tile y pos in the grid</param>
	/// <returns>The leyline energy value</returns>
	public float GetLeylineEnergy(int tileX, int tileY) => this.inner.GetLeylineEnergy(tileX, tileY);


	/// <summary>
	///     Get the mana density value at the specified tile.
	/// </summary>
	/// <param name="tileX">Tile x pos in the grid</param>
	/// <param name="tileY">Tile y pos in the grid</param>
	/// <returns>The mana density value</returns>
	public float GetManaDensity(int tileX, int tileY) => this.inner.GetManaDensity(tileX, tileY);


	/// <summary>
	///     Get the id of the element attuned at the specified tile.
	/// </summary>
	/// <param name="tileX">Tile x pos in the grid</param>
	/// <param name="tileY">Tile y pos in the grid</param>
	/// <returns>The attuened element's <see cref="ElementDefinition.Id" /></returns>
	public Ulid? GetManaAttunement(int tileX, int tileY) => this.inner.GetManaAttunement(tileX, tileY);
}
