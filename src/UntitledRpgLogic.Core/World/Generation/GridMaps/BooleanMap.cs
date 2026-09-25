namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

public sealed class BooleanMap : NoiseGrid2D<bool>
{
	public BooleanMap(int widthTiles, int heightTiles) : base(widthTiles, heightTiles)
	{
	}

	public BooleanMap(int widthTiles, int heightTiles, ReadOnlySpan<bool> cells) : base(widthTiles, heightTiles, cells)
	{
	}

	public void Toggle(int tileX, int tileY)
	{
		var toggled = !this.GetValueClamped(tileX, tileY);
		this.SetValue(tileX, tileY, toggled);
	}

	public bool IsTrue(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);
	public void SetTrue(int tileX, int tileY) => this.SetValue(tileX, tileY, true);
	public void SetFalse(int tileX, int tileY) => this.SetValue(tileX, tileY, false);
}
