namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for a Stat in the RPG logic.
/// </summary>
public interface IStat : IHasName, IHasValue, IHasGuid, IHasLogging
{
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