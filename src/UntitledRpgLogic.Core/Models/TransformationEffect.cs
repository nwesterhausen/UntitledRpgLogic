using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// A transformation effect, altering a character's state or abilities.
/// </summary>
public class TransformationEffect : Effect
{
	/// <summary>
	/// A transformation effect, altering a character's state or abilities.
	/// </summary>
	public TransformationEffect() => this.EffectType = EffectType.Transformation;
}