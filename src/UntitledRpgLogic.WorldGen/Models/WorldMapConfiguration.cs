namespace UntitledRpgLogic.WorldGen.Models;

/// <summary>
///     Specifies boundary size and topological constraints for bounded world map generation.
/// </summary>
public record WorldMapConfiguration
{
	/// <summary>
	///     Total width of the bounded world map in tiles (must be a multiple of 16).
	/// </summary>
	public int WidthTiles { get; init; } = 512;

	/// <summary>
	///     Total height of the bounded world map in tiles (must be a multiple of 16).
	/// </summary>
	public int HeightTiles { get; init; } = 512;

	/// <summary>
	///     Enforces an oceanic buffer around the perimeter so land forms an island or continent.
	/// </summary>
	public bool SurroundWithOcean { get; init; } = true;

	/// <summary>
	///     The normalized fraction (0.0 to 0.5) of the outer border dedicated to coastal ocean dropoff.
	/// </summary>
	public float OceanBorderThickness { get; init; } = 0.15f;

	/// <summary>
	///     Exponent shaping how steeply land drops off into the surrounding ocean (higher = flatter center, steeper coast).
	/// </summary>
	public float IslandFalloffSteepness { get; init; } = 2.5f;

	/// <summary>
	///     Global sea datum level.
	/// </summary>
	public short SeaLevel { get; init; }

	/// <summary>
	///     Minimum bedrock elevation allowed.
	/// </summary>
	public short MinElevation { get; init; } = -6000;

	/// <summary>
	///     Maximum bedrock elevation allowed.
	/// </summary>
	public short MaxElevation { get; init; } = 6000;
}
