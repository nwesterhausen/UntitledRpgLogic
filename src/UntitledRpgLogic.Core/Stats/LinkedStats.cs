using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Represents a dependency relationship where one stat's value influences another.
/// </summary>
[Table("linked_stats")]
public record LinkedStats
{
	/// <summary>
	///     Initializes an empty instance of the <see cref="LinkedStats" /> record for EF Core.
	/// </summary>
	[SetsRequiredMembers]
	public LinkedStats()
	{
		this.StatId = Ulid.Empty;
		this.DependsOnId = Ulid.Empty;
		this.Ratio = 0f;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="LinkedStats" /> record with defined dependencies.
	/// </summary>
	[SetsRequiredMembers]
	public LinkedStats(Ulid statId, Ulid dependsOnId, float ratio)
	{
		this.StatId = statId;
		this.DependsOnId = dependsOnId;
		this.Ratio = ratio;
	}

	/// <summary>
	///     Foreign key for the target dependent stat (Composite PK Part 1).
	/// </summary>
	public Ulid StatId { get; init; }

	/// <summary>
	///     Navigation property to the dependent stat definition.
	/// </summary>
	[ForeignKey(nameof(StatId))]
	public StatDefinition? Stat { get; init; }

	/// <summary>
	///     Foreign key for the prerequisite source stat (Composite PK Part 2).
	/// </summary>
	public Ulid DependsOnId { get; init; }

	/// <summary>
	///     Navigation property to the source stat definition being depended upon.
	/// </summary>
	[ForeignKey(nameof(DependsOnId))]
	public StatDefinition? DependsOnStat { get; init; }

	/// <summary>
	///     The percentage ratio of the source stat transferred into the dependent stat.
	/// </summary>
	public required float Ratio { get; set; }
}
