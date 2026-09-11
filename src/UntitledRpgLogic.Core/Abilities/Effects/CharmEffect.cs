using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     An effect that applies a status effect to an entity.
/// </summary>
public record CharmEffect : Effect
{

	/// <summary>
	/// 	Initializes default base values.
	/// </summary>
	public CharmEffect()
	{
		this.EffectType = EffectType.Charm;
	}

	/// <summary>
	/// 	Initializes a new <see cref="Effect" /> with <see cref="EffectType.Charm" />.
	/// </summary>
	public CharmEffect(Name name) : base(name, EffectType.Charm)
	{
	}
}
