namespace UntitledRpgLogic.WorldGen;

/// <summary>
///     Contains arrays for helping with finding neighbors in a grid.
/// </summary>
public static class NeighborHelper
{
	/// <summary>
	///     Provides an array of pairs for directions North, East, South, West (in that order).
	/// </summary>
	/// <remarks>
	///     <c>[(0,-1),(1,0),(0,1),(-1,0)]</c>
	/// </remarks>
	public static readonly (int dx, int dy)[] CardinalNeighbors =
	[
		(0, -1), // North
		(1, 0), // East
		(0, 1), // South
		(-1, 0) // West
	];

	/// <summary>
	///     Provides an array of pairs for directions North, East, South, West, NorthWest, NorthEast, SouthEast,
	///     SouthWest (in that order).
	/// </summary>
	/// <remarks>
	///     <c>[(0,-1),(1,0),(0,1),(-1,0),(-1,-1),(1,-1),(1,1),(-1,1)]</c>
	/// </remarks>
	public static readonly (int dx, int dy)[] AllNeighbors =
	[
		(0, -1), // North
		(1, 0), // East
		(0, 1), // South
		(-1, 0), // West
		(-1, -1), // North-West
		(1, -1), // North-East
		(1, 1), // South-East
		(-1, 1) // South-West
	];
}
