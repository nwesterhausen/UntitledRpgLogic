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
    mm = 5,

    /// <summary>
    ///     centimeter
    /// </summary>
    cm = 6,

    /// <summary>
    ///     meter
    /// </summary>
    m = 7,

    /// <summary>
    ///     kilometer
    /// </summary>
    km = 8
}