namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Configurable parameters for water accumulation, river routing, and sea flooding.
/// </summary>
public record HydrologySettings
{
	public short SeaLevel { get; init; }
	public int RaindropCycles { get; init; } = 2500;
	public ushort BaseRiverDepth { get; init; } = 4;
	public int MaxDescentSteps { get; init; } = 600;

	/// <summary>
	///     Minimum accumulated water flux required before a channel qualifies as a major river vein.
	///     Lower values create more small streams; higher values keep only major arterial rivers.
	/// </summary>
	public int RiverFluxThreshold { get; init; } = 25;

	/// <summary>
	///     Depth in meters to erode bedrock along consolidated riverbeds to guide natural flow.
	/// </summary>
	public short RiverBedErosionMeters { get; init; } = 15;
}
