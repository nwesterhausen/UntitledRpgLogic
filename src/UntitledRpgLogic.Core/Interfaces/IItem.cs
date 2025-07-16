using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     An interface that defines the basic properties of an item in the game.
/// </summary>
public interface IItem : IHasGuid, IHasName, IHasQuality, IIsCrafted, IHasDimensions
{
	/// <summary>
	///     The type of the item. This defines the general category of the item, such as Weapon, Armor, etc.
	/// </summary>
	public ItemType ItemType { get; }

	/// <summary>
	///     The subtype of the item. This can be used to further categorize the item within its type.
	/// </summary>
	public ItemSubtype ItemSubtype { get; }
}
