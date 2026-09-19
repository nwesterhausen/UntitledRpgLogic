using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Services.Domains;

namespace UntitledRpgLogic.UnitTests.Domain_Services;

[TestClass]
public sealed class SkillServiceTests
{
	private readonly SkillProgressionService progressionService = new();

	[TestMethod]
	public void CalculateExperienceForNextLevel_Linear_ScalesProportionally()
	{
		var levelingDef = new LevelingDefinition
		{
			StartingLevel = 1,
			MaxLevel = 10,
			PointsForFirstLevel = 100,
			ScalingFactorA = 50f,
			ScalingCurve = ScalingCurveType.Linear
		};

		var skill = new Skill { Level = 2, ExperiencePoints = 0, DefinitionId = Ulid.NewUlid() };

		// 100 + (50 * 2) = 200
		var xpRequired = this.progressionService.CalculateExperienceForNextLevel(skill, levelingDef);

		Assert.AreEqual(200, xpRequired);
	}

	[TestMethod]
	public void TryAddExperience_SufficientXp_LevelsUpAndDeductsPoints()
	{
		var levelingDef = new LevelingDefinition
		{
			StartingLevel = 1,
			MaxLevel = 10,
			PointsForFirstLevel = 100,
			ScalingFactorA = 0f,
			ScalingCurve = ScalingCurveType.Linear
		};

		var skill = new Skill { Level = 1, ExperiencePoints = 0, DefinitionId = Ulid.NewUlid() };

		var leveledUp = this.progressionService.TryAddExperience(skill, levelingDef, 250, out var levelsGained);

		Assert.IsTrue(leveledUp);
		Assert.AreEqual(2, levelsGained);
		Assert.AreEqual(3, skill.Level);
		Assert.AreEqual(50, skill.ExperiencePoints);
	}
}
