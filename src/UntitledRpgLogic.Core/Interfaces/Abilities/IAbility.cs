using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Effects;

namespace UntitledRpgLogic.Core.Interfaces.Abilities;

/// <summary>
///     Interface that describes an ability in the RPG logic. An ability can be anything that is player-activated, such
///     as a weapon attack, a spell, or a special skill. (Not to be confused with <see cref="ISkill" /> which actually
///     describes
///     skill levels and their effects.)
/// </summary>
public interface IAbility : IHasIdentifier, IHasName, IHasTooltipString
{
	/// <summary>
	/// Gets the collection of effects that this ability produces when active or equipped.
	/// </summary>
	public IEnumerable<IEffect> Effects { get; }

	/// <summary>
	///     The resource stat used by this ability, if any (e.g., mana, stamina). Special abilities may not use any resource.
	/// </summary>
	public IStat? ResourceUsed { get; }

	/// <summary>
	///     The cost in the resource stat to use this ability. If <see cref="ResourceUsed" /> is null, this should be 0.
	/// </summary>
	public int ResourceCost { get; }
}
