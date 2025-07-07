using System.Drawing;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Game;

/// <summary>
///     A base class for materials in the game. This class implements the <see cref="IMaterial" /> interface and provides
///     boilerplate for a lot of the common functionality that materials will need.
/// </summary>
public class BaseMaterial : IMaterial
{
    /// <summary>
    ///     Creates an instance of <see cref="BaseMaterial" /> using the provided configuration (which is loaded from TOML).
    /// </summary>
    /// <param name="config"></param>
    public BaseMaterial(MaterialDataConfig config)
    {
        Name = new Name(config.Name, config.PluralName, config.NameAsAdjective);
        Guid = config.ExplicitId ?? Guid.NewGuid();
        Id = Convert.ToBase64String(Guid.ToByteArray());
        ShortGuid = Guid.ToString("N")[..8].ToUpperInvariant();

        State = StateOfMatter.Solid; // Default to solid, will be updated as needed.
        Temperature = 20; // Default temperature in Celsius.
        Pressure = 101.325f; // Default pressure in kPa (standard atmospheric pressure).
        MolarMass = config.MolarMass;
        SolidCoefficientOfExpansion = config.SolidCoefficientOfExpansion;
        LiquidCoefficientOfExpansion = config.LiquidCoefficientOfExpansion;
        StateProperties = new Dictionary<StateOfMatter, MaterialStateProperties>
        {
            {
                StateOfMatter.Solid, new MaterialStateProperties
                {
                    Color = config.SolidColor,
                    TemperatureAtStateChange = config.TemperatureAtSolidStateChange,
                    DensityAtStateChange = config.DensityAtSolidStateChange
                }
            },
            {
                StateOfMatter.Liquid, new MaterialStateProperties
                {
                    Color = config.LiquidColor ?? Color.Red, // Default to red if not provided.
                    TemperatureAtStateChange = config.TemperatureAtLiquidStateChange,
                    DensityAtStateChange = config.DensityAtLiquidStateChange
                }
            },
            {
                StateOfMatter.Gas, new MaterialStateProperties
                {
                    Color = config.GasColor ?? Color.Gray, // Default to gray if not provided.
                    TemperatureAtStateChange = config.TemperatureAtGasStateChange,
                    DensityAtStateChange = config.DensityAtGasStateChange
                }
            }
        };
    }

    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }

    /// <inheritdoc />
    public Name Name { get; }

    /// <inheritdoc />
    public StateOfMatter State { get; }

    /// <inheritdoc />
    public Dictionary<StateOfMatter, MaterialStateProperties> StateProperties { get; }

    /// <inheritdoc />
    public double MolarMass { get; }

    /// <inheritdoc />
    public double SolidCoefficientOfExpansion { get; }

    /// <inheritdoc />
    public double LiquidCoefficientOfExpansion { get; }

    /// <inheritdoc />
    public float Temperature { get; }

    /// <inheritdoc />
    public float Pressure { get; }
}
