using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Inventory;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents the data for an item in the game. This is an immutable data container.
/// </summary>
public record Item : IItem
{
	/// <inheritdoc />
	public Ulid Id { get; init; } = Ulid.NewUlid();

	/// <inheritdoc />
	public Name Name { get; init; } = Name.Empty;

	/// <inheritdoc />
	public Quality Quality { get; init; }

	/// <inheritdoc />
	public ItemType ItemType { get; init; }

	/// <inheritdoc />
	public ItemSubtype ItemSubtype { get; init; }

	/// <inheritdoc />
	public Ulid CraftedBy { get; init; } = WellKnownIdentifiers.GameSystem;

	/// <inheritdoc />
	public DimensionScale DimensionScale { get; set; }

	/// <inheritdoc />
	public ShapeType ShapeType { get; init; }

	/// <inheritdoc />
	public float Width { get; set; }

	/// <inheritdoc />
	public float Height { get; set; }

	/// <inheritdoc />
	public float Depth { get; set; }

	/// <inheritdoc />
	public IMaterial PrimaryMaterial { get; set; } = Material.Empty;
}
