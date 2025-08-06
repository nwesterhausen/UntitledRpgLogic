namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
/// Properties related to the thermal characteristics of a material.
/// </summary>
public class ThermalPropertiesConfig
{
	/// <summary>
	/// The temperature in Celsius at which the material melts.
	/// </summary>
	public float? MeltingPoint { get; set; }

	/// <summary>
	/// The temperature in Celsius at which the material boils.
	/// </summary>
	public float? BoilingPoint { get; set; }

	/// <summary>
	/// The lowest temperature in Celsius at which a combustible material can ignite in air.
	/// </summary>
	public float? IgnitionTemperature { get; set; }

	/// <summary>
	/// A relative measure of how well the material conducts heat.
	/// </summary>
	public float? ThermalConductivity { get; set; }
}
