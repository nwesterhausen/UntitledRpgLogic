using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Identifies the ecological biome classification derived from elevation, temperature, and precipitation.
/// </summary>
[SuppressMessage("Design", "CA1028:Enum Storage should be Int32",
	Justification = "Biome type is stored in map data and needs to be byte-sized.")]
public enum BiomeType : byte
{
	/// <summary>
	///     Submerged oceanic terrain below sea level.
	/// </summary>
	Ocean = 0,

	/// <summary>
	///     Sandy or rocky coastal fringes adjacent to bodies of water.
	/// </summary>
	Beach = 1,

	/// <summary>
	///     Arid, low-moisture terrain with minimal vegetation.
	/// </summary>
	Desert = 2,

	/// <summary>
	///     Dry shrubland or savanna with low precipitation and warm temperatures.
	/// </summary>
	Savanna = 3,

	/// <summary>
	///     Open temperate grassland and prairie.
	/// </summary>
	Grassland = 4,

	/// <summary>
	///     Temperate deciduous woodland.
	/// </summary>
	TemperateForest = 5,

	/// <summary>
	///     Dense, warm forest characterized by high humidity and continuous rainfall.
	/// </summary>
	TropicalRainforest = 6,

	/// <summary>
	///     Boreal coniferous forest with cold winters and moderate moisture.
	/// </summary>
	Taiga = 7,

	/// <summary>
	///     Cold, wind-swept plains with permafrost and stunted vegetation.
	/// </summary>
	Tundra = 8,

	/// <summary>
	///     Perennially frozen ice sheets, pack ice, or high-altitude snow caps.
	/// </summary>
	Glacial = 9,

	/// <summary>
	///     Rugged, high-elevation alpine terrain above the tree line.
	/// </summary>
	Mountain = 10,

	/// <summary>
	///     A river is able to appear anywhere above sea level.
	/// </summary>
	River = 11,
	Lake = 12
}
