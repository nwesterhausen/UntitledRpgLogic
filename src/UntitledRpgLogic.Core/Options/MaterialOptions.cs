using UntitledRpgLogic.Core.Classes;

namespace UntitledRpgLogic.Core.Options;

/// <summary>
///     Options for configuring <see cref="Models.MaterialDefinition" />-derived objects.
/// </summary>
public class MaterialOptions
{
    /// <summary>
    ///     The name of the material.
    /// </summary>
    public required Name Name { get; init; }

    /// <summary>
    ///     The molar mass of the material in grams per mole (g/mol).
    /// </summary>
    public double MolarMass { get; init; } = 18.01528; // Default to water's molar mass

    /// <summary>
    ///     Coefficient of Expansion (α) in 1/°C for the solid state.
    /// </summary>
    public double SolidCoefficientOfExpansion { get; init; } = 0.00001; // Default to a typical value for metals

    /// <summary>
    ///     Coefficient of Expansion (β) in 1/°C for the liquid state.
    /// </summary>
    public double LiquidCoefficientOfExpansion { get; init; } = 0.0001; // Default to a typical value for liquids
}
