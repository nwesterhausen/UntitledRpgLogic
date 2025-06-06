using UntitledRpgLogic.Classes;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.Extensions;

/// <summary>
///     Extensions for material-related operations.
/// </summary>
public static class MaterialExtensions
{
    /// <summary>
    ///     The universal gas constant in J/(mol·K), used in the ideal gas law for calculating density in the gas state.
    /// </summary>
    private const double UniversalGasConstant = 8.314;

    /// <summary>
    ///     Calculate the density of a material based on its state, pressure, and temperature.
    /// </summary>
    /// <param name="material">the material to use</param>
    /// <param name="pressure">(optional) specify a pressure to use for the calculation</param>
    /// <param name="temperature">(optional) specify a temperature to use for the calculation</param>
    /// <returns>the Density of the material</returns>
    public static double CalculateDensity(this IMaterial material, int? pressure = null, int? temperature = null)
    {
        switch (material.State)
        {
            case StateOfMatter.Solid:
            {
                var p0 = material.StateProperties[StateOfMatter.Solid].DensityAtStateChange;
                var t0 = material.StateProperties[StateOfMatter.Solid].TemperatureAtStateChange;
                var t = temperature ?? material.Temperature;

                return p0 * (1 + material.SolidCoefficientOfExpansion * (t - t0));
            }
            case StateOfMatter.Liquid:
            {
                var p0 = material.StateProperties[StateOfMatter.Liquid].DensityAtStateChange;
                var t0 = material.StateProperties[StateOfMatter.Liquid].TemperatureAtStateChange;
                var t = temperature ?? material.Temperature;

                return p0 * (1 + material.LiquidCoefficientOfExpansion * (t - t0));
            }
            case StateOfMatter.Gas:
            {
                var t = (temperature ?? material.Temperature) + 273.15; // Convert Celsius to Kelvin
                var p = pressure ?? material.Pressure;

                // Ideal gas law: PV = nRT => Density (ρ) = m / V = (p * M) / (R * T)
                return p * material.MolarMass / (UniversalGasConstant * t);
            }
            default:
#if DEBUG
                throw new InvalidOperationException($"Cannot calculate density for unknown state: {State}");
#endif
                break;
        }

        return 0; // Default return value if no state matches
    }

    public static double CalculateWeight(this IMaterial material, double volume, int? pressure = null,
        int? temperature = null)
    {
        var density = material.Density;
        if (pressure != null || temperature != null)
            // update the density first
            density = material.CalculateDensity(pressure, temperature);

        // Weight = Density * Volume [g/cm³ * cm³ = g]
        return density * volume;
    }
}