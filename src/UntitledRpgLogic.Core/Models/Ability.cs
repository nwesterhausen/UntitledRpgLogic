using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// Defines the core data structure for an ability (Spell, Active, or Passive).
/// </summary>
public class Ability
{
	/// <summary>
	/// Gets or sets the unique identifier (PK).
	/// </summary>
	[Key]
	public Ulid Id { get; set; }

	/// <summary>
	/// Gets or sets the display name.
	/// </summary>
	public required Name Name { get; set; } = Name.Empty;

	/// <summary>
	/// Gets or sets the broad classification of this ability.
	/// </summary>
	public AbilityType AbilityType { get; set; }

	/// <summary>
	/// Gets or sets how the ability is delivered or targeted.
	/// </summary>
	public TargetType TargetType { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this ability can target the caster.
	/// If TargetType is 'Self', this is implicitly true.
	/// </summary>
	public bool AffectsCaster { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this ability can target allies.
	/// </summary>
	public bool AffectsAllies { get; set; }

	/// <summary>
	/// Gets or sets the number of targets this ability can affect.
	/// Per the design, this defaults to 1 unless overridden (usually by Projectiles).
	/// </summary>
	public int NumberOfTargets { get; set; } = 1;

	/// <summary>
	/// Gets or sets the base time (in seconds) required to activate the ability. 0 is instant.
	/// (Note: This field will be ignored by logic processing PassiveAbility types).
	/// </summary>
	public float CastTime { get; set; }

	/// <summary>
	/// Gets or sets the Foreign Key (FK) for the SkillDiscipline.
	/// </summary>
	public Ulid SkillDisciplineId { get; set; }

	/// <summary>
	/// Navigation property to the discipline this ability belongs to.
	/// </summary>
	[Required]
	[ForeignKey(nameof(SkillDisciplineId))]
	public virtual SkillDefinition SkillDiscipline { get; set; } = null!;

	// --- 1-to-Many Relationships (Owned complex types or separate tables) ---

	/// <summary>
	/// Gets or sets the list of stat costs required to activate the ability.
	/// </summary>
	public virtual ICollection<StatCost> StatCosts { get;  } = new List<StatCost>();

	/// <summary>
	/// Gets or sets requirements needed to permanently learn the ability.
	/// </summary>
	public virtual ICollection<LearningRequirement> LearningRequirements { get;  } = new List<LearningRequirement>();

	/// <summary>
	/// Gets or sets requirements checked just before activation.
	/// (Note: Ignored by PassiveAbility logic).
	/// </summary>
	public virtual ICollection<CastingRequirement> CastingRequirements { get;  } = new List<CastingRequirement>();

	/// <summary>
	/// Gets or sets the list of influences that contribute to the chance of activation failure.
	/// (Note: Ignored by PassiveAbility logic).
	/// </summary>
	public virtual ICollection<FailureInfluence> FailureInfluences { get;  } = new List<FailureInfluence>();

	// --- Many-to-Many Relationships (Require configuration in DbContext) ---

	/// <summary>
	/// Navigation property defining the effects activated on a successful activation (or applied passively).
	/// </summary>
	public virtual ICollection<Effect> ActiveEffects { get;  } = new List<Effect>();

	/// <summary>
	/// Navigation property defining the effects activated only if activation fails.
	/// (Note: Ignored by PassiveAbility logic).
	/// </summary>
	public virtual ICollection<Effect> FailureEffects { get;  } = new List<Effect>();
}
