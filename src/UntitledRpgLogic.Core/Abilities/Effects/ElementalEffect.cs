using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     An effect that affects an Ambient Index or "ambient", i.e. a measurable property of the world (e.g., Temperature,
///     Gravity, Weather).
/// </summary>
public record ElementalEffect : Effect
{
	/// <summary>
	///     Initializes default base values.
	/// </summary>
	public ElementalEffect() => this.EffectType = EffectType.Elemental;

	/// <summary>
	///     Initializes a new <see cref="Effect" /> with <see cref="EffectType.Elemental" />.
	/// </summary>
	public ElementalEffect(Name name) : base(name, EffectType.Elemental)
	{
	}
}
