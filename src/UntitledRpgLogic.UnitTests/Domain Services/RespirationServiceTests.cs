using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Environment;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public class RespirationServiceTests
{
	private readonly RespirationDomainService respirationService = new();

	[TestMethod]
	public void EvaluateRespiration_BelowMinPressure_ReturnsHypoxic()
	{
		var profile = new RespiratoryProfile
		{
			RequiresBreathing = true, MinRequiredPressure = 0.5f, MaxSafePressure = 2.0f
		};

		var atmosphere = new AtmosphereProfile { TotalPressureAtm = 0.2f };

		var state = this.respirationService.EvaluateRespiration(profile, atmosphere);

		Assert.AreEqual(RespirationState.Suffocation, state);
	}

	[TestMethod]
	public void EvaluateRespiration_ExceedsToxicityThreshold_ReturnsPoisoned()
	{
		var toxicMatId = Ulid.NewUlid();
		var profile = new RespiratoryProfile
		{
			RequiresBreathing = true,
			MinRequiredPressure = 0.5f,
			MaxSafePressure = 2.0f,
			ToxicSubstances = [new MaterialToxicityThreshold { MaterialId = toxicMatId, DangerousPressure = 0.05f }]
		};

		var atmosphere = new AtmosphereProfile
		{
			TotalPressureAtm = 1.0f,
			GasFractions =
				[new AtmosphericGasFraction { MaterialId = toxicMatId, Ratio = 0.10f }] // 0.10 atm >= 0.05 atm
		};

		var state = this.respirationService.EvaluateRespiration(profile, atmosphere);

		Assert.AreEqual(RespirationState.Poisoned, state);
	}

	[TestMethod]
	public void IsSubmerged_EntityBelowWaterSurface_ReturnsTrue()
	{
		// Terrain is 10, liquid depth is 5 (surface = 15). Entity is at 12.
		var submerged = this.respirationService.IsSubmerged(12, 10, 5);

		Assert.IsTrue(submerged);
	}

	[TestMethod]
	public void IsSubmerged_ZeroLiquidDepth_ReturnsFalse()
	{
		var submerged = this.respirationService.IsSubmerged(10, 10, 0);

		Assert.IsFalse(submerged);
	}
}
