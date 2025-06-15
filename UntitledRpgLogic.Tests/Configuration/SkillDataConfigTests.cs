using System.Globalization;
using UntitledRpgLogic.Configuration;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Game;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Tests.Configuration;

[TestClass]
public class SkillDataConfigTests
{
    [TestMethod]
    public void LoadSkillDataConfig_ValidToml_LoadsCorrectly()
    {
        string tempFilePath = Path.GetTempFileName();
        Guid explicitId = Guid.NewGuid();
        CultureInfo culture = CultureInfo.InvariantCulture;

        string tomlContent = $@"
ExplicitId = ""{explicitId}""
Name = ""Swordsmanship""
Description = ""The art of wielding swords.""

[LevelingOptions]
MaxLevel = 50
PointsForFirstLevel = 100
ScalingFactorA = {1.5f.ToString(culture)}
ScalingFactorB = {1.2f.ToString(culture)}
ScalingFactorC = 50
ScalingCurve = ""Parabolic""
";

        File.WriteAllText(tempFilePath, tomlContent);

        SkillDataConfig? config = null;
        BaseSkill? skill = null;
        try
        {
            config = Utility.LoadConfig<SkillDataConfig>(tempFilePath);

            Assert.IsNotNull(config);
            Assert.AreEqual(explicitId, config.ExplicitId);
            Assert.AreEqual("Swordsmanship", config.Name);
            Assert.AreEqual("The art of wielding swords.", config.Description);

            Assert.IsNotNull(config.LevelingOptions);
            LevelingOptions? levelingOptions = config.LevelingOptions;
            Assert.AreEqual(50, levelingOptions.MaxLevel);
            Assert.AreEqual(100, levelingOptions.PointsForFirstLevel);
            Assert.AreEqual(1.5f, levelingOptions.ScalingFactorA);
            Assert.AreEqual(1.2f, levelingOptions.ScalingFactorB);
            Assert.AreEqual(50, levelingOptions.ScalingFactorC);
            Assert.AreEqual(ScalingCurveType.Parabolic, levelingOptions.ScalingCurve);

            // Try to create a skill from the config
            skill = new BaseSkill(config);
            Assert.IsNotNull(skill);
            Assert.AreEqual(config.Name, skill.Name);
            Assert.AreEqual(levelingOptions.MaxLevel, skill.MaxLevel);
        }
        finally
        {
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }

    [TestMethod]
    public void LoadSkillDataConfig_MinimalToml_LoadsCorrectlyWithDefaults()
    {
        string tempFilePath = Path.GetTempFileName();

        string tomlContent = @"
Name = ""Alchemy""
";

        File.WriteAllText(tempFilePath, tomlContent);

        SkillDataConfig? config = null;
        BaseSkill? skill = null;
        try
        {
            config = Utility.LoadConfig<SkillDataConfig>(tempFilePath);

            Assert.IsNotNull(config);
            Assert.IsNull(config.ExplicitId);
            Assert.AreEqual("Alchemy", config.Name);
            Assert.IsNull(config.Description);
            Assert.IsNull(config.LevelingOptions); // Defaults to null

            // Try to create a skill from the config
            // BaseSkill constructor will use new LevelingOptions() if config.LevelingOptions is null
            skill = new BaseSkill(config);
            Assert.IsNotNull(skill);
            Assert.AreEqual(config.Name, skill.Name);

            // Must define defaults here, since they are not publicly exposed anywhere
            LevelingOptions defaultOptions = new()
            {
                MaxLevel = 1024,
                PointsForFirstLevel = 1,
                ScalingCurve = ScalingCurveType.None
            };
            Assert.AreEqual(defaultOptions.MaxLevel, skill.MaxLevel);
            Assert.AreEqual(defaultOptions.PointsForFirstLevel, skill.PointsForFirstLevel);
            Assert.AreEqual(defaultOptions.ScalingCurve, skill.ScalingCurve);
        }
        finally
        {
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }
}
