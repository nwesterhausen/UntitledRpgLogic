namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Categorizes the environment, boundary, and generation rules of a discrete world map.
/// </summary>
public enum MapType
{
	/// <summary>
	///     Continuous open-world surface terrain.
	/// </summary>
	Overworld = 0,

	/// <summary>
	///     Procedural or handcrafted subterranean dungeon instance.
	/// </summary>
	Dungeon = 1,

	/// <summary>
	///     Natural cave, cavern, or subterranean tunnel network.
	/// </summary>
	Cave = 2,

	/// <summary>
	///     Enclosed building or structural interior.
	/// </summary>
	BuildingInterior = 3,

	/// <summary>
	///     Isolated pocket dimension, demiplane, or arena.
	/// </summary>
	PocketDimension = 4,
}
