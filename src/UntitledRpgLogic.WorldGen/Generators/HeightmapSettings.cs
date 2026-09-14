namespace UntitledRpgLogic.WorldGen.Generators;

/// <summary>
///     Configurable parameters for generating large-scale terrain elevation grids.
/// </summary>
public record HeightmapSettings
{
	public float Frequency { get; init; } = 0.000_8f;
	public int Octaves { get; init; } = 5;
	public float Persistence { get; init; } = 0.45f;
	public float Lacunarity { get; init; } = 2.0f;
	public short MinElevation { get; init; } = -200;
	public short MaxElevation { get; init; } = 2000;
	public short SeaLevel { get; init; }
}
