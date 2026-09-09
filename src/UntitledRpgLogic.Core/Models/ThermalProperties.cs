namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Value object describing thermodynamic characteristics and phase change thresholds for a material.
/// </summary>
/// <remarks>Owned by <see cref="MaterialDefinition" />.</remarks>
public record ThermalProperties
{
	/// <summary>
	///     Initializes a new default instance of the <see cref="ThermalProperties" /> record.
	/// </summary>
	public ThermalProperties()
	{
	}

	/// <summary>
	///     Temperature in Celsius at which the material shifts from solid to liquid.
	/// </summary>
	public float MeltingPoint { get; init; }

	/// <summary>
	///     Temperature in Celsius at which the material boils into vapor.
	/// </summary>
	public float BoilingPoint { get; init; }

	/// <summary>
	///     Flashpoint or spontaneous autoignition temperature in Celsius for combustible materials.
	/// </summary>
	public float IgnitionTemperature { get; init; }

	/// <summary>
	///     Relative rate of thermal conductivity (heat transfer efficiency).
	/// </summary>
	public float ThermalConductivity { get; init; }
}
