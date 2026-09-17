namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Configurable parameters for water accumulation, river routing, and sea flooding.
/// </summary>
public record HydrologyConfiguration
{
	/// <summary>
	///     How much rain to simulate when simulating rainfall and river generation.
	/// </summary>
	/// <remarks>Recommended range: <c>2500 - 5000</c></remarks>
	public int RaindropCycles { get; init; } = 2500;

	/// <summary>
	///     Starting depth for established river channels.
	/// </summary>
	/// <remarks>Recommended range: <c>2 - 4</c></remarks>
	public ushort BaseRiverDepth { get; init; } = 3;

	/// <summary>
	///     How far we allow water to flow when simulating the rainfall and river generation.
	/// </summary>
	/// <remarks>Recommended range: <c>300 - 600</c></remarks>
	public int MaxDescentSteps { get; init; } = 400;

	/// <summary>
	///     Minimum accumulated water flux required before a channel qualifies as a major river vein.
	///     Lower values create more small streams; higher values keep only major arterial rivers.
	/// </summary>
	/// <remarks>Recommended range: <c>20 - 40</c></remarks>
	public int RiverFluxThreshold { get; init; } = 25;

	/// <summary>
	///     Depth in meters to erode bedrock along consolidated riverbeds to guide natural flow.
	/// </summary>
	/// <remarks>Recommended range: <c>10 - 20</c></remarks>
	public short RiverBedErosionMeters { get; init; } = 15;

	/// <summary>
	///     Enables flood-filling endorheic inland terminal basins into lakes.
	/// </summary>
	public bool FormInlandLakes { get; init; } = true;

	/// <summary>
	///     Maximum tile area a single formed lake can occupy before halting expansion.
	/// </summary>
	public int MaxLakeTiles { get; init; } = 150;

	/// <summary>
	///     Maximum vertical water depth in meters to fill above a terminal basin floor.
	/// </summary>
	public short MaxLakeDepthMeters { get; init; } = 25;

	/// <summary>
	///     Material definition to use for the water in the ocean during world gen.
	/// </summary>
	public Ulid OceanLiquidMaterialId { get; init; } = Ulid.Empty;

	/// <summary>
	///     Material definition to use for aquifers and lakes during world gen.
	/// </summary>
	public Ulid FreshwaterLiquidMaterialId { get; init; } = Ulid.Empty;
}
