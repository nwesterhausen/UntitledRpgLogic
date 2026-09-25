namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Contract for a service to generate noise maps using the <see cref="WorldMapConfiguration" /> as input.
/// </summary>
public interface INoisemapGenerator<out T>
	where T : NoiseGridDimensions
{
	/// <summary>
	///     Fills in the appropriate <see cref="NoiseGrid2D{T}" /> in the <paramref name="generationContext" />
	///     using the <see cref="WorldMapConfiguration" /> stored in the <paramref name="generationContext" />.
	///     Returns a copy that can be used or discarded.
	/// </summary>
	/// <returns>A new <see cref="NoiseGrid2D{T}" /> implementer with generated values.</returns>
	/// <param name="generationContext">The generation context which holds all the noise maps</param>
	public static abstract T Generate(ReadOnlyWorldGenContext generationContext);
}
