using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.Abilities;

/// <summary>
///     Contract for the coordinator that manages ability use.
/// </summary>
public interface IAbilityCoordinatorService
{
	/// <summary>
	///     Executes an ability cast from a source entity toward target entities,
	///     deducting resource costs, evaluating failure chances, applying effects,
	///     and persisting stat changes atomically.
	/// </summary>
	/// <param name="targetEntityIds">
	///     List of targets to use the ability on. Ignored if <see cref="TargetingType.Self"/> or <see cref="TargetingType.None"/>
	/// </param>
	/// <param name="cancellationToken">A cancellation token to cancel the task.</param>
	/// <param name="casterEntityId"><see cref="Entity.Id"/> of the casting entity</param>
	/// <param name="abilityId"><see cref="AbilityDefinition.Id"/> of the ability to cast.</param>
	public Task<AbilityExecutionResult> CastAbilityAsync(
		Ulid casterEntityId,
		Ulid abilityId,
		IReadOnlyCollection<Ulid> targetEntityIds,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Validates whether an entity meets casting preconditions and has sufficient stat resources.
	/// </summary>
	/// <param name="abilityId"><see cref="AbilityDefinition.Id"/> of the ability to check.</param>
	/// <param name="cancellationToken">A cancellation token to cancel the task.</param>
	/// <param name="casterEntityId"><see cref="Entity.Id"/> of the entity attempting to cast.</param>
	public Task<bool> CanCastAbilityAsync(
		Ulid casterEntityId,
		Ulid abilityId,
		CancellationToken cancellationToken = default);
}
