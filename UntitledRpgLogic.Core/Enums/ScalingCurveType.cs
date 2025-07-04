namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     How the experience points required for each level are scaled.
/// </summary>
public enum ScalingCurveType
{
    /// <summary>
    ///     Applies no additional scaling to the experience points required for each level.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Linear scaling simply increases the value by a constant amount.
    /// </summary>
    Linear = 1,

    /// <summary>
    ///     Polynomial scaling increases the value by a power of the level.
    /// </summary>
    Exponential = 2,

    /// <summary>
    ///     Logarithmic scaling increases the value by a logarithmic function of the level.
    /// </summary>
    Polynomial = 3
}
