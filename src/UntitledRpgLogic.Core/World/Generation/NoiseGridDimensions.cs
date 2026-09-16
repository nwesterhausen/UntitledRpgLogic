namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Base container representing 2D grid dimensions and coordinate boundary math without holding cell memory.
/// </summary>
public abstract class NoiseGridDimensions
{
	/// <summary>
	///     Create a new noise grid of specific size.
	/// </summary>
	/// <param name="widthTiles">Width of the grid</param>
	/// <param name="heightTiles">Height of the grid</param>
	protected NoiseGridDimensions(int widthTiles, int heightTiles)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(widthTiles);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heightTiles);

		this.WidthTiles = widthTiles;
		this.HeightTiles = heightTiles;
	}

	/// <summary>
	///     Gets the total horizontal dimension in tiles.
	/// </summary>
	public int WidthTiles { get; }

	/// <summary>
	///     Gets the total vertical dimension in tiles.
	/// </summary>
	public int HeightTiles { get; }

	/// <summary>
	///     Evaluates whether a tile coordinate falls strictly within the grid boundaries.
	/// </summary>
	public bool IsInBounds(int tileX, int tileY) =>
		tileX >= 0 && tileX < this.WidthTiles && tileY >= 0 && tileY < this.HeightTiles;

	/// <summary>
	///     Calculates the 1D stride index for coordinates without boundary validation.
	/// </summary>
	public int GetStrideIndex(int tileX, int tileY) => (tileY * this.WidthTiles) + tileX;

	/// <summary>
	///     Calculates the 1D stride index, clamping the coordinates into valid grid boundaries.
	/// </summary>
	public int GetClampedStrideIndex(int tileX, int tileY)
	{
		var clampedX = Math.Clamp(tileX, 0, this.WidthTiles - 1);
		var clampedY = Math.Clamp(tileY, 0, this.HeightTiles - 1);
		return (clampedY * this.WidthTiles) + clampedX;
	}
}
