using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Application coordination service managing database loading, validation,
///     resource deduction, effect execution, and transaction commits for ability invocations[cite: 3].
/// </summary>
public sealed class AbilityCoordinatorService : IAbilityCoordinatorService
{
	private readonly IEntityRepository<AbilityDefinition, Ulid> abilityRepository;
	private readonly IEffectApplicationService effectApplicationService;
	private readonly IEntityRepository<Entity, Ulid> entityRepository;
	private readonly IUnitOfWork unitOfWork;
	private readonly IAbilityValidationService validationService;

	/// <summary>
	///     Initializes a new instance of the <see cref="AbilityCoordinatorService" /> class.
	/// </summary>
	/// <param name="unitOfWork">The transaction and persistence coordinator.</param>
	/// <param name="entityRepository">The repository for entity aggregates.</param>
	/// <param name="abilityRepository">The repository for ability definitions.</param>
	/// <param name="validationService">The pure domain service checking requirements and costs.</param>
	/// <param name="effectApplicationService">The domain service applying active effects to target entities.</param>
	public AbilityCoordinatorService(
		IUnitOfWork unitOfWork,
		IEntityRepository<Entity, Ulid> entityRepository,
		IEntityRepository<AbilityDefinition, Ulid> abilityRepository,
		IAbilityValidationService validationService,
		IEffectApplicationService effectApplicationService)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
		this.abilityRepository = abilityRepository ?? throw new ArgumentNullException(nameof(abilityRepository));
		this.validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
		this.effectApplicationService = effectApplicationService ??
		                                throw new ArgumentNullException(nameof(effectApplicationService));
	}

	/// <inheritdoc />
	public async Task<bool> CanCastAbilityAsync(
		Ulid casterEntityId,
		Ulid abilityId,
		CancellationToken cancellationToken = default)
	{
		var caster = await this.LoadEntityWithStatsAndSkillsAsync(casterEntityId, cancellationToken)
			.ConfigureAwait(false);
		if (caster is null)
		{
			return false;
		}

		var ability = await this.abilityRepository.GetByIdAsync(
			abilityId,
			cancellationToken,
			a => a.StatCosts,
			a => a.CastingRequirements,
			a => a.FailureInfluences).ConfigureAwait(false);

		if (ability is null)
		{
			return false;
		}

		return this.validationService.CanAffordCosts(caster, ability);
	}

	/// <inheritdoc />
	public async Task<AbilityExecutionResult> CastAbilityAsync(
		Ulid casterEntityId,
		Ulid abilityId,
		IReadOnlyCollection<Ulid> targetEntityIds,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(targetEntityIds);

		var ability = await this.LoadAbilityWithEffectsAsync(abilityId, cancellationToken).ConfigureAwait(false);
		if (ability is null)
		{
			return AbilityExecutionResult.Failed(AbilityCastStatus.InvalidTarget,
				$"Ability '{abilityId}' was not found.");
		}

		var caster = await this.LoadEntityWithStatsAndSkillsAsync(casterEntityId, cancellationToken)
			.ConfigureAwait(false);
		if (caster is null)
		{
			return AbilityExecutionResult.Failed(AbilityCastStatus.InvalidTarget,
				$"Caster entity '{casterEntityId}' was not found.");
		}

		if (!this.validationService.CanAffordCosts(caster, ability))
		{
			return AbilityExecutionResult.Failed(AbilityCastStatus.InsufficientResources,
				"Insufficient resources to activate ability.");
		}

		if (!this.validationService.EvaluateCastingSuccess(caster, ability))
		{
			return AbilityExecutionResult.Failed(AbilityCastStatus.Backfired,
				"Ability backfired or failed requirement checks during invocation.");
		}

		var distinctTargetIds = targetEntityIds.Distinct().ToList();
		var targets = await this.entityRepository.GetByIdsAsync(
			distinctTargetIds,
			cancellationToken,
			e => e.Stats).ConfigureAwait(false);

		await this.unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			var consumedCosts = DeductCosts(caster, ability.StatCosts);
			var outcomes = this.ApplyEffectsAndComputeOutcomes(caster, targets, ability.ActiveEffects);

			await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			await this.unitOfWork.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);

			return new AbilityExecutionResult
			{
				IsSuccess = true,
				Status = AbilityCastStatus.Success,
				Message = "Ability cast successfully.",
				ConsumedCosts = consumedCosts,
				TargetOutcomes = outcomes
			};
		}
		catch
		{
			await this.unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
			throw;
		}
	}

	private static List<StatCost> DeductCosts(Entity caster, IEnumerable<StatCost> costs)
	{
		var consumedCosts = new List<StatCost>();
		foreach (var cost in costs)
		{
			var casterStat = caster.Stats
				.FirstOrDefault(s => s.InstancedStat?.DefinitionId == cost.StatId)
				?.InstancedStat;

			if (casterStat is not null)
			{
				casterStat.ApparentValue -= (int)MathF.Round(cost.Amount);
				consumedCosts.Add(cost);
			}
		}

		return consumedCosts;
	}

	private List<TargetEffectOutcome> ApplyEffectsAndComputeOutcomes(
		Entity caster,
		IReadOnlyList<Entity> targets,
		IEnumerable<Effect> effects)
	{
		var baselineValues = new Dictionary<(Ulid EntityId, string StatName), int>();
		foreach (var target in targets)
		{
			foreach (var es in target.Stats)
			{
				if (es.InstancedStat?.Definition is not null)
				{
					baselineValues[(target.Id, es.InstancedStat.Definition.Name.Singular)] =
						es.InstancedStat.ApparentValue;
				}
			}
		}

		var outcomes = new List<TargetEffectOutcome>();
		foreach (var effect in effects)
		{
			this.effectApplicationService.ApplyEffect(effect, caster, targets);

			foreach (var target in targets)
			{
				var healthStat = target.Stats
					.FirstOrDefault(s => string.Equals(s.InstancedStat?.Definition?.Name.Singular, "Health",
						StringComparison.OrdinalIgnoreCase))
					?.InstancedStat;

				if (healthStat is not null && baselineValues.TryGetValue((target.Id, "Health"), out var oldVal))
				{
					var delta = Math.Abs(healthStat.ApparentValue - oldVal);
					outcomes.Add(new TargetEffectOutcome(target.Id, effect.Id, delta, false));
					baselineValues[(target.Id, "Health")] = healthStat.ApparentValue;
				}
			}
		}

		return outcomes;
	}

	private Task<AbilityDefinition?> LoadAbilityWithEffectsAsync(Ulid abilityId, CancellationToken cancellationToken) =>
		this.abilityRepository.GetByIdAsync(
			abilityId,
			q => q.Include(a => a.StatCosts)
				.Include(a => a.CastingRequirements)
				.Include(a => a.FailureInfluences)
				.Include(a => a.ActiveEffects)
				.Include(a => a.FailureEffects),
			cancellationToken);

	private Task<Entity?> LoadEntityWithStatsAndSkillsAsync(Ulid entityId, CancellationToken cancellationToken) =>
		this.entityRepository.GetByIdAsync(
			entityId,
			q => q.Include(e => e.Stats)
				.ThenInclude(s => s.InstancedStat)
				.ThenInclude(stat => stat!.Definition)
				.Include(e => e.Skills)
				.ThenInclude(s => s.InstancedSkill),
			cancellationToken);
}
