using System.Drawing;
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
	public float Temperature { get; set; }

	/// <inheritdoc />
	public float Pressure { get; set; }


	/// <inheritdoc />
	public Dictionary<StateOfMatter, Color> Colors { get; init; } = [];


	/// <inheritdoc />
	public float MeltingPointCelcius { get; init; }


	/// <inheritdoc />
	public float BoilingPointCelcius { get; init; }


	/// <inheritdoc />
	public float? SublimationPointCelcius { get; init; }


	/// <inheritdoc />
	public float? SolidDensityGcm3 { get; init; }

	/// <inheritdoc />
	public float? LiquidDensityGcm3 { get; init; }

	/// <inheritdoc />
	public float? GasDensityGcm3 { get; init; }

	/// <inheritdoc />
	public float? TensileStrengthMPa { get; init; }

	/// <inheritdoc />
	public float? CompressiveStrengthMPa { get; init; }

	/// <inheritdoc />
	public float? HardnessMohs { get; init; }

	/// <inheritdoc />
	public FractureType? FractureType { get; init; }

	/// <inheritdoc />
	public float? YoungsModulusGPa { get; init; }

	/// <inheritdoc />
	public float? ImpactStrengthJm2 { get; init; }

	/// <inheritdoc />
	public float? FatigueLimitMPa { get; init; }

	/// <inheritdoc />
	public float? ThermalExpansionAlpha { get; init; }
}
