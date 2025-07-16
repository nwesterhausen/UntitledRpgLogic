using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines the data contract for an object that has leveling capabilities.
///     The 'Value' property represents the total accumulated experience points.
///     All calculation logic is handled by a dedicated service (e.g., ISkillService).
/// </summary>
public interface IHasLeveling : IHasMutableValue
{
    /// <summary>
    ///     The current level of the object. This is managed by a service.
    /// </summary>
    int Level { get; set; }

    /// <summary>
    ///     The maximum level the object can attain.
    /// </summary>
    int MaxLevel { get; }

    // --- Configuration Properties for Leveling Formulas ---

    /// <summary>
    ///     The primary scaling factor (A) for level progression.
    ///     Controls the base rate at which experience requirements increase per level.
    /// </summary>
    float ScalingFactorA { get; }

    /// <summary>
    ///     The secondary scaling factor (B) for level progression.
    ///     Used as an additional multiplier or offset in the experience formula to fine-tune curve steepness.
    /// </summary>
    float ScalingFactorB { get; }

    /// <summary>
    ///     The tertiary scaling factor (C) for level progression.
    ///     Used as an exponent or offset in polynomial or logarithmic scaling to adjust curve shape.
    /// </summary>
    float ScalingFactorC { get; }

    /// <summary>
    ///     The type of scaling curve used to determine experience requirements for each level.
    /// </summary>
    ScalingCurveType ScalingCurve { get; }

    /// <summary>
    ///     The total number of experience points required to advance from level 1 to level 2.
    /// </summary>
    int PointsForFirstLevel { get; }

    // --- Events ---

    /// <summary>
    ///     Event that is triggered when the level changes.
    /// </summary>
    event EventHandler<ValueChangedEventArgs>? LevelChanged;

    /// <summary>
    ///     A method for the owning service to invoke the LevelChanged event.
    /// </summary>
    void InvokeLevelChanged(ValueChangedEventArgs args);
}
