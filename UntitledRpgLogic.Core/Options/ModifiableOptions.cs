namespace UntitledRpgLogic.Core.Options;

/// <summary>
///     Options available when creating or configuring a stat modification.
/// </summary>
public class ModifiableOptions
{
    /// <summary>
    ///     Indicates whether the modification is permanent (true) or temporary (false).
    /// </summary>
    public bool? IsPermanent { get; init; }

    /// <summary>
    ///     Indicates whether the modification is positive (buff) or negative (debuff).
    /// </summary>
    public bool? IsPositive { get; init; }

    /// <summary>
    ///     Indicates whether the modification is additive (true) or multiplicative (false).
    /// </summary>
    public bool? IsAdditive { get; init; }

    /// <summary>
    ///     Indicates whether the modification is a percentage (true) or a flat value (false).
    /// </summary>
    public bool? IsPercentage { get; init; }

    /// <summary>
    ///     Indicates whether the modification scales with the base value (true) or the current value (false).
    /// </summary>
    public bool? ScalesOnBaseValue { get; init; }

    /// <summary>
    ///     The amount of the modification, either as a flat value or percentage.
    /// </summary>
    public float? Amount { get; init; }

    /// <summary>
    ///     The maximum number of stacks this modification can have.
    /// </summary>
    public int? MaxStacks { get; init; }

    /// <summary>
    ///     The current number of stacks for this modification.
    /// </summary>
    public int? CurrentStacks { get; init; }

    /// <summary>
    ///     The effect(s) that each stack of this modification has on the stat.
    /// </summary>
    public ModifierEffectOptions? StackEffect { get; init; }

    /// <summary>
    ///     The duration of the modification in seconds as a float. If the modification is permanent, this should be -1.
    /// </summary>
    public float? Duration { get; init; }

    /// <summary>
    ///     Indicates whether all stacks are lost when the duration expires (true), or only one stack is lost (false).
    /// </summary>
    public bool? LoseAllStacksOnExpiration { get; init; }

    /// <summary>
    ///     The priority of the modification, used to determine the order in which modifications are applied.
    /// </summary>
    public int? ModificationPriority { get; init; }
}
