namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
/// Properties related to the mechanical characteristics of a material.
/// </summary>
public class MechanicalPropertiesConfig
{
	/// <summary>
	/// The density of the material in kg/m^3.
	/// </summary>
	public float? Density { get; set; }

	/// <summary>
	/// A relative measure of resistance to scratching and indentation.
	/// Affects edge retention and armor penetration.
	/// </summary>
	public float? Hardness { get; set; }

	/// <summary>
	/// A relative measure of a material's ability to absorb energy and deform without fracturing.
	/// Affects durability and resistance to chipping/shattering.
	/// </summary>
	public float? Toughness { get; set; }

	/// <summary>
	/// A relative measure of a material's resistance to elastic deformation (bending).
	/// A high value means the material is very rigid.
	/// </summary>
	public float? Stiffness { get; set; }

	/// <summary>
	/// A relative measure of a material's ability to be hammered or pressed into shape without breaking.
	/// Primarily affects crafting possibilities.
	/// </summary>
	public float? Malleability { get; set; }

	/// <summary>
	/// A measure of a fluid's resistance to flow. Null for solids.
	/// </summary>
	public float? Viscosity { get; set; }

	/// <summary>
	/// The tendency of a liquid to shrink into the minimum surface area possible. Null for solids.
	/// Affects coating and droplet formation.
	/// </summary>
	public float? SurfaceTension { get; set; }

	/// <summary>
	/// A relative measure of how well a liquid sticks to other surfaces. Null for solids.
	/// </summary>
	public float? Adhesion { get; set; }
}
