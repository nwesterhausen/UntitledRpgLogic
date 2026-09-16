namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
/// 	Contract for a service to generate noise maps using the <see cref="WorldMapConfiguration" /> as input.
/// </summary>
public interface INoisemapGenerator<out T>
where T : NoiseGridDimensions
{
	/// <summary>
	///     Populates a <see cref="NoiseGrid2D{T}" /> implmenter with values appropriate to the type of grid.
	/// </summary>
	/// <returns>A new <see cref="NoiseGrid2D{T}" /> implementer with generated values.</returns>
	public static abstract T Generate(WorldMapConfiguration mapConfig, WorldGenContext? generationContext = null);
}
