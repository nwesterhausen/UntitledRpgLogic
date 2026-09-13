namespace UntitledRpgLogic.WorldGen.Generators;

/// <summary>
///     Configurable parameters for generating large-scale terrain elevation grids.
/// </summary>
public record HeightmapSettings
{
	public float Frequency { get; init; } = 0.005f;
	public int Octaves { get; init; } = 6;
	public float Persistence { get; init; } = 0.5f;
	public float Lacunarity { get; init; } = 2.0f;
	public short MinElevation { get; init; } = -8000;
	public short MaxElevation { get; init; } = 8000;
	public short SeaLevel { get; init; }
}
