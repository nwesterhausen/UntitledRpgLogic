using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Abilities.Effects;

namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Defines an owned modification delta applied to a character stat (e.g., HP, Mana, Strength).
/// </summary>
/// <remarks>Owned by <see cref="Effect" /> and serialized as JSON.</remarks>
public record AffectedStat : ChangeOptions
{
	/// <summary>
	///     Create an empty
	/// </summary>
	public AffectedStat()
	{
	}

	/// <summary>
	///     Create a new affected stat for given stat definition id.
	/// </summary>
	/// <param name="statId"></param>
	[SetsRequiredMembers]
	public AffectedStat(Ulid statId) : this() => this.StatId = statId;

	/// <summary>
	///     Identifier of the target <see cref="StatDefinition" /> being modified.
	/// </summary>
	public required Ulid StatId { get; init; }

	/// <inheritdoc />
	public override AffectedStat Apply(ChangeOptions options)
	{
		var mergedBase = base.Apply(options);

		if (options is AffectedStat affectedOptions)
		{
			return (AffectedStat)mergedBase with
			{
				StatId = affectedOptions.StatId == Ulid.Empty ? this.StatId : affectedOptions.StatId
			};
		}

		return (AffectedStat)mergedBase;
	}
}
