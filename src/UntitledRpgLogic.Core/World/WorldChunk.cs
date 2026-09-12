using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Environment;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Represents an active or persisted 2D terrain sub-grid belonging to a <see cref="MapDefinition" />.
/// </summary>
[Table("world_chunks")]
public record WorldChunk : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="WorldChunk" /> record.
	/// </summary>
	public WorldChunk() => this.Id = Ulid.NewUlid();

	/// <summary>
	///     Foreign key referencing the parent map definition.
	/// </summary>
	public Ulid MapId { get; init; }

	/// <summary>
	///     Navigation property to the parent map.
	/// </summary>
	[ForeignKey(nameof(MapId))]
	public MapDefinition? Map { get; init; }

	/// <summary>
	///     Horizontal grid coordinate of the chunk.
	/// </summary>
	public int ChunkX { get; init; }

	/// <summary>
	///     Vertical grid coordinate of the chunk.
	/// </summary>
	public int ChunkY { get; init; }

	/// <summary>
	///     Local palette mapping 1-byte indices on <see cref="Tile2D" /> to global <see cref="MaterialDefinition" /> ULIDs.
	/// </summary>
	public ICollection<Ulid> MaterialPalette { get; init; } = [];

	/// <summary>
	///     Optional local atmospheric override (null inherits the parent <see cref="MapDefinition.Atmosphere" />).
	/// </summary>
	public AtmosphereProfile? AtmosphereOverride { get; set; }

	/// <summary>
	///     Dynamic local ambient deviations (e.g., heat from ongoing fires or regional spell blights).
	/// </summary>
	public ICollection<AmbientValue> AmbientOverrides { get; init; } = [];

	/// <summary>
	///     Compressed Brotli byte payload containing the 16x16 <see cref="Tile2D" /> array.
	/// </summary>
	public byte[] CompressedTileBlob { get; set; } = [];

	/// <summary>
	///     Optimistic concurrency and sync version, incremented on terrain mutation.
	/// </summary>
	public uint Version { get; set; } = 1;

	/// <summary>
	///     Indicates unsaved in-memory mutations during simulation ticks.
	/// </summary>
	[NotMapped]
	public bool IsDirty { get; set; }

	/// <summary>
	///     Indicates whether cellular automata (water flow, fire spread) are active in this chunk.
	/// </summary>
	[NotMapped]
	public bool HasActiveSimulation { get; set; }

	/// <summary>
	///     The unique identifier for the chunk.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }
}
