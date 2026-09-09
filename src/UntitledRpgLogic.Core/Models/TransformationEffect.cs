using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     A transformation effect, altering a character's state or abilities.
/// </summary>
public record TransformationEffect : Effect
{
	/// <summary>
	/// 	Initializes default base values.
	/// </summary>
	public TransformationEffect()
	{
		this.EffectType = EffectType.Transformation;
	}

	/// <summary>
	/// 	Initializes a new <see cref="Effect" /> with <see cref="EffectType.Transformation" />.
	/// </summary>
	public TransformationEffect(Name name) : base(name, EffectType.Transformation)
	{
	}
}
