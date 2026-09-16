namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Configurable parameters for temperature gradients, environmental lapse rates, and moisture distribution.
/// </summary>
public record ClimateSettings
{
	/// <summary>
	///     Average temperature around the equator/middle latitude.
	/// </summary>
	public float EquatorTemperature { get; init; } = 32.0f;

	/// <summary>
	///     Average temperature at the poles (top/bottom latitude).
	/// </summary>
	public float PoleTemperature { get; init; } = -15.0f;

	/// <summary>
	///     Environmental cooling rate per 1000 units of elevation.
	/// </summary>
	public float LapseRatePer1000M { get; init; } = 6.5f;

	/// <summary>
	///     Abruptness of temperature change. Default value is <c>0.008f</c>
	/// </summary>
	public float TemperatureNoiseFrequency { get; init; } = 0.008f;

	/// <summary>
	///     Abruptness of rainfall areas changing. Default value is <c>0.006f</c>
	/// </summary>
	public float RainfallNoiseFrequency { get; init; } = 0.006f;

	/// <summary>
	///     Height above sea level where land is considered a mountain.
	/// </summary>
	public short MountainThreshold { get; init; } = 1800;
}
