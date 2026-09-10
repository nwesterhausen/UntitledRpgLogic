namespace UntitledRpgLogic.WorldGen.Models;

/// <summary>
///     Tracks surface liquid columns, liquid composition, and river presence across a 2D map coordinate space.
/// </summary>
public class TerrainHydrology
{
	private readonly bool[] _isRiver;
	private readonly ushort[] _liquidDepth;
	private readonly Ulid?[] _liquidMaterial;

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainHydrology" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the hydrological grid in tiles.</param>
	/// <param name="heightTiles">The total height of the hydrological grid in tiles.</param>
	public TerrainHydrology(int widthTiles, int heightTiles)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(widthTiles);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heightTiles);

		var totalSize = widthTiles * heightTiles;
		this.WidthTiles = widthTiles;
		this.HeightTiles = heightTiles;
		this._liquidDepth = new ushort[totalSize];
		this._liquidMaterial = new Ulid?[totalSize];
		this._isRiver = new bool[totalSize];
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainHydrology" /> class using existing flat arrays.
	/// </summary>
	/// <param name="widthTiles">The total width of the hydrological grid in tiles.</param>
	/// <param name="heightTiles">The total height of the hydrological grid in tiles.</param>
	/// <param name="liquidDepth">The flattened surface liquid depth array of length <c>widthTiles * heightTiles</c>.</param>
	/// <param name="liquidMaterial">The flattened liquid material ULID array of length <c>widthTiles * heightTiles</c>.</param>
	/// <param name="isRiver">The flattened river flag array of length <c>widthTiles * heightTiles</c>.</param>
	public TerrainHydrology(
		int widthTiles,
		int heightTiles,
		ushort[] liquidDepth,
		Ulid?[] liquidMaterial,
		bool[] isRiver)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(widthTiles);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heightTiles);
		ArgumentNullException.ThrowIfNull(liquidDepth);
		ArgumentNullException.ThrowIfNull(liquidMaterial);
		ArgumentNullException.ThrowIfNull(isRiver);

		var expectedSize = widthTiles * heightTiles;
		if (liquidDepth.Length != expectedSize || liquidMaterial.Length != expectedSize || isRiver.Length != expectedSize)
		{
			throw new ArgumentException($"All input arrays must have length equal to width * height ({expectedSize}).");
		}

		this.WidthTiles = widthTiles;
		this.HeightTiles = heightTiles;
		this._liquidDepth = liquidDepth;
		this._liquidMaterial = liquidMaterial;
		this._isRiver = isRiver;
	}

	/// <summary>
	///     Gets the total horizontal dimension of the hydrology grid in tiles.
	/// </summary>
	public int WidthTiles { get; }

	/// <summary>
	///     Gets the total vertical dimension of the hydrology grid in tiles.
	/// </summary>
	public int HeightTiles { get; }

	/// <summary>
	///     Retrieves the surface liquid depth in depth units at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The liquid depth, or 0 if out of bounds or dry.</returns>
	public ushort GetLiquidDepth(int tileX, int tileY)
	{
		if (tileX < 0 || tileX >= this.WidthTiles || tileY < 0 || tileY >= this.HeightTiles)
		{
			return 0;
		}

		return this._liquidDepth[(tileY * this.WidthTiles) + tileX];
	}

	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The material ULID of the liquid, or null if dry or out of bounds.</returns>
	public Ulid? GetLiquidMaterial(int tileX, int tileY)
	{
		if (tileX < 0 || tileX >= this.WidthTiles || tileY < 0 || tileY >= this.HeightTiles)
		{
			return null;
		}

		return this._liquidMaterial[(tileY * this.WidthTiles) + tileX];
	}

	/// <summary>
	///     Sets the liquid depth, material composition, and river classification for the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="depth">The fluid depth units.</param>
	/// <param name="materialId">The foreign identifier of the fluid material definition.</param>
	/// <param name="isRiverChannel">Indicates whether this coordinate is part of an active river channel.</param>
	public void SetLiquid(int tileX, int tileY, ushort depth, Ulid? materialId, bool isRiverChannel = false)
	{
		if (tileX >= 0 && tileX < this.WidthTiles && tileY >= 0 && tileY < this.HeightTiles)
		{
			var index = (tileY * this.WidthTiles) + tileX;
			this._liquidDepth[index] = depth;
			this._liquidMaterial[index] = materialId;
			this._isRiver[index] = isRiverChannel;
		}
	}

	/// <summary>
	///     Scans whether a designated river cell exists within a Chebyshev distance radius around an origin coordinate.
	/// </summary>
	/// <param name="originX">The center horizontal tile coordinate.</param>
	/// <param name="originY">The center vertical tile coordinate.</param>
	/// <param name="maxDistanceTiles">The maximum radius in tiles to inspect.</param>
	/// <returns><c>true</c> if at least one river cell is detected within the radius; otherwise, <c>false</c>.</returns>
	public bool IsRiverWithinDistance(int originX, int originY, int maxDistanceTiles)
	{
		var startX = Math.Max(0, originX - maxDistanceTiles);
		var endX = Math.Min(this.WidthTiles - 1, originX + maxDistanceTiles);
		var startY = Math.Max(0, originY - maxDistanceTiles);
		var endY = Math.Min(this.HeightTiles - 1, originY + maxDistanceTiles);

		for (var y = startY; y <= endY; y++)
		{
			var rowOffset = y * this.WidthTiles;
			for (var x = startX; x <= endX; x++)
			{
				if (this._isRiver[rowOffset + x])
				{
					return true;
				}
			}
		}

		return false;
	}
}
