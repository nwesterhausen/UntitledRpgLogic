using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Environment;
using UntitledRpgLogic.Core.World.Generation.GridMaps;

namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Root database entity representing a discrete 2D game map, dungeon level, or building interior.
/// </summary>
[Table("map_definitions")]
public record MapDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="MapDefinition" /> record.
	/// </summary>
	[SetsRequiredMembers]
	public MapDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Description = string.Empty;
		this.Type = MapType.Overworld;
		this.Atmosphere = AtmosphereProfile.StandardDefault;
		this.BaselineAmbients = new List<AmbientValue>();
		this.Chunks = new List<WorldChunk>();
		this.Transitions = new List<MapTransition>();
		this.OreDeposits = new List<OreDepositDefinition>();
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="MapDefinition" /> record with a name.
	/// </summary>
	/// <param name="name">The name of the map.</param>
	[SetsRequiredMembers]
	public MapDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     The localized display name of the map.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     Descriptive overview or lore notes for this area.
	/// </summary>
	[MaxLength(512)]
	public string Description { get; init; }

	/// <summary>
	///     The map classification type.
	/// </summary>
	public MapType Type { get; init; }

	/// <summary>
	///     Baseline atmospheric gas profile across this entire map.
	/// </summary>
	public AtmosphereProfile Atmosphere { get; init; }

	/// <summary>
	///     Baseline ambient climate values (e.g., Temperature, Gravity, Humidity).
	/// </summary>
	public ICollection<AmbientValue> BaselineAmbients { get; }

	/// <summary>
	///     Navigation property to all chunks persisted for this map.
	/// </summary>
	public virtual ICollection<WorldChunk> Chunks { get; }

	/// <summary>
	///     Transition points (doors, stairs, portals) originating on this map.
	/// </summary>
	public virtual ICollection<MapTransition> Transitions { get; }

	/// <summary>
	///     List of ore deposits that may be referenced by the <see cref="OreDepositMap" />.
	/// </summary>
	public ICollection<OreDepositDefinition> OreDeposits { get; }

	/// <summary>
	///     The primary random seed used for deterministic procedural generation.
	/// </summary>
	public uint Seed { get; init; }

	/// <summary>
	///     The procedural generator settings and macro recipe for this map.
	///     Null for handcrafted static interiors or non-procedural maps.
	/// </summary>
	public WorldMapConfiguration? GenerationConfig { get; init; }

	/// <summary>
	///     The unique identifier for the map.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; } = Ulid.NewUlid();
}
