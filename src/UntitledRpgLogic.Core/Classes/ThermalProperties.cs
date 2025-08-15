using UntitledRpgLogic.Core.Configuration;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents the thermal properties of a material, such as melting point, boiling point, and thermal conductivity.
/// </summary>
public record ThermalProperties
{
	/// <summary>
	///     An empty instance of <see cref="ThermalProperties" />.
	/// </summary>
	public static readonly ThermalProperties Empty = new(null, null, null, null);

	private ThermalProperties(float? meltingPoint, float? boilingPoint, float? ignitionTemperature, float? thermalConductivity)
	{
		this.MeltingPoint = meltingPoint;
		this.BoilingPoint = boilingPoint;
		this.IgnitionTemperature = ignitionTemperature;
		this.ThermalConductivity = thermalConductivity;
	}

	/// <summary>
	///     Gets the melting point of the material (°C).
	/// </summary>
	public float? MeltingPoint { get; }

	/// <summary>
	///     Gets the boiling point of the material (°C).
	/// </summary>
	public float? BoilingPoint { get; }

	/// <summary>
	///     Gets the ignition temperature of the material (°C).
	/// </summary>
	public float? IgnitionTemperature { get; }

	/// <summary>
	///     Gets the thermal conductivity of the material.
	/// </summary>
	public float? ThermalConductivity { get; }

	/// <summary>
	///     Creates a <see cref="ThermalProperties" /> instance from a configuration object.
	/// </summary>
	/// <param name="config">The configuration object containing thermal property values.</param>
	/// <returns>A new <see cref="ThermalProperties" /> instance.</returns>
	public static ThermalProperties FromConfig(ThermalPropertiesConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		return new ThermalProperties(config.MeltingPoint, config.BoilingPoint, config.IgnitionTemperature, config.ThermalConductivity);
	}
}
