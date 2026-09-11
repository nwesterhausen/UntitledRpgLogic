using UntitledRpgLogic.Core.Environment;

namespace UntitledRpgLogic.Core.Entities;

/// <summary>
///     Defines the fluid or gaseous respiration requirements and toxic thresholds for an entity.
///     All partial and total pressures are measured in standard atmospheres (atm).
/// </summary>
public record RespiratoryProfile
{
	/// <summary>
	///     Indicates whether the entity requires respiration to survive.
	///     If false, the entity is anaerobic and ignores suffocation or drowning (e.g., Undead, Golems, Constructs).
	/// </summary>
	public bool RequiresBreathing { get; init; } = true;

	/// <summary>
	///     The primary material required for respiration (e.g., Oxygen for humans, Water for fish, Methane for alien fauna).
	/// </summary>
	public Ulid? RequiredMediumMaterialId { get; init; }

	/// <summary>
	///     Alternative materials the entity can breathe (e.g., Amphibians breathing both atmospheric Oxygen and Water).
	/// </summary>
	public ICollection<Ulid> AlternativeBreathableMaterials { get; init; } = [];

	/// <summary>
	///     Minimum partial or hydrostatic pressure in atmospheres required to prevent suffocation (e.g., 0.16 atm for human O2).
	/// </summary>
	public float MinRequiredPressure { get; init; } = 0.16f;

	/// <summary>
	///     Maximum safe pressure in atmospheres before hyperoxia, compression sickness, or barotrauma occurs.
	/// </summary>
	public float MaxSafePressure { get; init; } = 1.4f;

	/// <summary>
	///     Materials that trigger poisoning or tissue necrosis upon inhalation or gill filtration.
	/// </summary>
	public ICollection<MaterialToxicityThreshold> ToxicSubstances { get; init; } = [];
}
