using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// An effect that moves an entity.
/// </summary>
public class MovementEffect : Effect
{
	/// <summary>
	/// An effect that moves an entity.
	/// </summary>
	public MovementEffect() => this.EffectType = EffectType.Movement;
}