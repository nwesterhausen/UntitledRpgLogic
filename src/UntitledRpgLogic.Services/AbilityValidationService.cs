using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Progression;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Pure domain service evaluating costs, requirements, and casting success for abilities.
/// </summary>
public sealed class AbilityValidationService : IAbilityValidationService
{
	private readonly IRandom random;

	/// <summary>
	///     Create the <see cref="AbilityValidationService" />
	/// </summary>
	/// <param name="random"></param>
	public AbilityValidationService(
		IRandom random) => this.random = random;

	/// <inheritdoc />
	public bool CanAffordCosts(Entity caster, AbilityDefinition ability)
	{
		ArgumentNullException.ThrowIfNull(caster);
		ArgumentNullException.ThrowIfNull(ability);

		if (ability.StatCosts.Count == 0)
		{
			return true;
		}

		var statLookup = caster.Stats
			.Where(s => s.InstancedStat is not null)
			.ToDictionary(s => s.InstancedStat!.DefinitionId, s => s.InstancedStat!);

		foreach (var cost in ability.StatCosts)
		{
			if (!statLookup.TryGetValue(cost.StatId, out var stat))
			{
				return false;
			}

			if (stat.ApparentValue < cost.Amount)
			{
				return false;
			}
		}

		return true;
	}

	/// <inheritdoc />
	public bool EvaluateCastingSuccess(Entity caster, AbilityDefinition ability)
	{
		ArgumentNullException.ThrowIfNull(caster);
		ArgumentNullException.ThrowIfNull(ability);

		if (!this.CanAffordCosts(caster, ability))
		{
			return false;
		}

		// Check hard casting requirements
		foreach (var req in ability.CastingRequirements)
		{
			if (!this.IsRequirementSatisfied(caster, req))
			{
				return false;
			}
		}

		// Evaluate failure influences (backfire / fumble rolls)
		foreach (var influence in ability.FailureInfluences)
		{
			var currentVal = this.GetEntityValue(caster, influence.RequirementType, influence.RequiredEntityId);
			if (currentVal >= influence.AmountAlwaysSucceed)
			{
				continue;
			}

			var shortfall = Math.Max(0f, influence.AmountNeeded - currentVal);
			var failureChance = Math.Clamp(shortfall * influence.InfluenceScale, 0f, 1f);

			if (failureChance > 0f && this.random.NextDouble() < failureChance)
			{
				return false;
			}
		}

		return true;
	}

	/// <inheritdoc />
	public bool CanLearnAbility(Entity entity, AbilityDefinition ability)
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(ability);

		foreach (var req in ability.LearningRequirements)
		{
			if (!this.IsRequirementSatisfied(entity, req))
			{
				return false;
			}
		}

		return true;
	}

	private bool IsRequirementSatisfied(Entity entity, CastingRequirement req)
	{
		var currentVal = this.GetEntityValue(entity, req.RequirementType, req.RequiredEntityId);
		return currentVal >= req.AmountNeeded;
	}

	private bool IsRequirementSatisfied(Entity entity, LearningRequirement req)
	{
		var currentVal = this.GetEntityValue(entity, req.RequirementType, req.RequiredEntityId);
		return currentVal >= req.AmountNeeded;
	}

	private float GetEntityValue(Entity entity, RequirementType type, Ulid definitionId) =>
		type switch
		{
			RequirementType.Stat =>
				entity.Stats.FirstOrDefault(s => s.InstancedStat?.DefinitionId == definitionId)?.InstancedStat
					?.ApparentValue ?? 0f,

			RequirementType.SkillLevel =>
				entity.Skills.FirstOrDefault(s => s.InstancedSkill?.DefinitionId == definitionId)?.InstancedSkill
					?.Level ?? 0f,

			RequirementType.OwnedItem =>
				entity.Inventory?.Items.Where(i => i.DefinitionId == definitionId).Sum(i => i.Quantity) ?? 0f,

			_ => 0f
		};
}
