using UntitledRpgLogic.Core.World.Generation.MapGrids;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

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
		TerrainHeightmap heightmap,
		TerrainHydrology hydrology,
		TerrainClimate climate,
		BiomeMaterialMapping materialMapping,
		uint seed)
	{
		this.Heightmap = heightmap ?? throw new ArgumentNullException(nameof(heightmap));
		this.Hydrology = hydrology ?? throw new ArgumentNullException(nameof(hydrology));
		this.Climate = climate ?? throw new ArgumentNullException(nameof(climate));
		this.MaterialMapping = materialMapping ?? throw new ArgumentNullException(nameof(materialMapping));
		this.Seed = seed;
	}

	public TerrainHeightmap Heightmap { get; }
	public TerrainHydrology Hydrology { get; }
	public TerrainClimate Climate { get; }
	public BiomeMaterialMapping MaterialMapping { get; }
	public uint Seed { get; }
}
