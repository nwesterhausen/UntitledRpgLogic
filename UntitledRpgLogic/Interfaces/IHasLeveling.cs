using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Events;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes which have leveling capabilities. Depends on the value reflecting its internal
///     experience number, and the level being derived from that value.
/// </summary>
public interface IHasLeveling : IHasChangeableValue
{
    /// <summary>
    ///     The level of the object, derived from the value.
    /// </summary>
    public int Level { get; }

    /// <summary>
    ///     The maximum level of the object.
    /// </summary>
    public int MaxLevel { get; }

    /// <summary>
    ///     The experience required to reach the next level.
    /// </summary>
    public int ExperienceToNextLevel { get; }

    /// <summary>
    ///     The primary scaling factor (A) for level progression.
    ///     Controls the base rate at which experience requirements increase per level.
    /// </summary>
    public float LevelScalingA { get; }

    /// <summary>
    ///     The secondary scaling factor (B) for level progression.
    ///     Used as an additional multiplier or offset in the experience formula to fine-tune curve steepness.
    /// </summary>
    public float ScalingFactorB { get; }

    /// <summary>
    ///     The tertiary scaling factor (C) for level progression.
    ///     Used as an exponent or offset in polynomial or logarithmic scaling to adjust curve shape.
    /// </summary>
    public int ScalingFactorC { get; }

    /// <summary>
    ///     The type of scaling curve used to determine experience requirements for each level.
    ///     Determines whether experience increases linearly, polynomially, logarithmically, or not at all.
    /// </summary>
    public ScalingCurveType ScalingCurve { get; }

    /// <summary>
    ///     The number of points required to reach the first level. (i.e. from level 0 to level 1)
    /// </summary>
    /// <remarks>Typically this is 1. Affects the overall scaling even though this value is not really used "in game."</remarks>
    public int PointsForFirstLevel { get; }

    /// <summary>
    ///     How far the object is from reaching the next level, expressed as a percentage (0.0 to 1.0).
    /// </summary>
    public float ProgressToNextLevel
    {
        get
        {
            if (ExperienceToNextLevel == 0)
                return 1.0f; // If no experience is needed for the next level, progress is complete.
            return Math.Clamp((float)Value / ExperienceToNextLevel, 0.0f, 1.0f);
        }
    }


    /// <summary>
    ///     Event that is triggered when the level changes.
    /// </summary>
    event EventHandler<ValueChangedEventArgs>? LevelChanged;
}