using UntitledRpgLogic.Core.World.Generation.GridMaps;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
/// </summary>
public sealed class ArcanaMaps : NoiseGridDimensions
{
	private readonly AlignmentMap alignment;
	private readonly LeylineMap leylines;
	private readonly ManaAttunementMap manaAttunement;
	private readonly ManaDensityMap manaDensity;
	private readonly SavageryMap savagery;

	/// <inheritdoc />
	public ArcanaMaps(int widthTiles, int heightTiles) : base(widthTiles, heightTiles)
	{
		this.alignment = new AlignmentMap(widthTiles, heightTiles);
		this.savagery = new SavageryMap(widthTiles, heightTiles);
		this.leylines = new LeylineMap(widthTiles, heightTiles);
		this.manaDensity = new ManaDensityMap(widthTiles, heightTiles);
		this.manaAttunement = new ManaAttunementMap(widthTiles, heightTiles);
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetAlignment(int tileX, int tileY, float value) => this.alignment.SetAlignment(tileX, tileY, value);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetSavagery(int tileX, int tileY, float value) => this.savagery.SetSavagery(tileX, tileY, value);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetLeylineEnergy(int tileX, int tileY, float value) =>
		this.leylines.SetLeylineEnergy(tileX, tileY, value);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetManaDensity(int tileX, int tileY, float value) =>
		this.manaDensity.SetManaDensity(tileX, tileY, value);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="element"></param>
	public void SetManaAttunement(int tileX, int tileY, Ulid element) =>
		this.manaAttunement.SetManaAttunement(tileX, tileY, element);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetAlignment(int tileX, int tileY) => this.alignment.GetAlignment(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetSavagery(int tileX, int tileY) => this.savagery.GetSavagery(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetLeylineEnergy(int tileX, int tileY) => this.leylines.GetLeylineEnergy(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetManaDensity(int tileX, int tileY) => this.manaDensity.GetManaDensity(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public Ulid? GetManaAttunement(int tileX, int tileY) => this.manaAttunement.GetManaAttunement(tileX, tileY);

	public void OverwriteLeylineMap(LeylineMap leylineMap)
	{
		ArgumentNullException.ThrowIfNull(leylineMap);
		this.leylines.ReplaceCells(leylineMap.Cells);
	}

	public void OverwriteAlignmentMap(AlignmentMap alignmentMap)
	{
		ArgumentNullException.ThrowIfNull(alignmentMap);
		this.alignment.ReplaceCells(alignmentMap.Cells);
	}

	public void OverwriteSavageryMap(SavageryMap savageryMap)
	{
		ArgumentNullException.ThrowIfNull(savageryMap);
		this.savagery.ReplaceCells(savageryMap.Cells);
	}

	public void OverwriteManaAttunementMap(ManaAttunementMap manaAttunementMap)
	{
		ArgumentNullException.ThrowIfNull(manaAttunementMap);
		this.manaAttunement.ReplaceCells(manaAttunementMap.Cells);
	}

	public void OverwriteManaDensityMap(ManaDensityMap manaDensityMap)
	{
		ArgumentNullException.ThrowIfNull(manaDensityMap);
		this.manaDensity.ReplaceCells(manaDensityMap.Cells);
	}
}
