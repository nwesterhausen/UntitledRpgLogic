namespace UntitledRpgLogic.Core.Common;

/// <summary>
///     Record which represents the dimensions of a object in physical space.
/// </summary>
public record Dimensions
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
	public ShapeType ShapeType { get; init; } = ShapeType.Cube;

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
}
