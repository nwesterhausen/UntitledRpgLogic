using System.Drawing;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Game;

/// <summary>
///     A base class for materials in the game. This class implements the <see cref="IMaterial" /> interface and provides
///     boilerplate for a lot of the common functionality that materials will need.
/// </summary>
public class BaseMaterial : IMaterial
{
	/// <summary>
	///     Creates an instance of <see cref="BaseMaterial" /> using the provided configuration (which is loaded from TOML).
	/// </summary>
	/// <param name="config"></param>
	public BaseMaterial(MaterialDataConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		this.Name = new Name(config.Name, config.PluralName, config.NameAsAdjective);
		this.Guid = config.ExplicitId ?? Guid.NewGuid();
		this.Id = Convert.ToBase64String(this.Guid.ToByteArray());
		this.ShortGuid = this.Guid.ToString("N")[..8].ToUpperInvariant();

		this.State = StateOfMatter.Solid; // Default to solid, will be updated as needed.
		this.Temperature = 20; // Default temperature in Celsius.
		this.Pressure = 101.325f; // Default pressure in kPa (standard atmospheric pressure).
		this.MolarMass = config.MolarMass;
		this.SolidCoefficientOfExpansion = config.SolidCoefficientOfExpansion;
		this.LiquidCoefficientOfExpansion = config.LiquidCoefficientOfExpansion;
		this.StateProperties = new Dictionary<StateOfMatter, MaterialStateProperties>
		{
			{
				StateOfMatter.Solid,
				new MaterialStateProperties
				{
					Color = config.SolidColor,
					TemperatureAtStateChange = config.TemperatureAtSolidStateChange ?? float.MinNumber,
					DensityAtStateChange = config.DensityAtSolidStateChange
				}
			},
			{
				StateOfMatter.Liquid, new MaterialStateProperties
				{
					Color = config.LiquidColor ?? Color.Red, // Default to red if not provided.
					TemperatureAtStateChange = config.TemperatureAtLiquidStateChange ?? float.MinNumber,
					DensityAtStateChange = config.DensityAtLiquidStateChange
				}
			},
			{
				StateOfMatter.Gas, new MaterialStateProperties
				{
					Color = config.GasColor ?? Color.Gray, // Default to gray if not provided.
					TemperatureAtStateChange = config.TemperatureAtGasStateChange,
					DensityAtStateChange = config.DensityAtGasStateChange
				}
			}
		};
	}

	/// <inheritdoc />
	public Guid Guid { get; }

	/// <inheritdoc />
	public string Id { get; }

	/// <inheritdoc />
	public string ShortGuid { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public StateOfMatter State { get; }

	/// <inheritdoc />
	public Dictionary<StateOfMatter, MaterialStateProperties> StateProperties { get; }

	/// <inheritdoc />
	public double MolarMass { get; }

	/// <inheritdoc />
	public double SolidCoefficientOfExpansion { get; }

	/// <inheritdoc />
	public double LiquidCoefficientOfExpansion { get; }

	/// <inheritdoc />
	public float Temperature { get; }

	/// <inheritdoc />
	public float Pressure { get; }
}
