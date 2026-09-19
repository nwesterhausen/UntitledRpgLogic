using UntitledRpgLogic.Core.World.Generation.GridMaps;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Tracks surface terrain data across a 2D map coordinate space.
/// </summary>
public sealed class TerrainMaps : NoiseGridDimensions
{
	private readonly GeologyLayerMap geologyLayer;
	private readonly HeightMap height;
	private readonly OreDepositMap oreDeposit;
	private readonly Dictionary<ushort, OreDepositDefinition> oreDepositDefinitions;
	private readonly SoilDepthMap soilDepth;
	private readonly SoilMaterialMap soilMaterial;
	private readonly VolcanismMap volcanism;

	/// <summary>
	/// </summary>
	/// <param name="widthTiles"></param>
	/// <param name="heightTiles"></param>
	public TerrainMaps(int widthTiles, int heightTiles) : base(widthTiles, heightTiles)
	{
		this.geologyLayer = new GeologyLayerMap(widthTiles, heightTiles);
		this.oreDeposit = new OreDepositMap(widthTiles, heightTiles);
		this.height = new HeightMap(widthTiles, heightTiles);
		this.soilDepth = new SoilDepthMap(widthTiles, heightTiles);
		this.soilMaterial = new SoilMaterialMap(widthTiles, heightTiles);
		this.volcanism = new VolcanismMap(widthTiles, heightTiles);
		this.oreDepositDefinitions = new Dictionary<ushort, OreDepositDefinition>();
	}

	/// <summary>
	/// </summary>
	/// <param name="widthTiles"></param>
	/// <param name="heightTiles"></param>
	/// <param name="geologyLayers"></param>
	/// <param name="geologyOres"></param>
	/// <param name="height"></param>
	/// <param name="soilDepths"></param>
	/// <param name="soilMaterials"></param>
	/// <param name="volcanism"></param>
	/// <param name="oreDepositDefinitions"></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="ArgumentException"></exception>
	public TerrainMaps(
		int widthTiles,
		int heightTiles,
		Ulid[] geologyLayers,
		ushort[] geologyOres,
		short[] height,
		ushort[] soilDepths,
		Ulid[] soilMaterials,
		float[] volcanism,
		OreDepositDefinition[] oreDepositDefinitions) : base(widthTiles, heightTiles)
	{
		ArgumentNullException.ThrowIfNull(geologyLayers);
		ArgumentNullException.ThrowIfNull(geologyOres);
		ArgumentNullException.ThrowIfNull(height);
		ArgumentNullException.ThrowIfNull(soilDepths);
		ArgumentNullException.ThrowIfNull(soilMaterials);
		ArgumentNullException.ThrowIfNull(volcanism);
		ArgumentNullException.ThrowIfNull(oreDepositDefinitions);

		var expectedSize = widthTiles * heightTiles;
		if (geologyLayers.Length != expectedSize || geologyOres.Length != expectedSize || height.Length != expectedSize
		    || soilDepths.Length != expectedSize || soilMaterials.Length != expectedSize ||
		    volcanism.Length != expectedSize)
		{
			throw new ArgumentException($"All input arrays must have length equal to width * height ({expectedSize}).");
		}

		this.geologyLayer = new GeologyLayerMap(widthTiles, heightTiles, geologyLayers);
		this.oreDeposit = new OreDepositMap(widthTiles, heightTiles, geologyOres);
		this.height = new HeightMap(widthTiles, heightTiles, height);
		this.soilDepth = new SoilDepthMap(widthTiles, heightTiles, soilDepths);
		this.soilMaterial = new SoilMaterialMap(widthTiles, heightTiles, soilMaterials);
		this.volcanism = new VolcanismMap(widthTiles, heightTiles, volcanism);
		this.oreDepositDefinitions = new Dictionary<ushort, OreDepositDefinition>();

		foreach (var deposit in oreDepositDefinitions)
		{
			this.oreDepositDefinitions.Add(deposit.DepositId, deposit);
		}
	}

	public ReadOnlySpan<short> HeightMapCells => this.height.Cells;

	public ReadOnlySpan<Ulid> GeologyLayerMapCells => this.geologyLayer.Cells;

	public ReadOnlySpan<ushort> OreDepositMapCells => this.oreDeposit.Cells;

