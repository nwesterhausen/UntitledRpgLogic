namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Specifies generation boundaries, island falloff parameters, and vertical limits for an overworld map.
/// </summary>
public record WorldMapConfiguration
{
	/// <summary>
	///     Gets the total horizontal extent of the map in tiles. Defaults to 512.
	/// </summary>
	public int WidthTiles { get; init; } = 512;

	/// <summary>
	///     Gets the total vertical extent of the map in tiles. Defaults to 512.
	/// </summary>
	public int HeightTiles { get; init; } = 512;

	/// <summary>
	///     Gets a value indicating whether ocean falloff masks are applied to force borders below sea level.
	/// </summary>
	public bool SurroundWithOcean { get; init; } = true;

	/// <summary>
	///     Gets the fractional perimeter margin where elevation degrades into ocean.
	/// </summary>
	public float OceanBorderThickness { get; init; } = 0.20f;

	/// <summary>
	///     Gets the exponential steepness applied to the island falloff curve.
	/// </summary>
	public float IslandFalloffSteepness { get; init; } = 2.0f;

	/// <summary>
	///     Gets the vertical datum elevation defining sea level in meters.
	/// </summary>
	public short SeaLevel { get; init; }

	/// <summary>
	///     Gets the minimum allowable bedrock elevation in meters.
	/// </summary>
	public short MinElevation { get; init; } = -2000;

	/// <summary>
	///     Gets the maximum allowable bedrock elevation in meters.
	/// </summary>
	public short MaxElevation { get; init; } = 4000;
}
