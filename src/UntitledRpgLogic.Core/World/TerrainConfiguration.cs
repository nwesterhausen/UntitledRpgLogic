namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Configurable parameters for generating large-scale terrain elevation grids.
/// </summary>
public record TerrainConfiguration
{
	/// <summary>
	///     Gets the minimum allowable bedrock elevation in meters.
	/// </summary>
	public short MinElevation { get; init; } = -2000;

	/// <summary>
	///     Gets the maximum allowable bedrock elevation in meters.
	/// </summary>
	public short MaxElevation { get; init; } = 4000;

	/// <summary>
	///     Gets the vertical datum elevation defining sea level in meters.
	/// </summary>
	public short SeaLevel { get; init; }

	/// <summary>
	///     Height above sea level where land is considered a mountain.
	/// </summary>
	public short MountainThreshold { get; init; } = 1800;

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
	///     Optionally override any noise generation settings used for terrain generation.
	/// </summary>
	public NoiseSettings NoiseGeneration { get; init; } = new()
	{
		Frequency = 48f, Octaves = 5, Persistence = 0.45f, Lacunarity = 2.0f
	};

	/// <summary>
	///     List of available materials for world generation.
	/// </summary>
	public IReadOnlyList<MaterialOption> Minerals { get; init; } = [];

	/// <summary>
	///     List of available non-mineral rock/stone for world generation.
	/// </summary>
	public IReadOnlyList<MaterialOption> Stone { get; init; } = [];
}
