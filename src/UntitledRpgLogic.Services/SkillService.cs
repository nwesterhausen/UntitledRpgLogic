using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Services;

// Assuming you might add more specific logs here

// For ArgumentNullException

namespace UntitledRpgLogic.Services;

/// <summary>
/// </summary>
public class SkillService : ISkillService
{
	private readonly ILevelingService<ISkill> levelingService;
	private readonly ILogger<SkillService> logger;

	/// <summary>
	/// </summary>
	/// <param name="levelingService"></param>
	/// <param name="logger"></param>
	public SkillService(ILevelingService<ISkill> levelingService, ILogger<SkillService> logger)
	{
		this.levelingService = levelingService;
		this.logger = logger;
	}

	/// <inheritdoc />
	public void AddPoints(ISkill skill, int points)
	{
		// --- Guard Clause ---
		ArgumentNullException.ThrowIfNull(skill);

		// --- Contextual Logging ---
		if (skill.Level >= skill.MaxLevel)
		{
			this.logger.LogWarning("Attempted to add {Points} points to max-level skill {SkillName}.", points,
				skill.Name.Singular);
		}

		// You might choose to return here if you don't want to add points to a max-level skill
		// return;
		this.logger.LogDebug("Adding {Points} points to skill {SkillName}", points, skill.Name.Singular);
		this.levelingService.AddPoints(skill, points);
	}

	/// <inheritdoc />
	public void RemovePoints(ISkill skill, int points)
	{
		// --- Guard Clause ---
		ArgumentNullException.ThrowIfNull(skill);

		this.logger.LogDebug("Removing {Points} points from skill {SkillName}", points, skill.Name.Singular);
		this.levelingService.RemovePoints(skill, points);
	}

	/// <inheritdoc />
	public void SetPoints(ISkill skill, int points)
	{
		// --- Guard Clause ---
		ArgumentNullException.ThrowIfNull(skill);

		this.logger.LogDebug("Setting points for skill {SkillName} to {Points}", skill.Name.Singular, points);
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
