using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extension methods for <see cref="Dimensions" />.
/// </summary>
public static class HasDimensionsExtensions
{
	/// <summary>
	///     Creates a human-readable string representing the object's key dimensions.
	/// </summary>
	/// <param name="dimensions">The object to format.</param>
	/// <returns>A formatted string describing the dimensions.</returns>
	public static string ToDimensionsString(this Dimensions dimensions)
	{
		ArgumentNullException.ThrowIfNull(dimensions);
		// Using a switch expression is cleaner and more concise.
		var details = dimensions.ShapeType switch
		{
			ShapeType.Sphere or ShapeType.Spheroid or ShapeType.Ellipsoid =>
				$"Radius: {dimensions.Width / 2f}",

			ShapeType.Cylinder or ShapeType.Cone =>
				$"Radius: {dimensions.Width / 2f}, Height: {dimensions.Height}",

			ShapeType.Pyramid or ShapeType.Cube or ShapeType.RectangularPrism =>
				$"W: {dimensions.Width}, H: {dimensions.Height}, D: {dimensions.Depth}",

			ShapeType.ConicalFrustum =>
				$"Top Radius: {dimensions.Width / 2f}, Bottom Radius: {dimensions.Depth / 2f}, Height: {dimensions.Height}",

			ShapeType.None => "No dimensions",

			// The default arm now throws an exception for unhandled shapes, which is safer.
			_ => throw new NotSupportedException(
				$"Unsupported shape type for dimension string conversion: {dimensions.ShapeType}")
		};

		// Append the dimension scale for clarity.
		return $"{details} ({dimensions.DimensionScale})";
	}

	/// <summary>
	///     Converts a dimensional value from a source scale to a target scale.
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
			DimensionScale.Km => value * 1000.0f,
			DimensionScale.None => 0f,
			_ => value // Default to assuming the original unit is meters if unknown
		};

		// Then, convert from the base unit to the target scale
		return toScale switch
		{
			DimensionScale.Mm => valueInMeters * 1000.0f,
			DimensionScale.Cm => valueInMeters * 100.0f,
			DimensionScale.M => valueInMeters,
			DimensionScale.Km => valueInMeters / 1000.0f,
			DimensionScale.None => 0f,
			_ => valueInMeters // Default to returning meters if target is unknown
		};
	}

	/// <summary>
	///     Calculates the volume of an object in the specified target scale.
	/// </summary>
	/// <param name="dimensions">The object with dimensions.</param>
	/// <param name="targetScale">The desired scale for the resulting volume.</param>
	/// <returns>The total volume, expressed in the target scale.</returns>
	public static float CalculateVolumeIn(Dimensions dimensions, DimensionScale targetScale)
	{
		ArgumentNullException.ThrowIfNull(dimensions);

		var clonedDimensions = dimensions with { };
		clonedDimensions.ChangeScale(targetScale);

		return clonedDimensions.CalculateVolume();
	}

	/// <summary>
	///     Calculates the volume of the object based on its shape and dimensions.
	/// </summary>
	/// <returns>Volume in cubic units of the current <see cref="DimensionScale" />, or 0f if shape is unknown.</returns>
	public static float CalculateVolume(this Dimensions dimensions)
	{
		ArgumentNullException.ThrowIfNull(dimensions);

		return dimensions.ShapeType switch
		{
			// Volume of a general ellipsoid is (1/6) * pi * W * H * D
			ShapeType.Sphere or ShapeType.Spheroid or ShapeType.Ellipsoid =>
				1f / 6f * MathF.PI * dimensions.Width * dimensions.Height * dimensions.Depth,

			ShapeType.Cylinder => MathF.PI * MathF.Pow(dimensions.Width / 2f, 2) * dimensions.Height,

			ShapeType.Cone => 1f / 3f * MathF.PI * MathF.Pow(dimensions.Width / 2f, 2) * dimensions.Height,

			// Volume of a pyramid: (1/3) * base_area * h. Assuming rectangular base.
			ShapeType.Pyramid => 1f / 3f * (dimensions.Width * dimensions.Depth) * dimensions.Height,

			// Assuming Cube means a rectangular prism. If it's a literal cube, use MathF.Pow(dimensions.Width, 3).
			ShapeType.Cube or ShapeType.RectangularPrism => dimensions.Width * dimensions.Height * dimensions.Depth,

			// Assuming Width is diameter of base 1 and Depth is diameter of base 2.
			ShapeType.ConicalFrustum => 1f / 3f * MathF.PI * dimensions.Height *
			                            (MathF.Pow(dimensions.Width / 2f, 2) +
			                             (dimensions.Width / 2f * (dimensions.Depth / 2f)) +
			                             MathF.Pow(dimensions.Depth / 2f, 2)),

			_ => 0f // A discard pattern handles any unlisted enum members.
		};
	}

	/// <summary>
	///     Converts the dimensions from its current scale to the specified target scale.
	/// </summary>
	/// <param name="dimensions">The dimensions to change the scale of.</param>
	/// <param name="targetScale">The dimension scale to convert to.</param>
	public static void ChangeScale(this Dimensions dimensions, DimensionScale targetScale)
	{
		ArgumentNullException.ThrowIfNull(dimensions);

		if (dimensions.DimensionScale == targetScale)
		{
			return;
		}

		var factorFromCurrentToMeters = dimensions.DimensionScale.GetMetersPerUnit();
		var factorFromMetersToTarget = 1f / targetScale.GetMetersPerUnit();
		var conversionFactor = factorFromCurrentToMeters * factorFromMetersToTarget;

		dimensions.Width *= conversionFactor;
		dimensions.Height *= conversionFactor;
		dimensions.Depth *= conversionFactor;

		// IMPORTANT: Update the scale property to reflect the new unit.
		dimensions.DimensionScale = targetScale;
	}

	/// <summary>
	///     Helper to get the conversion factor from a given unit to a canonical unit (meters).
	/// </summary>
	private static float GetMetersPerUnit(this DimensionScale scale) => scale switch
	{
		DimensionScale.Mm => 0.001f,
		DimensionScale.Cm => 0.01f,
		DimensionScale.M => 1f,
		DimensionScale.Km => 1000f,
		// By omitting the `_` discard pattern, the compiler will produce a warning (CS8509)
		// if a new member is added to DimensionScale and not handled here.
		// This provides the compile-time safety you were asking about.
		// For absolute runtime safety against invalid casted enum values, you can add:
		_ => throw new ArgumentOutOfRangeException(nameof(scale), $"Unsupported dimension scale: {scale}")
	};
}
