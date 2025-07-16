using System.ComponentModel;

namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Scale for mass measurements in the RPG logic.
/// </summary>
public enum MassScale
{
	/// <summary>
	///     An unspecified dimension scale (None).
	/// </summary>
	None = 0,

	/// <summary>
	///     milligram
	/// </summary>
	[Description("Milligram")] Mg = 10,

	/// <summary>
	///     gram
	/// </summary>
	[Description("Gram")] G = 11,

	/// <summary>
	///     kilogram
	/// </summary>
	[Description("Kilogram")] Kg = 12
}
