using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Core.Abilities;

/// <summary>
///     Represents the outcome of an ability invocation executed by a coordinator service.
/// </summary>
public record AbilityExecutionResult
{
	/// <summary>
	///     Indicates whether the ability was successfully activated and its active effects processed.
	/// </summary>
	public bool IsSuccess { get; init; }

	/// <summary>
	///     Categorical execution status (e.g., Success, InsufficientResources, RequirementsUnmet, Backfired).
	/// </summary>
	public AbilityCastStatus Status { get; init; } = AbilityCastStatus.Success;

	/// <summary>
	///     Descriptive explanation or localized failure reason if <see cref="IsSuccess" /> is false.
	/// </summary>
	public string Message { get; init; } = string.Empty;

	/// <summary>
	///     The stat costs that were actually deducted from the caster.
	/// </summary>
	public IReadOnlyCollection<StatCost> ConsumedCosts { get; init; } = [];

	/// <summary>
	///     Detailed breakdown of net effects applied to target entities (e.g., damage, healing).
	/// </summary>
	public IReadOnlyCollection<TargetEffectOutcome> TargetOutcomes { get; init; } = [];

	/// <summary>
	///     Create a result for a "failed" ability execution.
	/// </summary>
	/// <param name="status">Root reason for failure.</param>
	/// <param name="message">Exlanation for failure.</param>
	/// <returns></returns>
	public static AbilityExecutionResult Failed(AbilityCastStatus status, string message) => new()
	{
		IsSuccess = false, Status = status, Message = message
	};
}
