using UntitledRpgLogic.LibraryFile;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Summary of definition entities successfully ingested from a package.
/// </summary>
public sealed record PackageIngestionResult
{
	/// <summary>
	///     The manifest extracted from the package header.
	/// </summary>
	public required PackageManifest Manifest { get; init; }

	/// <summary>
	/// 	Count of loaded <see cref="EntityDefinition" />
	/// </summary>
	public int EntitiesLoaded { get; init; }
	/// <summary>
	/// 	Count of loaded <see cref="StatDefinition" />
	/// </summary>
	public int StatsLoaded { get; init; }
	/// <summary>
	/// 	Count of loaded <see cref="SkillDefinition" />
	/// </summary>
	public int SkillsLoaded { get; init; }
	/// <summary>
	/// 	Count of loaded <see cref="ItemDefinition" />
	/// </summary>
	public int ItemsLoaded { get; init; }
	/// <summary>
	/// 	Count of loaded <see cref="MaterialDefinition" />
	/// </summary>
	public int MaterialsLoaded { get; init; }
	/// <summary>
	/// 	Count of all loaded definitions
	/// </summary>
	public int TotalDefinitionsLoaded => this.EntitiesLoaded + this.StatsLoaded + this.SkillsLoaded + this.ItemsLoaded + this.MaterialsLoaded;
}
