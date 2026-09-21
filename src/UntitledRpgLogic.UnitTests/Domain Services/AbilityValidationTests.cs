using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Services.Domains;
using Random = UntitledRpgLogic.Extensions.Common.Random;

namespace UntitledRpgLogic.UnitTests.Domain_Services;

[TestClass]
public class AbilityValidationTests
{
	private static readonly Random Random = new();
	private readonly AbilityValidationService abilityValidationService = new(Random);

	[TestMethod]
	public void CanAffordCosts_InsufficientResource_ReturnsFalse()
	{
		var manaDef = new StatDefinition(new Name("Mana")) { MinValue = 0, MaxValue = 100 };
		var manaStat = new Stat(manaDef.Id) { ApparentValue = 10 };

		var caster = new Entity(new Name("Mage"))
		{
			Stats = [new EntityStats { InstancedStat = manaStat, InstancedStatId = manaStat.Id }]
		};

		var ability = new AbilityDefinition(new Name("Fireball"))
		{
			StatCosts = [new StatCost { StatId = manaDef.Id, Amount = 25f }]
		};

		var canAfford = this.abilityValidationService.CanAffordCosts(caster, ability);

		Assert.IsFalse(canAfford);
	}

	[TestMethod]
	public void CanLearnAbility_MeetsAllRequirements_ReturnsTrue()
	{
		var fireballSkillDef = new SkillDefinition(new Name("Destruction"));
		var destructionSkill = new Skill { DefinitionId = fireballSkillDef.Id, Level = 15 };

		var learner = new Entity(new Name("Apprentice"))
		{
			Skills =
			[
				new EntitySkills { InstancedSkill = destructionSkill, InstancedSkillId = destructionSkill.Id }
			]
		};

		var ability = new AbilityDefinition(new Name("Inferno"))
		{
			LearningRequirements =
			[
				new AbilityLearningRequirement
				{
					RequirementType = RequirementType.SkillLevel,
					RequiredEntityId = fireballSkillDef.Id,
					AmountNeeded = 10
				}
			]
		};

		var canLearn = this.abilityValidationService.CanLearnAbility(learner, ability);

		Assert.IsTrue(canLearn);
	}
}
