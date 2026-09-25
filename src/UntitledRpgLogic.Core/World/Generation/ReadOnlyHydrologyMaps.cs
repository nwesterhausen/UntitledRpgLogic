namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Provides a read-only view over a <see cref="HydrologyMaps" /> instance.
/// </summary>
public readonly struct ReadOnlyHydrologyMaps(HydrologyMaps inner)
{
	private readonly HydrologyMaps inner = inner ?? throw new ArgumentNullException(nameof(inner));

	public int WidthTiles => this.inner.WidthTiles;
	public int HeightTiles => this.inner.HeightTiles;

	public short GetHydraulicHead(int tileX, int tileY) => this.inner.GetHydraulicHead(tileX, tileY);

	public WaterBodyType GetWaterBodyType(int tileX, int tileY) => this.inner.GetWaterBodyType(tileX, tileY);

	public ushort GetLiquidDepth(int tileX, int tileY) => this.inner.GetLiquidDepth(tileX, tileY);

	public Ulid? GetLiquidMaterial(int tileX, int tileY) => this.inner.GetLiquidMaterial(tileX, tileY);

	public WaterSource GetWaterSource(int tileX, int tileY) => this.inner.GetWaterSource(tileX, tileY);

	public bool IsRiver(int tileX, int tileY) => this.inner.IsRiver(tileX, tileY);
}
