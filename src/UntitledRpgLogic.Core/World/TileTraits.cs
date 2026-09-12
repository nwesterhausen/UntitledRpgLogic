using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Bitwise flags tracking spatial constraints, hazardous conditions, and structures for an individual tile cell.
/// </summary>
[Flags]
[SuppressMessage(
	"Naming",
	"CA1028:Enum Storage should be Int32",
	Justification =
		"Underlying byte storage is required for compact memory alignment within unmanaged Tile2D binary arrays.")]
public enum TileTraits : byte
{
	/// <summary>
	///     No special status or physical restrictions.
	/// </summary>
	None = 0,

	/// <summary>
	///     Tile cannot be traversed by normal walking movement (e.g., solid walls, sheer bedrock).
	/// </summary>
	Impassable = 1 << 0,

	/// <summary>
	///     Tile is actively burning and inflicts thermal/fire damage.
	/// </summary>
	IsBurning = 1 << 1,

	/// <summary>
	///     Tile has been modified or placed by an entity (floors, constructed masonry, fortifications).
	/// </summary>
	IsConstructed = 1 << 2,

	/// <summary>
	///     Tile contains dense, concentrated gas or smoke differing from the general atmosphere.
	/// </summary>
	HasLocalGas = 1 << 3
}
