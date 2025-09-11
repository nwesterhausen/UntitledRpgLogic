using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Configuration for a Skill in the RPG logic.
/// </summary>
public record SkillConfig
{
	/// <summary>
	///     Items will always have a name. This is required.
	/// </summary>
	public required string Name { get; init; }

	/// <summary>
	///     A short description of the item. This is optional and can be used to provide additional context or flavor text
	/// </summary>
	public string? Description { get; init; }

	/// <summary>
	///     Options for the leveling behavior of the skill. If not provided, the default leveling options will be used.
	/// </summary>
	public LevelingOptions LevelingOptions { get; init; }

	/// <summary>
	///     The ULID identifier for this skill configuration. This is required.
	/// </summary>
	public Ulid Id { get; init; } = Ulid.NewUlid();
}
