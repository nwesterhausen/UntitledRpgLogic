using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.Abilities.Effects;

/// <summary>
///     Concrete effect subtype that conjures an entity or minion into the world.
/// </summary>
public record SummonEffect : Effect
{
	/// <summary>
	///     Initializes default base values.
	/// </summary>
	[SetsRequiredMembers]
	public SummonEffect() => this.EffectType = EffectType.Summon;

	/// <summary>
	///     Initializes a new <see cref="Effect" /> with <see cref="EffectType.Summon" />.
	/// </summary>
	[SetsRequiredMembers]
	public SummonEffect(Name name) : base(name, EffectType.Summon)
	{
	}

	/// <summary>
	///     Foreign key of the template entity to spawn.
	/// </summary>
	public Ulid SummonEntityTemplateId { get; init; }

	/// <summary>
	///     Link to the <see cref="Entity" /> to summon.
	/// </summary>
	[ForeignKey(nameof(SummonEntityTemplateId))]
	public Entity? SummonTemplate { get; init; }

	/// <summary>
	///     The amount of entities summoned.
	/// </summary>
	public int Quantity { get; init; } = 1;
}
