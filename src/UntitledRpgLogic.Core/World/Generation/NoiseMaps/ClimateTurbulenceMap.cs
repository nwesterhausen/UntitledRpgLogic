namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     Represents an amount of noise or turbulence which will affect the climate grids (temperature/wind/rain) across a 2D
///     map coordinate space.
/// </summary>
/// <remarks>
///     Store this as a floating point value that can be easily applied for variance: range <c>[-1.0, 1.0]</c>.
/// </remarks>
public sealed class ClimateTurbulenceMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ClimateTurbulenceMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public ClimateTurbulenceMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ClimateTurbulenceMap" /> class using an existing flat elevations
	///     array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="turbulenceArray">The flattened climate turbulence levels array of length <c>widthTiles * heightTiles</c>.</param>
	public ClimateTurbulenceMap(int widthTiles, int heightTiles, ReadOnlySpan<float> turbulenceArray)
		: base(widthTiles, heightTiles, turbulenceArray)
	{
	}

	/// <summary>
	///     Retrieves the climate turbulence level at the specified tile coordinate. Clamps coordinates to valid boundaries.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The climate turbulence level value at the given coordinate.</returns>
	public float GetTurbulence(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the climate turbulence level at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="turbulence">The climate turbulence level to assign.</param>
	public void SetTurbulence(int tileX, int tileY, float turbulence) => this.SetValue(tileX, tileY, turbulence);
}
