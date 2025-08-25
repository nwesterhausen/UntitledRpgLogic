using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Services;
using UntitledRpgLogic.Extensions.Logging;

namespace UntitledRpgLogic.Services;

/// <summary>
///     A generic service that provides leveling logic for any object implementing IHasLeveling.
/// </summary>
public class LevelingService<T> : ILevelingService<T> where T : IHasLeveling
{
	private readonly ILogger<LevelingService<T>> logger;

	/// <summary>
	///     Constructs a new instance of the LevelingService.
	/// </summary>
	/// <param name="logger"></param>
	public LevelingService(ILogger<LevelingService<T>> logger) => this.logger = logger;

	/// <inheritdoc />
	public void AddPoints(T target, int points)
	{
		if (points == 0)
		{
			return;
		}

		var oldValue = target.Value;
		target.Value += points;

		this.LogValueChange(target, oldValue);
		this.CheckForLevelChange(target);
	}

	/// <inheritdoc />
	public void RemovePoints(T target, int points) => this.AddPoints(target, -points);

	/// <inheritdoc />
	public void SetPoints(T target, int points)
	{
		var oldValue = target.Value;
		target.Value = points;

		this.LogValueChange(target, oldValue);
		this.CheckForLevelChange(target);
	}

	/// <inheritdoc />
	public int GetTotalPointsForLevel(T target, int targetLevel)
	{
		if (targetLevel <= 1)
		{
			return 0;
		}

		if (targetLevel > target.MaxLevel)
		{
			targetLevel = target.MaxLevel;
		}

		return target.ScalingCurve switch
		{
			ScalingCurveType.Linear => target.PointsForFirstLevel + (int)(target.ScalingFactorA * (targetLevel - 2)),
			ScalingCurveType.Exponential => (int)(target.PointsForFirstLevel *
												  Math.Pow(target.ScalingFactorA, targetLevel - 2)),
			ScalingCurveType.Polynomial => (int)((target.ScalingFactorA *
												  Math.Pow(targetLevel - 1, target.ScalingFactorB)) +
												 target.ScalingFactorC),
			ScalingCurveType.None => throw new NotImplementedException(),
			_ => throw new NotSupportedException($"Unsupported scaling curve type: {target.ScalingCurve}")
		};
	}

	/// <inheritdoc />
	public int GetPointsToNextLevel(T target)
	{
		if (target.Level >= target.MaxLevel)
		{
			return 0;
		}

		var totalPointsForNextLevel = this.GetTotalPointsForLevel(target, target.Level + 1);
		return Math.Max(0, totalPointsForNextLevel - target.Value);
	}

	/// <inheritdoc />
	public float GetProgressToNextLevel(T target)
	{
		if (target.Level >= target.MaxLevel)
		{
			return 1.0f;
		}

		var pointsForCurrentLevel = this.GetTotalPointsForLevel(target, target.Level);
		var pointsForNextLevel = this.GetTotalPointsForLevel(target, target.Level + 1);

		var pointsNeededForThisLevel = pointsForNextLevel - pointsForCurrentLevel;
		if (pointsNeededForThisLevel <= 0)
		{
			return 1.0f;
		}

		var pointsEarnedThisLevel = target.Value - pointsForCurrentLevel;
		return Math.Clamp((float)pointsEarnedThisLevel / pointsNeededForThisLevel, 0.0f, 1.0f);
	}

	private void LogValueChange(T target, int oldValue)
	{
		if (target.Value == oldValue)
		{
			return;
		}

		target.InvokeValueChanged(new ValueChangedEventArgs(oldValue, target.Value));
		var pointsChanged = target.Value - oldValue;

		if (target is IHasName namedTarget)
		{
			// Use the new generic helper
			this.logger.LogLevelablePointsChanged(typeof(T).Name, namedTarget.Name.Singular, pointsChanged, target.Value);
		}
		else
		{
			// Fallback for unnamed items
			this.logger.LogLevelablePointsChangedGeneric(typeof(T).Name, pointsChanged, target.Value);
		}
	}

	private void CheckForLevelChange(T target)
	{
		var oldLevel = target.Level;
		var newLevel = this.CalculateLevelFromPoints(target);

		if (newLevel != oldLevel)
		{
			target.Level = newLevel;
			target.InvokeLevelChanged(new ValueChangedEventArgs(oldLevel, newLevel));

			if (target is IHasName namedTarget)
			{
				// Use the new generic helper
				this.logger.LogLevelableChanged(typeof(T).Name, namedTarget.Name.Singular, oldLevel, newLevel);
			}
			else
			{
				// Fallback for unnamed items
				this.logger.LogLevelableChangedGeneric(typeof(T).Name, oldLevel, newLevel);
			}
		}
	}

	private int CalculateLevelFromPoints(T target)
	{
		var calculatedLevel = 1;
		while (calculatedLevel < target.MaxLevel)
		{
			if (target.Value < this.GetTotalPointsForLevel(target, calculatedLevel + 1))
			{
				break;
			}

			calculatedLevel++;
		}

		return calculatedLevel;
	}
}
