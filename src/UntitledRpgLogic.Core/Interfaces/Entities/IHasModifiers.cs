using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Interfaces.Entities;

/// <summary>
///     Defines an interactive entity subject to active status effects, buffs, or debuffs.
/// </summary>
public interface IHasModifiers
{
	/// <summary>
	///     Gets the collection of temporary or permanent status modifiers actively applied to this entity.
	/// </summary>
	ICollection<AppliedModifier> AppliedModifiers { get; }
}
