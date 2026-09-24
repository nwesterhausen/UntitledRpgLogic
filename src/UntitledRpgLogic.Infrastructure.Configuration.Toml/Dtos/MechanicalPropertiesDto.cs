using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class MechanicalPropertiesDto
{
	/// <summary>
	///     The density of the material in g/cm³ (or kg/m³).
	/// </summary>
	public float Density { get; init; }

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

	/// <summary>
	///     Convert the DTO into <see cref="MechanicalProperties" />
	/// </summary>
	/// <returns>The <see cref="MechanicalProperties" /> version of this</returns>
	public MechanicalProperties ToModel() => new()
	{
		Density = this.Density,
		Hardness = this.Hardness,
		Toughness = this.Toughness,
		Stiffness = this.Stiffness,
		Malleability = this.Malleability,
		Viscosity = this.Viscosity,
		SurfaceTension = this.SurfaceTension,
		Adhesion = this.Adhesion
	};

	/// <summary>
	///     Create a DTO for writing a TOML config file from the model
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException">if model is null</exception>
	public static MechanicalPropertiesDto FromModel(MechanicalProperties model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new MechanicalPropertiesDto
		{
			Density = model.Density,
			Hardness = model.Hardness,
			Toughness = model.Toughness,
			Stiffness = model.Stiffness,
			Malleability = model.Malleability,
			Viscosity = model.Viscosity,
			SurfaceTension = model.SurfaceTension,
			Adhesion = model.Adhesion
		};
	}
}
