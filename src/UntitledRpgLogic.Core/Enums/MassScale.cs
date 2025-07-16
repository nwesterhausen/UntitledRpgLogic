using System.ComponentModel;

namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Scale for mass measurements in the RPG logic.
/// </summary>
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
