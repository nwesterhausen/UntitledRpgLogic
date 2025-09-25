namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Properties related to the thermal characteristics of a material.
/// </summary>
public record ThermalPropertiesConfig
{
	/// <summary>
	///     The temperature in Celsius at which the material melts.
	/// </summary>
	public float MeltingPoint { get; init; } = 150f;

	/// <summary>
	///     The temperature in Celsius at which the material boils.
	/// </summary>
	public float BoilingPoint { get; init; } = 300f;

	/// <summary>
	///     The lowest temperature in Celsius at which a combustible material can ignite in air.
	/// </summary>
	/// <remarks>
	///     Defaults to 0 degrees, indicating non-combustibility.
	/// </remarks>
	public float IgnitionTemperature { get; init; }

	/// <summary>
	///     A relative measure of how well the material conducts heat.
	/// </summary>
	/// <remarks>
	///     Defaults to 0.0, indicating no thermal conductivity.
	/// </remarks>
	public float ThermalConductivity { get; init; }
}
