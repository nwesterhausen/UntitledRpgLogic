using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Defines the structure for an 'Effect' in a TOML file.
///     This would be a separate TOML file, loaded independently.
/// </summary>
public record EffectConfig
{
	/// <summary>
	///     Unique identifier for the effect.
	/// </summary>
	public Ulid Id { get; init; }

	/// <summary>
	///     The type of effect, e.g. "Damage", "Heal", "Buff".
	/// </summary>
	public EffectType EffectType { get; init; }

	/// <summary>
	///     How long the effect lasts, in seconds. Instantaneous effects would have Duration = 0.
	/// </summary>
	public float Duration { get; init; }

	/// <summary>
	///     List of stats that this effect modifies.
	/// </summary>
	public IReadOnlyList<AffectedStatConfig> AffectedStats { get; init; } = new List<AffectedStatConfig>();

	/// <summary>
	///     List of ambients that this effect modifies.
	/// </summary>
	public IReadOnlyList<AffectedAmbientConfig> AffectedAmbients { get; init; } = new List<AffectedAmbientConfig>();

	// --- Type-Specific Properties ---
	// The loader would check EffectType and only read the relevant properties.

	/// <summary>
	///     If the effect is a damage effect, the type of damage (e.g. "Fire", "Physical").
	/// </summary>
	public Ulid? DamageTypeId { get; init; }

	/// <summary>
	///     If the effect is a damage or heal effect, the base amount of damage or healing.
	/// </summary>
	public float? BaseAmount { get; init; }

	/// <summary>
	///     If the effect is a summon effect, the entity to summon.
	/// </summary>
	public Ulid? EntityToSummonId { get; init; }

	/// <summary>
	///     If the effect is a summon effect, the quantity of entities to summon.
	/// </summary>
	public int? Quantity { get; init; }
}
