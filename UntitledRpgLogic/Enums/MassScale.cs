using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Enums;

/// <summary>
///     Scale for mass measurements in the RPG logic.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum MassScale
{
    /// <summary>
    ///     milligram
    /// </summary>
    mg,

    /// <summary>
    ///     gram
    /// </summary>
    g,

    /// <summary>
    ///     kilogram
    /// </summary>
    kg
}