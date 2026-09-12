using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Skills;

namespace UntitledRpgLogic.Core.Progression;

/// <summary>
///     Contract for a service which coordinates progression (e.g. skill leveling).
/// </summary>
public interface IProgressionCoordinatorService
{
	/// <summary>
	///     Awards experience points to an entity's skill, evaluates potential level-up thresholds,
	///     updates apparent values, and commits the changes.
	/// </summary>
	/// <param name="entityId"><see cref="Entity.Id" /> of the entity owning the skill that is gaining experience</param>
	/// <param name="skillDefinitionId"><see cref="SkillDefinition.Id" /> of the skill to award experience to</param>
	/// <param name="experiencePoints">Amount of experience points to award to the skill</param>
	/// <param name="cancellationToken">A cancellation token to cancel the task.</param>
	public Task<ProgressionResult> AwardExperienceAsync(
		Ulid entityId,
		Ulid skillDefinitionId,
		int experiencePoints,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Checks learning requirements and permanently binds an ability to an entity.
	/// </summary>
	/// <param name="entityId"><see cref="Entity.Id" /> of the entity to learn the ability</param>
	/// <param name="abilityId"><see cref="AbilityDefinition.Id" /> to learn</param>
	/// <param name="cancellationToken">A cancellation token to cancel the task.</param>
	public Task<bool> LearnAbilityAsync(
		Ulid entityId,
		Ulid abilityId,
		CancellationToken cancellationToken = default);
}
