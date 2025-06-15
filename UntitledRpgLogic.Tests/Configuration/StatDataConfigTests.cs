using UntitledRpgLogic.Configuration;
using UntitledRpgLogic.Game;

namespace UntitledRpgLogic.Tests.Configuration;

[TestClass]
public class StatDataConfigTests
{
    [TestMethod]
    public void LoadStatDataConfig_ValidToml_LoadsCorrectly()
    {
        string tempFilePath = Path.GetTempFileName();
        Guid explicitId = Guid.NewGuid();

        string tomlContent = $@"
ExplicitId = ""{explicitId}""
Name = ""Strength""
Description = ""Measures physical power.""
MaxValue = 100
MinValue = 10
";

        File.WriteAllText(tempFilePath, tomlContent);

        StatDataConfig? config = null;
        BaseStat? stat = null;
        try
        {
            config = Utility.LoadConfig<StatDataConfig>(tempFilePath);

            Assert.IsNotNull(config);
            Assert.AreEqual(explicitId, config.ExplicitId);
            Assert.AreEqual("Strength", config.Name);
            Assert.AreEqual("Measures physical power.", config.Description);
            Assert.AreEqual(100, config.MaxValue);
            Assert.AreEqual(10, config.MinValue);

            // Try to create a stat from the config
            stat = new BaseStat(config);
            Assert.IsNotNull(stat);
            Assert.AreEqual(config.Name, stat.Name);
            Assert.AreEqual(config.MaxValue, stat.MaxValue);
            Assert.AreEqual(config.MinValue, stat.MinValue);
        }
        finally
        {
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }

    [TestMethod]
    public void LoadStatDataConfig_MinimalToml_LoadsCorrectlyWithDefaults()
    {
        string tempFilePath = Path.GetTempFileName();

        string tomlContent = @"
Name = ""Agility""
";

        File.WriteAllText(tempFilePath, tomlContent);

        StatDataConfig? config = null;
        BaseStat? stat = null;
        try
        {
            config = Utility.LoadConfig<StatDataConfig>(tempFilePath);

            Assert.IsNotNull(config);
            Assert.IsNull(config.ExplicitId);
            Assert.AreEqual("Agility", config.Name);
            Assert.IsNull(config.Description);
            Assert.IsNull(config.MaxValue); // Defaults to null in config, BaseStat will use DefaultValues
            Assert.IsNull(config.MinValue); // Defaults to null in config, BaseStat will use DefaultValues

            // Try to create a stat from the config
            stat = new BaseStat(config);
            Assert.IsNotNull(stat);
            Assert.AreEqual(config.Name, stat.Name);
            // BaseStat applies defaults if config values are null
            Assert.AreEqual(DefaultValues.STAT_DEFAULT_MAX_VALUE, stat.MaxValue);
            Assert.AreEqual(DefaultValues.STAT_DEFAULT_MIN_VALUE, stat.MinValue);
        }
        finally
        {
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }
}
