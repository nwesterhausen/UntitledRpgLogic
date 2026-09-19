using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Alternative to System.Random based on the Lehmer algorithm.
/// </summary>
public class Random : IRandom
{
	// Stores the seed for the Next methods.
	private uint seed;

	/// <summary>
	///     Initializes a new instance of the Random class, using a time-dependent default seed value.
	/// </summary>
	public Random() => this.seed = (uint)Environment.TickCount;

	/// <summary>
	///     Initializes a new instance of the Random class, using the specified seed value.
	/// </summary>
	/// <param name="seed"></param>
	public Random(int seed) => this.seed = (uint)seed;

	/// <summary>
	///     Initializes a new instance of the Random class, using the specified seed value.
	/// </summary>
	/// <param name="seed"></param>
	public Random(uint seed) => this.seed = seed;

	/// <summary>
	///     Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0
	/// </summary>
	/// <returns></returns>
	public double NextDouble() => this.GetNextDouble(0.0, 1.0);

	/// <summary>
	///     Returns a non-negative random floating-point number that is less than the specified maximum.
	/// </summary>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	public double NextDouble(double maxValue) => this.GetNextDouble(0.0, maxValue);

	/// <summary>
	///     Returns a random floating-point number that is within a specified range.
	/// </summary>
	/// <param name="minValue"></param>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	public double NextDouble(double minValue, double maxValue) => this.GetNextDouble(minValue, maxValue);

	/// <summary>
	///     Fills the elements of a specified array of bytes with random numbers.
	/// </summary>
	/// <param name="buffer">The array to be filled with random numbers.</param>
	public void NextBytes(byte[] buffer)
	{
		ArgumentNullException.ThrowIfNull(buffer);

		for (var i = 0; i < buffer.Length; i++)
		{
			buffer[i] = (byte)this.NextInt();
		}
	}

	/// <summary>
	///     Returns a non-negative random integer.
	/// </summary>
	/// <returns>Int32</returns>
	public int NextInt() => this.GetNextInt(0, int.MaxValue);

	/// <summary>
	///     Returns a non-negative random integer that is less than the specified maximum.
	/// </summary>
	/// <param name="maxValue">Int32</param>
	/// <returns>Int32</returns>
	public int NextInt(int maxValue) => this.GetNextInt(0, maxValue);

	/// <summary>
	///     Returns a random integer that is within a specified range.
	/// </summary>
	/// <param name="minValue">Int32</param>
	/// <param name="maxValue">Int32</param>
	/// <returns>Int32</returns>
	public int NextInt(int minValue, int maxValue) => this.GetNextInt(minValue, maxValue);

	// Returns a random int and sets the seed for the next pass.
	internal int GetNextInt(int minValue, int maxValue)
	{
		this.seed = Rnd(this.seed);
		return ConvertToIntRange(this.seed, minValue, maxValue);
	}

	// Returns a random double and sets the seed for the next pass.
	internal double GetNextDouble(double minValue, double maxValue)
	{
		this.seed = Rnd(this.seed);
		return ConvertToDoubleRange(this.seed, minValue, maxValue);
	}

	/// <summary>
	///     Returns a random integer that is within a specified range, using the specified seed value.
	/// </summary>
	/// <param name="seed"></param>
	/// <param name="minValue"></param>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	public static int RndInt(uint seed, int minValue, int maxValue) => GetInt(seed, minValue, maxValue);

	/// <summary>
	///     Returns a random integer that is within a specified range, using a time-dependent default seed value.
	/// </summary>
	/// <param name="minValue"></param>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	public static int RndInt(int minValue, int maxValue) => GetInt((uint)Environment.TickCount, minValue, maxValue);

	/// <summary>
	///     Returns a random double that is within a specified range, using the specified seed value.
	/// </summary>
	/// <param name="seed"></param>
	/// <param name="minValue"></param>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	public static double RndDouble(uint seed, double minValue, double maxValue) => GetDouble(seed, minValue, maxValue);

	/// <summary>
	///     Returns a random double that is within a specified range, using a time-dependent default seed value.
	/// </summary>
	/// <param name="minValue"></param>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	public static double RndDouble(double minValue, double maxValue) =>
		GetDouble((uint)Environment.TickCount, minValue, maxValue);

	internal static int GetInt(uint seed, int minValue, int maxValue) =>
		ConvertToIntRange(Rnd(seed), minValue, maxValue);

	internal static double GetDouble(uint seed, double minValue, double maxValue) =>
		ConvertToDoubleRange(Rnd(seed), minValue, maxValue);

	// Converts uint to integer within the given range.
	internal static int ConvertToIntRange(uint val, int minValue, int maxValue) =>
		(int)((val % (maxValue - minValue)) + minValue);

	// Converts uint to double within the given range.
	internal static double ConvertToDoubleRange(uint val, double minValue, double maxValue) =>
		((double)val / uint.MaxValue * (maxValue - minValue)) + minValue;

	// Pseudo-random number generator based on the Lehmer Algorithm
	// and javidx9's implementation:
	// https://github.com/OneLoneCoder/Javidx9/blob/master/PixelGameEngine/SmallerProjects/OneLoneCoder_PGE_ProcGen_Universe.cpp
	internal static uint Rnd(uint seed)
	{
		seed += 0xe120fc15;
		var tmp = (ulong)seed * 0x4a39b70d;
		var m1 = (uint)((tmp >> 32) ^ tmp);
		tmp = (ulong)m1 * 0x12fad5c9;
		return (uint)((tmp >> 32) ^ tmp);
	}
}
