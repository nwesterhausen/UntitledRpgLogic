using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes.EffectComponents;

/// <summary>
///     Represents a component that defines the physical dimensions of an effect.
/// </summary>
public class DimensionEffectComponent : IEffectComponent, IHasDimensions
{
	/// <summary>
	///     Initializes a new instance of the <see cref="DimensionEffectComponent" /> class.
	/// </summary>
	/// <param name="name">The name of this dimension component.</param>
	/// <param name="shapeType">The shape of the effect.</param>
	/// <param name="width">The width of the effect.</param>
	/// <param name="height">The height of the effect.</param>
	/// <param name="depth">The depth of the effect.</param>
	/// <param name="dimensionScale">The scale of the dimensions.</param>
	public DimensionEffectComponent(
		string name,
		ShapeType shapeType,
		float width,
		float height,
		float depth,
		DimensionScale dimensionScale)
	{
		this.Name = new Name(name);
		this.ShapeType = shapeType;
		this.Width = width;
		this.Height = height;
		this.Depth = depth;
		this.DimensionScale = dimensionScale;

		// Generate a new GUID for this component instance
		this.Identifier = Guid.NewGuid();
		this.Id = Convert.ToBase64String(this.Identifier.ToByteArray());
		this.ShortId = this.Identifier.ToString("N")[..8].ToUpperInvariant();
	}

	/// <inheritdoc />
	public Guid Identifier { get; }

	/// <inheritdoc />
	public string Id { get; }

	/// <inheritdoc />
	public string ShortId { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public EffectComponentType ComponentType => EffectComponentType.Dimensions;

	/// <summary>
	///     The type of shape the effect has.
	/// </summary>
	public ShapeType ShapeType { get; }

	/// <summary>
	///     The width of the effect in the specified dimension scale.
	/// </summary>
	public float Width { get; set; }

	/// <summary>
	///     The height of the effect in the specified dimension scale.
	/// </summary>
	public float Height { get; set; }

	/// <summary>
	///     The depth of the effect in the specified dimension scale.
	/// </summary>
	public float Depth { get; set; }

	/// <summary>
	///     The scale of the dimensions (e.g., cm, meters).
	/// </summary>
	public DimensionScale DimensionScale { get; set; }
}
