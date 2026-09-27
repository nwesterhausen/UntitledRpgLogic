namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Types of geologic rock formations.
/// </summary>
public enum GeologicFormation
{
	/// <summary>
	///     Any/none option. Doesn't require a specific rock type or maybe any at all.
	/// </summary>
	Any = 0,

	/// <summary>
	///     Igneous rocks are formed from cooling magma or lava.
	/// </summary>
	Igneous = 1,

	/// <summary>
	///     Otherwise known as plutonic igneous rock, it cools slowly beneath the earths surface, creating large mineral
	///     crystals. A common example is granite.
	/// </summary>
	IgneousIntrusive = 2,

	/// <summary>
	///     Otherwise known as volcanic igneous rock, it cools quickly on the surface after a volcanic eruption. It has a
	///     fine-grained or glassy texture. Examples include basalt and obsidian.
	/// </summary>
	IgneousExtrusive = 3,

	/// <summary>
	///     Sedimentary rocks are formed from the accumulation, compression and cementation of mineral particles, rock
	///     fragments,
	///     organic remains, or chemical precipitates in layers. These rocks are where fossils are found.
	/// </summary>
	Sedimentary = 10,

	/// <summary>
	///     Sedimentary rocks formed from clasts of pre-existing rocks which have been weathered, transported, and cemented
	///     together. Examples include sandstone, shale, mudstone, and conglomerate.
	/// </summary>
	DetritalSedimentary = 11,

	/// <summary>
	///     Sedimentary rocks formed when dissolved mineral ions precipitate directly out of a water solution, usually from
	///     evaporation. Examples include rock salt and evaporates.
	/// </summary>
	ChemicalSedimentary = 12,

	/// <summary>
	///     Sedimentary rocks formed from the accumulated remains of living organisms, like compressed plant material or
	///     shells.
	///     Examples include coal, coquina or chalk.
	/// </summary>
	OrganicSedimentary = 13,

	/// <summary>
	///     Metamorphic rocks are created from existing rocks (igneous or sedimentary) by extreme pressure and/or heat.
	/// </summary>
	Metamorphic = 20,

	/// <summary>
	///     Metamorphic rocks formed when extreme, directional pressure squeezes the rock, causing flat or elongated minerals
	///     to align in parallel layers/bands. Typically, examples of this type have a progression as time under the pressure
	///     increases. Examples include slate, phyllite, schist, and gneiss.
	/// </summary>
	MetamorphicFoliated = 21,

	/// <summary>
	///     Metamorphic rocks formed when extreme, uniform pressure squeezes the rock, or when high heat from nearby magma
	///     heats the rock under low pressure. Examples include marble and quartzite.
	/// </summary>
	MetamorphicNonFoliated = 22
}
