using System.Drawing;
using UntitledRpgLogic.Classes;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Configuration;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.Game;

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
        NameBehavior = new NameBehavior(config.Name, config.PluralName, config.NameAsAdjective);
        GuidBehavior = new GuidBehavior(config.ExplicitId);
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

    /// <summary>
    ///     Behavior to handle the name of the material, including its plural form and adjective form.
    /// </summary>
    private NameBehavior NameBehavior { get; }

    /// <summary>
    ///     Behavior to handle the GUID of the material, including its ID and short GUID.
    /// </summary>
    private GuidBehavior GuidBehavior { get; }

    /// <inheritdoc />
    public string Name => NameBehavior.Name;

    /// <inheritdoc />
    public string PluralName => NameBehavior.PluralName;

    /// <inheritdoc />
    public string NameAsAdjective => NameBehavior.NameAsAdjective;

    /// <inheritdoc />
    public Guid Guid => GuidBehavior.Guid;

    /// <inheritdoc />
    public string Id => GuidBehavior.Id;

    /// <inheritdoc />
    public string ShortGuid => GuidBehavior.ShortGuid;

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