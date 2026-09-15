namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Configurable parameters for generating large-scale terrain elevation grids.
/// </summary>
public record HeightmapSettings
{
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
	///     Gets the minimum allowable bedrock elevation in meters.
	/// </summary>
	public short MinElevation { get; init; } = -2000;

	/// <summary>
	///     Gets the maximum allowable bedrock elevation in meters.
	/// </summary>
	public short MaxElevation { get; init; } = 4000;

	public float Frequency { get; init; } = 0.000_8f;
	public int Octaves { get; init; } = 5;
	public float Persistence { get; init; } = 0.45f;
	public float Lacunarity { get; init; } = 2.0f;

	/// <summary>
	///     Gets the vertical datum elevation defining sea level in meters.
	/// </summary>
	public short SeaLevel { get; init; }
}
