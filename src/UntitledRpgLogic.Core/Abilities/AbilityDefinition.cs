using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Core.Abilities;

/// <summary>
///     Database catalog model defining an ability (Spell, Active Skill, or Passive Perk).
/// </summary>
[Table("abilities")]
public record AbilityDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="AbilityDefinition" /> record with default values for EF Core.
	/// </summary>
	public AbilityDefinition() { }

	/// <summary>
	///     Initializes a new instance of the <see cref="AbilityDefinition" /> record with a designated name.
	/// </summary>
	/// <param name="name">The display name of the ability.</param>
	public AbilityDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     Initializes a new instance of the <see cref="AbilityDefinition" /> record with a designated name and skill
	///     discipline.
	/// </summary>
	/// <param name="name">The display name of the ability.</param>
	/// <param name="skillDisciplineId">The Id of the skill (by definition) that this ability belongs to.</param>
	[SetsRequiredMembers]
	public AbilityDefinition(Name name, Ulid skillDisciplineId) : this()
	{
		this.Name = name;
		this.SkillDisciplineId = skillDisciplineId;
	}

	/// <summary>
	///     The display name of the ability.
	/// </summary>
	public required Name Name { get; init; } = Name.Empty;

	/// <summary>
	///     The broad classification (Active, Passive, Spell, Channel).
	/// </summary>
	public AbilityType AbilityType { get; init; } = AbilityType.None;

	/// <summary>
	///     How the ability is targeted or delivered (Self, SingleTarget, AreaOfEffect, Projectile).
	/// </summary>
	public TargetingType TargetingType { get; init; } = TargetingType.None;

	/// <summary>
	///     Indicates whether this ability can affect the caster.
	/// </summary>
	public bool AffectsCaster { get; init; }

	/// <summary>
	///     Indicates whether this ability can affect friendly targets.
	/// </summary>
	public bool AffectsAllies { get; init; }

	/// <summary>
	///     The number of targets this ability can simultaneously strike or select.
	/// </summary>
	[Range(1, int.MaxValue)]
	public int NumberOfTargets { get; init; } = 1;

	/// <summary>
	///     The activation/invocation time in seconds (0 for instant cast).
	/// </summary>
	public float CastTime { get; init; }

	/// <summary>
	///     Foreign key referencing the skill discipline this ability belongs to.
	/// </summary>
	public Ulid SkillDisciplineId { get; init; }

	/// <summary>
	///     Navigation property to the skill discipline template.
	/// </summary>
	[ForeignKey(nameof(SkillDisciplineId))]
	public SkillDefinition? SkillDiscipline { get; init; }

	// --- 1-to-Many Collections (Owned or Dependent Tables) ---

	/// <summary>
	///     Stat costs (Mana, Stamina, Health) required to cast this ability.
	/// </summary>
	public ICollection<StatCost> StatCosts { get; init; } = [];

	/// <summary>
	///     Prerequisites required to permanently learn or unlock this ability.
	/// </summary>
	public ICollection<LearningRequirement> LearningRequirements { get; init; } = [];

	/// <summary>
	///     Preconditions verified immediately prior to activation.
	/// </summary>
	public ICollection<CastingRequirement> CastingRequirements { get; init; } = [];

	/// <summary>
	///     Environmental or state influences contributing to activation failure chance.
	/// </summary>
	public ICollection<FailureInfluence> FailureInfluences { get; init; } = [];

	// --- Many-to-Many Relationships (Configured in AbilityConfiguration) ---

	/// <summary>
	///     Effects applied upon successful activation.
	/// </summary>
	public ICollection<Effect> ActiveEffects { get; } = new List<Effect>();

	/// <summary>
	///     Effects applied when activation fails or backfires.
	/// </summary>
	public ICollection<Effect> FailureEffects { get; } = new List<Effect>();

	/// <summary>
	///     The unique identifier for the ability (can be supplied from external TOML config).
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; } = Ulid.NewUlid();
}
