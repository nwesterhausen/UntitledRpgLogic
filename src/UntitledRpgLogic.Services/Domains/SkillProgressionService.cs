using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Core.Skills;

namespace UntitledRpgLogic.Services.Domains;

/// <summary>
///     Service for evaluating skill experience curves and advancing proficiency levels.
/// </summary>
public sealed class SkillProgressionService : ISkillProgressionService
{
	/// <inheritdoc />
	public int CalculateExperienceForNextLevel(Skill skill, LevelingDefinition levelingDefinition)
	{
		ArgumentNullException.ThrowIfNull(skill);
		ArgumentNullException.ThrowIfNull(levelingDefinition);

		if (skill.Level >= levelingDefinition.MaxLevel)
		{
			return int.MaxValue;
		}

		var currentLevel = skill.Level;
		var a = levelingDefinition.ScalingFactorA;
		var b = levelingDefinition.ScalingFactorB;
		var c = levelingDefinition.ScalingFactorC;
		var basePoints = levelingDefinition.PointsForFirstLevel;

		var requiredPoints = levelingDefinition.ScalingCurve switch
		{
			ScalingCurveType.Linear =>
				basePoints + (int)MathF.Round(a * currentLevel),

			ScalingCurveType.Exponential =>
				(int)MathF.Round(basePoints * MathF.Pow(MathF.Max(1.0f, a), currentLevel)),

			ScalingCurveType.Polynomial =>
				(int)MathF.Round(basePoints + (a * MathF.Pow(currentLevel, 2)) + (b * currentLevel) + c),

			_ => basePoints
		};

		return Math.Max(1, requiredPoints);
	}

	/// <inheritdoc />
	public bool TryAddExperience(
		Skill skill,
		LevelingDefinition levelingDefinition,
		int addedXp,
		out int levelsGained)
	{
		ArgumentNullException.ThrowIfNull(skill);
		ArgumentNullException.ThrowIfNull(levelingDefinition);

		levelsGained = 0;
		if (addedXp <= 0 || skill.Level >= levelingDefinition.MaxLevel)
		{
			return false;
		}

		skill.ExperiencePoints += addedXp;

		while (skill.Level < levelingDefinition.MaxLevel)
		{
			var requiredForNext = this.CalculateExperienceForNextLevel(skill, levelingDefinition);
			if (skill.ExperiencePoints < requiredForNext)
			{
				break;
			}

			skill.ExperiencePoints -= requiredForNext;
			skill.Level++;
			levelsGained++;
		}

		return levelsGained > 0;
	}
}
