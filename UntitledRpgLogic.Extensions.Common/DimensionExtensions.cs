namespace UntitledRpgLogic.Extensions;

/// <summary>
///     Extensions for IHasDimensions interface to provide additional functionality related to dimensions.
/// </summary>
public static class DimensionExtensions
{
    /// <summary>
    ///     Converts the dimensions to a string representation.
    /// </summary>
    /// <param name="dimensions">The dimensions to convert.</param>
    /// <returns>A string representation of the dimensions.</returns>
    public static string ToDimensionString(this IHasDimensions dimensions)
    {
        switch (dimensions.ShapeType)
        {
            case ShapeType.Sphere or ShapeType.Spheroid or ShapeType.Ellipsoid:
                return $"Radius: {dimensions.Width / 2}";
            case ShapeType.Cylinder:
            case ShapeType.Cone:
                return $"Radius: {dimensions.Width / 2}, Height: {dimensions.Height}";
            case ShapeType.Pyramid:
                return $"Width: {dimensions.Width}, Height: {dimensions.Height}, Depth: {dimensions.Depth}";
            case ShapeType.Cube:
                return $"Width: {dimensions.Width}";
            case ShapeType.ConicalFrustum:
                return
                    $"Top Radius: {dimensions.Width / 2}, Bottom Radius: {dimensions.Depth / 2}, Height: {dimensions.Height}";
            default:
#if DEBUG
                throw new NotSupportedException("Unsupported shape type for dimension string conversion.");
#endif
                return $"Width: {dimensions.Width}, Height: {dimensions.Height}, Depth: {dimensions.Depth}";
        }
    }

    /// <summary>
    ///     Calculates the volume of the object based on its dimensions and shape type.
    /// </summary>
    /// <param name="dimensions"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static float CalculateVolume(this IHasDimensions dimensions)
    {
        switch (dimensions.ShapeType)
        {
            // (1/6) * π * W * H * H for Sphere, Spheroid, or Ellipsoid
            case ShapeType.Sphere or ShapeType.Spheroid or ShapeType.Ellipsoid:
                return 1f / 6f * MathF.PI * dimensions.Width * dimensions.Height * dimensions.Height;
            // π * r^2 * h for Cylinder
            case ShapeType.Cylinder:
                return MathF.PI * MathF.Pow(dimensions.Width / 2, 2) * dimensions.Height;
            // (1/3) * π * r^2 * h for Cone
            case ShapeType.Cone:
                return 1f / 3f * MathF.PI * MathF.Pow(dimensions.Width / 2, 2) * dimensions.Height;
            // (1/3) * W * H * D for Pyramid
            case ShapeType.Pyramid:
                return 1f / 3f * dimensions.Width * dimensions.Height * dimensions.Depth;
            // Width * Height * Depth for Rectangular Prism
            case ShapeType.Cube:
                return MathF.Pow(dimensions.Width, 3);
            // (1/3) * π * h * (r1^2 + r1 * r2 + r2^2) for Conical Frustum
            case ShapeType.ConicalFrustum:
                return 1f / 3f * MathF.PI * dimensions.Height * (MathF.Pow(dimensions.Width / 2, 2) +
                                                                 (dimensions.Width / 2 * (dimensions.Depth / 2)) +
                                                                 MathF.Pow(dimensions.Depth / 2, 2));
            default:
#if DEBUG
                throw new ArgumentOutOfRangeException("Unsupported shape type for volume calculation.");
#endif
                return 0f; // Default case, should not happen if all shape types are handled
        }
    }

    /// <summary>
    ///     Convert the dimensions to a different scale.
    /// </summary>
    /// <param name="dimensions"></param>
    /// <param name="scale"></param>
    public static void ChangeScale(this IHasDimensions dimensions, DimensionScale scale)
    {
        if (dimensions.DimensionScale == scale) return;

        switch (scale)
        {
            case DimensionScale.cm:
                dimensions.ConvertToCm();
                return;
            case DimensionScale.m:
                dimensions.ConvertToM();
                return;
            case DimensionScale.km:
                dimensions.ConvertToKm();
                return;
            case DimensionScale.mm:
                dimensions.ConvertToMm();
                return;
        }
    }

    /// <summary>
    ///     Convert the dimensions to millimeters (mm).
    /// </summary>
    /// <param name="dimensions"></param>
    public static void ConvertToMm(this IHasDimensions dimensions)
    {
        switch (dimensions.DimensionScale)
        {
            case DimensionScale.cm:
                dimensions.Width *= 10;
                dimensions.Height *= 10;
                dimensions.Depth *= 10;
                break;
            case DimensionScale.m:
                dimensions.Width *= 1000;
                dimensions.Height *= 1000;
                dimensions.Depth *= 1000;
                break;
            case DimensionScale.km:
                dimensions.Width *= 1_000_000;
                dimensions.Height *= 1_000_000;
                dimensions.Depth *= 1_000_000;
                break;
            case DimensionScale.mm:
                // Already in mm, do nothing
                break;
        }
    }

    /// <summary>
    ///     Convert the dimensions to centimeters (cm).
    /// </summary>
    /// <param name="dimensions"></param>
    public static void ConvertToCm(this IHasDimensions dimensions)
    {
        switch (dimensions.DimensionScale)
        {
            case DimensionScale.mm:
                dimensions.Width /= 10;
                dimensions.Height /= 10;
                dimensions.Depth /= 10;
                break;
            case DimensionScale.m:
                dimensions.Width *= 100;
                dimensions.Height *= 100;
                dimensions.Depth *= 100;
                break;
            case DimensionScale.km:
                dimensions.Width *= 100_000;
                dimensions.Height *= 100_000;
                dimensions.Depth *= 100_000;
                break;
            case DimensionScale.cm:
                // Already in cm, do nothing
                break;
        }
    }

    /// <summary>
    ///     Convert the dimensions to meters (m).
    /// </summary>
    /// <param name="dimensions"></param>
    public static void ConvertToM(this IHasDimensions dimensions)
    {
        switch (dimensions.DimensionScale)
        {
            case DimensionScale.mm:
                dimensions.Width /= 1000;
                dimensions.Height /= 1000;
                dimensions.Depth /= 1000;
                break;
            case DimensionScale.cm:
                dimensions.Width /= 100;
                dimensions.Height /= 100;
                dimensions.Depth /= 100;
                break;
            case DimensionScale.km:
                dimensions.Width *= 1000;
                dimensions.Height *= 1000;
                dimensions.Depth *= 1000;
                break;
            case DimensionScale.m:
                // Already in m, do nothing
                break;
        }
    }

    /// <summary>
    ///     Convert the dimensions to kilometers (km).
    /// </summary>
    /// <param name="dimensions"></param>
    public static void ConvertToKm(this IHasDimensions dimensions)
    {
        switch (dimensions.DimensionScale)
        {
            case DimensionScale.mm:
                dimensions.Width /= 1_000_000;
                dimensions.Height /= 1_000_000;
                dimensions.Depth /= 1_000_000;
                break;
            case DimensionScale.cm:
                dimensions.Width /= 100_000;
                dimensions.Height /= 100_000;
                dimensions.Depth /= 100_000;
                break;
            case DimensionScale.m:
                dimensions.Width /= 1000;
                dimensions.Height /= 1000;
                dimensions.Depth /= 1000;
                break;
            case DimensionScale.km:
                // Already in km, do nothing
                break;
        }
    }
}
