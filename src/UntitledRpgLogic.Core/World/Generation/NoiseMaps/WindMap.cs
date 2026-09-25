using System.Numerics;

namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     Represents the typical wind heading and velocity in a world map grid. Influenced by <see cref="HeightMap" />,
///     <see cref="LeylineMap" />, and <see cref="TemperatureMap" />.
/// </summary>
/// <remarks>
///     <c>Vector.X</c> = <c>wind heading</c>, <c>Vector.Y</c> = <c>wind velocity</c>
/// </remarks>
public sealed class WindMap : NoiseGrid2D<Vector2>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="WindMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public WindMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="WindMap" /> class using an existing flat materials array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="windVector">The flattened vector array of length <c>widthTiles * heightTiles</c>.</param>
	public WindMap(int widthTiles, int heightTiles, ReadOnlySpan<Vector2> windVector)
		: base(widthTiles, heightTiles, windVector)
	{
	}

	/// <summary>
	///     Retrieves the wind heading and velocity at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The wind heading and velocity at the tile, or null if out of bounds.</returns>
	public Vector2 GetWindVector(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetWindHeading(int tileX, int tileY) => this.GetValueClamped(tileX, tileY).X;

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetWindVelocity(int tileX, int tileY) => this.GetValueClamped(tileX, tileY).Y;

	/// <summary>
	///     Sets the wind heading and velocity at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="windVector">The wind heading and velocity to assign.</param>
	public void SetWindVector(int tileX, int tileY, Vector2 windVector) => this.SetValue(tileX, tileY, windVector);
}
