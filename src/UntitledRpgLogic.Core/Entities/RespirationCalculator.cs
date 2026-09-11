using UntitledRpgLogic.Core.Environment;

namespace UntitledRpgLogic.Core.Entities;

/// <summary>
///     Calculates an entity's respiratory state based on atmospheric and hydrostatic fluid pressures.
/// </summary>
public static class RespirationCalculator
{
	/// <summary>
	///     Conversion constant: meters of fluid depth equivalent to 1 atmosphere of hydrostatic pressure.
	/// </summary>
	public const float DepthPerAtmosphere = 10.0f;

	/// <summary>
	///     Evaluates respiration state based on whether the entity is breathing open air or submerged in liquid.
	/// </summary>
	/// <param name="profile">The respiratory traits and thresholds of the entity.</param>
	/// <param name="atmosphere">The local atmosphere profile of the chunk or map.</param>
	/// <param name="submergedLiquidMaterialId">The ULID of the fluid the entity is currently submerged in (null if surfaced/dry).</param>
	/// <param name="submergedDepthUnits">Depth in world units (meters) of the liquid above the entity's breathing apparatus.</param>
	public static RespirationState Evaluate(
		RespiratoryProfile profile,
		AtmosphereProfile atmosphere,
		Ulid? submergedLiquidMaterialId,
		ushort submergedDepthUnits)
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
			var liquidHydrostaticPressure = atmosphere.TotalPressureAtm + (submergedDepthUnits / DepthPerAtmosphere);

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
