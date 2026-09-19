using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Represents a specific material constituent within an item's assembly.
/// </summary>
public record ItemMaterialComponent
{
	/// <summary>
	///     The functional slot or placement for this material.
	/// </summary>
	public MaterialSlot Slot { get; init; } = MaterialSlot.Primary;

	/// <summary>
	///     The id of the material used in this slot.
	/// </summary>
	public Ulid MaterialId { get; init; }

	/// <summary>
	///     The material used in this slot.
	/// </summary>
	[ForeignKey(nameof(MaterialId))]
	public MaterialDefinition? Material { get; init; }

	/// <summary>
	///     The fractional mass/volume this component contributes (0.0 to 1.0).
	///     Used to calculate composite weight, conductivity, or durability.
	/// </summary>
	public float Proportion { get; init; } = 1.0f;
}
