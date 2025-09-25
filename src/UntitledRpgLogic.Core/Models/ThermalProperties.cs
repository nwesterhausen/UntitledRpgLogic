using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Properties related to the thermal characteristics of a material.
/// </summary>
public record ThermalProperties
{
	/// <summary>
	///		A default constructor.
	/// </summary>
	public ThermalProperties()
	{}

	/// <summary>
	///     The temperature in Celsius at which the material melts.
	/// </summary>
	public float MeltingPoint { get; init; }

	/// <summary>
	///     The temperature in Celsius at which the material boils.
	/// </summary>
	public float BoilingPoint { get; init; }

	/// <summary>
	///     The lowest temperature in Celsius at which a combustible material can ignite in air.
	/// </summary>
	public float IgnitionTemperature { get; init; }

	/// <summary>
	///     A relative measure of how well the material conducts heat.
	/// </summary>
	public float ThermalConductivity { get; init; }

	/// <summary>
	///		A unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; init; }
}
