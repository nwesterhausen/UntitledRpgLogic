using UntitledRpgLogic.Core.Configuration;

namespace UntitledRpgLogic.Core.Classes;
/// <summary>
/// Represents the electrical properties of a material, such as conductivity.
/// </summary>
public record ElectricalProperties
{
	/// <summary>
	/// Gets the electrical conductivity of the material.
	/// </summary>
	public float? Conductivity { get; }

	/// <summary>
	/// Gets a value indicating whether the material is conductive.
	/// </summary>
	public bool IsConductive => this.Conductivity > 0;

	/// <summary>
	/// Creates a <see cref="ElectricalProperties"/> instance from a configuration object.
	/// </summary>
	/// <param name="config">The configuration object containing electrical property values.</param>
	/// <returns>A new <see cref="ElectricalProperties"/> instance.</returns>
	public static ElectricalProperties FromConfig(ElectricalPropertiesConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		return new ElectricalProperties(config.Conductivity);
	}
	private ElectricalProperties(float? conductivity)
	{
		Conductivity = conductivity;
	}
}
