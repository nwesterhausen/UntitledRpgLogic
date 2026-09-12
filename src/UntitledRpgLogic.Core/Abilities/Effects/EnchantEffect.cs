using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     An effect that imbues an object-type entity with one or more effects.
/// </summary>
public record EnchantEffect : Effect
{
	/// <summary>
	///     Initializes default base values.
	/// </summary>
	[SetsRequiredMembers]
	public EnchantEffect() => this.EffectType = EffectType.Enchant;

	/// <summary>
	///     Initializes a new <see cref="Effect" /> with <see cref="EffectType.Enchant" />.
	/// </summary>
	[SetsRequiredMembers]
	public EnchantEffect(Name name) : base(name, EffectType.Enchant)
	{
	}
}
