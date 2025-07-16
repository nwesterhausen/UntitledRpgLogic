using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extension methods for types implementing <see cref="IHasDimensions" />.
/// </summary>
public static class HasDimensionsExtensions
{
	/// <summary>
	///     Creates a human-readable string representing the object's key dimensions.
	/// </summary>
	/// <param name="dimensions">The object to format.</param>
	/// <returns>A formatted string describing the dimensions.</returns>
	public static string ToDimensionsString(this IHasDimensions dimensions)
	{
		ArgumentNullException.ThrowIfNull(dimensions, nameof(dimensions));
		// Using a switch expression is cleaner and more concise.
		var details = dimensions.ShapeType switch
		{
			ShapeType.Sphere or ShapeType.Spheroid or ShapeType.Ellipsoid =>
				$"Radius: {dimensions.Width / 2f}",

			ShapeType.Cylinder or ShapeType.Cone =>
				$"Radius: {dimensions.Width / 2f}, Height: {dimensions.Height}",

			ShapeType.Pyramid or ShapeType.Cube =>
				$"W: {dimensions.Width}, H: {dimensions.Height}, D: {dimensions.Depth}",

			ShapeType.ConicalFrustum =>
				$"Top Radius: {dimensions.Width / 2f}, Bottom Radius: {dimensions.Depth / 2f}, Height: {dimensions.Height}",
			ShapeType.RectangularPrism => throw new NotImplementedException(),

			// The default arm now throws an exception for unhandled shapes, which is safer.
			_ => throw new NotSupportedException(
				$"Unsupported shape type for dimension string conversion: {dimensions.ShapeType}")
		};

		// Append the dimension scale for clarity.
		return $"{details} ({dimensions.DimensionScale})";
	}
}
