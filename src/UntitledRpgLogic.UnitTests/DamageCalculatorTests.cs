using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public class DamageCalculatorTests
{
	private readonly DamageCalculator _calculator = new();

	[TestMethod]
	public void CalculatePointDamage_WithPercentage_AddsProportionOfTargetBase()
	{
		var options = new DamageOptions { FlatDamage = 10, PercentageDamage = 0.20f };
		var stat = new Stat { BaseValue = 200 };

		var damage = this._calculator.CalculatePointDamage(options, stat);

		Assert.AreEqual(50, damage); // 10 flat + (200 * 0.20 = 40)
	}

	[TestMethod]
	public void CalculateMitigatedDamage_WithResistanceAndMitigation_ReducesCorrectly()
	{
		// 100 raw, 25% resist (75), minus 10 flat = 65
		var damage = this._calculator.CalculateMitigatedDamage(100, 0.25f, 10);

		Assert.AreEqual(65, damage);
	}

	[TestMethod]
	public void CalculateMitigatedDamage_MitigationExceedsDamage_FloorsAtZero()
	{
		var damage = this._calculator.CalculateMitigatedDamage(10, 0.50f, 20);

		Assert.AreEqual(0, damage);
	}

	[TestMethod]
	public void CalculatePointDamage_NullArguments_ThrowsArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => this._calculator.CalculatePointDamage(null!, new Stat()));
		Assert.Throws<ArgumentNullException>(() => this._calculator.CalculatePointDamage(new DamageOptions(), null!));
	}

	[
		TestMethod]
	public void CalculatePointDamage_FlatDamageOnly_ReturnsExactAmount()
	{
		var stat = new Stat { ApparentValue = 100 };
		var options = new DamageOptions { FlatDamage = 25 };

		var damage = this._calculator.CalculatePointDamage(options, stat);

		Assert.AreEqual(25, damage);
	}

	[TestMethod]
	public void CalculatePointDamage_PercentageOfCurrent_CalculatesProportionally()
	{
		var stat = new Stat { ApparentValue = 200 };
		var options = new DamageOptions { PercentageDamage = 0.15f }; // 15% of 200 = 30

		var damage = this._calculator.CalculatePointDamage(options, stat);

		Assert.AreEqual(30, damage);
	}

	[TestMethod]
	public void CalculatePointDamage_PercentageOfMax_UsesDefinitionMaximum()
	{
		var statDef = new StatDefinition(new Name("Health"))
		{
			MinValue = 0, MaxValue = 500, Variation = StatVariation.Major
		};

		var stat = new Stat { Definition = statDef, ApparentValue = 150 };

		var options = new DamageOptions { PercentageDamageOfMax = 0.10f }; // 10% of 500 = 50

		var damage = this._calculator.CalculatePointDamage(options, stat);

		Assert.AreEqual(50, damage);
	}

	[TestMethod]
	public void CalculatePointDamage_CombinedDamage_SumsAllComponentsCorrectly()
	{
		var statDef = new StatDefinition(new Name("Health"))
		{
			MinValue = 0, MaxValue = 1000, Variation = StatVariation.Major
		};

		var stat = new Stat { Definition = statDef, ApparentValue = 500 };

		var options = new DamageOptions
		{
			FlatDamage = 50, // 50
			PercentageDamage = 0.10f, // 10% of 500 = 50
			PercentageDamageOfMax = 0.05f // 5% of 1000 = 50
		};

		var damage = this._calculator.CalculatePointDamage(options, stat);

		Assert.AreEqual(150, damage);
	}

	[TestMethod]
	public void CalculateMitigatedDamage_TotalImmunity_ReturnsZero()
	{
		var finalDamage = this._calculator.CalculateMitigatedDamage(250, 1.0f, 0);

		Assert.AreEqual(0, finalDamage);
	}

	[TestMethod]
	public void CalculateMitigatedDamage_NegativeRawDamage_ReturnsZero()
	{
		var finalDamage = this._calculator.CalculateMitigatedDamage(-50, 0.2f, 5);

		Assert.AreEqual(0, finalDamage);
	}
}
