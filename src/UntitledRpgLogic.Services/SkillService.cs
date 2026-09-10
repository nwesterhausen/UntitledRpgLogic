using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Services;
using UntitledRpgLogic.Extensions.Logging;

// Assuming you might add more specific logs here

// For ArgumentNullException

namespace UntitledRpgLogic.Services;

/// <summary>
/// </summary>
/// <remarks>
/// </remarks>
/// <param name="levelingService"></param>
/// <param name="logger"></param>
public class SkillService(ILevelingService<ISkill> levelingService, ILogger<SkillService> logger) : ISkillService
{
	private readonly ILevelingService<ISkill> levelingService = levelingService;
	private readonly ILogger<SkillService> logger = logger;

	/// <inheritdoc />
	public void AddPoints(ISkill skill, int points)
	{
		// --- Guard Clause ---
		ArgumentNullException.ThrowIfNull(skill);

		// --- Contextual Logging ---
		if (skill.Level >= skill.MaxLevel)
		{
			this.logger.AttemptedIncreaseSkillAtMaxLevel(skill.Name.Singular, points);
			return;
		}

		this.logger.SkillValueIncrease(skill.Name.Singular, points);
		this.levelingService.AddPoints(skill, points);
	}

	/// <inheritdoc />
	public void RemovePoints(ISkill skill, int points)
	{
		// --- Guard Clause ---
		ArgumentNullException.ThrowIfNull(skill);


		this.logger.SkillValueDecrease(skill.Name.Singular, points);
		this.levelingService.RemovePoints(skill, points);
	}

	/// <inheritdoc />
	public void SetPoints(ISkill skill, int points)
	{
		// --- Guard Clause ---
		ArgumentNullException.ThrowIfNull(skill);

		this.logger.SkillValueSet(skill.Name.Singular, points);
		this.levelingService.SetPoints(skill, points);
	}

	/// <inheritdoc />
	public int GetPointsToNextLevel(ISkill skill)
	{
		ArgumentNullException.ThrowIfNull(skill);
		return this.levelingService.GetPointsToNextLevel(skill);
	}

	// The delegation for these methods is already clean and correct.
	// Adding guard clauses here provides an extra layer of safety.
	/// <inheritdoc />
	public int GetTotalPointsForLevel(ISkill skill, int targetLevel)
	{
		ArgumentNullException.ThrowIfNull(skill);
		return this.levelingService.GetTotalPointsForLevel(skill, targetLevel);
	}

	/// <inheritdoc />
	public float GetProgressToNextLevel(ISkill skill)
	{
		ArgumentNullException.ThrowIfNull(skill);
		return this.levelingService.GetProgressToNextLevel(skill);
	}
}
