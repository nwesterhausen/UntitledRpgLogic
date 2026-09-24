using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     An intermediate step in creating a new <see cref="Item" /> based on an <see cref="ItemDefinition" />. A
///     <see cref="ShapedComponent" /> defines what material makes up which component of an item. This record also
///     holds a cached reference for volume, weight and durability.
/// </summary>
[Table("shaped_components")]
public record ShapedComponent
{
	/// <summary>
	///     An identity for the component.
	/// </summary>
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; init; }

	/// <summary>
	///     The ID of the <see cref="ItemShape" /> of this component
	/// </summary>
	public Ulid ShapeId { get; init; }

	/// <summary>
	///     The <see cref="MaterialDefinition" /> of the material this item is made of.
	/// </summary>
	public Ulid MaterialId { get; init; }

	/// <summary>
	///     The cached weight value
	/// </summary>
	public float Weight { get; init; }

	/// <summary>
	///     The cached volume
	/// </summary>
	public float Volume { get; init; }

	/// <summary>
	///     The scale of the volume value
	/// </summary>
	public DimensionScale VolumeScale { get; init; }

	/// <summary>
	///     The current durability of this component.
	/// </summary>
	public float Durability { get; set; }

	/// <summary>
	///     The maximum durability of this component.
	/// </summary>
	public float MaxDurability { get; init; }
}
