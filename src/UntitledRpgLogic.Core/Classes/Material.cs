using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents the data for a material in the game. This is an immutable data container.
/// </summary>
public record Material : IMaterial
{
	/// <inheritdoc />
	public Guid Guid { get; init; }

	/// <inheritdoc />
	public string Id { get; init; } = string.Empty;

	/// <inheritdoc />
	public string ShortGuid { get; init; } = string.Empty;

	/// <inheritdoc />
	public Name Name { get; init; } = Name.Empty;

	/// <inheritdoc />
	public StateOfMatter State { get; init; }

	/// <inheritdoc />
	public Dictionary<StateOfMatter, MaterialStateProperties> StateProperties { get; init; } = [];

	/// <inheritdoc />
	public double MolarMass { get; init; }

	/// <inheritdoc />
	public double SolidCoefficientOfExpansion { get; init; }

	/// <inheritdoc />
	public double LiquidCoefficientOfExpansion { get; init; }

	/// <inheritdoc />
	public float Temperature { get; init; }

	/// <inheritdoc />
	public float Pressure { get; init; }
}
