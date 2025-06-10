using System.Globalization;
using UntitledRpgLogic.Configuration;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Extensions;
using UntitledRpgLogic.Game;

namespace UntitledRpgLogic.Tests.Configuration;

[TestClass]
public class ItemDataConfigTests
{
    [TestMethod]
    public void LoadItemDataConfig_ValidToml_LoadsCorrectly()
    {
        var tempFilePath = Path.GetTempFileName();
        var explicitId = Guid.NewGuid();
        var craftedById = Guid.NewGuid();
        var materialId = ReservedGuids.MaterialCopper;

        // Ensure consistent float string representation
        var culture = CultureInfo.InvariantCulture;

        var tomlContent = $@"
ExplicitId = ""{explicitId}""
Name = ""Test Sword""
PluralName = ""Test Swords""
NameAsAdjective = ""Test Swordy""
ItemQuality = ""Epic""
ItemType = ""Weapon""
ItemSubtype = ""Longsword""
Description = ""A finely crafted test sword.""
CraftedBy = ""{craftedById}""
DimensionScale = ""m""
ShapeType = ""Cylinder""
Width = {1.5f.ToString(culture)}
Height = {10.2f.ToString(culture)}
Depth = {0.5f.ToString(culture)}
MaterialId = ""{materialId}""
";

        File.WriteAllText(tempFilePath, tomlContent);

        ItemDataConfig? config = null;
        BaseItem? item = null;
        try
        {
            config = Utility.LoadConfig<ItemDataConfig>(tempFilePath);

            Assert.IsNotNull(config);
            Assert.AreEqual(explicitId, config.ExplicitId);
            Assert.AreEqual("Test Sword", config.Name);
            Assert.AreEqual("Test Swords", config.PluralName);
            Assert.AreEqual("Test Swordy", config.NameAsAdjective);
            Assert.AreEqual(Quality.Epic, config.ItemQuality);
            Assert.AreEqual(ItemType.Weapon, config.ItemType);
            Assert.AreEqual(ItemSubtype.LongSword, config.ItemSubtype);
            Assert.AreEqual("A finely crafted test sword.", config.Description);
            Assert.AreEqual(craftedById, config.CraftedBy);
            Assert.AreEqual(DimensionScale.m, config.DimensionScale);
            Assert.AreEqual(ShapeType.Cylinder, config.ShapeType);
            Assert.AreEqual(1.5f, config.Width);
            Assert.AreEqual(10.2f, config.Height);
            Assert.AreEqual(0.5f, config.Depth);
            Assert.AreEqual(materialId, config.MaterialId);

            // Try to make an item out of it
            item = new BaseItem(config);
            item.CalculateVolume();
            Assert.IsNotNull(item);
        }
        finally
        {
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }

    [TestMethod]
    public void LoadItemDataConfig_MinimalToml_LoadsCorrectlyWithDefaults()
    {
        var tempFilePath = Path.GetTempFileName();
        var materialId = ReservedGuids.MaterialCopper;

        // Ensure consistent float string representation
        var culture = CultureInfo.InvariantCulture;

        var tomlContent = $@"
Name = ""Basic Dagger""
ItemType = ""Weapon""
Width = {5.0f.ToString(culture)}
Height = {20.0f.ToString(culture)}
MaterialId = ""{materialId}""
";

        File.WriteAllText(tempFilePath, tomlContent);

        ItemDataConfig? config = null;
        BaseItem? item = null;
        try
        {
            config = Utility.LoadConfig<ItemDataConfig>(tempFilePath);

            Assert.IsNotNull(config);
            Assert.IsNull(config.ExplicitId);
            Assert.AreEqual("Basic Dagger", config.Name);
            Assert.IsNull(config.PluralName); // Defaults to null
            Assert.IsNull(config.NameAsAdjective); // Defaults to null
            Assert.IsNull(config
                .ItemQuality); // Defaults to null (Quality.None if accessed via a getter that provides default)
            Assert.AreEqual(ItemType.Weapon, config.ItemType);
            Assert.IsNull(config
                .ItemSubtype); // Defaults to null (ItemSubtype.None if accessed via a getter that provides default)
            Assert.IsNull(config.Description);
            Assert.IsNull(config.CraftedBy);
            Assert.IsNull(config
                .DimensionScale); // Defaults to null (DimensionScale.cm if accessed via a getter that provides default)
            Assert.IsNull(config.ShapeType);
            Assert.AreEqual(5.0f, config.Width);
            Assert.AreEqual(20.0f, config.Height);
            Assert.IsNull(config.Depth); // Defaults to null (1.0f if accessed via a getter that provides default)
            Assert.AreEqual(materialId, config.MaterialId);

            // Try to make an item out of it
            item = new BaseItem(config);

            Assert.IsNotNull(item);
        }
        finally
        {
            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
        }
    }
}