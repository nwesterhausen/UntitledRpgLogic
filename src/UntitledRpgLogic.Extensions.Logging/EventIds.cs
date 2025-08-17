namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Event IDs for logging various events in the RPG logic.
/// </summary>
public static class EventIds
{
	// Category 1: Events related to the game engine services, like networking, game state changes, and game engine initialization.
	// Game Engine Events (1000-1099)

	// Networking Events (1100-1199)
	/// <summary>
	/// Event fired when the networking service starts successfully.
	/// </summary>
	public const int NetworkServiceStarted = 1100;

	// Category 2: Events related to the RPG logic, such as item storage, currency movement, stat modifications, and entity creation.
	// Item-related Events (3000-3099)

	// Stat-related Events (3100-3199)
	/// <summary>
	///     Event fired when a stat is created or initialized.
	/// </summary>
	public const int StatCreated = 3100;

	/// <summary>
	///     Event fired when a stat is damaged, lowering its value.
	/// </summary>
	public const int StatDamageTaken = 3101;

	/// <summary>
	///     Event fired when a stat is healed, restoring its value.
	/// </summary>
	public const int StatHealed = 3102;

	/// <summary>
	///     Event fired when a modifier is applied to a stat, changing its value.
	/// </summary>
	public const int ModifierApplied = 3103;

	/// <summary>
	///     Event fired when an illegal change is attempted on a stat, such as exceeding maximum or minimum values.
	/// </summary>
	public const int StatIllegalChange = 3199;

	// Skill-related Events (3200-3299) - Now more generic
	/// <summary>
	///     Event fired when a skill is created or initialized.
	/// </summary>
	public const int SkillCreated = 3200;

	/// <summary>
	///     Event fired when a skill is leveled up, increasing its level.
	/// </summary>
	public const int SkillAddPointsMaxLevel = 3201;

	// Entity-related Events (3300-3399)
	/// <summary>
	///     Event fired when an entity is created or initialized.
	/// </summary>
	public const int EntityCreated = 3300;

	// Leveling-related Events (3400-3499)
	/// <summary>
	///     Event fired when a levelable entity's level changes, such as a skill or character.
	/// </summary>
	public const int LevelableLevelChanged = 3400;

	/// <summary>
	///     Event fired when a levelable entity's points change, such as when points are added or removed.
	/// </summary>
	public const int LevelablePointsChanged = 3401;

	/// <summary>
	///     Event fired when a levelable entity's points change on a named item, such as a skill or character.
	/// </summary>
	public const int LevelablePointsChangedOnNamedItem = 3402;

	/// <summary>
	///     Event fired when a levelable entity's level changes on a named item, such as a skill or character.
	/// </summary>
	public const int LevelableLevelChangedOnNamedItem = 3403;
}
