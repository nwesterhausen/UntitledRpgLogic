using UntitledRpgLogic.Core.Materials;
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
		Heightmap heightmap,
		HydrologyMap hydrology,
		ClimateGrid climate,
		BiomeMaterialMapping materialMapping,
		uint seed)
	{
		this.Heightmap = heightmap ?? throw new ArgumentNullException(nameof(heightmap));
		this.Hydrology = hydrology ?? throw new ArgumentNullException(nameof(hydrology));
		this.Climate = climate ?? throw new ArgumentNullException(nameof(climate));
		this.MaterialMapping = materialMapping ?? throw new ArgumentNullException(nameof(materialMapping));
		this.Seed = seed;
	}

	/// <summary>
	/// 	The noisemap for terrain height.
	/// </summary>
	public Heightmap Heightmap { get; }

	/// <summary>
	/// 	The noisemap for terrain hydrology.
	/// </summary>
	public HydrologyMap Hydrology { get; }

	/// <summary>
	/// 	The determined climate at grid locations based on height and hydrology.
	/// </summary>
	public ClimateGrid Climate { get; }

	/// <summary>
	/// 	A mapping of <see cref="MaterialDefinition" /> to <see cref="BiomeType"/> for the base ground material in each biome.
	/// </summary>
	public BiomeMaterialMapping MaterialMapping { get; }

	/// <summary>
	/// 	The seed used to generate the noise maps.
	/// </summary>
	public uint Seed { get; }
}
