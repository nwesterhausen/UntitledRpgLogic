using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Defines the expected structure of an 'Ability' section in a TOML config file.
///     This DTO is deserialized from the file and then mapped to the EF Core 'Ability' entity.
/// </summary>
public record AbilityConfig
{
	// --- Metadata ---
	/// <summary>
	///     Unique identifier for the ability.
	/// </summary>
	public Ulid Id { get; init; }

	/// <summary>
	///     Human-readable name of the ability.
	/// </summary>
	public string Name { get; init; } = string.Empty;

	// --- Type Information ---
	/// <summary>
	///     The type of ability, e.g. "Spell", "PassiveSkill", "ActiveSkill".
	/// </summary>
	public AbilityType AbilityType { get; init; } = AbilityType.None;

	/// <summary>
	///     The targeting type of the ability, e.g. "Aoe", "Self"
	/// </summary>
	public TargetType TargetType { get; init; } = TargetType.None;

	/// <summary>
	///     Whether the ability can affect the caster themselves.
	/// </summary>
	public bool AffectsCaster { get; init; }

	/// <summary>
	///     Whether the ability can affect allies.
	/// </summary>
	public bool AffectsAllies { get; init; }

	// --- Core Properties ---
	/// <summary>
	///     The skill discipline associated with this ability, e.g. "Fire Magic", "Swordsmanship".
	/// </summary>
	public Ulid SkillDisciplineId { get; init; }

	/// <summary>
	///     The number of targets this ability can affect.
	/// </summary>
	public int NumberOfTargets { get; init; } = 1;

	/// <summary>
	///     The time it takes to cast or activate the ability, in seconds.
	/// </summary>
	public float CastTime { get; init; }

	// --- Complex Child Sections ---
	/// <summary>
	///     List of stat costs associated with using this ability.
	/// </summary>
	public IReadOnlyList<StatCostConfig> StatCosts { get; init; } = new List<StatCostConfig>();

	/// <summary>
	///     List of requirements that must be met to learn this ability.
	/// </summary>
	public IReadOnlyList<RequirementConfig> LearningRequirements { get; init; } = new List<RequirementConfig>();

	/// <summary>
	///     List of requirements that must be met to cast this ability.
	/// </summary>
	public IReadOnlyList<RequirementConfig> CastingRequirements { get; init; } = new List<RequirementConfig>();

	/// <summary>
	///     List of influences that modify the success chance of this ability.
	/// </summary>
	public IReadOnlyList<FailureInfluenceConfig> FailureInfluences { get; init; } = new List<FailureInfluenceConfig>();

	// --- Effect Links ---
	/// <summary>
	///     The effects that are applied when the ability is successfully activated.
	/// </summary>
	public IReadOnlyList<Ulid> ActiveEffectIds { get; init; } = new List<Ulid>();

	/// <summary>
	///     The effects that are applied when the ability fails to activate.
	/// </summary>
	public IReadOnlyList<Ulid> FailureEffectIds { get; init; } = new List<Ulid>();
}
