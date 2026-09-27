using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Defines the requirements for a specific biome.
/// </summary>
public sealed record BiomeDefinition : IDefined
{
	// Climate Thresholds
	/// <summary>
	///     Minimum temperature requirement for the biome
	/// </summary>
	public float MinTemperature { get; init; }

	/// <summary>
	///     Maximum temperature biome can be located in
	/// </summary>
	public float MaxTemperature { get; init; }

	/// <summary>
	///     Minimum amount of moisture/humidity for the biome
	/// </summary>
	public float MinMoisture { get; init; }

	/// <summary>
	///     Maximum amount of moisture/humidty for the biome
	/// </summary>
	public float MaxMoisture { get; init; }

	/// <summary>
	///     Minimum elevation to find the biome
	/// </summary>
	public float MinElevation { get; init; }

	/// <summary>
	///     Maximum elevation to find the biome
	/// </summary>
	public float MaxElevation { get; init; }

	// Optional Overrides
	/// <summary>
	///     Minimum amount of tectonic activity (volcanism) for biome
	/// </summary>
	public float MinTectonic { get; init; }

	/// <summary>
	///     Minimum amount of rainfall/water accumulation for the biome
	/// </summary>
	public float MinWaterAccumulation { get; init; }

	/// <summary>
	///     The required bedrock formation for the biome. Defaults to a non-restrictive "any."
	/// </summary>
	public GeologicFormation RequiredBedrockFormation { get; init; } = GeologicFormation.Any;

	// Material ULID References
	/// <summary>
	///     Define a surface liquid material override. By default, will be fresh or saltwater based on if in ocean.
	/// </summary>
	public Ulid? SurfaceLiquidId { get; init; }

	/// <summary>
	///     Define a rain liquid material override. By default, this will be assigned freshwater
	/// </summary>
	public Ulid? RainLiquidId { get; init; }

	/// <summary>
	///     Define a soil material override. By default, the normal dirt for the underlying bedrock will be used.
	/// </summary>
	public Ulid? SoilMaterialId { get; init; }

	/// <inheritdoc />
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; } = Ulid.NewUlid();

	/// <inheritdoc />
	public Name Name { get; init; } = Name.Empty;

	/// <inheritdoc />
	[MaxLength(1024)]
	public string Description { get; init; } = string.Empty;
}
