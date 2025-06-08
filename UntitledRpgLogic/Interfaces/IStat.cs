using UntitledRpgLogic.Enums;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for a Stat in the RPG logic.
/// </summary>
public interface IStat : IHasName, IHasValue, IHasGuid, IHasLogging
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
    ///     Apply a modifier to the stat, which can be a buff, debuff, or any other effect that modifies the stat's value.
    /// </summary>
    /// <param name="modifier"></param>
    void ApplyModifier(IModifier modifier);

    /// <summary>
    ///     Event raised when the base value of the stat changes. Should trigger recalculation of the apparent value.
    /// </summary>
    event Action? BaseValueChanged;
}