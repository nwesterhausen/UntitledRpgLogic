namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Provides a read-only view over an <see cref="ArcanaMaps" /> instance.
/// </summary>
public readonly struct ReadOnlyArcanaMaps(ArcanaMaps inner)
{
	private readonly ArcanaMaps inner = inner ?? throw new ArgumentNullException(nameof(inner));

	public int WidthTiles => this.inner.WidthTiles;
	public int HeightTiles => this.inner.HeightTiles;

	public float GetAlignment(int tileX, int tileY) => this.inner.GetAlignment(tileX, tileY);

	public float GetSavagery(int tileX, int tileY) => this.inner.GetSavagery(tileX, tileY);

	public float GetLeylineEnergy(int tileX, int tileY) => this.inner.GetLeylineEnergy(tileX, tileY);

	public float GetManaDensity(int tileX, int tileY) => this.inner.GetManaDensity(tileX, tileY);

	public Ulid? GetManaAttunement(int tileX, int tileY) => this.inner.GetManaAttunement(tileX, tileY);
}
