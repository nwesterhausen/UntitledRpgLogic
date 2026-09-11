namespace UntitledRpgLogic.Core.Materials;

/// <summary>
///     Value object describing the physical and structural characteristics of a material.
/// </summary>
/// <remarks>Owned by <see cref="MaterialDefinition" />.</remarks>
public record MechanicalProperties
{
	/// <summary>
	///     Initializes a new default instance of the <see cref="MechanicalProperties" /> record.
	/// </summary>
	public MechanicalProperties()
	{
	}

	/// <summary>
	///     The density of the material in g/cm³ (or kg/m³).
	/// </summary>
	public required float Density { get; init; }

	/// <summary>
	///     Resistance to scratching and surface indentation (Mohs or Janka scale).
	///     Affects edge retention and penetration resistance. Null for fluids.
	/// </summary>
	public float? Hardness { get; init; }

	/// <summary>
	///     Ability to absorb mechanical energy and deform without fracturing.
	///     Affects weapon/armor durability and shatter resistance. Null for fluids.
	/// </summary>
	public float? Toughness { get; init; }

	/// <summary>
	///     Resistance to elastic bending deformation (stiffness / Young's modulus).
	///     Null for fluids.
	/// </summary>
	public float? Stiffness { get; init; }

	/// <summary>
	///     Ability to be hammered, rolled, or pressed without breaking.
	///     Affects forging and smithing recipes. Null for fluids.
	/// </summary>
	public float? Malleability { get; init; }

	/// <summary>
	///     Fluid resistance to flow. Null for solid matter.
	/// </summary>
	public float? Viscosity { get; init; }

	/// <summary>
	///     Fluid tendency to minimize surface area (droplet formation and capillary action). Null for solids.
	/// </summary>
	public float? SurfaceTension { get; init; }

	/// <summary>
	///     Tendency of a fluid to adhere to external solid surfaces (coatings, wetness). Null for solids.
	/// </summary>
	public float? Adhesion { get; init; }
}
