namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines how an effect targets entities or areas.
/// </summary>
public enum TargetingType
{
    /// <summary>
    ///     No specific targeting type defined.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Targets a single entity.
    /// </summary>
    Single = 1,

    /// <summary>
    ///     Targets an area, affecting all entities within that area.
    /// </summary>
    AreaOfEffect = 2,

    /// <summary>
    ///     Requires physical touch to apply the effect.
    /// </summary>
    Touch = 3,

    /// <summary>
    ///     Targets the caster or user of the effect.
    /// </summary>
    Self = 4
}
