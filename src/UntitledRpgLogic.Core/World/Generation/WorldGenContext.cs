using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Encapsulates the precomputed macro simulation layers for chunk sampling.
/// </summary>
public class WorldGenContext
{
	/// <summary>
	///     Encapsulates the precomputed macro simulation layers for chunk sampling.
	/// </summary>
	public WorldGenContext(
		TerrainMaps terrain,
		HydrologyMaps hydrology,
		ClimateMaps climate,
		ArcanaMaps arcana,
		BiomeMaterialMapping materialMapping,
		WorldMapConfiguration worldMapConfiguration)
	{
		this.Terrain = terrain ?? throw new ArgumentNullException(nameof(terrain));
		this.Hydrology = hydrology ?? throw new ArgumentNullException(nameof(hydrology));
		this.Climate = climate ?? throw new ArgumentNullException(nameof(climate));
		this.Arcana = arcana ?? throw new ArgumentNullException(nameof(arcana));
		this.MaterialMapping = materialMapping ?? throw new ArgumentNullException(nameof(materialMapping));
		this.MapConfig = worldMapConfiguration ?? throw new ArgumentNullException(nameof(worldMapConfiguration));
	}

	/// <summary>
	///     Create a new, empty world generation context for a world map of size <paramref name="mapConfig.WidthTiles" /> by
	///     <paramref name="mapConfig.HeightTiles" />.
	/// </summary>
	/// <param name="mapConfig">The world map generation configuration to use</param>
	public WorldGenContext(WorldMapConfiguration mapConfig)
	{
		ArgumentNullException.ThrowIfNull(mapConfig);

		this.MapConfig = mapConfig;
		this.Terrain = new TerrainMaps(mapConfig.WidthTiles, mapConfig.HeightTiles);
		this.Hydrology = new HydrologyMaps(mapConfig.WidthTiles, mapConfig.HeightTiles);
		this.Climate = new ClimateMaps(mapConfig.WidthTiles, mapConfig.HeightTiles);
		this.Arcana = new ArcanaMaps(mapConfig.WidthTiles, mapConfig.HeightTiles);
	}

	/// <summary>
	///     The world/map generation configuration to use.
	/// </summary>
	public WorldMapConfiguration MapConfig { get; }

	/// <summary>
	///     Terrain related noisemaps and map grids.
	/// </summary>
	public TerrainMaps Terrain { get; }

	/// <summary>
	///     Hydrology related noisemaps and map grids.
	/// </summary>
	public HydrologyMaps Hydrology { get; }

	/// <summary>
	///     Climate realted noisemaps and map grids.
	/// </summary>
	public ClimateMaps Climate { get; }

	/// <summary>
	///     Arcana realted noisemaps and map grids.
	/// </summary>
	public ArcanaMaps Arcana { get; }

	/// <summary>
	///     A mapping of <see cref="MaterialDefinition" /> to <see cref="BiomeType" /> for the base ground material in each
	///     biome.
	/// </summary>
	[Obsolete("Holdover from initial generation engine. Should be replaced with catalog-aware material context.")]
	public BiomeMaterialMapping MaterialMapping { get; } = new();
}
