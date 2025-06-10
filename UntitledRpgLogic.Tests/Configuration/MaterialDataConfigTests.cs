using System.Drawing;
using System.Globalization;
using UntitledRpgLogic.Configuration;
using UntitledRpgLogic.Game;

namespace UntitledRpgLogic.Tests.Configuration;

[TestClass]
public class MaterialDataConfigTests
{
    [TestMethod]
    public void LoadMaterialDataConfig_ValidToml_LoadsCorrectly()
    {
        var tempFilePath = Path.GetTempFileName();
        var explicitId = Guid.NewGuid();

        // Ensure consistent float/double string representation
        var culture = CultureInfo.InvariantCulture;

        // Define colors as hex strings for TOML
        var liquidColorHex = "#FF0000"; // Red
        var solidColorHex = "#00FF00"; // Green
        var gasColorHex = "#0000FF"; // Blue

        var expectedLiquidColor = ColorTranslator.FromHtml(liquidColorHex);
        var expectedSolidColor = ColorTranslator.FromHtml(solidColorHex);
        var expectedGasColor = ColorTranslator.FromHtml(gasColorHex);

        var tomlContent = $@"
ExplicitId = ""{explicitId}""
Name = ""Testium""
PluralName = ""Testia""
NameAsAdjective = ""Testial""
LiquidColor = ""{liquidColorHex}""
TemperatureAtLiquidStateChange = {25.5f.ToString(culture)}
DensityAtLiquidStateChange = {1.2f.ToString(culture)}
SolidColor = ""{solidColorHex}""
TemperatureAtSolidStateChange = {0.0f.ToString(culture)}
DensityAtSolidStateChange = {2.5f.ToString(culture)}
GasColor = ""{gasColorHex}""
TemperatureAtGasStateChange = {100.0f.ToString(culture)}
DensityAtGasStateChange = {0.5f.ToString(culture)}
MolarMass = {58.44.ToString(culture)}
SolidCoefficientOfExpansion = {0.000012.ToString(culture)}
LiquidCoefficientOfExpansion = {0.000210.ToString(culture)}
";

        File.WriteAllText(tempFilePath, tomlContent);

        MaterialDataConfig? config = null;
        BaseMaterial? material = null;
        try
        {
            config = Utility.LoadConfig<MaterialDataConfig>(tempFilePath);

            Assert.IsNotNull(config);
            Assert.AreEqual(explicitId, config.ExplicitId);
            Assert.AreEqual("Testium", config.Name);
            Assert.AreEqual("Testia", config.PluralName);
            Assert.AreEqual("Testial", config.NameAsAdjective);

            Assert.IsNotNull(config.LiquidColor);
            Assert.AreEqual(expectedLiquidColor.ToArgb(), config.LiquidColor.Value.ToArgb());
            Assert.AreEqual(25.5f, config.TemperatureAtLiquidStateChange);
            Assert.AreEqual(1.2f, config.DensityAtLiquidStateChange);

            Assert.AreEqual(expectedSolidColor.ToArgb(), config.SolidColor.ToArgb());
            Assert.AreEqual(0.0f, config.TemperatureAtSolidStateChange);
            Assert.AreEqual(2.5f, config.DensityAtSolidStateChange);

            Assert.IsNotNull(config.GasColor);
            Assert.AreEqual(expectedGasColor.ToArgb(), config.GasColor.Value.ToArgb());
            Assert.AreEqual(100.0f, config.TemperatureAtGasStateChange);
            Assert.AreEqual(0.5f, config.DensityAtGasStateChange);

            Assert.AreEqual(58.44, config.MolarMass);
            Assert.AreEqual(0.000012, config.SolidCoefficientOfExpansion);
            Assert.AreEqual(0.000210, config.LiquidCoefficientOfExpansion);

            // Try to create a material from the config
            material = new BaseMaterial(config);
            Assert.IsNotNull(material);
        }
        finally
        {
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }

    [TestMethod]
    public void LoadMaterialDataConfig_MinimalToml_LoadsCorrectlyWithDefaults()
    {
        var tempFilePath = Path.GetTempFileName();

        var culture = CultureInfo.InvariantCulture;
        var solidColorHex = "#CCCCCC"; // Gray
        var expectedSolidColor = ColorTranslator.FromHtml(solidColorHex);

        var tomlContent = $@"
Name = ""Basic Rock""
TemperatureAtLiquidStateChange = {1200.0f.ToString(culture)}
DensityAtLiquidStateChange = {2.2f.ToString(culture)}
SolidColor = ""{solidColorHex}""
TemperatureAtSolidStateChange = {1100.0f.ToString(culture)}
DensityAtSolidStateChange = {2.7f.ToString(culture)}
TemperatureAtGasStateChange = {2500.0f.ToString(culture)}
DensityAtGasStateChange = {0.8f.ToString(culture)}
MolarMass = {60.08.ToString(culture)}
SolidCoefficientOfExpansion = {0.000005.ToString(culture)}
LiquidCoefficientOfExpansion = {0.00005.ToString(culture)}
";

        File.WriteAllText(tempFilePath, tomlContent);

        MaterialDataConfig? config = null;
        BaseMaterial? material = null;
        try
        {
            config = Utility.LoadConfig<MaterialDataConfig>(tempFilePath);

            Assert.IsNotNull(config);
            Assert.IsNull(config.ExplicitId);
            Assert.AreEqual("Basic Rock", config.Name);
            Assert.IsNull(config.PluralName);
            Assert.IsNull(config.NameAsAdjective);

            Assert.IsNull(config.LiquidColor); // Optional, so defaults to null
            Assert.AreEqual(1200.0f, config.TemperatureAtLiquidStateChange);
            Assert.AreEqual(2.2f, config.DensityAtLiquidStateChange);

            Assert.AreEqual(expectedSolidColor.ToArgb(), config.SolidColor.ToArgb());
            Assert.AreEqual(1100.0f, config.TemperatureAtSolidStateChange);
            Assert.AreEqual(2.7f, config.DensityAtSolidStateChange);

            Assert.IsNull(config.GasColor); // Optional, so defaults to null
            Assert.AreEqual(2500.0f, config.TemperatureAtGasStateChange);
            Assert.AreEqual(0.8f, config.DensityAtGasStateChange);

            Assert.AreEqual(60.08, config.MolarMass);
            Assert.AreEqual(0.000005, config.SolidCoefficientOfExpansion);
            Assert.AreEqual(0.00005, config.LiquidCoefficientOfExpansion);

            // Try to create a material from the config
            material = new BaseMaterial(config);
            Assert.IsNotNull(material);
        }
        finally
        {
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }
}