using UntitledRpgLogic.Core.Elements;

namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Stores the <see cref="ElementDefinition.Id" /> a tile's mana is attuned with.
/// </summary>
public sealed class ManaAttunementMap : NoiseGrid2D<Ulid?>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ManaAttunementMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public ManaAttunementMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ManaAttunementMap" /> class using array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="elements">The flattened element definition ID array of length <c>widthTiles * heightTiles</c>.</param>
	public ManaAttunementMap(int widthTiles, int heightTiles, Ulid?[] elements)
		: base(widthTiles, heightTiles, elements)
	{
	}


	/// <summary>
	///     Retrieves the mana attunement (by element defintion id) at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The <see cref="ElementDefinition.Id" /> of the mana attunement.</returns>
	public Ulid? GetManaAttunement(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the mana attunement at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="element">The mana attunement to assign.</param>
	public void SetManaAttunement(int tileX, int tileY, Ulid? element) => this.SetValue(tileX, tileY, element);
}
