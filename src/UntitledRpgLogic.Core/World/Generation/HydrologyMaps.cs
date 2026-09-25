using UntitledRpgLogic.Core.World.Generation.GridMaps;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Tracks surface liquid columns, liquid composition, and river presence across a 2D map coordinate space.
/// </summary>
public sealed class HydrologyMaps : NoiseGridDimensions
{
	private readonly AquiferMap aquifer;
	private readonly BasinMap basin;
	private readonly LiquidDepthMap liquidDepth;
	private readonly LiquidMaterialMap liquidMaterial;
	private readonly WaterSourceMap waterSource;

	/// <summary>
	///     Initializes a new instance of the <see cref="HydrologyMaps" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the hydrological grid in tiles.</param>
	/// <param name="heightTiles">The total height of the hydrological grid in tiles.</param>
	public HydrologyMaps(int widthTiles, int heightTiles) : base(widthTiles, heightTiles)
	{
		this.aquifer = new AquiferMap(widthTiles, heightTiles);
		this.basin = new BasinMap(widthTiles, heightTiles);
		this.liquidDepth = new LiquidDepthMap(widthTiles, heightTiles);
		this.liquidMaterial = new LiquidMaterialMap(widthTiles, heightTiles);
		this.waterSource = new WaterSourceMap(widthTiles, heightTiles);
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="HydrologyMaps" /> class using existing flat arrays.
	/// </summary>
	/// <param name="widthTiles">The total width of the hydrological grid in tiles.</param>
	/// <param name="heightTiles">The total height of the hydrological grid in tiles.</param>
	/// <param name="basin"></param>
	/// <param name="liquidDepth">The flattened surface liquid depth array of length <c>widthTiles * heightTiles</c>.</param>
	/// <param name="liquidMaterial">The flattened liquid material ULID array of length <c>widthTiles * heightTiles</c>.</param>
	/// <param name="aquifer"></param>
	/// <param name="waterSource"></param>
	public HydrologyMaps(
		int widthTiles,
		int heightTiles,
		short[] aquifer,
		WaterBodyType[] basin,
		ushort[] liquidDepth,
		Ulid?[] liquidMaterial,
		WaterSource[] waterSource) : base(widthTiles, heightTiles)
	{
		ArgumentNullException.ThrowIfNull(aquifer);
		ArgumentNullException.ThrowIfNull(basin);
		ArgumentNullException.ThrowIfNull(liquidDepth);
		ArgumentNullException.ThrowIfNull(liquidMaterial);
		ArgumentNullException.ThrowIfNull(waterSource);

		var expectedSize = widthTiles * heightTiles;
		if (liquidDepth.Length != expectedSize || liquidMaterial.Length != expectedSize ||
		    aquifer.Length != expectedSize || basin.Length != expectedSize || waterSource.Length != expectedSize)
		{
			throw new ArgumentException($"All input arrays must have length equal to width * height ({expectedSize}).");
		}

		this.aquifer = new AquiferMap(widthTiles, heightTiles);
		this.basin = new BasinMap(widthTiles, heightTiles);
		this.liquidDepth = new LiquidDepthMap(widthTiles, heightTiles, liquidDepth);
		this.liquidMaterial = new LiquidMaterialMap(widthTiles, heightTiles, liquidMaterial);
		this.waterSource = new WaterSourceMap(widthTiles, heightTiles, waterSource);
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns></returns>
	public short GetHydraulicHead(int tileX, int tileY) => this.aquifer.GetAquifer(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns></returns>
	public WaterBodyType GetWaterBodyType(int tileX, int tileY) => this.basin.GetWaterBody(tileX, tileY);

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
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns></returns>
	public WaterSource GetWaterSource(int tileX, int tileY) => this.waterSource.GetWaterSource(tileX, tileY);

	/// <summary>
	///     Sets the liquid depth, material composition, and river classification for the specified tile coordinate.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="depth">The fluid depth units.</param>
	/// <param name="materialId">The foreign identifier of the fluid material definition.</param>
	/// <param name="waterBody">Indicates a replacement for the type of water body here.</param>
	/// <param name="sourceType">
	///     If this is a <see cref="WaterBodyType.Spring" />, the source type can be overriden with this
	///     value.
	/// </param>
	public void SetLiquid(int tileX, int tileY, ushort depth, Ulid? materialId, WaterBodyType? waterBody = null,
		WaterSource? sourceType = null)
	{
		this.ThrowIfNotInBounds(tileX, tileY);

		this.liquidDepth.SetLiquidDepth(tileX, tileY, depth);
		this.liquidMaterial.SetLiquidMaterial(tileX, tileY, materialId);

		if (waterBody != null) { this.basin.SetWaterBody(tileX, tileY, waterBody.Value); }

		if (sourceType != null)
		{
			this.waterSource.SetWaterSource(tileX, tileY, sourceType.Value);
		}
	}

	/// <summary>
	///     Checks whether the specified tile is part of a river channel.
	/// </summary>
	public bool IsRiver(int tileX, int tileY) => this.basin.GetWaterBody(tileX, tileY) == WaterBodyType.River;

	public void OverwriteBasinMap(BasinMap basinMap)
	{
		ArgumentNullException.ThrowIfNull(basinMap);
		this.basin.ReplaceCells(basinMap.Cells);
	}
}
