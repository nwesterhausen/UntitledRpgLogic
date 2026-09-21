using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class MechanicalPropertiesDto
{
	[JsonPropertyName("density")] public float Density { get; init; }

	[JsonPropertyName("hardness")] public float? Hardness { get; init; }

	[JsonPropertyName("toughness")] public float? Toughness { get; init; }

	[JsonPropertyName("stiffness")] public float? Stiffness { get; init; }

	[JsonPropertyName("malleability")] public float? Malleability { get; init; }

	[JsonPropertyName("viscosity")] public float? Viscosity { get; init; }

	[JsonPropertyName("surface_tension")] public float? SurfaceTension { get; init; }

	[JsonPropertyName("adhesion")] public float? Adhesion { get; init; }

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
