using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extensions for material-related operations.
/// </summary>
public static class MaterialExtensions
{
	/// <summary>
	///     Calculate the weight of a material based on its volume, pressure, and temperature.
	/// </summary>
	/// <param name="material"></param>
	/// <param name="volume"></param>
	/// <returns></returns>
	public static double CalculateWeight(this IMaterial material, double volume)
	{
		ArgumentNullException.ThrowIfNull(material, nameof(material));

		if (material.Mechanical.Density is null or <= 0)
		{
			return 0f;
		}

		// Weight (kg) = Density (kg/m^3) * Volume (m^3)
		return material.Mechanical.Density.Value * volume;
	}
	/// <summary>
	/// Gets the material's attunement value for a specific magic type.
	/// A positive value could indicate a weakness or amplification, while a negative value could indicate resistance.
	/// </summary>
	/// <param name="material">The material.</param>
	/// <param name="magicTypeGuid">The Guid of the magic type to check.</param>
	/// <returns>The attunement value, or 0f if no attunement is defined for that magic type.</returns>
	public static float GetAttunement(this IMaterial material, Guid magicTypeGuid)
	{
		ArgumentNullException.ThrowIfNull(material, nameof(material));

		if (material.Fantastical.ElementalAttunement is null)
		{
			return 0f;
		}

		return material.Fantastical.ElementalAttunement.GetValueOrDefault(magicTypeGuid, 0f);
	}
}
