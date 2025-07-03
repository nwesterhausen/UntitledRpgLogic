using UntitledRpgLogic.Classes;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for dependent stats that rely on other stats to calculate their value.
/// </summary>
public interface IDependentStat : IStat
{
    /// <summary>
    ///     Links this dependent stat to another stat, allowing it to calculate its value based on the linked stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="valueCalculation"></param>
    /// <param name="priority"></param>
    void LinkTo(IStat stat, LinkedStat.ApplyValue valueCalculation, int priority = 0);

    /// <summary>
    ///     Unlinks a stat from this dependent stat, removing the dependency.
    /// </summary>
    /// <param name="stat">Stat to remove from the list of linked stats</param>
    void Unlink(IStat stat);
}
