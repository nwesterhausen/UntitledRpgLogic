using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a passage, door, cave entrance, or portal between two map positions.
/// </summary>
[Table("map_transitions")]
public record MapTransition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="MapTransition" /> record.
	/// </summary>
	public MapTransition()
	{
		this.Id = Ulid.NewUlid();
		this.TransitionTag = "Door";
	}

	/// <summary>
	///     The unique identifier for the transition.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     Foreign key referencing the originating map.
	/// </summary>
	public Ulid SourceMapId { get; init; }

	/// <summary>
	///     Navigation property to the originating map.
	/// </summary>
	[ForeignKey(nameof(SourceMapId))]
	public MapDefinition? SourceMap { get; init; }

	/// <summary>
	///     World X coordinate on the originating map.
	/// </summary>
	public float SourceX { get; init; }

	/// <summary>
	///     World Y coordinate on the originating map.
	/// </summary>
	public float SourceY { get; init; }

	/// <summary>
	///     Foreign key referencing the destination map.
	/// </summary>
	public Ulid TargetMapId { get; init; }

	/// <summary>
	///     Navigation property to the destination map.
	/// </summary>
	[ForeignKey(nameof(TargetMapId))]
	public MapDefinition? TargetMap { get; init; }

	/// <summary>
	///     Arrival X coordinate on the target map.
	/// </summary>
	public float TargetX { get; init; }

	/// <summary>
	///     Arrival Y coordinate on the target map.
	/// </summary>
	public float TargetY { get; init; }

	/// <summary>
	///     Semantic descriptor for the transition trigger (e.g., "Door", "CaveEntrance", "LadderUp").
	/// </summary>
	[MaxLength(128)]
	public string TransitionTag { get; init; }
}
