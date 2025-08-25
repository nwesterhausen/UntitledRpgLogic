using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
/// Extensions for MaterialDataConfig to handle inheritance and merging of properties.
/// </summary>
public static class MaterialConfigExtensions
{
	/// <summary>
	/// Merges the properties of a base MaterialDataConfig into a child config.
	/// Properties explicitly defined in the child config will take precedence.
	/// </summary>
	/// <param name="childConfig">The child configuration that inherits properties.</param>
	/// <param name="baseConfig">The base configuration to inherit from.</param>
	/// <returns>A new, merged MaterialDataConfig.</returns>
	public static MaterialDataConfig MergeWith(this MaterialDataConfig childConfig, MaterialDataConfig baseConfig)
	{
		ArgumentNullException.ThrowIfNull(childConfig, nameof(childConfig));
		ArgumentNullException.ThrowIfNull(baseConfig, nameof(baseConfig));

		return new MaterialDataConfig
		{
			// Child always wins for simple properties if they are set
			Name = !string.IsNullOrEmpty(childConfig.Name) ? childConfig.Name : baseConfig.Name,
			NameAsAdjective = !string.IsNullOrEmpty(childConfig.NameAsAdjective) ? childConfig.NameAsAdjective : baseConfig.NameAsAdjective,
			PluralName = !string.IsNullOrEmpty(childConfig.PluralName) ? childConfig.PluralName : baseConfig.PluralName,
			Identifier = childConfig.Identifier, // Ulid is always from the child
			Extends = childConfig.Extends ?? baseConfig.Extends,

			// Merge complex properties
			Mechanical = MergeMechanical(baseConfig.Mechanical, childConfig.Mechanical),
			Thermal = MergeThermal(baseConfig.Thermal, childConfig.Thermal),
			Electrical = MergeElectrical(baseConfig.Electrical, childConfig.Electrical),
			Fantastical = MergeFantastical(baseConfig.Fantastical, childConfig.Fantastical),
			States = MergeStates(baseConfig.States, childConfig.States)
		};
	}

	private static MechanicalPropertiesConfig MergeMechanical(MechanicalPropertiesConfig baseProps, MechanicalPropertiesConfig childProps) =>
		new()
		{
			Density = childProps.Density ?? baseProps.Density,
			Hardness = childProps.Hardness ?? baseProps.Hardness,
			Toughness = childProps.Toughness ?? baseProps.Toughness,
			Stiffness = childProps.Stiffness ?? baseProps.Stiffness,
			Malleability = childProps.Malleability ?? baseProps.Malleability,
			Viscosity = childProps.Viscosity ?? baseProps.Viscosity,
			SurfaceTension = childProps.SurfaceTension ?? baseProps.SurfaceTension,
			Adhesion = childProps.Adhesion ?? baseProps.Adhesion
		};

	private static ThermalPropertiesConfig MergeThermal(ThermalPropertiesConfig baseProps, ThermalPropertiesConfig childProps) =>
		new()
		{
			MeltingPoint = childProps.MeltingPoint ?? baseProps.MeltingPoint,
			BoilingPoint = childProps.BoilingPoint ?? baseProps.BoilingPoint,
			IgnitionTemperature = childProps.IgnitionTemperature ?? baseProps.IgnitionTemperature,
			ThermalConductivity = childProps.ThermalConductivity ?? baseProps.ThermalConductivity
		};

	private static ElectricalPropertiesConfig MergeElectrical(ElectricalPropertiesConfig baseProps, ElectricalPropertiesConfig childProps) =>
		new()
		{
			Conductivity = childProps.Conductivity ?? baseProps.Conductivity,
		};

	private static FantasticalPropertiesConfig MergeFantastical(FantasticalPropertiesConfig baseProps, FantasticalPropertiesConfig childProps)
	{
		// Merge dictionaries
		var mergedAttunement = new Dictionary<Ulid, float>(baseProps.ElementalAttunement ?? new());
		if (childProps.ElementalAttunement != null)
		{
			foreach (var (element, value) in childProps.ElementalAttunement)
			{
				mergedAttunement[element] = value;
			}
		}

		return new FantasticalPropertiesConfig
		{
			AetherialConductivity = childProps.AetherialConductivity ?? baseProps.AetherialConductivity,
			ManaCapacity = childProps.ManaCapacity ?? baseProps.ManaCapacity,
			Purity = childProps.Purity ?? baseProps.Purity,
			Luminosity = childProps.Luminosity ?? baseProps.Luminosity,
			ElementalAttunement = mergedAttunement
		};
		;
	}

	private static Dictionary<StateOfMatter, StateSpecificPropertiesConfig> MergeStates(
		IReadOnlyDictionary<StateOfMatter, StateSpecificPropertiesConfig> baseStates,
		IReadOnlyDictionary<StateOfMatter, StateSpecificPropertiesConfig> childStates)
	{
		var merged = new Dictionary<StateOfMatter, StateSpecificPropertiesConfig>(baseStates);
		foreach (var (state, props) in childStates)
		{
			merged[state] = props; // Child state properties completely overwrite base
		}
		return merged;
	}
}
