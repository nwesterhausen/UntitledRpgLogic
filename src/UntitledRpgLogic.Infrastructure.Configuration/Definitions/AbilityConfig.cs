using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
/// Defines the expected structure of an 'Ability' section in a TOML config file.
/// This DTO is deserialized from the file and then mapped to the EF Core 'Ability' entity.
/// </summary>
public record AbilityConfig
{
  // --- Metadata ---
  /// <summary>
  /// Unique identifier for the ability.
  /// </summary>
  public Ulid Id { get; set; }

  /// <summary>
  /// Human-readable name of the ability.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  // --- Type Information ---
  /// <summary>
  /// The type of ability, e.g. "Spell", "PassiveSkill", "ActiveSkill".
  /// </summary>
  public AbilityType AbilityType { get; init; } = AbilityType.None;

  /// <summary>
  /// The targeting type of the ability, e.g. "Aoe", "Self"
  /// </summary>
  public TargetType TargetType { get; init; } = TargetType.None;

  /// <summary>
  /// Whether the ability can affect the caster themselves.
  /// </summary>
  public bool AffectsCaster { get; set; }

  /// <summary>
  /// Whether the ability can affect allies.
  /// </summary>
  public bool AffectsAllies { get; set; }

  // --- Core Properties ---
  /// <summary>
  /// The skill discipline associated with this ability, e.g. "Fire Magic", "Swordsmanship".
  /// </summary>
  public Ulid SkillDisciplineId { get; set; }

  /// <summary>
  /// The number of targets this ability can affect.
  /// </summary>
  public int NumberOfTargets { get; set; } = 1;

  /// <summary>
  /// The time it takes to cast or activate the ability, in seconds.
  /// </summary>
  public float CastTime { get; set; }

  // --- Complex Child Sections ---
  /// <summary>
  /// List of stat costs associated with using this ability.
  /// </summary>
  public List<StatCostConfig> StatCosts { get; set; } = new();

  /// <summary>
  /// List of requirements that must be met to learn this ability.
  /// </summary>
  public List<RequirementConfig> LearningRequirements { get; set; } = new();

  /// <summary>
  /// List of requirements that must be met to cast this ability.
  /// </summary>
  public List<RequirementConfig> CastingRequirements { get; set; } = new();

  /// <summary>
  /// List of influences that modify the success chance of this ability.
  /// </summary>
  public List<FailureInfluenceConfig> FailureInfluences { get; set; } = new();

  // --- Effect Links ---
  /// <summary>
  /// The effects that are applied when the ability is successfully activated.
  /// </summary>
  public List<Ulid> ActiveEffectIds { get; set; } = new();

  /// <summary>
  /// The effects that are applied when the ability fails to activate.
  /// </summary>
  public List<Ulid> FailureEffectIds { get; set; } = new();
}
