namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Represents a modification that can be applied to a stat, such as a buff or debuff.
///     Supports stacking, duration, and different application rules.
/// </summary>
public interface IModifier : IHasName
{
    /// <summary>
    ///     Gets a value indicating whether the modification is permanent (true) or temporary (false).
    /// </summary>
    bool IsPermanent { get; }

    /// <summary>
    ///     Gets the current effective amount of the modification after considering stacks and duration.
    /// </summary>
    float EffectiveAmount { get; }

    /// <summary>
    ///     Gets a tooltip display string representing the modification (e.g., "+10%", "-5", "+20%").
    /// </summary>
    string Display { get; }

    /// <summary>
    ///     Gets the maximum number of stacks this modification can have.
    /// </summary>
    int MaxStacks { get; }

    /// <summary>
    ///     Gets the current number of stacks for this modification.
    /// </summary>
    int CurrentStacks { get; }

    /// <summary>
    ///     The effects this modification applies to the stat when it is active, for 0 or no stacks.
    /// </summary>
    IModifierEffect? ModificationEffect { get; }

    /// <summary>
    ///     The effect(s) that each stack of this modification has on the stat.
    /// </summary>
    IEnumerable<IModifierEffect>? StackEffects { get; }

    /// <summary>
    ///     Gets the duration of the modification in seconds as a float. If the modification is permanent, this should be -1.
    /// </summary>
    float Duration { get; }

    /// <summary>
    ///     Gets a value indicating whether all stacks are lost when the duration expires (true), or only one stack is lost
    ///     (false).
    /// </summary>
    bool LoseAllStacksOnExpiration { get; }

    /// <summary>
    ///     Gets the priority of the modification, used to determine the order in which modifications are applied.
    /// </summary>
    int Priority { get; }

    /// <summary>
    ///     Adds stacks to this modification.
    /// </summary>
    /// <param name="amount">The number of stacks to add. Defaults to 1.</param>
    void AddStack(int amount = 1);

    /// <summary>
    ///     Removes stacks from this modification.
    /// </summary>
    /// <param name="amount">The number of stacks to remove. Defaults to 1.</param>
    void RemoveStack(int amount = 1);

    /// <summary>
    ///     Processes the passage of time for this modification, updating duration and stacks as needed.
    /// </summary>
    /// <param name="deltaTime">The amount of time, in seconds, to process.</param>
    void ProcessDeltaTime(float deltaTime);

    /// <summary>
    ///     Applies the modification to the provided values and returns the modified value.
    /// </summary>
    /// <param name="baseValue">The base value of the stat before modifications.</param>
    /// <param name="currentValue">The current value of the stat before this modification is applied.</param>
    /// <param name="maxValue"></param>
    /// <returns>The modified stat value after applying this modification.</returns>
    int ApplyModification(int baseValue, int currentValue, int maxValue);

    /// <summary>
    ///     Reset the duration of this modification to its initial value.
    /// </summary>
    void RefreshDuration();

    /// <summary>
    ///     Occurs when the modification has expired and is no longer applied.
    /// </summary>
    event EventHandler ModificationExpired;

    /// <summary>
    ///     Occurs when the number of stacks for this modification changes.
    /// </summary>
    event EventHandler<int> StacksChanged;

    /// <summary>
    ///     Occurs when the duration of this modification changes.
    /// </summary>
    event EventHandler<float> DurationChanged;

    /// <summary>
    ///     Occurs when the modification is applied to a stat.
    ///     The event argument is the resulting value after application.
    /// </summary>
    event EventHandler<int> ModificationApplied;
}
