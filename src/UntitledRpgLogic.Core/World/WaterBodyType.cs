using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Describes the body of water in a tile.
/// </summary>
[SuppressMessage("Naming", "CA1028:Enum Storage should be Int32",
	Justification = "Byte underlying type is required for compact memory alignment in unmanaged tile/grid arrays.")]
public enum WaterBodyType : byte
{
	/// <summary>
	///     No water or not enough to be considered a body of water.
	/// </summary>
	None = 0,

	/// <summary>
	///     Large saltwater body of water.
	/// </summary>
	Ocean = 1,

	/// <summary>
	///     Medium to large body of freshwater.
	/// </summary>
	Lake = 2,

	/// <summary>
	///     Small body of freshwater
	/// </summary>
	Pond = 3,

	/// <summary>
	///     Flowing water
	/// </summary>
	River = 4,

	/// <summary>
	///     A point where water emerges from to form a river or standing lake.
	/// </summary>
	Spring = 5
}
