using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Different types of water sources (springs) that may appear.
/// </summary>
[SuppressMessage("Naming", "CA1028:Enum Storage should be Int32",
	Justification = "Byte underlying type is required for compact memory alignment in unmanaged tile/grid arrays.")]
public enum WaterSource : byte
{
	/// <summary>
	///     No water source
	/// </summary>
	None = 0,

	/// <summary>
	///     A natural spring
	/// </summary>
	Natural = 1,

	/// <summary>
	///     A geothermal spring
	/// </summary>
	Geothermal = 2,

	/// <summary>
	///     An arcane or magical spring
	/// </summary>
	Arcane = 3
}
