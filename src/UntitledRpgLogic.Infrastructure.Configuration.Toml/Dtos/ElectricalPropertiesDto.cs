using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class ElectricalPropertiesDto
{
	/// <summary>
	///     A relative measure of how well the material conducts electrical current.
	///     0 indicates an absolute insulator; values above 1.0 represent high-efficiency conductors.
	/// </summary>
	[JsonPropertyName("conductivity")]
	public float Conductivity { get; init; } = 0.25f;

	public ElectricalProperties ToModel() => new(this.Conductivity);

	public static ElectricalPropertiesDto FromModel(ElectricalProperties model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new ElectricalPropertiesDto { Conductivity = model.Conductivity };
	}
}
