namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Configurable parameters for temperature gradients, environmental lapse rates, and moisture distribution.
/// </summary>
public record ClimateConfiguration
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
	///     Whether to create a pole on the north end of the map.
	/// </summary>
	public bool NorthPole { get; init; } = true;

	/// <summary>
	///     Whether to create a pole on the south end of the map.
	/// </summary>
	public bool SouthPol { get; init; } = true;

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
	///     Material to use for rain during world generation.
	/// </summary>
	public Ulid RainLiquidMaterialId { get; init; } = Ulid.Empty;
}
