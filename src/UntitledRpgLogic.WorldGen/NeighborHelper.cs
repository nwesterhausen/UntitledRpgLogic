namespace UntitledRpgLogic.WorldGen;

/// <summary>
///     Contains arrays for helping with finding neighbors in a grid.
/// </summary>
public static class NeighborHelper
{
	public static readonly (int x, int y) North = (0, -1);
	public static readonly (int x, int y) East = (1, 0);
	public static readonly (int x, int y) South = (0, 1);
	public static readonly (int x, int y) West = (-1, 0);

	public static readonly (int x, int y) NorthWest = (-1, -1);
	public static readonly (int x, int y) NorthEast = (1, -1);
	public static readonly (int x, int y) SouthWest = (-1, 1);
	public static readonly (int x, int y) SouthEast = (1, 1);

	/// <summary>
	///     Provides an array of pairs for directions North, East, South, West (in that order).
	/// </summary>
	/// <remarks>
	///     <c>[(0,-1),(1,0),(0,1),(-1,0)]</c>
	/// </remarks>
	public static readonly (int dx, int dy)[] CardinalNeighbors =
	[
		North,
		South,
		East,
		West
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
		.. CardinalNeighbors,
		NorthWest,
		NorthEast,
		SouthEast,
		SouthWest
	];
}
