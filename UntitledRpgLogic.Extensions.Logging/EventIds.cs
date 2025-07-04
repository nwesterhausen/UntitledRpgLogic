namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Event IDs for logging various events in the RPG logic.
/// </summary>
public static class EventIds
{
    // Stat-related Events (1000-1999)
    public const int STAT_CREATED = 1000;
    public const int STAT_DAMAGE_TAKEN = 1001;
    public const int STAT_HEALED = 1002;
    public const int MODIFIER_APPLIED = 1003;
    public const int STAT_ILLEGAL_CHANGE = 1099;

    // Skill-related Events (2000-2999) - Now more generic
    public const int SKILL_CREATED = 2000;
    public const int SKILL_ADD_POINTS_MAX_LEVEL = 2003;

    // Entity-related Events (3000-3999)
    public const int ENTITY_CREATED = 3000;

    // Leveling-related Events (4000-4999) - NEW
    public const int LEVELABLE_LEVEL_CHANGED = 4000;
    public const int LEVELABLE_POINTS_CHANGED = 4001;
}
