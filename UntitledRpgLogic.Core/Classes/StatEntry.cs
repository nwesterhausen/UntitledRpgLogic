using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     A data container for a stat and its related modifiers, mitigations, and damage state.
///     The logic for operating on this data is handled by an IStatService.
/// </summary>
/// <typeparam name="T">The stat contained within this entry</typeparam>
public class StatEntry<T> : IDamageable where T : IStat
{
    /// <summary>
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="isDamageable"></param>
    /// <param name="isHealable"></param>
    public StatEntry(T stat, bool isDamageable = false, bool isHealable = false)
    {
        if (isHealable && !isDamageable)
            throw new ArgumentException("A stat cannot be healable if it is not damageable.", nameof(isHealable));

        Stat = stat;
        IsDamageable = isDamageable;
        IsHealable = isHealable;
    }

    /// <summary>The stat that this entry represents.</summary>
    public T Stat { get; }

    /// <summary>Whether the stat is damageable or not.</summary>
    public bool IsDamageable { get; }

    /// <summary>Whether the stat can be healed or not.</summary>
    public bool IsHealable { get; }


    /// <summary>A list of mitigations that apply to this stat entry.</summary>
    public List<IAppliesDamageMitigation> Mitigations { get; } = [];

    /// <summary>A list of modifiers that apply to this stat entry.</summary>
    public List<IModifier> Modifiers { get; } = [];

    /// <summary>
    ///     A helper to calculate what percentage of the stat's maximum value a given point value represents.
    ///     This is simple, dependency-free logic that is acceptable in a data class.
    /// </summary>
    public float PointsAsPercentageOfMax(int pointValue)
    {
        if (Stat.MaxValue <= 0) return 0f;
        return pointValue / (float)Stat.MaxValue * 100f;
    }

    #region IDamageable Implementation

    // Explicitly implement the interface property to avoid confusion.
    IStat IDamageable.Stat => Stat;

    /// <inheritdoc />
    public int CurrentDamage { get; set; }

    /// <inheritdoc />
    public float CurrentPercentageDamage { get; set; }

    #endregion
}
