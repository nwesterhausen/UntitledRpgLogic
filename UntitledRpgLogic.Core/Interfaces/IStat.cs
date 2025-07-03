using UntitledRpgLogic.Enums;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for a Stat in the RPG logic.
/// </summary>
public interface IStat : IHasName, IHasChangeableValue, IHasGuid, IHasLogging, IInstantiable
{
    /// <summary>
    ///     The variation of the stat, which helps qualify how to display the stat in the UI or how it behaves in the game
    ///     logic.
    /// </summary>
    StatVariation Variation { get; }

    /// <summary>
    ///     The maximum value the stat can reach. This is used to limit the stat's value and prevent it from exceeding a
    ///     certain threshold.
    /// </summary>
    int MaxValue { get; }

    /// <summary>
    ///     The minimum value the stat can have. Only useful if the stat should be starting at a value above zero that it
    ///     cannot drop below.
    /// </summary>
    int MinValue { get; }

    /// <summary>
    ///     The effective maximum value of the stat, which is the difference between MaxValue and MinValue.
    /// </summary>
    public int EffectiveMaxValue => MaxValue - MinValue;

    /// <summary>
    ///     The effective value of the stat, which is the difference between Value and MinValue.
    /// </summary>
    public int EffectiveValue => Value - MinValue;

    /// <summary>
    ///     The effective percentage of the stat, which is the EffectiveValue divided by EffectiveMaxValue.
    /// </summary>
    public float EffectivePercent => (float)EffectiveValue / EffectiveMaxValue;

    /// <summary>
    ///     The percentage of the stat, which is the Value divided by MaxValue.
    /// </summary>
    public float Percent => EffectivePercent;

    /// <summary>
    ///     The current base value of the stat, which is the underlying value before any modifications.
    /// </summary>
    public int BaseValue { get; }

    /// <summary>
    ///     Get the stats that are linked to this stat. This is used to retrieve all the stats that this stat depends on.
    /// </summary>
    Dictionary<Guid, float> LinkedStats { get; }

    /// <summary>
    ///     Apply a modifier to the stat, which can be a buff, debuff, or any other effect that modifies the stat's value.
    /// </summary>
    /// <param name="modifier"></param>
    void ApplyModifier(IModifier modifier);

    /// <summary>
    ///     Event raised when the base value of the stat changes. Should trigger recalculation of the apparent value.
    /// </summary>
    event Action? BaseValueChanged;

    /// <summary>
    ///     Link a stat to this stat. This is used to create dependencies between stats, where one stat's value is based
    ///     on or affected by another stat's value. This is useful for creating complex relationships between stats, such as
    ///     Health being dependent on Strength or Defense being affected by Agility.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="ratio">
    ///     A simple ratio that defines what percentage of the linked stat's value is added to the dependent
    ///     stat's value.
    /// </param>
    void LinkStat(IStat stat, float ratio = 1.0f);
}
