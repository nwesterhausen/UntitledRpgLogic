namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Configures default material identifiers applied during chunk palette discretization across biomes.
/// </summary>
public record BiomeMaterialMapping
{
	/// <summary>
	///     Gets the material identifier representing shallow surface water.
	/// </summary>
	public Ulid WaterMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Gets the material identifier representing deep ocean water.
	/// </summary>
	public Ulid DeepWaterMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Gets the material identifier representing sand and desert sediment.
	/// </summary>
	public Ulid SandMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Gets the material identifier representing grass surface cover.
	/// </summary>
	public Ulid GrassMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Gets the material identifier representing exposed topsoil and dirt.
	/// </summary>
	public Ulid DirtMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Gets the material identifier representing mountain bedrock and stone.
	/// </summary>
	public Ulid StoneMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Gets the material identifier representing tundra snow layers.
	/// </summary>
	public Ulid SnowMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Gets the material identifier representing glacial packed ice.
	/// </summary>
	public Ulid IceMaterialId { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Resolves the corresponding ground material identifier for a given ecological biome classification.
	/// </summary>
	/// <param name="biome">The biome type to evaluate.</param>
	/// <returns>The resolved material definition identifier.</returns>
	public Ulid ResolveGroundMaterial(BiomeType biome) => biome switch
	{
		BiomeType.Ocean or BiomeType.Beach or BiomeType.Desert => this.SandMaterialId,
		BiomeType.Savanna or BiomeType.TemperateForest or BiomeType.TropicalRainforest or BiomeType.Taiga => this
			.DirtMaterialId,
		BiomeType.Grassland => this.GrassMaterialId,
		BiomeType.Tundra => this.SnowMaterialId,
		BiomeType.Glacial => this.IceMaterialId,
		_ => this.StoneMaterialId
	};
}
