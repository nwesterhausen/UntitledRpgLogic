using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Environment;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Pure domain service evaluating gas concentrations, atmospheric pressures,
///     toxicity thresholds, and liquid submersion.
/// </summary>
public sealed class RespirationDomainService : IRespirationDomainService
{
	/// <inheritdoc />
	public RespirationState EvaluateRespiration(RespiratoryProfile profile, AtmosphereProfile atmosphere)
	{
		ArgumentNullException.ThrowIfNull(profile);
		ArgumentNullException.ThrowIfNull(atmosphere);

		if (!profile.RequiresBreathing)
		{
			return RespirationState.Normal;
		}

		// 1. Evaluate total ambient pressure limits
		if (atmosphere.TotalPressureAtm < profile.MinRequiredPressure)
		{
			return RespirationState.Suffocation;
		}

		if (atmosphere.TotalPressureAtm > profile.MaxSafePressure)
		{
			return RespirationState.Asphyxiation;
		}

		// 2. Evaluate toxic gas thresholds
		foreach (var toxic in profile.ToxicSubstances)
		{
			var fraction = atmosphere.GasFractions.FirstOrDefault(g => g.MaterialId == toxic.MaterialId);
			if (fraction is null)
			{
				continue;
			}

			var partialPressure = atmosphere.TotalPressureAtm * fraction.Ratio;

			if (partialPressure >= toxic.LethalPressure || partialPressure >= toxic.DangerousPressure)
			{
				return RespirationState.Poisoned;
			}
		}

		// 3. Verify presence of required medium gas
		// Quick exit if we don't have a required medium gas.
		if (!profile.RequiredMediumMaterialId.HasValue)
		{
			return RespirationState.Normal;
		}

		{
			var medium = atmosphere.GasFractions
				.FirstOrDefault(g => g.MaterialId == profile.RequiredMediumMaterialId.Value);

			if (medium is null || medium.Ratio <= 0f)
			{
				return RespirationState.Suffocation;
			}
		}

		return RespirationState.Normal;
	}

	/// <inheritdoc />
	public bool IsSubmerged(short entityElevation, short terrainElevation, ushort liquidDepth)
	{
		if (liquidDepth == 0)
		{
			return false;
		}

		var surfaceElevation = terrainElevation + liquidDepth;
		return entityElevation < surfaceElevation;
	}
}
