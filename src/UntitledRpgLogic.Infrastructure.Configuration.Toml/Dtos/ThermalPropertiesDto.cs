using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class ThermalPropertiesDto
{
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

	[JsonPropertyName("thermal_conductivity")]
	public float ThermalConductivity { get; init; }

	public ThermalProperties ToModel() => new()
	{
		MeltingPoint = this.MeltingPoint,
		BoilingPoint = this.BoilingPoint,
		IgnitionTemperature = this.IgnitionTemperature,
		ThermalConductivity = this.ThermalConductivity
	};

	public static ThermalPropertiesDto FromModel(ThermalProperties model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new ThermalPropertiesDto
		{
			MeltingPoint = model.MeltingPoint,
			BoilingPoint = model.BoilingPoint,
			IgnitionTemperature = model.IgnitionTemperature,
			ThermalConductivity = model.ThermalConductivity
		};
	}
}
