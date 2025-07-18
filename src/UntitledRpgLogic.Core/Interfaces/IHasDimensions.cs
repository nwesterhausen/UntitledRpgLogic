// C:/Users/nwest/source/repos/UntitledRpgLogic/UntitledRpgLogic.Core/Interfaces/IHasDimensions.cs

using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines an interface for classes that have dimensions: width, height, and depth.
/// </summary>
public interface IHasDimensions
{
	/// <summary>
	///     What scale the dimensions are in.
	/// </summary>
	/// <remarks>
	///     This property must be settable to allow for scale conversions.
	/// </remarks>
	public DimensionScale DimensionScale { get; set; }

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
	///     The volume of the object. This can be cached in the implementing class
	///     if performance is a concern.
	/// </summary>
	public float Volume => this.CalculateVolume();

	/// <summary>
	///     Calculates the volume of the object based on its shape and dimensions.
	/// </summary>
	/// <returns>Volume in cubic units of the current <see cref="DimensionScale" />, or 0f if shape is unknown.</returns>
	public float CalculateVolume() => this.ShapeType switch
	{
		// Volume of a general ellipsoid is (1/6) * pi * W * H * D
		ShapeType.Sphere or ShapeType.Spheroid or ShapeType.Ellipsoid =>
			1f / 6f * MathF.PI * this.Width * this.Height * this.Depth,

		ShapeType.Cylinder => MathF.PI * MathF.Pow(this.Width / 2f, 2) * this.Height,

		ShapeType.Cone => 1f / 3f * MathF.PI * MathF.Pow(this.Width / 2f, 2) * this.Height,

		// Volume of a pyramid: (1/3) * base_area * h. Assuming rectangular base.
		ShapeType.Pyramid => 1f / 3f * (this.Width * this.Depth) * this.Height,

		// Assuming Cube means a rectangular prism. If it's a literal cube, use MathF.Pow(this.Width, 3).
		ShapeType.Cube => this.Width * this.Height * this.Depth,

		// Assuming Width is diameter of base 1 and Depth is diameter of base 2.
		ShapeType.ConicalFrustum => 1f / 3f * MathF.PI * this.Height *
		                            (MathF.Pow(this.Width / 2f, 2) +
		                             (this.Width / 2f * (this.Depth / 2f)) +
		                             MathF.Pow(this.Depth / 2f, 2)),
		ShapeType.RectangularPrism => throw new NotImplementedException(),
		_ => 0f // A discard pattern handles any unlisted enum members.
	};

	/// <summary>
	///     Converts the dimensions from its current scale to the specified target scale.
	/// </summary>
	/// <param name="targetScale">The dimension scale to convert to.</param>
	public void ChangeScale(DimensionScale targetScale)
	{
		if (this.DimensionScale == targetScale)
		{
			return;
		}

		var factorFromCurrentToMeters = GetMetersPerUnit(this.DimensionScale);
		var factorFromMetersToTarget = 1f / GetMetersPerUnit(targetScale);
		var conversionFactor = factorFromCurrentToMeters * factorFromMetersToTarget;

		this.Width *= conversionFactor;
		this.Height *= conversionFactor;
		this.Depth *= conversionFactor;

		// IMPORTANT: Update the scale property to reflect the new unit.
		this.DimensionScale = targetScale;
	}

	/// <summary>
	///     Helper to get the conversion factor from a given unit to a canonical unit (meters).
	/// </summary>
	private static float GetMetersPerUnit(DimensionScale scale) => scale switch
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
