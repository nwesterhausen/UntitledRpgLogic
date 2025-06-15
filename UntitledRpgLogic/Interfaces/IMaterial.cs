using UntitledRpgLogic.Classes;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Extensions;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Represents a material with its properties and states of matter. Can be used to determine a weight or roughly how
///     a material might behave in different conditions.
/// </summary>
public interface IMaterial : IHasName, IHasGuid
{
    /// <summary>
    ///     The current state of matter of the material.
    /// </summary>
    StateOfMatter State { get; }

    /// <summary>
    ///     The properties of the material in different states of matter.
    /// </summary>
    Dictionary<StateOfMatter, MaterialStateProperties> StateProperties { get; }

    /// <summary>
    ///     The molar mass of the material in grams per mole (g/mol).
    /// </summary>
    double MolarMass { get; }

    /// <summary>
    ///     Coefficient of Expansion (α) in 1/°C for the solid state.
    /// </summary>
    double SolidCoefficientOfExpansion { get; }

    /// <summary>
    ///     Coefficient of Expansion (β) in 1/°C for the liquid state.
    /// </summary>
    double LiquidCoefficientOfExpansion { get; }

    /// <summary>
    ///     The materials current temperature in degrees Celsius (°C).
    /// </summary>
    float Temperature { get; }

    /// <summary>
    ///     The pressure exerted on the material in Kilopascals (kPa).
    /// </summary>
    float Pressure { get; }

    /// <summary>
    ///     The density of the material in grams per cubic centimeter (g/cm³).
    /// </summary>
    double Density => this.CalculateDensity();
}
