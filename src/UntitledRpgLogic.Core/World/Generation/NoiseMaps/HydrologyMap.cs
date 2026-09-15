using UntitledRpgLogic.Core.World.Generation.MapGrids;

namespace UntitledRpgLogic.Core.World.Generation.NoiseMaps;

/// <summary>
///     Tracks surface liquid columns, liquid composition, and river presence across a 2D map coordinate space.
/// </summary>
public sealed class HydrologyMap : NoiseGridDimensions
{
	private readonly IsRiverGrid isRiver;
	private readonly LiquidDepthGrid liquidDepth;
	private readonly LiquidMaterialGrid liquidMaterial;

	/// <summary>
	///     Initializes a new instance of the <see cref="HydrologyMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the hydrological grid in tiles.</param>
	/// <param name="heightTiles">The total height of the hydrological grid in tiles.</param>
	public HydrologyMap(int widthTiles, int heightTiles) : base(widthTiles, heightTiles)
	{
		this.liquidDepth = new LiquidDepthGrid(widthTiles, heightTiles);
		this.liquidMaterial = new LiquidMaterialGrid(widthTiles, heightTiles);
		this.isRiver = new IsRiverGrid(widthTiles, heightTiles);
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="HydrologyMap" /> class using existing flat arrays.
	/// </summary>
	/// <param name="widthTiles">The total width of the hydrological grid in tiles.</param>
	/// <param name="heightTiles">The total height of the hydrological grid in tiles.</param>
	/// <param name="liquidDepth">The flattened surface liquid depth array of length <c>widthTiles * heightTiles</c>.</param>
	/// <param name="liquidMaterial">The flattened liquid material ULID array of length <c>widthTiles * heightTiles</c>.</param>
	/// <param name="isRiver">The flattened river flag array of length <c>widthTiles * heightTiles</c>.</param>
	public HydrologyMap(
		int widthTiles,
		int heightTiles,
		ushort[] liquidDepth,
		Ulid?[] liquidMaterial,
		bool[] isRiver) : base(widthTiles, heightTiles)
	{
		ArgumentNullException.ThrowIfNull(liquidDepth);
		ArgumentNullException.ThrowIfNull(liquidMaterial);
		ArgumentNullException.ThrowIfNull(isRiver);

		var expectedSize = widthTiles * heightTiles;
		if (liquidDepth.Length != expectedSize || liquidMaterial.Length != expectedSize ||
		    isRiver.Length != expectedSize)
		{
			throw new ArgumentException($"All input arrays must have length equal to width * height ({expectedSize}).");
		}

		this.liquidDepth = new LiquidDepthGrid(widthTiles, heightTiles, liquidDepth);
		this.liquidMaterial = new LiquidMaterialGrid(widthTiles, heightTiles, liquidMaterial);
		this.isRiver = new IsRiverGrid(widthTiles, heightTiles);
	}

	/// <summary>
	///     Retrieves the surface liquid depth in depth units at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The liquid depth, or 0 if out of bounds or dry.</returns>
	public ushort GetLiquidDepth(int tileX, int tileY) => this.liquidDepth.GetLiquidDepth(tileX, tileY);

	/// <summary>
	///     Retrieves the material identifier for the liquid at the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The material ULID of the liquid, or null if dry or out of bounds.</returns>
	public Ulid? GetLiquidMaterial(int tileX, int tileY) => this.liquidMaterial.GetLiquidMaterial(tileX, tileY);

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
		if (!this.IsInBounds(tileX, tileY))
		{
			throw new ArgumentOutOfRangeException(nameof(tileX), "Coordinates outside map bounds.");
		}

		this.liquidDepth.SetLiquidDepth(tileX, tileY, depth);
		this.liquidMaterial.SetLiquidMaterial(tileX, tileY, materialId);
		this.isRiver.SetIsRiver(tileX, tileY, isRiverChannel);
	}

	/// <summary>
	///     Checks whether the specified tile is part of a river channel.
	/// </summary>
	public bool IsRiver(int tileX, int tileY) => this.isRiver.GetIsRiver(tileX, tileY);
}
