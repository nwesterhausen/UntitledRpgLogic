using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public class EffectApplicationServiceTests
{
	private readonly StatDefinition healthDef;
	private readonly EffectApplicationService service;
	private readonly Entity target;
	private readonly Stat targetHealth;

	public EffectApplicationServiceTests()
	{
		this.service = new EffectApplicationService(new DamageCalculator());

		this.healthDef = new StatDefinition(new Name("Health"))
		{
			MinValue = 0,
			MaxValue = 100,
			Variation = StatVariation.Major
		};

		this.targetHealth = new Stat(this.healthDef.Id)
		{
			Definition = this.healthDef,
			BaseValue = 100,
			ApparentValue = 100
		};

		this.target = new Entity(new Name("Target Dummy"))
		{
			Stats =
			[
				new EntityStats { InstancedStat = this.targetHealth, InstancedStatId = this.targetHealth.Id }
			]
		};
	}

	[TestMethod]
	public void ApplyEffect_DamageEffect_ReducesHealthApparentValue()
	{
		var damageEffect = new DamageEffect(new Name("Firebolt")) { BaseDamage = 35f, IgnoresArmor = true };

		this.service.ApplyEffect(damageEffect, targets: [this.target]);

		Assert.AreEqual(65, this.targetHealth.ApparentValue);
	}

	[TestMethod]
	public void ApplyEffect_DamageEffect_ClampsAtMinValue()
	{
		var lethalDamage = new DamageEffect(new Name("Execute")) { BaseDamage = 250f, IgnoresArmor = true };

		this.service.ApplyEffect(lethalDamage, targets: [this.target]);

		Assert.AreEqual(0, this.targetHealth.ApparentValue);
	}

	[TestMethod]
	public void ApplyEffect_HealEffect_WithoutOverheal_CapsAtDefinitionMax()
	{
		this.targetHealth.ApparentValue = 85;

		var healEffect = new HealEffect(new Name("Minor Heal")) { BaseHealAmount = 30f, CanOverheal = false };

		this.service.ApplyEffect(healEffect, targets: [this.target]);

		Assert.AreEqual(100, this.targetHealth.ApparentValue);
	}

	[TestMethod]
	public void ApplyEffect_HealEffect_WithOverheal_ExceedsDefinitionMax()
	{
		this.targetHealth.ApparentValue = 90;

		var overhealEffect = new HealEffect(new Name("Divine Shielding")) { BaseHealAmount = 40f, CanOverheal = true };

		this.service.ApplyEffect(overhealEffect, targets: [this.target]);

		Assert.AreEqual(130, this.targetHealth.ApparentValue);
	}
}
