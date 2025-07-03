using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.Classes;

/// <summary>
///     Represents a stat dependency that links a dependent stat to another stat. When the linked stat's value changes,
///     the value is updated and then the dependent stat's value is recalculated. <seealso cref="IDependentStat" />
/// </summary>
public class LinkedStat
{
    /// <summary>
    ///     Delegate called to apply the value of the linked stat to the dependent stat's value.
    /// </summary>
    public delegate int ApplyValue(int dependentValue);

    /// <summary>
    ///     The ID of the stat 'linked' here. This is to make sure the stat is not added twice and to enable it to be
    ///     removed later on if needed.
    /// </summary>
    public required Guid StatId { get; init; }

    /// <summary>
    ///     The value of the stat that is linked here. This is the value that will be used to calculate the dependent stat's
    ///     value.
    /// </summary>
    public int StatValue { get; set; }

    /// <summary>
    ///     Priority of the linked stat to determine the order in which linked stats are applied to the dependent stat's value.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    ///     Calculation method that applies the value of the linked stat to the dependent stat's value.
    /// </summary>
    public required ApplyValue ValueCalculation { get; init; }
}
