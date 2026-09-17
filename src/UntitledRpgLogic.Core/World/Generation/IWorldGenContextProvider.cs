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
	/// <param name="worldConfig">The configuration for generating the world.</param>
	public WorldGenContext GetOrCreateContext(MapDefinition map, WorldMapConfiguration worldConfig);
}
