using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     A transformation effect, altering a character's state or abilities.
/// </summary>
public record TransformationEffect : Effect
{
	/// <summary>
	///     Initializes default base values.
	/// </summary>
	[SetsRequiredMembers]
	public TransformationEffect() => this.EffectType = EffectType.Transformation;

	/// <summary>
	///     Initializes a new <see cref="Effect" /> with <see cref="EffectType.Transformation" />.
	/// </summary>
	[SetsRequiredMembers]
	public TransformationEffect(Name name) : base(name, EffectType.Transformation)
	{
	}
}
