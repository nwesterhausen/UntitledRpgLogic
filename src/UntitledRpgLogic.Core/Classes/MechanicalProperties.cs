using UntitledRpgLogic.Core.Configuration;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
/// Represents the mechanical properties of a material, such as density, hardness, toughness, and more.
/// </summary>
public record MechanicalProperties
{
	/// <summary>
	/// Gets the density of the material (kg/m³).
	/// </summary>
	public float? Density { get; }

	/// <summary>
	/// Gets the hardness of the material.
	/// </summary>
	public float? Hardness { get; }

	/// <summary>
	/// Gets the toughness of the material.
	/// </summary>
	public float? Toughness { get; }

	/// <summary>
	/// Gets the stiffness of the material.
	/// </summary>
	public float? Stiffness { get; }

	/// <summary>
	/// Gets the malleability of the material.
	/// </summary>
	public float? Malleability { get; }

	/// <summary>
	/// Gets the viscosity of the material.
	/// </summary>
	public float? Viscosity { get; }

	/// <summary>
	/// Gets the surface tension of the material.
	/// </summary>
	public float? SurfaceTension { get; }

	/// <summary>
	/// Gets the adhesion of the material.
	/// </summary>
	public float? Adhesion { get; }

	private MechanicalProperties(float? density, float? hardness, float? toughness, float? stiffness, float? malleability, float? viscosity, float? surfaceTension, float? adhesion)
	{
		Density = density;
		Hardness = hardness;
		Toughness = toughness;
		Stiffness = stiffness;
		Malleability = malleability;
		Viscosity = viscosity;
		SurfaceTension = surfaceTension;
		Adhesion = adhesion;
	}

	/// <summary>
	/// Creates a <see cref="MechanicalProperties"/> instance from a configuration object.
	/// </summary>
	/// <param name="config">The configuration object containing mechanical property values.</param>
	/// <returns>A new <see cref="MechanicalProperties"/> instance.</returns>
	public static MechanicalProperties FromConfig(MechanicalPropertiesConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));
		return new MechanicalProperties(config.Density, config.Hardness, config.Toughness, config.Stiffness, config.Malleability, config.Viscosity, config.SurfaceTension, config.Adhesion);
	}
}
