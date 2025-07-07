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
    public const int STAT_CREATED = 1000;

    /// <summary>
    ///     Event fired when a stat is damaged, lowering its value.
    /// </summary>
    public const int STAT_DAMAGE_TAKEN = 1001;

    /// <summary>
    ///     Event fired when a stat is healed, restoring its value.
    /// </summary>
    public const int STAT_HEALED = 1002;

    /// <summary>
    ///     Event fired when a modifier is applied to a stat, changing its value.
    /// </summary>
    public const int MODIFIER_APPLIED = 1003;

    /// <summary>
    ///     Event fired when an illegal change is attempted on a stat, such as exceeding maximum or minimum values.
    /// </summary>
    public const int STAT_ILLEGAL_CHANGE = 1099;

    // Skill-related Events (2000-2999) - Now more generic
    /// <summary>
    ///     Event fired when a skill is created or initialized.
    /// </summary>
    public const int SKILL_CREATED = 2000;

    /// <summary>
    ///     Event fired when a skill is leveled up, increasing its level.
    /// </summary>
    public const int SKILL_ADD_POINTS_MAX_LEVEL = 2003;

    // Entity-related Events (3000-3999)
    /// <summary>
    ///     Event fired when an entity is created or initialized.
    /// </summary>
    public const int ENTITY_CREATED = 3000;

    // Leveling-related Events (4000-4999) - NEW
    /// <summary>
    ///     Event fired when a levelable entity's level changes, such as a skill or character.
    /// </summary>
    public const int LEVELABLE_LEVEL_CHANGED = 4000;

    /// <summary>
    ///     Event fired when a levelable entity's points change, such as when points are added or removed.
    /// </summary>
    public const int LEVELABLE_POINTS_CHANGED = 4001;
}
