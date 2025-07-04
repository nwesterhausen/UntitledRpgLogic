using System.Drawing;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Game;

/// <summary>
///     Provides a few predefined materials that can be used in the game.
/// </summary>
public static class PredefinedMaterials
{
    /// <summary>
    ///     Copper is a common metal used in various applications, known for its electrical conductivity and malleability.
    /// </summary>
    private static readonly IMaterial Copper = new BaseMaterial(new MaterialDataConfig
    {
        Name = "copper",
        TemperatureAtLiquidStateChange = 1084.62f,
        DensityAtLiquidStateChange = 8.02f,
        SolidColor = Color.SaddleBrown,
        TemperatureAtSolidStateChange = 0,
        DensityAtSolidStateChange = 8.935f,
        TemperatureAtGasStateChange = 2562,
        DensityAtGasStateChange = 0,
        MolarMass = 63.546f, // g/mol
        SolidCoefficientOfExpansion = 0.0000167f,
        LiquidCoefficientOfExpansion = 0,
        ExplicitId = ReservedGuids.MaterialCopper
    });
}
