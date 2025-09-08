using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Base abstract class defining an Effect payload.
///     This class will use TPH (Table-Per-Hierarchy) inheritance in EFCore,
///     discriminated by the 'EffectType' property.
/// </summary>
public abstract class Effect
{
	/// <summary>
	///     Gets or sets the unique identifier (PK).
	/// </summary>
	[Key]
	public Ulid Id { get; set; }

	/// <summary>
	///     Gets the discriminator type used by EFCore to map the inheritance.
	/// </summary>
	public EffectType EffectType { get; protected set; }

	/// <summary>
	///     Gets or sets the duration (in seconds) the effect lasts. 0 = Instant/Permanent (until dispelled).
	/// </summary>
	public float Duration { get; set; }

	/// <summary>
	///     Gets or sets modifications applied to character stats (e.g., +10 HP, -5 Stamina).
	/// </summary>
	public virtual ICollection<AffectedStat> AffectedStats { get; } = new List<AffectedStat>();

	/// <summary>
	///     Gets or sets modifications applied to world ambients (e.g., +10 Temperature, -1 Gravity).
	/// </summary>
	public virtual ICollection<AffectedAmbient> AffectedAmbients { get; } = new List<AffectedAmbient>();

	// --- Many-to-Many (Inverse Navigation) ---

	/// <summary>
	///     Inverse navigation for abilities that use this effect successfully.
	/// </summary>
	public virtual ICollection<Ability> AbilitiesUsingAsActive { get; } = new List<Ability>();

	/// <summary>
	///     Inverse navigation for abilities that use this effect upon failure.
	/// </summary>
	public virtual ICollection<Ability> AbilitiesUsingAsFailure { get; } = new List<Ability>();
}
