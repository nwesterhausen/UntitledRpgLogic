using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// An effect that imbues an object-type entity with one or more effects.
/// </summary>
public class EnchantEffect : Effect
{
	/// <summary>
	/// An effect that enchants an object.
	/// </summary>
	public EnchantEffect() => this.EffectType = EffectType.Enchant;
}
