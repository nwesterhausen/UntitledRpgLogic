namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Provides a non-mutating view over an underlying <see cref="WorldGenContext" />.
/// </summary>
public readonly struct ReadOnlyWorldGenContext(WorldGenContext inner)
{
	private readonly WorldGenContext inner = inner ?? throw new ArgumentNullException(nameof(inner));

	public WorldMapConfiguration MapConfig => this.inner.MapConfig;

	public ReadOnlyTerrainMaps Terrain => new(this.inner.Terrain);

	public ReadOnlyHydrologyMaps Hydrology => new(this.inner.Hydrology);

	public ReadOnlyClimateMaps Climate => new(this.inner.Climate);

	public ReadOnlyArcanaMaps Arcana => new(this.inner.Arcana);

	[Obsolete("Holdover from initial generation engine. Should be replaced with catalog-aware material context.")]
	public BiomeMaterialMapping MaterialMapping => this.inner.MaterialMapping;
}
