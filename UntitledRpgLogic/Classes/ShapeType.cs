namespace UntitledRpgLogic.Classes;

/// <summary>
///     Shape types to be used and aid in dimension calculations for volume/area/etc.
/// </summary>
public enum ShapeType
{
    /// <summary>
    ///     Spherical shape
    /// </summary>
    Sphere,

    /// <summary>
    ///     Spheroid shape, which is a 3D shape that is a generalization of a sphere. The width is the major axis, height is
    ///     the
    ///     minor axis.
    /// </summary>
    Spheroid,

    /// <summary>
    ///     Ellipsoid shape, which is a 3D shape that is a generalization of a sphere. The width is the major axis, height is
    ///     the
    ///     minor axis, and depth is the intermediate axis.
    /// </summary>
    Ellipsoid,

    /// <summary>
    ///     Cylindrical shape.
    /// </summary>
    Cylinder,

    /// <summary>
    ///     Cone-shaped object, includes cones and conical frustums. The width is used for radius and height for cone height.
    /// </summary>
    Cone,

    /// <summary>
    ///     Conical frustum, which is a cone with the top cut off. The width is the radius of the base, height is the full
    ///     height of the cone, and depth is the height of the frustum.
    /// </summary>
    ConicalFrustum,

    /// <summary>
    ///     A regular pyramid
    /// </summary>
    Pyramid,

    /// <summary>
    ///     A cube
    /// </summary>
    Cube,

    /// <summary>
    ///     A rectangular prism
    /// </summary>
    RectangularPrism
}