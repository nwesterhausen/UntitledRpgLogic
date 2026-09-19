using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Infrastructure.Data;
using UntitledRpgLogic.IntegrationTests.Common;

namespace UntitledRpgLogic.IntegrationTests.Coordinators;

[TestClass]
public sealed class AbilityCoordinatorIntegrationTests : CoordinatorIntegrationTestBase
{
	[TestMethod]
	public async Task CastAbilityAsync_SufficientResources_DeductsCostAppliesDamageAndPersistsAcrossScopes()
	{
		var token = this.TestContext.CancellationToken;
		var dbName = $"ability_int_{Guid.NewGuid():N}";
		var provider = CreateTestProvider(dbName);

		await using (provider.ConfigureAwait(false))
		{
			await InitializeDatabaseAsync(provider, token).ConfigureAwait(false);

			var casterId = Ulid.NewUlid();
			var targetId = Ulid.NewUlid();
			var abilityId = Ulid.NewUlid();
			var manaDefId = Ulid.NewUlid();
			var healthDefId = Ulid.NewUlid();

			// 1. Seed Caster, Target, Stats, and Ability with DamageEffect
			var seedScope = provider.CreateAsyncScope();
			await using (seedScope.ConfigureAwait(false))
			{
				var context = seedScope.ServiceProvider.GetRequiredService<RpgDbContext>();

				var manaDef = new StatDefinition(new Name("Mana")) { Id = manaDefId, MinValue = 0, MaxValue = 100 };
				var healthDef =
					new StatDefinition(new Name("Health")) { Id = healthDefId, MinValue = 0, MaxValue = 200 };
				var skillDef = new SkillDefinition(new Name("Destruction"));

				var manaStat = new Stat(manaDefId) { BaseValue = 100, ApparentValue = 100 };
				var healthStat = new Stat(healthDefId) { BaseValue = 200, ApparentValue = 200 };

				var caster = new Entity(new Name("Mage"))
				{
					Id = casterId,
					Stats = [new EntityStats { InstancedStat = manaStat, InstancedStatId = manaStat.Id }]
				};

				var target = new Entity(new Name("Dummy"))
				{
					Id = targetId,
					Stats = [new EntityStats { InstancedStat = healthStat, InstancedStatId = healthStat.Id }]
				};

				var ability = new AbilityDefinition(new Name("Chaos Bolt"))
				{
					Id = abilityId,
					SkillDisciplineId = skillDef.Id,
					SkillDiscipline = skillDef,
					StatCosts = [new StatCost { StatId = manaDefId, Amount = 40f }]
				};

				var damageEffect = new DamageEffect(new Name("Chaos Damage"),
					healthDefId,
					new StatChangeOptions { FlatChange = 60 },
					true);
				ability.ActiveEffects.Add(damageEffect);

				await context.StatDefinitions.AddRangeAsync([manaDef, healthDef], token).ConfigureAwait(false);
				await context.SkillDefinitions.AddAsync(skillDef, token).ConfigureAwait(false);
				await context.Abilities.AddAsync(ability, token).ConfigureAwait(false);
				await context.Entities.AddRangeAsync([caster, target], token).ConfigureAwait(false);
				await context.SaveChangesAsync(token).ConfigureAwait(false);
			}

			// 2. Cast Ability via coordinator in a fresh execution scope
			var castScope = provider.CreateAsyncScope();
			await using (castScope.ConfigureAwait(false))
			{
				var coordinator = castScope.ServiceProvider.GetRequiredService<IAbilityCoordinatorService>();
				var result = await coordinator.CastAbilityAsync(casterId, abilityId, [targetId], token)
					.ConfigureAwait(false);

				Assert.IsTrue(result.IsSuccess);
				Assert.AreEqual(AbilityCastStatus.Success, result.Status);
				Assert.HasCount(1, result.ConsumedCosts);
				Assert.HasCount(1, result.TargetOutcomes);
				Assert.AreEqual(60f, result.TargetOutcomes.First().AppliedValue);
			}

			// 3. Verify final persisted stats in an independent DbContext scope
			var verifyScope = provider.CreateAsyncScope();
			await using (verifyScope.ConfigureAwait(false))
			{
				var entityRepo = verifyScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				var verifyCaster = await entityRepo.GetByIdAsync(
					casterId,
					q => q.Include(e => e.Stats).ThenInclude(s => s.InstancedStat),
					token).ConfigureAwait(false);

				var verifyTarget = await entityRepo.GetByIdAsync(
					targetId,
					q => q.Include(e => e.Stats).ThenInclude(s => s.InstancedStat),
					token).ConfigureAwait(false);

				Assert.IsNotNull(verifyCaster);
				Assert.IsNotNull(verifyTarget);

				var casterMana = verifyCaster.Stats.First().InstancedStat;
				Assert.IsNotNull(casterMana);
				Assert.AreEqual(60, casterMana.ApparentValue);

				var targetHealth = verifyTarget.Stats.First().InstancedStat;
				Assert.IsNotNull(targetHealth);
				Assert.AreEqual(140, targetHealth.ApparentValue);
			}

			await CleanupDatabaseAsync(provider, dbName).ConfigureAwait(false);
		}
	}
}
