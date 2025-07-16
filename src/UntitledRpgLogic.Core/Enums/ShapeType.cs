namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Shape types to be used and aid in dimension calculations for volume/area/etc.
/// </summary>
public enum ShapeType
{
    /// <summary>
    ///     Spherical shape
    /// </summary>
    Sphere = 0,

    /// <summary>
    ///     Spheroid shape, which is a 3D shape that is a generalization of a sphere. The width is the major axis, height is
    ///     the
    ///     minor axis.
    /// </summary>
    Spheroid = 1,

    /// <summary>
    ///     Ellipsoid shape, which is a 3D shape that is a generalization of a sphere. The width is the major axis, height is
    ///     the
    ///     minor axis, and depth is the intermediate axis.
    /// </summary>
    Ellipsoid = 2,

    /// <summary>
    ///     Cylindrical shape.
    /// </summary>
    Cylinder = 3,

    /// <summary>
    ///     Cone-shaped object, includes cones and conical frustums. The width is used for radius and height for cone height.
    /// </summary>
    Cone = 4,

    /// <summary>
    ///     Conical frustum, which is a cone with the top cut off. The width is the radius of the base, height is the full
    ///     height of the cone, and depth is the height of the frustum.
    /// </summary>
    ConicalFrustum = 5,

    /// <summary>
    ///     A regular pyramid
    /// </summary>
    Pyramid = 6,

    /// <summary>
    ///     A cube
    /// </summary>
    Cube = 7,

    /// <summary>
    ///     A rectangular prism
    /// </summary>
    RectangularPrism = 8
}
