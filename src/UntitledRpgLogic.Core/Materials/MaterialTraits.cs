namespace UntitledRpgLogic.Core.Materials;

/// <summary>
///     Defines bitwise classification and behavioral characteristics for materials and crafting ingredients.
/// </summary>
[Flags]
public enum MaterialTraits
{
	/// <summary>
	///     No classification flags set. Represents an inert or baseline material.
	/// </summary>
	None = 0,

	/// <summary>
	///     Indicates the material occurs naturally as mineable ore deposits, mineral veins, or raw stone within the world terrain.
	/// </summary>
	NaturalOre = 1 << 0,

	/// <summary>
	///     Indicates the material has undergone smelting, refining, or metallurgical purification (e.g., metal bars, ingots, or refined sheets).
	/// </summary>
	RefinedMetal = 1 << 1,

	/// <summary>
	///     Indicates the material originates from biological matter, such as wood, plant fibers, animal hide, bone, or woven cloth.
	/// </summary>
	Organic = 1 << 2,

	/// <summary>
	///     Indicates the material is prone to rapid oxidation or catching fire when exposed to heat, open flame, or thermal damage.
	/// </summary>
	Combustible = 1 << 3,

	/// <summary>
	///     Indicates the material naturally exists as a gaseous constituent of ambient air or atmospheric blends.
	/// </summary>
	AtmosphericGas = 1 << 4,

	/// <summary>
	///     Indicates the material exists primarily in a liquid or viscous fluid state under standard room-temperature conditions.
	/// </summary>
	Fluid = 1 << 5,

	/// <summary>
	///     Indicates the material possesses high affinity for magical energy, allowing it to naturally channel, store, or resonate with mana.
	/// </summary>
	MagicalConductor = 1 << 6,
}
