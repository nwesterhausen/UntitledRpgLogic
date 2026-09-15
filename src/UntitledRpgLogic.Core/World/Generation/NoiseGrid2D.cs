namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Abstract base container representing a 2D scalar or typed simulation grid backed by a flattened 1D array.
/// </summary>
/// <typeparam name="T">The primitive cell element type.</typeparam>
public abstract class NoiseGrid2D<T> : NoiseGridDimensions
{
	/// <summary>
	///     The 1D array holding all the cells in this grid.
	/// </summary>
	private readonly T[] cells;

	/// <summary>
	///     Initializes a new default instance of the <see cref="NoiseGrid2D{T}" /> class with empty cells.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <exception cref="ArgumentOutOfRangeException">
	///     Thrown if <paramref name="widthTiles" /> or <paramref name="heightTiles" /> is less than or equal to zero.
	/// </exception>
	protected NoiseGrid2D(int widthTiles, int heightTiles) : base(widthTiles, heightTiles) =>
		this.cells = new T[widthTiles * heightTiles];

	/// <summary>
	///     Initializes a new instance of the <see cref="NoiseGrid2D{T}" /> class backed by an existing flattened cell array.
	/// </summary>
	/// <param name="widthTiles">The total width of the grid in tiles.</param>
	/// <param name="heightTiles">The total height of the grid in tiles.</param>
	/// <param name="cells">The flat 1D array of cells of length <c>widthTiles * heightTiles</c>.</param>
	/// <exception cref="ArgumentOutOfRangeException">
	///     Thrown if <paramref name="widthTiles" /> or <paramref name="heightTiles" /> is less than or equal to zero.
	/// </exception>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="cells" /> is <see langword="null" />.</exception>
	/// <exception cref="ArgumentException">
	///     Thrown if the length of <paramref name="cells" /> does not equal <c>widthTiles * heightTiles</c>.
	/// </exception>
	protected NoiseGrid2D(int widthTiles, int heightTiles, T[] cells)
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
	///     Gets the internal cell array directly.
	/// </summary>
	protected T[] Cells => this.cells;

	/// <summary>
	///     Retrieves the value of the cell at the given tile coordinate, clamping to grid boundaries if outside.
	/// </summary>
	/// <param name="tileX">The horizontal tile coordinate.</param>
	/// <param name="tileY">The vertical tile coordinate.</param>
	/// <returns>The value stored at the clamped cell position.</returns>
	protected T GetValueClamped(int tileX, int tileY) => this.cells[this.GetClampedStrideIndex(tileX, tileY)];

	/// <summary>
	///     Retrieves the value of the cell at the given tile coordinate, or a fallback default if out of bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile coordinate.</param>
	/// <param name="tileY">The vertical tile coordinate.</param>
	/// <param name="defaultValue">The fallback value returned when coordinates fall outside the grid.</param>
	/// <returns>The cell value if within bounds; otherwise, <paramref name="defaultValue" />.</returns>
	protected T? GetValueOrDefault(int tileX, int tileY, T? defaultValue = default) =>
		this.IsInBounds(tileX, tileY) ? this.cells[this.GetStrideIndex(tileX, tileY)] : defaultValue;

	/// <summary>
	///     Assigns a value to the cell at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile coordinate.</param>
	/// <param name="tileY">The vertical tile coordinate.</param>
	/// <param name="value">The value to assign to the target cell.</param>
	protected void SetValue(int tileX, int tileY, T value)
	{
		if (this.IsInBounds(tileX, tileY))
		{
			this.cells[this.GetStrideIndex(tileX, tileY)] = value;
		}
	}

	/// <summary>
	///     Provides direct read-only access to the underlying cell buffer.
	/// </summary>
	public ReadOnlySpan<T> AsReadOnlySpan() => this.cells;

	/// <summary>
	///     Creates a shallow clone of the flattened cell array.
	/// </summary>
	public T[] CloneCells() => (T[])this.cells.Clone();

	/// <summary>
	///     Copies the underlying cells into a destination array or span.
	/// </summary>
	public void CopyTo(Span<T> destination) => this.cells.AsSpan().CopyTo(destination);
}
