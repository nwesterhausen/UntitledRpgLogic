namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Abstract base container representing a 2D scalar or typed simulation grid backed by a flattened 1D array.
/// </summary>
/// <typeparam name="T">The primitive cell element type.</typeparam>
public abstract class TerrainGrid2D<T> : TerrainGridDimensions
{
	/// <summary>
	///     The 1D array holding all the cells in this grid.
	/// </summary>
	private readonly T[] cells;

	/// <summary>
	/// </summary>
	/// <param name="widthTiles"></param>
	/// <param name="heightTiles"></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	protected TerrainGrid2D(int widthTiles, int heightTiles) : base(widthTiles, heightTiles) =>
		this.cells = new T[widthTiles * heightTiles];

	/// <summary>
	/// </summary>
	/// <param name="widthTiles"></param>
	/// <param name="heightTiles"></param>
	/// <param name="cells"></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="ArgumentException"></exception>
	protected TerrainGrid2D(int widthTiles, int heightTiles, T[] cells)
		: base(widthTiles, heightTiles)
	{
		ArgumentNullException.ThrowIfNull(cells);

		var expectedSize = widthTiles * heightTiles;
		if (cells.Length != expectedSize)
		{
			throw new ArgumentException(
				$"Cells array length ({cells.Length}) must equal width * height ({expectedSize}).",
				nameof(cells));
		}

		this.cells = cells;
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	protected T GetValueClamped(int tileX, int tileY) => this.cells[this.GetClampedStrideIndex(tileX, tileY)];

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="defaultValue"></param>
	/// <returns></returns>
	protected T? GetValueOrDefault(int tileX, int tileY, T? defaultValue = default) =>
		this.IsInBounds(tileX, tileY) ? this.cells[this.GetStrideIndex(tileX, tileY)] : defaultValue;

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	protected void SetValue(int tileX, int tileY, T value)
	{
		if (this.IsInBounds(tileX, tileY))
		{
			this.cells[this.GetStrideIndex(tileX, tileY)] = value;
		}
	}
}
