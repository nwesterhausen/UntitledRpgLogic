using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     An effect that applies a positive or neutral impact to the target, enhancing their attributes or abilities.
/// </summary>
public record BuffEffect : Effect
{

	/// <summary>
	/// 	Initializes default base values.
	/// </summary>
	public BuffEffect()
	{
		this.EffectType = EffectType.Buff;
	}

	/// <summary>
	/// 	Initializes a new <see cref="Effect" /> with <see cref="EffectType.Buff" />.
	/// </summary>
	public BuffEffect(Name name) : base(name, EffectType.Buff)
	{
	}
}
