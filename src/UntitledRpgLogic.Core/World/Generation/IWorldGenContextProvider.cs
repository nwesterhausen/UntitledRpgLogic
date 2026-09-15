namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Provides or generates the macro world simulation context for a given map.
/// </summary>
public interface IWorldGenContextProvider
{
	/// <summary>
	///     Gets or generates the <see cref="WorldGenContext" /> for the specified map.
	/// </summary>
	/// <param name="map">The map definition entity.</param>
	/// <param name="seed">The generation seed.</param>
	/// <param name="widthTiles">The total horizontal map dimension in tiles (default: 512).</param>
	/// <param name="heightTiles">The total vertical map dimension in tiles (default: 512).</param>
	public WorldGenContext GetOrCreateContext(MapDefinition map, uint seed, int widthTiles = 512,
		int heightTiles = 512);
}
