using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     An interface that defines the basic properties of an item in the game.
/// </summary>
public interface IItem : IHasIdentifier, IHasName, IHasQuality, IIsCrafted, IHasDimensions
{
	/// <summary>
	///     The type of the item. This defines the general category of the item, such as Weapon, Armor, etc.
	/// </summary>
	public ItemType ItemType { get; }

	/// <summary>
	///     The subtype of the item. This can be used to further categorize the item within its type.
	/// </summary>
	public ItemSubtype ItemSubtype { get; }

	/// <summary>
	/// 	 The primary material the item is made from. This can affect the item's properties, such as weight, durability, and
	/// 	 magical attributes.
	/// </summary>
	public IMaterial PrimaryMaterial { get; }
}
