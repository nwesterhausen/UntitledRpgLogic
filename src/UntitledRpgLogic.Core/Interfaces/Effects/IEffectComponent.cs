using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Common;

namespace UntitledRpgLogic.Core.Interfaces.Effects;

/// <summary>
///     Represents a single, modular component that defines a specific aspect or property of a game effect or spell.
///     These components are the building blocks that compose complex effects.
/// </summary>
public interface IEffectComponent : IHasIdentifier, IHasName
{
	/// <summary>
	///     Gets the type of this effect component, useful for discrimination when processing.
	/// </summary>
	public EffectComponentType ComponentType { get; }
}
