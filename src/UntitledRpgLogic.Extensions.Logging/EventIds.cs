namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Event IDs for logging various events in the RPG logic.
/// </summary>
public static class EventIds
{
	// Stat-related Events (1000-1999)
	/// <summary>
	///     Event fired when a stat is created or initialized.
	/// </summary>
	public const int StatCreated = 1000;

	/// <summary>
	///     Event fired when a stat is damaged, lowering its value.
	/// </summary>
	public const int StatDamageTaken = 1001;

	/// <summary>
	///     Event fired when a stat is healed, restoring its value.
	/// </summary>
	public const int StatHealed = 1002;

	/// <summary>
	///     Event fired when a modifier is applied to a stat, changing its value.
	/// </summary>
	public const int ModifierApplied = 1003;

	/// <summary>
	///     Event fired when an illegal change is attempted on a stat, such as exceeding maximum or minimum values.
	/// </summary>
	public const int StatIllegalChange = 1099;

	// Skill-related Events (2000-2999) - Now more generic
	/// <summary>
	///     Event fired when a skill is created or initialized.
	/// </summary>
	public const int SkillCreated = 2000;

	/// <summary>
	///     Event fired when a skill is leveled up, increasing its level.
	/// </summary>
	public const int SkillAddPointsMaxLevel = 2003;

	// Entity-related Events (3000-3999)
	/// <summary>
	///     Event fired when an entity is created or initialized.
	/// </summary>
	public const int EntityCreated = 3000;

	// Leveling-related Events (4000-4999) - NEW
	/// <summary>
	///     Event fired when a levelable entity's level changes, such as a skill or character.
	/// </summary>
	public const int LevelableLevelChanged = 4000;

	/// <summary>
	///     Event fired when a levelable entity's points change, such as when points are added or removed.
	/// </summary>
	public const int LevelablePointsChanged = 4001;

	/// <summary>
	///     Event fired when a levelable entity's points change on a named item, such as a skill or character.
	/// </summary>
	public const int LevelablePointsChangedOnNamedItem = 4002;

	/// <summary>
	///     Event fired when a levelable entity's level changes on a named item, such as a skill or character.
	/// </summary>
	public const int LevelableLevelChangedOnNamedItem = 4003;
}
