using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Infrastructure.Data;
using UntitledRpgLogic.IntegrationTests.Common;

namespace UntitledRpgLogic.IntegrationTests.Coordinators;

[TestClass]
public sealed class ProgressionCoordinatorIntegrationTests : CoordinatorIntegrationTestBase
{
	[TestMethod]
	public async Task AwardExperienceAsync_LinearCurve_AdvancesLevelAndPersists()
	{
		var token = this.TestContext.CancellationToken;
		var dbName = $"prog_int_{Guid.NewGuid():N}";
		var provider = CreateTestProvider(dbName);

		await using (provider.ConfigureAwait(false))
		{
			await InitializeDatabaseAsync(provider, token).ConfigureAwait(false);

			var entityId = Ulid.NewUlid();
			var skillDefId = Ulid.NewUlid();

			// 1. Seed Entity, Linear LevelingDefinition, and Instanced Skill
			var seedScope = provider.CreateAsyncScope();
			await using (seedScope.ConfigureAwait(false))
			{
				var context = seedScope.ServiceProvider.GetRequiredService<RpgDbContext>();

				var levelingDef = new LevelingDefinition
				{
					StartingLevel = 1,
					MaxLevel = 5,
					PointsForFirstLevel = 100,
					ScalingCurve = ScalingCurveType.Linear,
					ScalingFactorA = 50f
				};

				var skillDef = new SkillDefinition(new Name("Blacksmithing"))
				{
					Id = skillDefId, LevelingDefinition = levelingDef
				};

				var skill = new Skill { DefinitionId = skillDefId, Level = 1, ExperiencePoints = 0 };

				var entity = new Entity(new Name("Artisan"))
				{
					Id = entityId,
					Skills = [new EntitySkills { InstancedSkill = skill, InstancedSkillId = skill.Id }]
				};

				await context.LevelingDefinitions.AddAsync(levelingDef, token).ConfigureAwait(false);
				await context.SkillDefinitions.AddAsync(skillDef, token).ConfigureAwait(false);
				await context.Entities.AddAsync(entity, token).ConfigureAwait(false);
				await context.SaveChangesAsync(token).ConfigureAwait(false);
			}

			// 2. Award 250 XP via coordinator
			var awardScope = provider.CreateAsyncScope();
			await using (awardScope.ConfigureAwait(false))
			{
				var coordinator = awardScope.ServiceProvider.GetRequiredService<IProgressionCoordinatorService>();
				var result = await coordinator.AwardExperienceAsync(entityId, skillDefId, 250, token)
					.ConfigureAwait(false);

				Assert.AreEqual(1, result.PreviousLevel);
				Assert.AreEqual(2, result.CurrentLevel);
				Assert.AreEqual(1, result.LevelsGained);
				Assert.AreEqual(100, result.TotalExperiencePoints);
			}

			// 3. Verify in independent scope
			var verifyScope = provider.CreateAsyncScope();
			await using (verifyScope.ConfigureAwait(false))
			{
				var entityRepo = verifyScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();
				var loaded = await entityRepo.GetByIdAsync(
					entityId,
					q => q.Include(e => e.Skills).ThenInclude(s => s.InstancedSkill),
					token).ConfigureAwait(false);

				Assert.IsNotNull(loaded);
				var activeSkill = loaded.Skills.First().InstancedSkill;
				Assert.IsNotNull(activeSkill);
				Assert.AreEqual(2, activeSkill.Level);
				Assert.AreEqual(100, activeSkill.ExperiencePoints);
			}

			await CleanupDatabaseAsync(provider, dbName).ConfigureAwait(false);
		}
	}
}
