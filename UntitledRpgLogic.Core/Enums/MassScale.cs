using System.ComponentModel;
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
    [Description("Milligram")] mg = 10,

    /// <summary>
    ///     gram
    /// </summary>
    [Description("Gram")] g = 11,

    /// <summary>
    ///     kilogram
    /// </summary>
    [Description("Kilogram")] kg = 12
}
