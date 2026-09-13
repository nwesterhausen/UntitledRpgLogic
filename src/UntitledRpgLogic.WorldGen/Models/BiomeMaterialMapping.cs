using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.WorldGen.Models;

/// <summary>
///     Defines the default ground and liquid material identifiers assigned to each biome type.
/// </summary>
public record BiomeMaterialMapping
{
	public Ulid WaterMaterialId { get; init; } = Ulid.NewUlid();
	public Ulid DeepWaterMaterialId { get; init; } = Ulid.NewUlid();
	public Ulid SandMaterialId { get; init; } = Ulid.NewUlid();
	public Ulid GrassMaterialId { get; init; } = Ulid.NewUlid();
	public Ulid DirtMaterialId { get; init; } = Ulid.NewUlid();
	public Ulid StoneMaterialId { get; init; } = Ulid.NewUlid();
	public Ulid SnowMaterialId { get; init; } = Ulid.NewUlid();
	public Ulid IceMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Resolves the ground material definition ID for a given biome.
	/// </summary>
	public Ulid ResolveGroundMaterial(BiomeType biome) => biome switch
	{
		BiomeType.Ocean => this.SandMaterialId,
		BiomeType.Beach => this.SandMaterialId,
		BiomeType.Desert => this.SandMaterialId,
		BiomeType.Savanna => this.DirtMaterialId,
		BiomeType.Grassland => this.GrassMaterialId,
		BiomeType.TemperateForest => this.DirtMaterialId,
		BiomeType.TropicalRainforest => this.DirtMaterialId,
		BiomeType.Taiga => this.DirtMaterialId,
		BiomeType.Tundra => this.SnowMaterialId,
		BiomeType.Glacial => this.IceMaterialId,
		BiomeType.Mountain => this.StoneMaterialId,
		_ => this.StoneMaterialId
	};
}
