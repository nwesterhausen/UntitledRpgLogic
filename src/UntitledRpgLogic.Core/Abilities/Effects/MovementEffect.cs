using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     An effect that moves an entity.
/// </summary>
public record MovementEffect : Effect
{
	/// <summary>
	///     Initializes default base values.
	/// </summary>
	public MovementEffect() => this.EffectType = EffectType.Movement;

	/// <summary>
	///     Initializes a new <see cref="Effect" /> with <see cref="EffectType.Movement" />.
	/// </summary>
	public MovementEffect(Name name) : base(name, EffectType.Movement)
	{
	}
}