	public ReadOnlySpan<ushort> SoilDepthMapCells => this.soilDepth.Cells;

	public ReadOnlySpan<Ulid> SoilMaterialMapCells => this.soilMaterial.Cells;

	public ReadOnlySpan<float> VolcanismMapCells => this.volcanism.Cells;

	public HeightMap HeightMapAsCopy() => new(
		this.WidthTiles, this.HeightTiles, [.. this.height.Cells]);

	public GeologyLayerMap GeologyLayerMapAsCopy() => new(
		this.WidthTiles, this.HeightTiles, [.. this.geologyLayer.Cells]);

	public OreDepositMap OreDepositMapAsCopy() => new(
		this.WidthTiles, this.HeightTiles, [.. this.oreDeposit.Cells]);

	public SoilDepthMap SoilDepthMapAsCopy() => new(
		this.WidthTiles, this.HeightTiles, [.. this.soilDepth.Cells]);

	public SoilMaterialMap SoilMaterialMapAsCopy() => new(
		this.WidthTiles, this.HeightTiles, [.. this.soilMaterial.Cells]);

	public VolcanismMap VolcanismMapAsCopy() => new(
		this.WidthTiles, this.HeightTiles, [.. this.volcanism.Cells]);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public Ulid? GetRockLayerMaterialId(int tileX, int tileY) => this.geologyLayer.GetGeologyLayer(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public ushort GetOreDepositId(int tileX, int tileY) => this.oreDeposit.GetGeologyOres(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="oreDepositId"></param>
	/// <returns></returns>
	public OreDepositDefinition GetOreDeposit(ushort oreDepositId) => this.oreDepositDefinitions[oreDepositId];

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public OreDepositDefinition GetOreDeposit(int tileX, int tileY)
	{
		this.ThrowIfNotInBounds(tileX, tileY);

		var depositId = this.GetOreDepositId(tileX, tileY);
		return this.oreDepositDefinitions[depositId];
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public short GetElevation(int tileX, int tileY) => this.height.GetElevation(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public ushort GetSoildDepth(int tileX, int tileY) => this.soilDepth.GetSoilDepth(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public Ulid GetSoilMaterial(int tileX, int tileY) => this.soilMaterial.GetSoilMaterial(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetVolcanism(int tileX, int tileY) => this.volcanism.GetVolcanism(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="soilMaterialId"></param>
	/// <param name="depth"></param>
	public void SetSoil(int tileX, int tileY, Ulid soilMaterialId, ushort depth)
	{
		this.ThrowIfNotInBounds(tileX, tileY);

		this.soilDepth.SetSoilDepth(tileX, tileY, depth);
		this.soilMaterial.SetSoilMaterial(tileX, tileY, soilMaterialId);
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetVolcanism(int tileX, int tileY, float value)
	{
		this.ThrowIfNotInBounds(tileX, tileY);

		this.volcanism.SetVolcanism(tileX, tileY, value);
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="elevation"></param>
	public void SetElevation(int tileX, int tileY, short elevation)
	{
		this.ThrowIfNotInBounds(tileX, tileY);

		this.height.SetElevation(tileX, tileY, elevation);
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="oreDeposit"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public void SetOreDeposit(int tileX, int tileY, OreDepositDefinition oreDeposit)
	{
		this.ThrowIfNotInBounds(tileX, tileY);
		ArgumentNullException.ThrowIfNull(oreDeposit);

		// ReSharper disable once RedundantDictionaryContainsKeyBeforeAdding
		if (!this.oreDepositDefinitions.ContainsKey(oreDeposit.DepositId))
		{
			this.oreDepositDefinitions.Add(oreDeposit.DepositId, oreDeposit);
		}
		else
		{
			// This needs to be logged as a warning so that if something weird happens, it can be tracked down.
			this.oreDepositDefinitions[oreDeposit.DepositId] = oreDeposit;
		}
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="rockLayerMaterial"></param>
	public void SetRockLayerMaterialId(int tileX, int tileY, Ulid rockLayerMaterial)
	{
		this.ThrowIfNotInBounds(tileX, tileY);

		this.geologyLayer.SetGeologyLayer(tileX, tileY, rockLayerMaterial);
	}
}
