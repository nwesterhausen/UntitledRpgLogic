using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public class DamageCalculatorTests
{
	private readonly DamageCalculator calculator = new();

	[TestMethod]
	public void CalculatePointDamage_FlatOnly_ReturnsExactFlatDamage()
	{
		var options = new DamageOptions { FlatDamage = 25, PercentageDamage = 0f };
		var stat = new Stat { BaseValue = 100 };

		var damage = this.calculator.CalculatePointDamage(options, stat);

		Assert.AreEqual(25, damage);
	}

	[TestMethod]
	public void CalculatePointDamage_WithPercentage_AddsProportionOfTargetBase()
	{
		var options = new DamageOptions { FlatDamage = 10, PercentageDamage = 0.20f };
		var stat = new Stat { BaseValue = 200 };

		var damage = this.calculator.CalculatePointDamage(options, stat);

		Assert.AreEqual(50, damage); // 10 flat + (200 * 0.20 = 40)
	}

	[TestMethod]
	public void CalculateMitigatedDamage_WithResistanceAndMitigation_ReducesCorrectly()
	{
		// 100 raw, 25% resist (75), minus 10 flat = 65
		var damage = this.calculator.CalculateMitigatedDamage(rawDamage: 100, resistancePercent: 0.25f, flatMitigation: 10);

		Assert.AreEqual(65, damage);
	}

	[TestMethod]
	public void CalculateMitigatedDamage_MitigationExceedsDamage_FloorsAtZero()
	{
		var damage = this.calculator.CalculateMitigatedDamage(rawDamage: 10, resistancePercent: 0.50f, flatMitigation: 20);

		Assert.AreEqual(0, damage);
	}

	[TestMethod]
	public void CalculatePointDamage_NullArguments_ThrowsArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(
			() => this.calculator.CalculatePointDamage(null!, new Stat()));
		Assert.Throws<ArgumentNullException>(() => this.calculator.CalculatePointDamage(new DamageOptions(), null!));
	}
}
