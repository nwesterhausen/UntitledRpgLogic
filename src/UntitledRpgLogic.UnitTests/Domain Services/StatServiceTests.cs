using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Services.Domains;

namespace UntitledRpgLogic.UnitTests.Domain_Services;

[TestClass]
public class StatServiceTests
{
	private readonly StatCalculationService statCalculationService = new();

	[TestMethod]
	public void CalculateApparentValue_WithLinkedDependency_AppliesProportion()
	{
		var strengthDef = new StatDefinition(new Name("Strength")) { MinValue = 0, MaxValue = 100 };

		var attackDef = new StatDefinition(new Name("Attack Power")) { MinValue = 0, MaxValue = 500 };

		attackDef.LinkedStats.Add(new LinkedStats
		{
			StatId = attackDef.Id, DependsOnId = strengthDef.Id, Ratio = 1.5f
		});

		var strengthStat = new Stat { DefinitionId = strengthDef.Id, Definition = strengthDef, ApparentValue = 20 };

		var attackStat = new Stat { DefinitionId = attackDef.Id, Definition = attackDef, BaseValue = 10 };

		// 10 base + (20 * 1.5 = 30) = 40
		var apparent = this.statCalculationService.CalculateApparentValue(attackStat, [strengthStat]);

		Assert.AreEqual(40, apparent);
	}

	[TestMethod]
	public void ClampToDefinitionBounds_ExceedsBounds_ClampsAccurately()
	{
		var def = new StatDefinition(new Name("Health")) { MinValue = 0, MaxValue = 100 };

		Assert.AreEqual(100, this.statCalculationService.ClampToDefinitionBounds(def, 150));
		Assert.AreEqual(0, this.statCalculationService.ClampToDefinitionBounds(def, -20));
	}

	[TestMethod]
	public void CalculatePointDamage_WithPercentage_AddsProportionOfTargetBase()
	{
		var options = new ChangeOptions { FlatChange = 10, PercentageChange = 0.20f };
		var stat = new Stat { BaseValue = 200, ApparentValue = 200 };

		var damage = this.statCalculationService.CalculatePointChange(options, stat);

		Assert.AreEqual(50, damage); // 10 flat + (200 * 0.20)
	}

	[TestMethod]
	public void CalculateMitigatedDamage_WithResistanceAndMitigation_ReducesCorrectly()
	{
		// 100 raw, 25% resist (75), minus 10 flat = 65
		var damage = this.statCalculationService.CalculateMitigatedPointChange(100, 0.25f, 10);

		Assert.AreEqual(65, damage);
	}

	[TestMethod]
	public void CalculateMitigatedDamage_MitigationExceedsDamage_FloorsAtZero()
	{
		var damage = this.statCalculationService.CalculateMitigatedPointChange(10, 0.50f, 20);

		Assert.AreEqual(0, damage);
	}

	[TestMethod]
	public void CalculatePointDamage_NullArguments_ThrowsArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => this.statCalculationService.CalculatePointChange(null!, new Stat()));
		Assert.Throws<ArgumentNullException>(() =>
			this.statCalculationService.CalculatePointChange(new ChangeOptions(), null!));
	}

	[
		TestMethod]
	public void CalculatePointDamage_FlatDamageOnly_ReturnsExactAmount()
	{
		var stat = new Stat { ApparentValue = 100 };
		var options = new ChangeOptions { FlatChange = 25 };

		var damage = this.statCalculationService.CalculatePointChange(options, stat);

		Assert.AreEqual(25, damage);
	}

	[TestMethod]
	public void CalculatePointDamage_PercentageOfCurrent_CalculatesProportionally()
	{
		var stat = new Stat { ApparentValue = 200 };
		var options = new ChangeOptions { PercentageChange = 0.15f }; // 15% of 200 = 30

		var damage = this.statCalculationService.CalculatePointChange(options, stat);

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

		var options = new ChangeOptions { PercentageChangeOfMax = 0.10f }; // 10% of 500 = 50

		var damage = this.statCalculationService.CalculatePointChange(options, stat);

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

		var options = new ChangeOptions
		{
			FlatChange = 50, // 50
			PercentageChange = 0.10f, // 10% of 500 = 50
			PercentageChangeOfMax = 0.05f // 5% of 1000 = 50
		};

		var damage = this.statCalculationService.CalculatePointChange(options, stat);

		Assert.AreEqual(150, damage);
	}

	[TestMethod]
	public void CalculateMitigatedDamage_TotalImmunity_ReturnsZero()
	{
		var finalDamage = this.statCalculationService.CalculateMitigatedPointChange(250, 1.0f, 0);

		Assert.AreEqual(0, finalDamage);
	}

	[TestMethod]
	public void CalculateMitigatedDamage_NegativeRawDamage_ReturnsZero()
	{
		var finalDamage = this.statCalculationService.CalculateMitigatedPointChange(-50, 0.2f, 5);

		Assert.AreEqual(0, finalDamage);
	}
}
