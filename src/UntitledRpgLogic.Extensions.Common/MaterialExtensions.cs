using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Inventory;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extensions for material-related operations.
/// </summary>
public static class MaterialExtensions
{
	/// <summary>
	/// Gets the material's attunement value for a specific magic type.
	/// A positive value could indicate a weakness or amplification, while a negative value could indicate resistance.
	/// </summary>
	/// <param name="material">The material.</param>
	/// <param name="magicTypeId">The Ulid of the magic type to check.</param>
	/// <returns>The attunement value, or 0f if no attunement is defined for that magic type.</returns>
	public static float GetAttunement(this IMaterial material, Ulid magicTypeId)
	{
		ArgumentNullException.ThrowIfNull(material, nameof(material));

		if (material.Fantastical.ElementalAttunement is null)
		{
			return 0f;
		}

		return material.Fantastical.ElementalAttunement.GetValueOrDefault(magicTypeId, 0f);
	}

	/// <summary>
	/// Calculates the weight of an item based on its volume and material density.
	/// </summary>
	/// <param name="item">The item to calculate the weight for.</param>
	/// <returns>The calculated weight in kilograms. Returns 0 if density is not defined.</returns>
	public static float CalculateWeight(this IItem item)
	{
		ArgumentNullException.ThrowIfNull(item, nameof(item));

		var density = item.PrimaryMaterial.Mechanical.Density;
		if (density is null or <= 0)
		{
			return 0f;
		}

		// Calculate volume in cubic meters to match density in kg/m^3
		var volumeInMeters = item.CalculateVolumeIn(DimensionScale.M);

		// Weight (kg) = Density (kg/m^3) * Volume (m^3)
		return density.Value * volumeInMeters;
	}
}
