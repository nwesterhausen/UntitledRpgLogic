using System.Drawing;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Represents a material with its properties and states of matter. Can be used to determine a weight or roughly how
///     a material might behave in different conditions.
/// </summary>
public interface IMaterial : IHasName, IHasGuid
{
	/// <summary>
	///     The current state of matter of the material.
	/// </summary>
	public StateOfMatter State { get; }

	/// <summary>
	///     The molar mass of the material in grams per mole (g/mol).
	/// </summary>
	public double MolarMass { get; }

	/// <summary>
	///     Coefficient of Expansion (α) in 1/°C for the solid state.
	/// </summary>
	public double SolidCoefficientOfExpansion { get; }

	/// <summary>
	///     Coefficient of Expansion (β) in 1/°C for the liquid state.
	/// </summary>
	public double LiquidCoefficientOfExpansion { get; }

	/// <summary>
	/// 	The material's colors at each state of matter.
	/// </summary>
	public Dictionary<StateOfMatter, Color> Colors { get; }

	/// <summary>
	/// The melting point of the material in Celcius.
	/// </summary>
	public float MeltingPointCelcius { get; }
	/// <summary>
	/// The boiling point of the material in Celcius
	/// </summary>
	public float BoilingPointCelcius { get; }
	/// <summary>
	/// The sublimiation point (if exists) of the material in Celcius
	/// </summary>
	public float? SublimationPointCelcius { get; }
	/// <summary>
	/// Whether this material sublimates.
	/// </summary>
	public bool CanSublimate => this.SublimationPointCelcius is null;

	/// <summary>
	/// The density of the material in its solid state in g/cm³.
	/// </summary>
	public float? SolidDensityGcm3 { get; }

	/// <summary>
	/// The density of the material in its liquid state, typically at its melting point, in g/cm³.
	/// </summary>
	public float? LiquidDensityGcm3 { get; }

	/// <summary>
	/// The density of the material in its gaseous state, typically at its boiling point, in g/cm³.
	/// </summary>
	public float? GasDensityGcm3 { get; }

	/// <summary>
	/// The ultimate tensile strength in Megapascals (MPa).
	/// Represents resistance to being pulled apart.
	/// </summary>
	public float? TensileStrengthMPa { get; }

	/// <summary>
	/// The ultimate compressive strength in Megapascals (MPa).
	/// Represents resistance to being crushed.
	/// </summary>
	public float? CompressiveStrengthMPa { get; }

	/// <summary>
	/// The material's hardness on the Mohs scale. A key factor for sharpness and wear resistance.
	/// </summary>
	public float? HardnessMohs { get; }

	/// <summary>
	/// A textual description of how the material breaks (e.g., "Brittle", "Conchoidal", "Hackly").
	/// This is a primary indicator of toughness vs. brittleness.
	/// </summary>
	public FractureType? FractureType { get; }

	/// <summary>
	/// Resistance to elastic deformation. Defines Stiffness (Rigidity vs. Flexibility)
	/// </summary>
	public float? YoungsModulusGPa { get; }

	/// <summary>
	/// Ability to absorb a sudden blow. Defines Durability vs. Shock/Impact
	/// </summary>
	public float? ImpactStrengthJm2 { get; }

	/// <summary>
	/// Resistance to failure from cyclic stress. Defines Durability vs. Repeated Use/Wear
	/// </summary>
	public float? FatigueLimitMPa { get; }

	/// <summary>
	/// How much a material expands/contracts with heat. Defines Durability vs. Temperature Change
	/// </summary>
	/// <remarks>
	/// This can be simply set from the <see cref="SolidCoefficientOfExpansion"/>, lower is better.
	/// </remarks>
	public float? ThermalExpansionAlpha { get; }

	/// <summary>
	///     The materials current temperature in degrees Celsius (°C).
	/// </summary>
	public float Temperature { get; }

	/// <summary>
	///     The current pressure exerted on the material in Kilopascals (kPa).
	/// </summary>
	public float Pressure { get; }
}
