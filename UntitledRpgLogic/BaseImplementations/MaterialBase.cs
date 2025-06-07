using UntitledRpgLogic.Classes;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Extensions;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <inheritdoc />
public abstract class MaterialBase : IMaterial
{
    /// <summary>
    ///     Hint for the state of matter based on temperature.
    ///     The key is the temperature at which the state changes, and the value is the corresponding state of matter.
    /// </summary>
    private readonly Dictionary<int, StateOfMatter> _temperatureStateHint;

    /// <summary>
    ///     Create a new material with its properties and states of matter using MaterialOptions.
    /// </summary>
    /// <param name="options">The options for configuring the material.</param>
    protected MaterialBase(MaterialOptions options)
    {
        PluralName = options.PluralName;
        StateProperties = new Dictionary<StateOfMatter, MaterialStateProperties>
        {
            { StateOfMatter.Solid, options.SolidStateProperties },
            { StateOfMatter.Liquid, options.LiquidStateProperties },
            { StateOfMatter.Gas, options.GasStateProperties }
        };
        MolarMass = options.MolarMass;
        SolidCoefficientOfExpansion = options.SolidCoefficientOfExpansion;
        LiquidCoefficientOfExpansion = options.LiquidCoefficientOfExpansion;

        Temperature = 21; // Default room temperature in Celsius, or set elsewhere
        Pressure = 101; // Default atmospheric pressure, or set elsewhere

        _temperatureStateHint = new Dictionary<int, StateOfMatter>
        {
            { options.SolidStateProperties.TemperatureAtStateChange, StateOfMatter.Solid },
            { options.LiquidStateProperties.TemperatureAtStateChange, StateOfMatter.Liquid },
            { options.GasStateProperties.TemperatureAtStateChange, StateOfMatter.Gas }
        };

        UpdateStateOfMatter();
        UpdateDensity();
    }

    /// <summary>
    ///     The density of the material in grams per cubic centimeter (g/cm³).
    /// </summary>
    public double Density { get; private set; }

    /// <inheritdoc />
    public PluralName PluralName { get; }

    /// <inheritdoc />
    public StateOfMatter State { get; private set; }

    /// <inheritdoc />
    public Dictionary<StateOfMatter, MaterialStateProperties> StateProperties { get; }

    /// <inheritdoc />
    public double MolarMass { get; }

    /// <inheritdoc />
    public double SolidCoefficientOfExpansion { get; }

    /// <inheritdoc />
    public double LiquidCoefficientOfExpansion { get; }

    /// <inheritdoc />
    public int Temperature { get; }

    /// <inheritdoc />
    public int Pressure { get; }

    /// <summary>
    ///     An estimation of the density of the material in its current state, based on its state of matter, temperature, and
    ///     pressure.
    /// </summary>
    /// <param name="pressure">Optional pressure override for the calculation.</param>
    /// <param name="temperature">Optional temperature override for the calculation.</param>
    private void UpdateDensity(int? pressure = null, int? temperature = null)
    {
        Density = this.CalculateDensity(pressure, temperature);
    }

    /// <summary>
    ///     Update the state of matter based on the current temperature.
    /// </summary>
    private void UpdateStateOfMatter()
    {
        // Find the state of matter based on the current temperature
        foreach (var kvp in _temperatureStateHint.OrderBy(kvp => kvp.Key))
        {
            if (Temperature >= kvp.Key) continue;

            State = kvp.Value;
            return;
        }

        // If no state found, default to gas
        State = StateOfMatter.Gas;
    }
}