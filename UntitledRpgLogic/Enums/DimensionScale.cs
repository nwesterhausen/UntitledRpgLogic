using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Enums;

/// <summary>
///     Available Dimensions to use for shapes
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum DimensionScale
{
    /// <summary>
    ///     millimeter
    /// </summary>
    [Description("Millimeter")] mm = 5,

    /// <summary>
    ///     centimeter
    /// </summary>
    [Description("Centimeter")] cm = 6,

    /// <summary>
    ///     meter
    /// </summary>
    [Description("Meter")] m = 7,

    /// <summary>
    ///     kilometer
    /// </summary>
    [Description("Kilometer")] km = 8
}