using System.Linq.Expressions;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Services.Coordinators;
using UntitledRpgLogic.Services.Domains;
using Random = UntitledRpgLogic.Extensions.Common.Random;

namespace UntitledRpgLogic.UnitTests.Coordinators;

[TestClass]
public sealed class ProgressionCoordinatorServiceTests
{
	private static readonly Random Random = new();
	private readonly AbilityValidationService abilityValidationService = new(Random);

	private readonly SkillProgressionService skillProgressionService = new();

	[TestMethod]
	public async Task AwardExperienceAsync_ValidGain_AdvancesLevelAndSavesChanges()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var entityRepo = new FakeEntityRepository<Entity>();
			var skillDefRepo = new FakeEntityRepository<SkillDefinition>();
			var abilityDefRepo = new FakeEntityRepository<AbilityDefinition>();

			var levelingDef = new LevelingDefinition
			{
				StartingLevel = 1,
				MaxLevel = 10,
				PointsForFirstLevel = 100,
				ScalingCurve = ScalingCurveType.Linear,
				ScalingFactorA = 0f
			};

			var skillDef = new SkillDefinition(new Name("Swordsmanship")) { LevelingDefinition = levelingDef };
			skillDefRepo.Entities[skillDef.Id] = skillDef;

			var activeSkill = new Skill { DefinitionId = skillDef.Id, Level = 1, ExperiencePoints = 0 };
			var entity = new Entity(new Name("Warrior"))
			{
				Skills = [new EntitySkills { InstancedSkill = activeSkill, InstancedSkillId = activeSkill.Id }]
			};
			entityRepo.Entities[entity.Id] = entity;

			var coordinator = new ProgressionCoordinatorService(
				uow,
				entityRepo,
				skillDefRepo,
				abilityDefRepo,
				this.skillProgressionService,
				this.abilityValidationService);

			var result = await coordinator.AwardExperienceAsync(entity.Id, skillDef.Id, 150).ConfigureAwait(false);

			Assert.AreEqual(1, result.PreviousLevel);
			Assert.AreEqual(2, result.CurrentLevel);
			Assert.IsTrue(result.DidLevelUp);
			Assert.AreEqual(1, result.LevelsGained);
			Assert.AreEqual(50, result.TotalExperiencePoints);
			Assert.IsTrue(uow.ChangesSaved);
		}
	}

	[TestMethod]
	public async Task LearnAbilityAsync_SatisfiedPrerequisites_ReturnsTrue()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var entityRepo = new FakeEntityRepository<Entity>();
			var skillDefRepo = new FakeEntityRepository<SkillDefinition>();
			var abilityDefRepo = new FakeEntityRepository<AbilityDefinition>();

			var skillDef = new SkillDefinition(new Name("Pyromancy"));
			var activeSkill = new Skill { DefinitionId = skillDef.Id, Level = 10 };
			var entity = new Entity(new Name("Mage"))
			{
				Skills = [new EntitySkills { InstancedSkill = activeSkill, InstancedSkillId = activeSkill.Id }]
			};
			entityRepo.Entities[entity.Id] = entity;

			var ability = new AbilityDefinition(new Name("Flame Pillar"))
			{
				LearningRequirements =
				[
					new AbilityLearningRequirement
					{
						RequirementType = RequirementType.SkillLevel,
						RequiredEntityId = skillDef.Id,
						AmountNeeded = 5
					}
				]
			};
			abilityDefRepo.Entities[ability.Id] = ability;

			var coordinator = new ProgressionCoordinatorService(
				uow,
				entityRepo,
				skillDefRepo,
				abilityDefRepo,
				this.skillProgressionService,
				this.abilityValidationService);

			var canLearn = await coordinator.LearnAbilityAsync(entity.Id, ability.Id).ConfigureAwait(false);

			Assert.IsTrue(canLearn);
			Assert.IsTrue(uow.ChangesSaved);
		}
	}

	private sealed class FakeUnitOfWork : IUnitOfWork
	{
		public bool ChangesSaved { get; private set; }

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			this.ChangesSaved = true;
			return Task.FromResult(1);
		}

		public Task BeginTransactionAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
		public Task CommitTransactionAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
		public Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
		public void Dispose() { }
		public ValueTask DisposeAsync() => ValueTask.CompletedTask;
	}

	private sealed class FakeEntityRepository<TEntity> : IEntityRepository<TEntity, Ulid>
		where TEntity : class, IDbEntity<Ulid>
	{
		public Dictionary<Ulid, TEntity> Entities { get; } = [];

		public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			this.Entities[entity.Id] = entity;
			return Task.CompletedTask;
		}

		public Task<TEntity?> GetByIdAsync(
			Ulid id,
			CancellationToken cancellationToken = default,
			params Expression<Func<TEntity, object?>>[] includes)
		{
			this.Entities.TryGetValue(id, out var entity);
			return Task.FromResult(entity);
		}

		public Task<TEntity?> GetByIdAsync(
			Ulid id,
			Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
			CancellationToken cancellationToken = default)
		{
			this.Entities.TryGetValue(id, out var entity);
			return Task.FromResult(entity);
		}

		public Task<IReadOnlyList<TEntity>> GetByIdsAsync(
			IEnumerable<Ulid> ids,
			CancellationToken cancellationToken = default,
			params Expression<Func<TEntity, object?>>[] includes)
		{
			var list = ids.Where(this.Entities.ContainsKey).Select(id => this.Entities[id]).ToList();
			return Task.FromResult<IReadOnlyList<TEntity>>(list);
		}

		public void Update(TEntity entity) => this.Entities[entity.Id] = entity;
		public void Remove(TEntity entity) => this.Entities.Remove(entity.Id);
	}
}
