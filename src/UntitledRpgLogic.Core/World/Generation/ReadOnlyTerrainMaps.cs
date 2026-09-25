namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Provides a read-only view over a <see cref="TerrainMaps" /> instance.
/// </summary>
public readonly struct ReadOnlyTerrainMaps(TerrainMaps inner)
{
	private readonly TerrainMaps inner = inner ?? throw new ArgumentNullException(nameof(inner));

	public int WidthTiles => this.inner.WidthTiles;
	public int HeightTiles => this.inner.HeightTiles;

	public ReadOnlySpan<short> HeightMapCells => this.inner.HeightMapCells;
	public ReadOnlySpan<Ulid> GeologyLayerMapCells => this.inner.GeologyLayerMapCells;
	public ReadOnlySpan<ushort> OreDepositMapCells => this.inner.OreDepositMapCells;
	public ReadOnlySpan<ushort> SoilDepthMapCells => this.inner.SoilDepthMapCells;
	public ReadOnlySpan<Ulid> SoilMaterialMapCells => this.inner.SoilMaterialMapCells;
	public ReadOnlySpan<float> VolcanismMapCells => this.inner.VolcanismMapCells;

	public short GetElevation(int tileX, int tileY) => this.inner.GetElevation(tileX, tileY);
	public ushort GetSoildDepth(int tileX, int tileY) => this.inner.GetSoildDepth(tileX, tileY);
	public Ulid GetSoilMaterial(int tileX, int tileY) => this.inner.GetSoilMaterial(tileX, tileY);
	public float GetVolcanism(int tileX, int tileY) => this.inner.GetVolcanism(tileX, tileY);
	public OreDepositDefinition GetOreDeposit(int tileX, int tileY) => this.inner.GetOreDeposit(tileX, tileY);
	public OreDepositDefinition GetOreDeposit(ushort id) => this.inner.GetOreDeposit(id);
	public Ulid? GetRockLayerMaterialId(int tileX, int tileY) => this.inner.GetRockLayerMaterialId(tileX, tileY);
	public ushort GetOreDepositId(int tileX, int tileY) => this.inner.GetOreDepositId(tileX, tileY);
}
