using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Environment;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     Abstract base record defining a magical, physical, or status outcome applied by an ability or world interaction.
/// </summary>
[Table("effects")]
public abstract record Effect : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes default base values for EF Core materialization.
	/// </summary>
	[SetsRequiredMembers]
	protected Effect()
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Effect" /> record with a designated name and type.
	/// </summary>
	[SetsRequiredMembers]
	protected Effect(Name name, EffectType effectType) : this()
	{
		this.Name = name;
		this.EffectType = effectType;
	}

	/// <summary>
	///     The display name of the effect.
	/// </summary>
	public required Name Name { get; init; } = Name.Empty;

	/// <summary>
	///     Descriptive flavor text detailing the effect's mechanics.
	/// </summary>
	[MaxLength(1024)]
	public string Description { get; init; } = string.Empty;

	/// <summary>
	///     The classification of this effect, serving as the EF Core TPH discriminator.
	/// </summary>
	public EffectType EffectType { get; init; } = EffectType.None;

	/// <summary>
	///     How long the effect persists in seconds (0 for instantaneous effects).
	/// </summary>
	public float Duration { get; init; }

	/// <summary>
	///     The frequency in seconds at which periodic ticks occur (0 if non-periodic).
	/// </summary>
	public float TickInterval { get; init; }

	/// <summary>
	///     Specific stat modifications (e.g., -50 Health, +10 Strength) applied by this effect.
	/// </summary>
	public ICollection<AffectedStat> AffectedStats { get; init; } = [];

	/// <summary>
	///     Environmental modifications (e.g., +150°C local temperature) applied by this effect.
	/// </summary>
	public ICollection<AffectedAmbient> AffectedAmbients { get; init; } = [];

	/// <summary>
	///     Abilities that trigger this effect on successful activation.
	/// </summary>
	public virtual ICollection<AbilityDefinition> TriggeringAbilities { get; } = new List<AbilityDefinition>();

	/// <summary>
	///     The unique database identifier for this effect template.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; } = Ulid.NewUlid();
}
