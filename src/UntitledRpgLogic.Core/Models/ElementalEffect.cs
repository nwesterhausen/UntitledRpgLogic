using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// An effect that affects an Ambient Index or "ambient", i.e. a measurable property of the world (e.g., Temperature, Gravity, Weather).
/// </summary>
public class ElementalEffect : Effect
{
	/// <summary>
	/// An effect that affects an ambient.
	/// </summary>
	public ElementalEffect() => this.EffectType = EffectType.Elemental;
}