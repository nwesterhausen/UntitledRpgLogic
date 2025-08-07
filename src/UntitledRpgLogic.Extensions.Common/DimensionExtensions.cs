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
	/// <summary>
	/// Converts a dimensional value from a source scale to a target scale.
	/// </summary>
	/// <param name="value">The dimensional value to convert.</param>
	/// <param name="fromScale">The original scale of the value.</param>
	/// <param name="toScale">The target scale to convert to.</param>
	/// <returns>The converted value in the target scale.</returns>
	public static float Convert(this float value, DimensionScale fromScale, DimensionScale toScale)
	{
		// First, convert the input value to a base unit (meters)
		var valueInMeters = fromScale switch
		{
			DimensionScale.Mm => value / 1000.0f,
			DimensionScale.Cm => value / 100.0f,
			DimensionScale.M => value,
			_ => value // Default to assuming the original unit is meters if unknown
		};

		// Then, convert from the base unit to the target scale
		return toScale switch
		{
			DimensionScale.Mm => valueInMeters * 1000.0f,
			DimensionScale.Cm => valueInMeters * 100.0f,
			DimensionScale.M => valueInMeters,
			_ => valueInMeters // Default to returning meters if target is unknown
		};
	}

	/// <summary>
	/// Calculates the volume of an object in the specified target scale.
	/// </summary>
	/// <param name="dimensions">The object with dimensions.</param>
	/// <param name="targetScale">The desired scale for the resulting volume.</param>
	/// <returns>The total volume, expressed in the target scale.</returns>
	public static float CalculateVolumeIn(this IHasDimensions dimensions, DimensionScale targetScale)
	{
		ArgumentNullException.ThrowIfNull(dimensions, nameof(dimensions));

		var length = dimensions.Depth.Convert(dimensions.DimensionScale, targetScale);
		var width = dimensions.Width.Convert(dimensions.DimensionScale, targetScale);
		var height = dimensions.Height.Convert(dimensions.DimensionScale, targetScale);
		return length * width * height;
	}
}
