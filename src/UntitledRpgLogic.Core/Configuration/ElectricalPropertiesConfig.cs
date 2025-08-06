namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
/// Properties related to the electrical characteristics of a material.
/// </summary>
public class ElectricalPropertiesConfig
{
	/// <summary>
	/// A relative measure of how well the material conducts electricity.
	/// 0 indicates a perfect insulator.
	/// </summary>
	public float? Conductivity { get; set; }
}
