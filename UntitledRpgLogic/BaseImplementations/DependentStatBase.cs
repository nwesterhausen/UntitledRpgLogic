using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     A stat that depends on other stats or conditions to determine its value.
/// </summary>
public abstract class DependentStatBase : StatBase
{
    /// <summary>
    ///     Create a new instance of <see cref="DependentStatBase" />.
    /// </summary>
    /// <param name="options"></param>
    protected DependentStatBase(StatOptions options) : base(options)
    {
    }

    /// <summary>
    ///     Link this stat to another stat with a specific ratio. This is used to create dependencies between stats,
    ///     but only for minor, pseudo, or complex stats. Major stats are designed to be independent and cannot be linked
    ///     to other stats.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="ratio"></param>
    internal void LinkTo(StatBase other, double ratio = 1.0)
    {
        if (Variation == StatVariation.Major)
        {
            // Major stats cannot be linked to other stats.
            var ex = new InvalidOperationException("Major stats cannot be linked to other stats.");
#if DEBUG
            throw ex;
#endif
            LogError(ex, LoggingEventIds.STAT_LINKED);
            return;
        }

        LogEvent(LoggingEventIds.STAT_LINKED, Name, other.Name, ratio);
        // Logic to link this stat to another stat with a specific ratio
    }
}