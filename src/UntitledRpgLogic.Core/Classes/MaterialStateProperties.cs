using System.Drawing;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Properties of a material in different states of matter. Provides information about the color, temperature and
///     denisty of the material when it changes state.
/// </summary>
public class MaterialStateProperties
{
	/// <summary>
	///     A color to represent the material in this state. This can be used to visually represent the material in a game or
	///     application.
	/// </summary>
	public Color Color { get; set; }

	/// <summary>
	///     The temperature at which the material changes state, in degrees Celsius (°C).
	/// </summary>
	public float TemperatureAtStateChange { get; set; }

	/// <summary>
	///     The density of the material at the state change, in grams per cubic centimeter (g/cm³).
	/// </summary>
	public float DensityAtStateChange { get; set; }
}
