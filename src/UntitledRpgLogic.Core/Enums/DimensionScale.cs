using System.ComponentModel;

namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Available Dimensions to use for shapes
/// </summary>
public enum DimensionScale
{
	/// <summary>
	///     An unspecified dimension scale (None).
	/// </summary>
	None = 0,

	/// <summary>
	///     millimeter
	/// </summary>
	[Description("Millimeter")] Mm = 5,

	/// <summary>
	///     centimeter
	/// </summary>
	[Description("Centimeter")] Cm = 6,

	/// <summary>
	///     meter
	/// </summary>
	[Description("Meter")] M = 7,

	/// <summary>
	///     kilometer
	/// </summary>
	[Description("Kilometer")] Km = 8
}
