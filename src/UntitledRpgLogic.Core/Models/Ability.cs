using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Database catalog model defining an ability (Spell, Active Skill, or Passive Perk).
/// </summary>
[Table("abilities")]
public record Ability : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="Ability" /> record with default values for EF Core.
	/// </summary>
	public Ability()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.AbilityType = AbilityType.PassiveAbility;
		this.TargetingType = TargetingType.Self;
		this.NumberOfTargets = 1;
		this.CastTime = 0f;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Ability" /> record with a designated name.
	/// </summary>
	/// <param name="name">The display name of the ability.</param>
	public Ability(Name name) : this() => this.Name = name;

	/// <summary>
	///     The unique identifier for the ability (can be supplied from external TOML config).
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The display name of the ability.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     The broad classification (Active, Passive, Spell, Channel).
	/// </summary>
	public AbilityType AbilityType { get; init; }

	/// <summary>
	///     How the ability is targeted or delivered (Self, SingleTarget, AreaOfEffect, Projectile).
	/// </summary>
	public TargetingType TargetingType { get; init; }

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
	public int NumberOfTargets { get; init; }

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
	public virtual ICollection<Effect> ActiveEffects { get; } = new List<Effect>();

	/// <summary>
	///     Effects applied when activation fails or backfires.
	/// </summary>
	public virtual ICollection<Effect> FailureEffects { get; } = new List<Effect>();
}
