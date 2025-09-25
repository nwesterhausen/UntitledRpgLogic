using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents a link between two stats, where one stat is dependent on another. Provides a one-to-many relationship
/// </summary>
public class LinkedStats
{
	/// <summary>
	///     Create an empty instance of the LinkedStats class (required by EF Core)
	/// </summary>
	public LinkedStats()
	{
		this.DependentStatId = Ulid.Empty;
		this.LinkedStatId = Ulid.Empty;
		this.Ratio = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="LinkedStats" /> class with the specified dependent stat ID,
	///     linked stat ID, and ratio.
	/// </summary>
	/// <param name="dependentStatId">The unique identifier of the dependent stat.</param>
	/// <param name="linkedStatId">The unique identifier of the linked stat.</param>
	/// <param name="ratio">The ratio that defines the percentage of the linked stat's value added to the dependent stat.</param>
	public LinkedStats(Ulid dependentStatId, Ulid linkedStatId, float ratio)
	{
		this.DependentStatId = dependentStatId;
		this.LinkedStatId = linkedStatId;
		this.Ratio = ratio;
	}

	/// <summary>
	///     The unique identifier for the dependent stat. This is used to identify the stat that depends on another stat.
	/// </summary>
	public Ulid DependentStatId { get; init; }

	/// <summary>
	///     The unique identifier for the linked stat. This is used to identify the stat that is being depended on.
	/// </summary>
	public Ulid LinkedStatId { get; init; }

	/// <summary>
	///     A simple ratio that defines what percentage of the linked stat's value is added to the dependent stat's value.
	/// </summary>
	public required float Ratio { get; init; }

	/// <summary>
	///     The dependent stat that this link refers to. This is the stat that depends on another stat for its value.
	/// </summary>
	[ForeignKey(nameof(DependentStatId))]
	public StatDefinition? DependentStat { get; init; }

	/// <summary>
	///     The linked stat that this link refers to. This is the stat that is being depended on by another stat.
	/// </summary>
	[ForeignKey(nameof(LinkedStatId))]
	public StatDefinition? LinkedStat { get; init; }
}
