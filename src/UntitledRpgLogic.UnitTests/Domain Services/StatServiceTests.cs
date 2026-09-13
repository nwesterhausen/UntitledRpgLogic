using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

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
}
