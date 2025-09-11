using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Properties related to the electrical characteristics of a material.
/// </summary>
public record ElectricalProperties
{
	/// <summary>
	///		Constructs a new default record.
	/// </summary>
	public ElectricalProperties()
	{
	}

	/// <summary>
	///		Constructs a new record with the specified conductivity.
	/// </summary>
	/// <param name="conductivity">measure of conductivity for the material</param>
	public ElectricalProperties(float conductivity) => this.Conductivity = conductivity;

	/// <summary>
	///     A relative measure of how well the material conducts electricity.
	///     0 indicates a perfect insulator.
	/// </summary>
	/// <remarks>
	///     The default value is 0.25, indicating some conductivity.
	/// </remarks>
	public float Conductivity { get; init; } = 0.25f;

	/// <summary>
	///		A unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; init; }
}
