using System.Numerics;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Provides a read-only view over a <see cref="ClimateMaps" /> instance.
/// </summary>
public readonly struct ReadOnlyClimateMaps(ClimateMaps inner)
{
	private readonly ClimateMaps inner = inner ?? throw new ArgumentNullException(nameof(inner));

	public int WidthTiles => this.inner.WidthTiles;
	public int HeightTiles => this.inner.HeightTiles;

	public BiomeType GetBiome(int tileX, int tileY) => this.inner.GetBiome(tileX, tileY);

	public float GetTurbulence(int tileX, int tileY) => this.inner.GetTurbulence(tileX, tileY);

	public Vector2 GetWindVector(int tileX, int tileY) => this.inner.GetWindVector(tileX, tileY);

	public float GetWindHeading(int tileX, int tileY) => this.inner.GetWindHeading(tileX, tileY);

	public float GetWindVelocity(int tileX, int tileY) => this.inner.GetWindVelocity(tileX, tileY);

	public float GetTemperature(int tileX, int tileY) => this.inner.GetTemperature(tileX, tileY);

	public float GetRainfall(int tileX, int tileY) => this.inner.GetRainfall(tileX, tileY);
}
