namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines the general category of an item.
/// </summary>
public enum ItemType
{
	/// <summary>
	///     Represents an undefined or unclassified item type.
	/// </summary>
	None = 0,

	/// <summary>
	///     Miscellaneous items with little to no practical use, often sold for a small amount of currency.
	/// </summary>
	Junk = 8,

	/// <summary>
	///     Items primarily used for combat to inflict damage.
	/// </summary>
	Weapon = 1,

	/// <summary>
	///     Protective gear worn on the body to reduce damage taken.
	/// </summary>
	Armor = 2,

	/// <summary>
	///     Items held in one hand to block attacks and provide defensive bonuses.
	/// </summary>
	Shield = 3,

	/// <summary>
	///     Clothing or accessories worn for aesthetic or minor stat-boosting purposes, not primarily for defense.
	/// </summary>
	Apparel = 4,

	/// <summary>
	///     Items that are used up on activation, providing temporary effects, healing, or other benefits.
	/// </summary>
	Consumable = 5,

	/// <summary>
	///     Projectiles or charges required for ranged weapons.
	/// </summary>
	Ammunition = 6,

	/// <summary>
	///     Tools or items that assist in crafting, repairing, or other non-combat activities.
	/// </summary>
	Tool = 7
}
