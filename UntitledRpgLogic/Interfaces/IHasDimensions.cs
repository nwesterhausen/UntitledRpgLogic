using UntitledRpgLogic.Enums;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Defines an interface for classes that have dimensions: width, height, and depth.
/// </summary>
public interface IHasDimensions
{
    /// <summary>
    ///     What scale the dimensions are in.
    /// </summary>
    public DimensionScale DimensionScale { get; }

    /// <summary>
    ///     The type of shape the object is. This is used to determine how the dimensions are interpreted.
    /// </summary>
    public ShapeType ShapeType { get; }

    /// <summary>
    ///     The width of the object in the specified dimension scale.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    ///     The height of the object in the specified dimension scale.
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    ///     The depth of the object in the specified dimension scale.
    /// </summary>
    public float Depth { get; set; }

    /// <summary>
    ///     The volume of the object in the specified dimension scale.
    /// </summary>
    public float Volume { get; }
}