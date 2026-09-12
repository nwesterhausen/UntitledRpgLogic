using System;

namespace UntitledRpgLogic.Core.Abilities;

using UntitledRpgLogic.Core.Entities;

/// <summary>
///     Pure domain service verifying stat resources, learning prerequisites, and casting eligibility.
/// </summary>
public interface IAbilityValidationService
{
	/// <summary>
	///     Verifies if an entity has sufficient resources to pay the stat costs of an ability.
	/// </summary>
	public bool CanAffordCosts(Entity caster, AbilityDefinition ability);

	/// <summary>
	///     Evaluates casting requirements and calculates whether the cast backfires based on failure influences.
	/// </summary>
	public bool EvaluateCastingSuccess(Entity caster, AbilityDefinition ability, Random random);

	/// <summary>
	///     Checks if an entity satisfies all prerequisite conditions to learn an ability.
	/// </summary>
	public bool CanLearnAbility(Entity entity, AbilityDefinition ability);
}
