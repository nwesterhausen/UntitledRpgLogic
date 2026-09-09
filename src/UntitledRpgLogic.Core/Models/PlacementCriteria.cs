namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines the terrain, elevation, and hydrological rules required to place a handcrafted structure,
///     settlement, or sub-map stamp onto a procedural map.
/// </summary>
public record PlacementCriteria
{
	/// <summary>
	///     The lowest acceptable bedrock elevation allowed across the stamp footprint.
	/// </summary>
	public short MinElevation { get; init; } = -32768;

	/// <summary>
	///     The highest acceptable bedrock elevation allowed across the stamp footprint.
	/// </summary>
	public short MaxElevation { get; init; } = 32767;

	/// <summary>
	///     The maximum permissible difference between the minimum and maximum elevations across the stamp footprint.
	///     Prevents structures from stamping across sheer cliffs or jagged peaks.
	/// </summary>
	public float MaxSlopeVariance { get; init; } = 10f;

	/// <summary>
	///     Indicates whether the stamp location must be situated near an active river channel.
	/// </summary>
	public bool RequiresRiverProximity { get; init; }

	/// <summary>
	///     The maximum Chebyshev tile distance to scan for an adjacent river cell when <see cref="RequiresRiverProximity" /> is true.
	/// </summary>
	public int MaxDistanceToRiverTiles { get; init; } = 4;
}
