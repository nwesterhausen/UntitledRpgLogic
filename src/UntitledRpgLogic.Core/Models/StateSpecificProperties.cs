using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Properties specific to a material's state of matter.
/// </summary>
public record StateSpecificProperties
{
	/// <summary>
	///     The color of the material in this state, represented as a hex string (e.g., "#FF0000").
	/// </summary>
	public string Color { get; init; } = "#FFFFFF";

	/// <summary>
	///		A unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; init; }

	/// <summary>
	/// 	The state of matter these properties apply to.
	/// </summary>
	public StateOfMatter State { get; init; } = StateOfMatter.None;


	/// <summary>
	/// 	Mechanical properties of the material in this state, if differing from the default
	/// </summary>
	public MechanicalProperties? MechanicalProperties { get; set; }
	/// <summary>
	/// 	Thermal properties of the material in this state, if differing from the default
	/// </summary>
	public ThermalProperties? ThermalProperties { get; set; }
	/// <summary>
	/// 	Electrical properties of the material in this state, if differing from the default
	/// </summary>
	public ElectricalProperties? ElectricalProperties { get; set; }
	/// <summary>
	/// 	Fantastical properties of the material in this state, if differing from the default
	/// </summary>
	public FantasticalProperties? FantasticalProperties { get; set; }
}
