using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
///     Configuration for a Skill in the RPG logic.
/// </summary>
public record SkillDataConfig : ITomlConfig
{
	/// <summary>
	///     The ULID identifier for this skill configuration. This is required.
	/// </summary>
	public Ulid Identifier { get; set; }

	/// <summary>
	///     Items will always have a name. This is required.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	///     A short description of the item. This is optional and can be used to provide additional context or flavor text
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	///     Options for the leveling behavior of the skill. If not provided, the default leveling options will be used.
	/// </summary>
	public LevelingOptions? LevelingOptions { get; set; }

	/// <inheritdoc />
	public ConfigType ConfigType => ConfigType.Skill;
}
