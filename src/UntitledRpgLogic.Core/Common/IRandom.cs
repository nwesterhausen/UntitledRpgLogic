namespace UntitledRpgLogic.Core.Common;

/// <summary>
///     Interface contract for a random-number generator.
/// </summary>
public interface IRandom
{
	/// <summary>
	///     Get the next random integer.
	/// </summary>
	/// <returns>The next random integer.</returns>
	public int NextInt();

	/// <summary>
	///     Get the next random integer.
	/// </summary>
	/// <param name="maxValue">The maximum value the random integer may be.</param>
	/// <returns>The next random integer, restricted to be less than <paramref name="maxValue" />.</returns>
	public int NextInt(int maxValue);

	/// <summary>
	///     Get the next random integer.
	/// </summary>
	/// <param name="minValue">The minimum value the random integer may be.</param>
	/// <param name="maxValue">The maximum value the random integer may be.</param>
	/// <returns>
	///     The next random integer, restricted to be less than <paramref name="maxValue" /> and more than
	///     <paramref name="minValue" />.
	/// </returns>
	public int NextInt(int minValue, int maxValue);

	/// <summary>
	///     Fills a buffer of bytes with random <see cref="byte" />s.
	/// </summary>
	/// <param name="buffer">A buffer to fill with random values.</param>
	public void NextBytes(byte[] buffer);

	/// <summary>
	///     Get the next random double.
	/// </summary>
	/// <returns>The next random double.</returns>
	public double NextDouble();

	/// <summary>
	///     Get the next random double.
	/// </summary>
	/// <param name="maxValue">The maximum value the random double may be.</param>
	/// <returns>The next random double, restricted to be less than <paramref name="maxValue" />.</returns>
	public double NextDouble(double maxValue);

	/// <summary>
	///     Get the next random double.
	/// </summary>
	/// <param name="minValue">The minimum value the random double may be.</param>
	/// <param name="maxValue">The maximum value the random double may be.</param>
	/// <returns>
	///     The next random double, restricted to be less than <paramref name="maxValue" /> and more than
	///     <paramref name="minValue" />.
	/// </returns>
	public double NextDouble(double minValue, double maxValue);
}
