using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Classes;

/// <summary>
///     Available Dimensions to use for shapes
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum DimensionScale
{
    /// <summary>
    ///     millimeter
    /// </summary>
    mm,

    /// <summary>
    ///     centimeter
    /// </summary>
    cm,

    /// <summary>
    ///     meter
    /// </summary>
    m,

    /// <summary>
    ///     kilometer
    /// </summary>
    km
}