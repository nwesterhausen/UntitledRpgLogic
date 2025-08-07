namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Possible types of fracturing for materials
/// </summary>
public enum FractureType
{
	/// <summary>
	///     An unspecified fraction type (None).
	/// </summary>
	None = 0,

	/// <summary>
	/// Shatters or powders easily when struck. The vast majority of minerals are brittle.
	/// </summary>
	/// <remarks>
	/// Poor Edge Retention. A brittle material can be sharpened (if hard) but the edge will chip and flake away very easily under impact.
	/// </remarks>
	Brittle,

	/// <summary>
	/// Can be hammered into thin sheets without breaking.
	/// </summary>
	/// <remarks>
	/// Excellent Toughness. Doesn't hold a sharp edge as well as a hard material, but it will bend or deform instead of breaking. The ideal property for armor.
	/// </remarks>
	Malleable,

	/// <summary>
	/// Can be stretched into a wire. Similar to malleable.
	/// </summary>
	/// <remarks>
	/// Excellent Toughness. Indicates the material will deform under stress. Essential for wires, chains, and flexible components.
	/// </remarks>
	Ductile,

	/// <summary>
	/// Can be cut into shavings with a knife.
	/// </summary>
	/// <remarks>
	/// Moderate Toughness, Low Hardness. Softer than brittle materials. Can be worked easily but won't hold an edge against harder materials.
	/// </remarks>
	Sectile,

	/// <summary>
	/// The material can be bent, but it stays bent after the pressure is released.
	/// </summary>
	/// <remarks>
	/// A form of Plasticity. Useful for items that need to be custom-fit or shaped once and then hold that shape.
	/// </remarks>
	Flexible,

	/// <summary>
	/// The material can be bent, and it springs back to its original shape when pressure is released.
	/// </summary>
	/// <remarks>
	/// High Resilience. Doesn't hold a rigid edge, but is extremely resistant to permanent damage from bending forces. Perfect for things like springs or bow limbs.
	/// </remarks>
	Elastic
}
