namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     A map describing the alignment between good and evil for tiles in a map grid. Alignment is then used to
///     influence generated flora, fauna, dungeons, or whatever else is created in the world.
/// </summary>
/// <remarks>
///     Measured from <c>[-1.0, 1.0]</c> in a floating point scale; <c>-1</c> corresponding to fully evil, <c>1</c>
///     corresponding to the opposite.
/// </remarks>
public sealed class AlignmentMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="AlignmentMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	public AlignmentMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="AlignmentMap" /> class using an existing flat elevations array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="alignments">The flattened alignments array of length <c>widthTiles * heightTiles</c>.</param>
	public AlignmentMap(int widthTiles, int heightTiles, ReadOnlySpan<float> alignments)
		: base(widthTiles, heightTiles, alignments)
	{
	}

	/// <summary>
	///     Retrieves the alignment at the specified tile coordinate. Clamps coordinates to valid boundaries.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The alignment value at the given coordinate.</returns>
	public float GetAlignment(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the alignment at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="alignment">The alignment to assign.</param>
	public void SetAlignment(int tileX, int tileY, float alignment) => this.SetValue(tileX, tileY, alignment);
}
