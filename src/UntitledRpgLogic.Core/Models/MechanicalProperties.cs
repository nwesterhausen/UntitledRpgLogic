using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Properties related to the mechanical characteristics of a material.
/// </summary>
public record MechanicalProperties
{
	/// <summary>
	///		Constructs a new default record.
	/// </summary>
	public MechanicalProperties()
	{}

	/// <summary>
	///     The density of the material in cm/m^3.
	/// </summary>
	public required float Density { get; init; }

	/// <summary>
	///     For solids:
	///     A relative measure of resistance to scratching and indentation.
	///     Affects edge retention and armor penetration.
	/// </summary>
	/// <remarks>
	///		For metals, use the Mohs scale. For wood, use the Janka scale value / 400.
	///		The default value is similar to copper or oak wood.
	/// </remarks>
	public float? Hardness { get; init; }

	/// <summary>
	///     For solids:
	///     A relative measure of a material's ability to absorb energy and deform without fracturing.
	///     Affects durability and resistance to chipping/shattering.
	/// </summary>
	public float? Toughness { get; init; }

	/// <summary>
	///     For solids:
	///     A relative measure of a material's resistance to elastic deformation (bending).
	///     A high value means the material is very rigid.
	/// </summary>
	public float? Stiffness { get; init; }

	/// <summary>
	///     For solids:
	///     A relative measure of a material's ability to be hammered or pressed into shape without breaking.
	///     Primarily affects crafting possibilities.
	/// </summary>
	public float? Malleability { get; init; }

	/// <summary>
	///     For liquids and gases:
	///     A measure of a fluid's resistance to flow. Null for solids.
	/// </summary>
	public float? Viscosity { get; init; }

	/// <summary>
	///     For liquids and gases:
	///     The tendency of a liquid to shrink into the minimum surface area possible. Null for solids.
	///     Affects coating and droplet formation.
	/// </summary>
	public float? SurfaceTension { get; init; }

	/// <summary>
	///     For liquids and gases:
	///     A relative measure of how well a liquid sticks to other surfaces. Null for solids.
	/// </summary>
	public float? Adhesion { get; init; }

	/// <summary>
	///		A unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; init; }
}
