using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     Detailed breakdown of net effects applied to the target of an effect.
/// </summary>
public record TargetEffectOutcome
{
	/// <summary>
	///     Construct a new TargetEffectOutcome
	/// </summary>
	public TargetEffectOutcome(Ulid TargetEntityId,
		Ulid EffectId,
		float AppliedValue,
		bool WasMitigated)
	{
		this.TargetEntityId = TargetEntityId;
		this.EffectId = EffectId;
		this.AppliedValue = AppliedValue;
		this.WasMitigated = WasMitigated;
	}

	/// <summary>
	///     <see cref="Entity.Id" /> of the target
	/// </summary>
	public Ulid TargetEntityId { get; init; }

	/// <summary>
	///     <see cref="Effect.Id" /> of the effect
	/// </summary>
	public Ulid EffectId { get; init; }

	/// <summary>
	///     Amount of damage, healing, change, etc. applied
	/// </summary>
	public float AppliedValue { get; init; }

	/// <summary>
	///     Whether the effect was mitigated at all.
	/// </summary>
	public bool WasMitigated { get; init; }
}
