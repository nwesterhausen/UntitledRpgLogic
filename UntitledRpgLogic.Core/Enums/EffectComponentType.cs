namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines the various types of effect components that can compose a larger effect or spell.
/// </summary>
public enum EffectComponentType
{
    /// <summary>
    ///     No specific component type assigned.
    /// </summary>
    None = 0,

    /// <summary>
    ///     A component defining physical dimensions (width, height, depth, shape).
    /// </summary>
    Dimensions = 1,

    /// <summary>
    ///     A component defining physics-related properties (e.g., momentum, acceleration, direction).
    /// </summary>
    Physics = 2,

    /// <summary>
    ///     A component defining elemental properties (e.g., fire, ice, lightning, heat).
    /// </summary>
    Elemental = 3,

    /// <summary>
    ///     A component defining the duration or lifespan of an effect.
    /// </summary>
    Duration = 4,

    /// <summary>
    ///     A component defining how an effect targets entities (e.g., single, area, touch).
    /// </summary>
    Targeting = 5,

    /// <summary>
    ///     A component that applies stat modifications (buffs/debuffs).
    /// </summary>
    StatModification = 6,

    /// <summary>
    ///     A component defining properties for summoning creatures or entities.
    /// </summary>
    Summoning = 7
}
