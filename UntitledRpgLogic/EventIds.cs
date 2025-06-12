using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic;

/// <summary>
///     Event IDs for logging various events in the RPG logic.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class EventIds
{
    /// <summary>
    ///     Integer value for stat creation event.
    /// </summary>
    public const int STAT_CREATED_INT_VALUE = 1000;

    /// <summary>
    ///     Integer value for stat linked event.
    /// </summary>
    public const int STAT_LINKED_INT_VALUE = 1001;

    /// <summary>
    ///     Integer value for stat unlinked event.
    /// </summary>
    public const int STAT_UNLINKED_INT_VALUE = 1002;

    /// <summary>
    ///     Integer value for stat link exists event.
    /// </summary>
    public const int STAT_LINK_EXISTS_INT_VALUE = 1003;

    /// <summary>
    ///     Integer value for stat link not found event.
    /// </summary>
    public const int STAT_LINK_NOT_FOUND_INT_VALUE = 1004;

    /// <summary>
    ///     Integer value for stat value changed event.
    /// </summary>
    public const int STAT_VALUE_CHANGED_INT_VALUE = 1005;

    /// <summary>
    ///     Integer value for invalid sender in stat change event.
    /// </summary>
    public const int STAT_INVALID_SENDER_INT_VALUE = 1006;

    /// <summary>
    ///     Integer value for illegal stat change event.
    /// </summary>
    public const int STAT_ILLEGAL_CHANGE_INT_VALUE = 1099;

    /// <summary>
    ///     Integer value for skill creation event.
    /// </summary>
    public const int SKILL_CREATED_INT_VALUE = 2000;

    /// <summary>
    ///     Integer value for skill level changed event.
    /// </summary>
    public const int SKILL_LEVEL_CHANGED_INT_VALUE = 2001;

    /// <summary>
    ///     Integer value for skill points changed event.
    /// </summary>
    public const int SKILL_POINTS_CHANGED_INT_VALUE = 2002;

    /// <summary>
    ///     Integer value for entity created event.
    /// </summary>
    public const int ENTITY_CREATED_INT_VALUE = 3000;

    /// <summary>
    ///     Event ID for stat creation.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>stat</term>
    ///                 <description>StatBase</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId STAT_CREATED = new(STAT_CREATED_INT_VALUE, nameof(STAT_CREATED));

    /// <summary>
    ///     Event ID for when a stat is linked to another stat.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>majorName</term>
    ///                 <description>string</description>
    ///             </item>
    ///             <item>
    ///                 <term>minorName</term>
    ///                 <description>string</description>
    ///             </item>
    ///             <item>
    ///                 <term>ratio</term>
    ///                 <description>double</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId STAT_LINKED = new(STAT_LINKED_INT_VALUE, nameof(STAT_LINKED));

    /// <summary>
    ///     Event ID for when a stat is unlinked from another stat.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>majorName</term>
    ///                 <description>string</description>
    ///             </item>
    ///             <item>
    ///                 <term>minorName</term>
    ///                 <description>string</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId STAT_UNLINKED = new(STAT_UNLINKED_INT_VALUE, nameof(STAT_UNLINKED));

    /// <summary>
    ///     Event ID for when a stat value changes.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>delta</term>
    ///                 <description>int</description>
    ///             </item>
    ///             <item>
    ///                 <term>statName</term>
    ///                 <description>string</description>
    ///             </item>
    ///             <item>
    ///                 <term>modifier</term>
    ///                 <description>string</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId STAT_VALUE_CHANGED = new(STAT_VALUE_CHANGED_INT_VALUE, nameof(STAT_VALUE_CHANGED));

    /// <summary>
    ///     Event ID for when a stat is attempted to be changed illegally.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>statName</term>
    ///                 <description>string</description>
    ///             </item>
    ///             <item>
    ///                 <term>attemptedAction</term>
    ///                 <description>string</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId
        STAT_ILLEGAL_CHANGE = new(STAT_ILLEGAL_CHANGE_INT_VALUE, nameof(STAT_ILLEGAL_CHANGE));

    /// <summary>
    ///     Event ID for skill creation.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>skill</term>
    ///                 <description>SkillBase</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId SKILL_CREATED = new(SKILL_CREATED_INT_VALUE, nameof(SKILL_CREATED));

    /// <summary>
    ///     Event ID for when a skill level changes.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>skillName</term>
    ///                 <description>string</description>
    ///             </item>
    ///             <item>
    ///                 <term>oldLevel</term>
    ///                 <description>int</description>
    ///             </item>
    ///             <item>
    ///                 <term>newLevel</term>
    ///                 <description>int</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId
        SKILL_LEVEL_CHANGED = new(SKILL_LEVEL_CHANGED_INT_VALUE, nameof(SKILL_LEVEL_CHANGED));

    /// <summary>
    ///     Event ID for when skill points change.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>skillName</term>
    ///                 <description>string</description>
    ///             </item>
    ///             <item>
    ///                 <term>pointsChange</term>
    ///                 <description>string</description>
    ///             </item>
    ///             <item>
    ///                 <term>progress</term>
    ///                 <description>string</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId SKILL_POINTS_CHANGED =
        new(SKILL_POINTS_CHANGED_INT_VALUE, nameof(SKILL_POINTS_CHANGED));

    /// <summary>
    ///     Event ID for when an entity is created.
    ///     <para>
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Parameter</term>
    ///                 <description>Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>entity</term>
    ///                 <description>EntityBase</description>
    ///             </item>
    ///         </list>
    ///     </para>
    /// </summary>
    public static readonly EventId ENTITY_CREATED = new(ENTITY_CREATED_INT_VALUE, nameof(ENTITY_CREATED));

    /// <summary>
    ///     Event ID for when a stat link already exists.
    /// </summary>
    public static readonly EventId STAT_LINK_EXISTS =
        new(STAT_LINK_EXISTS_INT_VALUE, nameof(STAT_LINK_EXISTS));

    /// <summary>
    ///     Event ID for when a stat link is not found (and we expect it to be found).
    /// </summary>
    public static readonly EventId STAT_LINK_NOT_FOUND =
        new(STAT_LINK_NOT_FOUND_INT_VALUE, nameof(STAT_LINK_NOT_FOUND));

    /// <summary>
    ///     Event ID for when a stat change is attempted with an invalid sender.
    /// </summary>
    public static readonly EventId STAT_INVALID_SENDER =
        new(STAT_INVALID_SENDER_INT_VALUE, nameof(STAT_INVALID_SENDER));
}