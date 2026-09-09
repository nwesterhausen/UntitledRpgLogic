namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Identifies the functional role or placement of a material within an item.
/// </summary>
public enum MaterialSlot
{
	/// <summary>
	/// 	This is the main material used: blade, axe head, breastplate body, etc.
	/// </summary>
	Primary = 0,

	/// <summary>
	/// 	This is the 2nd most used material: grip wrap, hilt, armor lining, etc.
	/// </summary>
	Secondary = 1,

	/// <summary>
	/// 	This is the 3rd most used material: pommel, guard, rivets, buckles, etc.
	/// </summary>
	Tertiary = 2,

	/// <summary>
	/// 	An embedded gemstone or other jewel used for additional properties.
	/// </summary>
	Gemstone = 3,

	/// <summary>
	/// 	A coating of the item: plating, poison, varnish, etc.
	/// </summary>
	Coating = 4
}
