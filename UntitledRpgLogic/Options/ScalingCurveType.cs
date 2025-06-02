namespace UntitledRpgLogic.Options;

/// <summary>
///     How the experience points required for each level are scaled.
/// </summary>
public enum ScalingCurveType
{
    /// <summary>
    ///     Applies no additional scaling to the experience points required for each level.
    /// </summary>
    None,

    /// <summary>
    ///     Linear scaling simply increases the value by a constant amount.
    /// </summary>
    Linear,

    /// <summary>
    ///     Polynomial scaling increases the value by a power of the level.
    /// </summary>
    Parabolic,

    /// <summary>
    ///     Logarithmic scaling increases the value by a logarithmic function of the level.
    /// </summary>
    Logarithmic
}