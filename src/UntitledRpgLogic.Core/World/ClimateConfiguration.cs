namespace UntitledRpgLogic.Core.World;

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
	public bool SouthPole { get; init; } = true;

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
	///     Default material to use for freshwater liquid.
	/// </summary>
	public Ulid FreshwaterLiquidMaterialId { get; init; } = Ulid.Empty;

	/// <summary>
	///     Default material to use for rain liquid, will default to be the same as <see cref="FreshwaterLiquidMaterialId" />
	/// </summary>
	public Ulid RainLiquidMaterialId { get; init; } = Ulid.Empty;

	/// <summary>
	///     Default material to use for saltwater liquid.
	/// </summary>
	public Ulid SaltwaterLiquidMaterialId { get; init; } = Ulid.Empty;

	/// <summary>
	///     List of biomes to assign during world generation. Listed by their IDs.
	/// </summary>
	public IReadOnlyCollection<Ulid> BiomeDefinitions { get; init; } = Array.Empty<Ulid>();
}
