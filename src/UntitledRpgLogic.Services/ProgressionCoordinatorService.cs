using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Core.Skills;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Application coordination service managing skill experience awarding, level progression,
///     ability prerequisite evaluations, and persistence commits[cite: 2, 3].
/// </summary>
public sealed class ProgressionCoordinatorService : IProgressionCoordinatorService
{
	private readonly IEntityRepository<AbilityDefinition, Ulid> abilityDefinitionRepository;
	private readonly IAbilityValidationService abilityValidationService;
	private readonly IEntityRepository<Entity, Ulid> entityRepository;
	private readonly IEntityRepository<SkillDefinition, Ulid> skillDefinitionRepository;
	private readonly ISkillProgressionService skillProgressionService;
	private readonly IUnitOfWork unitOfWork;

	/// <summary>
	///     Initializes a new instance of the <see cref="ProgressionCoordinatorService" /> class.
	/// </summary>
	/// <param name="unitOfWork">The unit of work for managing transaction boundaries.</param>
	/// <param name="entityRepository">The repository for entity aggregates[cite: 1, 2].</param>
	/// <param name="skillDefinitionRepository">The repository for skill definitions[cite: 1, 2].</param>
	/// <param name="abilityDefinitionRepository">The repository for ability definitions[cite: 1, 2].</param>
	/// <param name="skillProgressionService">The pure domain service for experience and level curves[cite: 2].</param>
	/// <param name="abilityValidationService">The pure domain service for checking prerequisites[cite: 2].</param>
	public ProgressionCoordinatorService(
		IUnitOfWork unitOfWork,
		IEntityRepository<Entity, Ulid> entityRepository,
		IEntityRepository<SkillDefinition, Ulid> skillDefinitionRepository,
		IEntityRepository<AbilityDefinition, Ulid> abilityDefinitionRepository,
		ISkillProgressionService skillProgressionService,
		IAbilityValidationService abilityValidationService)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
		this.skillDefinitionRepository = skillDefinitionRepository ??
										 throw new ArgumentNullException(nameof(skillDefinitionRepository));
		this.abilityDefinitionRepository = abilityDefinitionRepository ??
										   throw new ArgumentNullException(nameof(abilityDefinitionRepository));
		this.skillProgressionService =
			skillProgressionService ?? throw new ArgumentNullException(nameof(skillProgressionService));
		this.abilityValidationService = abilityValidationService ??
										throw new ArgumentNullException(nameof(abilityValidationService));
	}

	/// <inheritdoc />
	public async Task<ProgressionResult> AwardExperienceAsync(
		Ulid entityId,
		Ulid skillDefinitionId,
		int experiencePoints,
		CancellationToken cancellationToken = default)
	{
		if (experiencePoints <= 0)
		{
			return new ProgressionResult { SkillDefinitionId = skillDefinitionId, ExperienceGained = 0 };
		}

		var entity = await this.LoadEntityAggregateAsync(entityId, cancellationToken).ConfigureAwait(false);
		if (entity is null)
		{
			return new ProgressionResult { SkillDefinitionId = skillDefinitionId };
		}

		var skillDef = await this.skillDefinitionRepository.GetByIdAsync(
			skillDefinitionId,
			q => q.Include(s => s.LevelingDefinition)
				.Include(s => s.Abilities),
			cancellationToken).ConfigureAwait(false);

		if (skillDef?.LevelingDefinition is null)
		{
			return new ProgressionResult { SkillDefinitionId = skillDefinitionId };
		}

		var entitySkill = entity.Skills.FirstOrDefault(s => s.InstancedSkill?.DefinitionId == skillDefinitionId);
		if (entitySkill?.InstancedSkill is null)
		{
			return new ProgressionResult { SkillDefinitionId = skillDefinitionId };
		}

		var activeSkill = entitySkill.InstancedSkill;
		var prevLevel = activeSkill.Level;

		this.skillProgressionService.TryAddExperience(
			activeSkill,
			skillDef.LevelingDefinition,
			experiencePoints,
			out _);

		var unlockedAbilities = this.ResolveNewlyUnlockedAbilities(entity, skillDef);

		await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		return new ProgressionResult
		{
			SkillDefinitionId = skillDefinitionId,
			ExperienceGained = experiencePoints,
			PreviousLevel = prevLevel,
			CurrentLevel = activeSkill.Level,
			TotalExperiencePoints = activeSkill.ExperiencePoints,
			UnlockedAbilityIds = unlockedAbilities
		};
	}

	/// <inheritdoc />
	public async Task<bool> LearnAbilityAsync(
		Ulid entityId,
		Ulid abilityId,
		CancellationToken cancellationToken = default)
	{
		var entity = await this.LoadEntityAggregateAsync(entityId, cancellationToken).ConfigureAwait(false);
		if (entity is null)
		{
			return false;
		}

		var ability = await this.abilityDefinitionRepository.GetByIdAsync(
			abilityId,
			q => q.Include(a => a.LearningRequirements),
			cancellationToken).ConfigureAwait(false);

		if (ability is null)
		{
			return false;
		}

		if (!this.abilityValidationService.CanLearnAbility(entity, ability))
		{
			return false;
		}

		await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		return true;
	}

	private List<Ulid> ResolveNewlyUnlockedAbilities(Entity entity, SkillDefinition skillDef)
	{
		var unlocked = new List<Ulid>();
		foreach (var ability in skillDef.Abilities)
		{
			if (this.abilityValidationService.CanLearnAbility(entity, ability))
			{
				unlocked.Add(ability.Id);
			}
		}

		return unlocked;
	}

	private Task<Entity?> LoadEntityAggregateAsync(Ulid entityId, CancellationToken cancellationToken) =>
		this.entityRepository.GetByIdAsync(
			entityId,
			q => q.Include(e => e.Skills)
				.ThenInclude(s => s.InstancedSkill)
				.Include(e => e.Stats)
				.ThenInclude(s => s.InstancedStat),
			cancellationToken);
}
