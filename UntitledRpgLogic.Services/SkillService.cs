using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Interfaces;

// Assuming you might add more specific logs here

// For ArgumentNullException

namespace UntitledRpgLogic.Services;

/// <summary>
/// </summary>
public class SkillService : ISkillService
{
    private readonly ILevelingService<ISkill> _levelingService;
    private readonly ILogger<SkillService> _logger;

    /// <summary>
    /// </summary>
    /// <param name="levelingService"></param>
    /// <param name="logger"></param>
    public SkillService(ILevelingService<ISkill> levelingService, ILogger<SkillService> logger)
    {
        _levelingService = levelingService;
        _logger = logger;
    }

    /// <inheritdoc />
    public void AddPoints(ISkill skill, int points)
    {
        // --- Guard Clause ---
        ArgumentNullException.ThrowIfNull(skill);

        // --- Contextual Logging ---
        if (skill.Level >= skill.MaxLevel)
            _logger.LogWarning("Attempted to add {Points} points to max-level skill {SkillName}.", points,
                skill.Name.Singular);
        // You might choose to return here if you don't want to add points to a max-level skill
        // return; 
        _logger.LogDebug("Adding {Points} points to skill {SkillName}", points, skill.Name.Singular);
        _levelingService.AddPoints(skill, points);
    }

    /// <inheritdoc />
    public void RemovePoints(ISkill skill, int points)
    {
        // --- Guard Clause ---
        ArgumentNullException.ThrowIfNull(skill);

        _logger.LogDebug("Removing {Points} points from skill {SkillName}", points, skill.Name.Singular);
        _levelingService.RemovePoints(skill, points);
    }

    /// <inheritdoc />
    public void SetPoints(ISkill skill, int points)
    {
        // --- Guard Clause ---
        ArgumentNullException.ThrowIfNull(skill);

        _logger.LogDebug("Setting points for skill {SkillName} to {Points}", skill.Name.Singular, points);
        _levelingService.SetPoints(skill, points);
    }

    /// <inheritdoc />
    public int GetPointsToNextLevel(ISkill skill)
    {
        ArgumentNullException.ThrowIfNull(skill);
        return _levelingService.GetPointsToNextLevel(skill);
    }

    // The delegation for these methods is already clean and correct.
    // Adding guard clauses here provides an extra layer of safety.
    /// <inheritdoc />
    public int GetTotalPointsForLevel(ISkill skill, int targetLevel)
    {
        ArgumentNullException.ThrowIfNull(skill);
        return _levelingService.GetTotalPointsForLevel(skill, targetLevel);
    }

    /// <inheritdoc />
    public float GetProgressToNextLevel(ISkill skill)
    {
        ArgumentNullException.ThrowIfNull(skill);
        return _levelingService.GetProgressToNextLevel(skill);
    }
}
