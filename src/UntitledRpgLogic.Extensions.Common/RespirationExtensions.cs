using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Environment;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Calculation extensions for determining entity submersion and respiration states.
/// </summary>
public static class RespirationExtensions
{
	/// <summary>
	///     Calculates whether an entity is submerged in liquid based on position and tile depth.
	/// </summary>
	public static bool IsSubmerged(this WorldPosition position, float tileElevation, float liquidDepth)
	{
		ArgumentNullException.ThrowIfNull(position);

		var liquidSurface = tileElevation + liquidDepth;
		return liquidDepth > 0f && position.Elevation < liquidSurface;
	}

	/// <summary>
	///     Calculates the fraction of gas available in the surrounding atmosphere.
	/// </summary>
	public static float GetEffectiveGasFraction(this AtmosphereProfile atmosphere, Ulid gasMaterialId)
	{
		ArgumentNullException.ThrowIfNull(atmosphere);

		return atmosphere.GasFractions
			.FirstOrDefault(g => g.MaterialId == gasMaterialId)
			?.Ratio ?? 0f;
	}


	/// <summary>
	///     Evaluates respiration state based on whether the entity is breathing open air or submerged in liquid.
	/// </summary>
	/// <param name="profile">The respiratory traits and thresholds of the entity.</param>
	/// <param name="atmosphere">The local atmosphere profile of the chunk or map.</param>
	/// <param name="submergedLiquidMaterialId">The ULID of the fluid the entity is currently submerged in (null if surfaced/dry).</param>
	/// <param name="submergedDepthUnits">Depth in world units (meters) of the liquid above the entity's breathing apparatus.</param>
	/// <param name="depthPerAtmosphere">How deep in <paramref name="submergedLiquidMaterialId"/> to accumulate 1atm of pressure</param>
	public static RespirationState Evaluate(
		this RespiratoryProfile profile,
		AtmosphereProfile atmosphere,
		Ulid? submergedLiquidMaterialId,
		ushort submergedDepthUnits,
		float depthPerAtmosphere = 10f)
	{
		ArgumentNullException.ThrowIfNull(profile);
		ArgumentNullException.ThrowIfNull(atmosphere);

		if (!profile.RequiresBreathing)
		{
			return RespirationState.Optimal;
		}

		// --- Case A: Submerged in Liquid ---
		if (submergedLiquidMaterialId.HasValue && submergedDepthUnits > 0)
		{
			var liquidId = submergedLiquidMaterialId.Value;
			var liquidHydrostaticPressure = atmosphere.TotalPressureAtm + (submergedDepthUnits / depthPerAtmosphere);

			// 1. Check for toxic liquids (Acid, Magma, Poison Sludge)
			var toxic = profile.ToxicSubstances.FirstOrDefault(t => t.MaterialId == liquidId);
			if (toxic is not null)
			{
				return liquidHydrostaticPressure >= toxic.LethalPressure
					? RespirationState.LethallyPoisoned
					: RespirationState.Poisoned;
			}

			// 2. Check if the entity can breathe this liquid (Fish, Water Elemental, etc.)
			var canBreatheLiquid = profile.RequiredMediumMaterialId == liquidId
								   || profile.AlternativeBreathableMaterials.Contains(liquidId);

			if (canBreatheLiquid)
			{
				if (liquidHydrostaticPressure < profile.MinRequiredPressure)
				{
					return RespirationState.Suffocating;
				}

				return liquidHydrostaticPressure > profile.MaxSafePressure
					? RespirationState.OverpressureToxicity
					: RespirationState.Optimal;
			}

			// Submerged in a liquid the entity cannot respire (standard drowning)
			return RespirationState.Suffocating;
		}

		// --- Case B: Exposed to Gaseous Atmosphere ---
		// 1. Check for airborne toxicity (Miasma, Smoke, Toxic Spores)
		foreach (var toxic in profile.ToxicSubstances)
		{
			var partialPressure = atmosphere.GetPartialPressure(toxic.MaterialId);
			if (partialPressure >= toxic.LethalPressure)
			{
				return RespirationState.LethallyPoisoned;
			}
			if (partialPressure >= toxic.DangerousPressure)
			{
				return RespirationState.Poisoned;
			}
		}

		// 2. Check required gas medium breathability
		if (profile.RequiredMediumMaterialId.HasValue)
		{
			var targetMaterial = profile.RequiredMediumMaterialId.Value;
			var partialPressure = atmosphere.GetPartialPressure(targetMaterial);

			if (partialPressure < profile.MinRequiredPressure)
			{
				// Check alternative fallback breathable gases
				foreach (var alt in profile.AlternativeBreathableMaterials)
				{
					var altPressure = atmosphere.GetPartialPressure(alt);
					if (altPressure >= profile.MinRequiredPressure)
					{
						return altPressure > profile.MaxSafePressure
							? RespirationState.OverpressureToxicity
							: RespirationState.Optimal;
					}
				}

				return RespirationState.Suffocating;
			}

			if (partialPressure > profile.MaxSafePressure)
			{
				return RespirationState.OverpressureToxicity;
			}
		}

		return RespirationState.Optimal;
	}
}
