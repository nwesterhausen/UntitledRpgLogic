using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Core.Options;

// For Math.Clamp and exceptions

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     A data container representing a skill. All logic is handled by a service.
///     This class includes guardrails to ensure its internal state remains valid.
/// </summary>
public class Skill : ISkill
{
    private int _level;

    // Private backing fields for properties that require validation.
    private int _value;

    /// <summary>
    ///     Constructs a new Skill instance from a <see cref="SkillDataConfig" />.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="instanceId"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Skill(SkillDataConfig config, Guid? instanceId = null)
    {
        // IHasGuid
        Guid = config.ExplicitId ?? Guid.NewGuid();
        Id = Convert.ToBase64String(Guid.ToByteArray());
        ShortGuid = Guid.ToString("N")[..8].ToUpperInvariant();

        // IHasName
        Name = Name.Deserialize(config.Name);

        // IInstantiable
        InstanceId = instanceId ?? Guid.Empty;

        // IHasLeveling - Configuration with Validation
        LevelingOptions levelingOptions = config.LevelingOptions ?? new LevelingOptions();

        MaxLevel = levelingOptions.MaxLevel ?? DefaultValues.SKILL_DEFAULT_MAX_LEVEL;
        if (MaxLevel < 1)
            throw new ArgumentOutOfRangeException(nameof(config.LevelingOptions.MaxLevel),
                "MaxLevel must be at least 1.");

        PointsForFirstLevel = levelingOptions.PointsForFirstLevel ?? DefaultValues.SKILL_DEFAULT_POINTS_FOR_FIRST_LEVEL;
        if (PointsForFirstLevel < 1)
            throw new ArgumentOutOfRangeException(nameof(config.LevelingOptions.PointsForFirstLevel),
                "PointsForFirstLevel must be at least 1.");

        ScalingFactorA = levelingOptions.ScalingFactorA ?? DefaultValues.SKILL_DEFAULT_SCALING_ALPHA;
        ScalingFactorB = levelingOptions.ScalingFactorB ?? DefaultValues.SKILL_DEFAULT_SCALING_BETA;
        ScalingFactorC = levelingOptions.ScalingFactorC ?? DefaultValues.SKILL_DEFAULT_SCALING_GAMMA;
        ScalingCurve = levelingOptions.ScalingCurve ?? DefaultValues.SKILL_DEFAULT_SCALING_CURVE;

        // IHasLeveling - Initial State using properties to enforce guardrails
        Level = 1;
        Value = 0; // Represents current experience points
    }

    // IHasGuid Implementation
    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }

    // IHasName Implementation
    /// <inheritdoc />
    public Name Name { get; }

    // IInstantiable Implementation
    /// <inheritdoc />
    public Guid InstanceId { get; init; }

    // IHasLeveling Implementation with Guardrails

    /// <inheritdoc cref="IHasMutableValue" />
    public int Value
    {
        get => _value;
        set => _value = Math.Max(0, value); // Experience cannot be negative
    }

    /// <inheritdoc />
    public int Level
    {
        get => _level;
        set => _level = Math.Clamp(value, 1, MaxLevel); // Level is always between 1 and MaxLevel
    }

    /// <inheritdoc />
    public int MaxLevel { get; }

    // NOTE: ExperienceToNextLevel is a calculated value and does not belong on the data class.
    // It is computed by the SkillService. This property should be removed from the ISkill interface.

    /// <inheritdoc />
    public float ScalingFactorA { get; }

    /// <inheritdoc />
    public float ScalingFactorB { get; }

    /// <inheritdoc />
    public float ScalingFactorC { get; }

    /// <inheritdoc />
    public ScalingCurveType ScalingCurve { get; }

    /// <inheritdoc />
    public int PointsForFirstLevel { get; }

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? LevelChanged;

    // Methods for the service to invoke events
    /// <inheritdoc />
    public void InvokeValueChanged(ValueChangedEventArgs args)
    {
        ValueChanged?.Invoke(this, args);
    }

    /// <inheritdoc />
    public void InvokeLevelChanged(ValueChangedEventArgs args)
    {
        LevelChanged?.Invoke(this, args);
    }

    /// <summary>
    ///     Creates a new Skill instance from a database model.
    /// </summary>
    public static Skill FromDbModel(SkillDefinition dbModel, Guid? instanceId = null)
    {
        // This factory method reuses the main constructor, so all validation is applied.
        return new Skill(new SkillDataConfig
        {
            ExplicitId = dbModel.Id,
            Name = dbModel.Name,
            LevelingOptions = new LevelingOptions
            {
                MaxLevel = dbModel.MaxLevel,
                ScalingFactorA = dbModel.ScalingFactorA,
                ScalingFactorB = dbModel.ScalingFactorB,
                ScalingFactorC = dbModel.ScalingFactorC,
                PointsForFirstLevel = dbModel.PointsForFirstLevel,
                ScalingCurve = dbModel.ScalingCurve
            }
        }, instanceId);
    }

    // NOTE: The AddPoints, RemovePoints, and SetPoints methods have been removed.
    // This logic is the responsibility of the SkillService and should be removed from the ISkill interface
    // and its base interfaces (like IHasChangeableValue).
}
