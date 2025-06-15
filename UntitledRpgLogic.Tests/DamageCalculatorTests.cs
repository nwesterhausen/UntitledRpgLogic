using Moq;
using UntitledRpgLogic.Game;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Tests;

[TestClass]
public class DamageCalculatorTests
{
    private DamageCalculator _damageCalculator;
    private Mock<IStat> _mockStat;

    [TestInitialize]
    public void Setup()
    {
        _damageCalculator = new DamageCalculator();
        _mockStat = new Mock<IStat>();
    }

    // Tests for GetPointDamageFromOptions

    [TestMethod]
    public void GetPointDamageFromOptions_FlatDamage_ReturnsFlatDamage()
    {
        // Arrange
        DamageOptions options = new() { FlatDamage = 10 };
        _mockStat.Setup(s => s.Value).Returns(100);
        _mockStat.Setup(s => s.MaxValue).Returns(200);

        // Act
        int result = _damageCalculator.GetPointDamageFromOptions(options, _mockStat.Object);

        // Assert
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void GetPointDamageFromOptions_PercentageDamage_ReturnsCorrectPercentage()
    {
        // Arrange
        DamageOptions options = new() { PercentageDamage = 10f }; // 10%
        _mockStat.Setup(s => s.Value).Returns(100);
        _mockStat.Setup(s => s.MaxValue).Returns(200);

        // Act
        int result = _damageCalculator.GetPointDamageFromOptions(options, _mockStat.Object);

        // Assert
        Assert.AreEqual(10, result); // 10% of 100
    }

    [TestMethod]
    public void GetPointDamageFromOptions_PercentageDamageOfMax_ReturnsCorrectPercentage()
    {
        // Arrange
        DamageOptions options = new() { PercentageDamageOfMax = 10f }; // 10%
        _mockStat.Setup(s => s.Value).Returns(100);
        _mockStat.Setup(s => s.MaxValue).Returns(200);

        // Act
        int result = _damageCalculator.GetPointDamageFromOptions(options, _mockStat.Object);

        // Assert
        Assert.AreEqual(20, result); // 10% of 200
    }

    [TestMethod]
    public void GetPointDamageFromOptions_NoDamageOptions_ReturnsZero()
    {
        // Arrange
        DamageOptions options = new();
        _mockStat.Setup(s => s.Value).Returns(100);
        _mockStat.Setup(s => s.MaxValue).Returns(200);

        // Act
        int result = _damageCalculator.GetPointDamageFromOptions(options, _mockStat.Object);

        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetPointDamageFromOptions_MultipleOptions_ReturnsFirstApplicable()
    {
        // Arrange: FlatDamage is first
        DamageOptions options1 = new() { FlatDamage = 5, PercentageDamage = 10f };
        _mockStat.Setup(s => s.Value).Returns(100);
        // Act
        int result1 = _damageCalculator.GetPointDamageFromOptions(options1, _mockStat.Object);
        // Assert
        Assert.AreEqual(5, result1);

        // Arrange: PercentageDamage is first (FlatDamage is null)
        DamageOptions options2 = new() { PercentageDamage = 10f, PercentageDamageOfMax = 20f };
        _mockStat.Setup(s => s.Value).Returns(100);
        // Act
        int result2 = _damageCalculator.GetPointDamageFromOptions(options2, _mockStat.Object);
        // Assert
        Assert.AreEqual(10, result2); // 10% of 100
    }

    // Tests for CalculateFinalDamage

    [TestMethod]
    public void CalculateFinalDamage_NoMitigations_ReturnsOriginalDamage()
    {
        // Arrange
        List<IAppliesDamageMitigation> mitigations = new();
        int initialDamage = 100;

        // Act
        int result = _damageCalculator.CalculateFinalDamage(initialDamage, mitigations);

        // Assert
        Assert.AreEqual(initialDamage, result);
    }

    [TestMethod]
    public void CalculateFinalDamage_SingleMitigation_AppliesMitigation()
    {
        // Arrange
        Mock<IAppliesDamageMitigation> mockMitigation = new();
        mockMitigation.Setup(m => m.MitigationPriority).Returns(1);
        mockMitigation.Setup(m => m.ApplyMitigation(It.IsAny<int>()))
            .Returns((int d) => d - 10); // Reduces damage by 10
        List<IAppliesDamageMitigation> mitigations = new() { mockMitigation.Object };
        int initialDamage = 100;

        // Act
        int result = _damageCalculator.CalculateFinalDamage(initialDamage, mitigations);

        // Assert
        Assert.AreEqual(90, result);
    }

    [TestMethod]
    public void CalculateFinalDamage_MultipleMitigations_AppliesInPriorityOrder()
    {
        // Arrange
        Mock<IAppliesDamageMitigation> mockMitigation1 = new(); // Priority 2, reduces by 5
        mockMitigation1.Setup(m => m.MitigationPriority).Returns(2);
        mockMitigation1.Setup(m => m.ApplyMitigation(It.IsAny<int>())).Returns((int d) => d - 5);

        Mock<IAppliesDamageMitigation> mockMitigation2 = new(); // Priority 1, halves damage
        mockMitigation2.Setup(m => m.MitigationPriority).Returns(1);
        mockMitigation2.Setup(m => m.ApplyMitigation(It.IsAny<int>())).Returns((int d) => d / 2);

        // Order in list is intentionally different from priority order
        List<IAppliesDamageMitigation> mitigations = new() { mockMitigation1.Object, mockMitigation2.Object };
        int initialDamage = 100;

        // Act
        int result = _damageCalculator.CalculateFinalDamage(initialDamage, mitigations);

        // Assert
        // Expected: 100 -> 50 (priority 1) -> 45 (priority 2)
        Assert.AreEqual(45, result);
    }

    [TestMethod]
    public void CalculateFinalDamage_MitigationReducesToZeroOrLess_ReturnsZeroOrLess()
    {
        // Arrange
        Mock<IAppliesDamageMitigation> mockMitigation = new();
        mockMitigation.Setup(m => m.MitigationPriority).Returns(1);
        mockMitigation.Setup(m => m.ApplyMitigation(It.IsAny<int>()))
            .Returns((int d) => d - 150); // Reduces damage by 150
        List<IAppliesDamageMitigation> mitigations = new() { mockMitigation.Object };
        int initialDamage = 100;

        // Act
        int result = _damageCalculator.CalculateFinalDamage(initialDamage, mitigations);

        // Assert
        Assert.AreEqual(-50, result); // Damage can be negative if mitigations are strong enough
    }
}
