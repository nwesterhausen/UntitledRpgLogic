namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Bitwise classification flags categorizing an entity archetype's biological, mechanical, or mystical nature.
/// </summary>
[Flags]
public enum EntityClassification
{
	/// <summary>
	/// 	An invalid entity or entity not bearing any classifications (unlikely)
	/// </summary>
	None = 0,

	/// <summary>
	/// 	the entity is a humanoid
	/// </summary>
	Humanoid = 1 << 0,

	/// <summary>
	/// 	the entity is a beast
	/// </summary>
	Beast = 1 << 1,

	/// <summary>
	/// 	the entity is undead
	/// </summary>
	Undead = 1 << 2,

	/// <summary>
	/// 	the entity is an elemental
	/// </summary>
	Elemental = 1 << 3,

	/// <summary>
	/// 	the entity is a construct
	/// </summary>
	Construct = 1 << 4,

	/// <summary>
	/// 	the entity is a plant
	/// </summary>
	Plant = 1 << 5,

	/// <summary>
	/// 	the entity is an aberration (created via corruption)
	/// </summary>
	Aberration = 1 << 6,

	/// <summary>
	/// 	the entity is a god
	/// </summary>
	God = 1 << 7,

	/// <summary>
	/// 	the entity is a boss (special classification)
	/// </summary>
	Boss = 1 << 8,

	/// <summary>
	/// 	chests, urns, crates, bags
	/// </summary>
	Container = 1 << 20,

	/// <summary>
	/// 	levers, doors, shrines, (probably any Containers), (probably any actors with dialog/trade)
	/// </summary>
	Interactable = 1 << 21,

	/// <summary>
	/// 	anvils, workbenches, loom, forge, etc
	/// </summary>
	WorkStation = 1 << 22,
}
