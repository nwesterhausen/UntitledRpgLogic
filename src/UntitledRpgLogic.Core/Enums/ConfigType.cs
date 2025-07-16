namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Specifies the type of configuration used within the application.
/// </summary>
/// <remarks>
///     The <see cref="ConfigType" /> enumeration defines various configuration types that can be used to
///     categorize and manage different data configurations within the application. This allows for flexible and organized
///     handling of configuration data, such as items, materials, skills, and stats.
/// </remarks>
public enum ConfigType
{
	/// <summary>
	///     Default / Unknown configuration type.
	/// </summary>
	Unknown,

	/// <summary>
	///     Configuration that describes the author of a configuration module.
	/// </summary>
	Author,

	/// <summary>
	///     Configuration that describes a module, which can contain multiple configurations.
	/// </summary>
	ModuleInfo,

	/// <summary>
	///     Configuration for an ItemDataConfig.
	/// </summary>
	Item,

	/// <summary>
	///     Configuration for a MaterialDataConfig.
	/// </summary>
	Material,

	/// <summary>
	///     Configuration for a SkillDataConfig.
	/// </summary>
	Skill,

	/// <summary>
	///     Configuration for a StatDataConfig.
	/// </summary>
	Stat
	// Add other configuration types as your game grows
}
