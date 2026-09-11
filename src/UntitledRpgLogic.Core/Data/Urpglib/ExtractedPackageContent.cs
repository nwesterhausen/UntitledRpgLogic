using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Core.Data.Urpglib;

/// <summary>
/// 	The definitions extracted from a `.urpglib` file.
/// </summary>
public sealed record ExtractedPackageContent
{
	/// <summary>
	/// 	Collection of the defined entities.
	/// </summary>
	public IReadOnlyCollection<EntityDefinition> Entities { get; init; } = [];
	/// <summary>
	/// 	Collection of the defined stats.
	/// </summary>
	public IReadOnlyCollection<StatDefinition> Stats { get; init; } = [];
	/// <summary>
	/// 	Collection of the defined skills.
	/// </summary>
	public IReadOnlyCollection<SkillDefinition> Skills { get; init; } = [];
	/// <summary>
	/// 	Collection of the defined items.
	/// </summary>
	public IReadOnlyCollection<ItemDefinition> Items { get; init; } = [];
	/// <summary>
	/// 	Collection of the defined materials.
	/// </summary>
	public IReadOnlyCollection<MaterialDefinition> Materials { get; init; } = [];
}
