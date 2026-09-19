namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Specifies generation boundaries, island falloff parameters, and vertical limits for an overworld map.
/// </summary>
public record WorldMapConfiguration
{
	/// <summary>
	///     The seed used for random generation.
	/// </summary>
	public long Seed { get; init; } = 0x12345678u;

	/// <summary>
	///     Gets the total horizontal extent of the map in tiles. Defaults to 512.
	/// </summary>
	public int WidthTiles { get; init; } = 512;

	/// <summary>
	///     Gets the total vertical extent of the map in tiles. Defaults to 512.
	/// </summary>
	public int HeightTiles { get; init; } = 512;

	/// <summary>
	///     Specific elevation noise frequencies, octaves, and vertical limits.
	/// </summary>
	public TerrainConfiguration Terrain { get; init; } = new();

	/// <summary>
	///     Precipitation cycles, river carving flux, and sea fill rules.
	/// </summary>
	public HydrologyConfiguration Hydrology { get; init; } = new();

	/// <summary>
	///     Latitudinal temperature curves, lapse rates, and moisture distribution.
	/// </summary>
	public ClimateConfiguration Climate { get; init; } = new();

	/// <summary>
	///     Alignment, savagery, and magical options.
	/// </summary>
	public ArcanaConfiguration Arcana { get; init; } = new();
}
