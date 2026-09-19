namespace UntitledRpgLogic.Core.Progression;

/// <summary>
///     Represents the outcome of experience progression or leveling applied to an entity.
/// </summary>
public record ProgressionResult
{
	/// <summary>
	///     The skill discipline or progression track that received experience.
	/// </summary>
	public Ulid SkillDefinitionId { get; init; }

	/// <summary>
	///     The amount of raw experience points added.
	/// </summary>
	public int ExperienceGained { get; init; }

	/// <summary>
	///     The skill's level prior to the experience gain.
	/// </summary>
	public int PreviousLevel { get; init; }

	/// <summary>
	///     The new active proficiency level.
	/// </summary>
	public int CurrentLevel { get; init; }

	/// <summary>
	///     The total accumulated experience points after the gain.
	/// </summary>
	public int TotalExperiencePoints { get; init; }

	/// <summary>
	///     Whether the experience gain crossed a threshold that resulted in one or more level advances.
	/// </summary>
	public bool DidLevelUp => this.CurrentLevel > this.PreviousLevel;

	/// <summary>
	///     The number of discrete level boundaries crossed.
	/// </summary>
	public int LevelsGained => Math.Max(0, this.CurrentLevel - this.PreviousLevel);

	/// <summary>
	///     Abilities or perks that became unlocked or available to learn as a result of leveling.
	/// </summary>
	public IReadOnlyCollection<Ulid> UnlockedAbilityIds { get; init; } = [];
}
