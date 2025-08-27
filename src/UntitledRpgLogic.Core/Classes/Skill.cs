using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces.Common;
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
	private int level;

	// Private backing fields for properties that require validation.
	private int value;

	/// <summary>
	///     Constructs a new Skill instance from a <see cref="SkillDataConfig" />.
	/// </summary>
	/// <param name="config"></param>
	/// <param name="instanceId"></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public Skill(SkillDataConfig config, Ulid? instanceId = null)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		this.Identifier = config.Identifier;

		this.Name = Name.Deserialize(config.Name);

		this.InstanceId = instanceId ?? Ulid.NewUlid();

		var levelingOptions = config.LevelingOptions ?? new LevelingOptions();

		this.MaxLevel = levelingOptions.MaxLevel ?? DefaultValues.SkillDefaultMaxLevel;
		if (this.MaxLevel < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(config),
				"LevelingOptions.MaxLevel must be at least 1.");
		}

		this.PointsForFirstLevel = levelingOptions.PointsForFirstLevel ?? DefaultValues.SkillDefaultPointsForFirstLevel;
		if (this.PointsForFirstLevel < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(config),
				"LevelingOptions.PointsForFirstLevel must be at least 1.");
		}

		this.ScalingFactorA = levelingOptions.ScalingFactorA ?? DefaultValues.SkillDefaultScalingAlpha;
		this.ScalingFactorB = levelingOptions.ScalingFactorB ?? DefaultValues.SkillDefaultScalingBeta;
		this.ScalingFactorC = levelingOptions.ScalingFactorC ?? DefaultValues.SkillDefaultScalingGamma;
		this.ScalingCurve = levelingOptions.ScalingCurve ?? DefaultValues.SkillDefaultScalingCurve;

		// IHasLeveling - Initial State using properties to enforce guardrails
		this.Level = 1;
		this.Value = 0; // Represents current experience points
	}

	/// <inheritdoc />
	public Ulid Identifier { get; }

	// IHasName Implementation
	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public Ulid InstanceId { get; init; }

	// IHasLeveling Implementation with Guardrails

	/// <inheritdoc cref="IHasMutableValue" />
	public int Value
	{
		get => this.value;
		set => this.value = Math.Max(0, value); // Experience cannot be negative
	}

	/// <inheritdoc />
	public int Level
	{
		get => this.level;
		set => this.level = Math.Clamp(value, 1, this.MaxLevel); // Level is always between 1 and MaxLevel
	}

	/// <inheritdoc />
	public int MaxLevel { get; }

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
	public void InvokeValueChanged(ValueChangedEventArgs args) => this.ValueChanged?.Invoke(this, args);

	/// <inheritdoc />
	public void InvokeLevelChanged(ValueChangedEventArgs args) => this.LevelChanged?.Invoke(this, args);

	/// <summary>
	///     Creates a new Skill instance from a database model.
	/// </summary>
	public static Skill FromDbModel(SkillDefinition dbModel, Ulid? instanceId = null)
	{
		ArgumentNullException.ThrowIfNull(dbModel, nameof(dbModel));
		// This factory method reuses the main constructor, so all validation is applied.
		return new Skill(
			new SkillDataConfig
			{
				Identifier = dbModel.Id,
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
