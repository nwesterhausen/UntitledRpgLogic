// C:/Users/nwest/source/repos/UntitledRpgLogic/UntitledRpgLogic.Services/LevelingService.cs

using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Services;

/// <summary>
///     A generic service that provides leveling logic for any object implementing IHasLeveling.
/// </summary>
public class LevelingService<T> : ILevelingService<T> where T : IHasLeveling
{
    private readonly ILogger<LevelingService<T>> _logger;

    public LevelingService(ILogger<LevelingService<T>> logger)
    {
        _logger = logger;
    }

    public void AddPoints(T target, int points)
    {
        if (points == 0) return;

        int oldValue = target.Value;
        target.Value += points;

        LogValueChange(target, oldValue);
        CheckForLevelChange(target);
    }

    public void RemovePoints(T target, int points)
    {
        AddPoints(target, -points);
    }

    public void SetPoints(T target, int points)
    {
        int oldValue = target.Value;
        target.Value = points;

        LogValueChange(target, oldValue);
        CheckForLevelChange(target);
    }

    public int GetTotalPointsForLevel(T target, int targetLevel)
    {
        if (targetLevel <= 1) return 0;
        if (targetLevel > target.MaxLevel) targetLevel = target.MaxLevel;

        return target.ScalingCurve switch
        {
            ScalingCurveType.Linear => target.PointsForFirstLevel + (int)(target.ScalingFactorA * (targetLevel - 2)),
            ScalingCurveType.Exponential => (int)(target.PointsForFirstLevel *
                                                  Math.Pow(target.ScalingFactorA, targetLevel - 2)),
            ScalingCurveType.Polynomial => (int)((target.ScalingFactorA *
                                                  Math.Pow(targetLevel - 1, target.ScalingFactorB)) +
                                                 target.ScalingFactorC),
            _ => throw new NotSupportedException($"Unsupported scaling curve type: {target.ScalingCurve}")
        };
    }

    public int GetPointsToNextLevel(T target)
    {
        if (target.Level >= target.MaxLevel) return 0;
        int totalPointsForNextLevel = GetTotalPointsForLevel(target, target.Level + 1);
        return Math.Max(0, totalPointsForNextLevel - target.Value);
    }

    public float GetProgressToNextLevel(T target)
    {
        if (target.Level >= target.MaxLevel) return 1.0f;

        int pointsForCurrentLevel = GetTotalPointsForLevel(target, target.Level);
        int pointsForNextLevel = GetTotalPointsForLevel(target, target.Level + 1);

        int pointsNeededForThisLevel = pointsForNextLevel - pointsForCurrentLevel;
        if (pointsNeededForThisLevel <= 0) return 1.0f;

        int pointsEarnedThisLevel = target.Value - pointsForCurrentLevel;
        return Math.Clamp((float)pointsEarnedThisLevel / pointsNeededForThisLevel, 0.0f, 1.0f);
    }

    private void LogValueChange(T target, int oldValue)
    {
        if (target.Value == oldValue) return;

        target.InvokeValueChanged(new ValueChangedEventArgs(oldValue, target.Value));
        int pointsChanged = target.Value - oldValue;

        if (target is IHasName namedTarget)
            // Use the new generic helper
            _logger.LogLevelablePointsChanged(typeof(T).Name, namedTarget.Name.Singular, pointsChanged, target.Value);
        else
            // Fallback for unnamed items
            _logger.LogLevelablePointsChangedGeneric(typeof(T).Name, pointsChanged, target.Value);
    }

    private void CheckForLevelChange(T target)
    {
        int oldLevel = target.Level;
        int newLevel = CalculateLevelFromPoints(target);

        if (newLevel != oldLevel)
        {
            target.Level = newLevel;
            target.InvokeLevelChanged(new ValueChangedEventArgs(oldLevel, newLevel));

            if (target is IHasName namedTarget)
                // Use the new generic helper
                _logger.LogLevelableChanged(typeof(T).Name, namedTarget.Name.Singular, oldLevel, newLevel);
            else
                // Fallback for unnamed items
                _logger.LogLevelableChangedGeneric(typeof(T).Name, oldLevel, newLevel);
        }
    }

    private int CalculateLevelFromPoints(T target)
    {
        int calculatedLevel = 1;
        while (calculatedLevel < target.MaxLevel)
        {
            if (target.Value < GetTotalPointsForLevel(target, calculatedLevel + 1)) break;

            calculatedLevel++;
        }

        return calculatedLevel;
    }
}
