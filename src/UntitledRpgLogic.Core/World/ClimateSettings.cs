namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Configurable parameters for temperature gradients, environmental lapse rates, and moisture distribution.
/// </summary>
public record ClimateSettings
{
	public float EquatorTemperature { get; init; } = 32.0f; // °C at equator (middle latitude)
	public float PoleTemperature { get; init; } = -15.0f; // °C at poles (top/bottom latitude)
	public float LapseRatePer1000M { get; init; } = 6.5f; // Environmental cooling rate per 1,000 units elevation
	public float TemperatureNoiseFrequency { get; init; } = 0.008f;
	public float RainfallNoiseFrequency { get; init; } = 0.006f;
	public short MountainThreshold { get; init; } = 1800;
}
