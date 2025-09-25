using System.ComponentModel.DataAnnotations;

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
}
