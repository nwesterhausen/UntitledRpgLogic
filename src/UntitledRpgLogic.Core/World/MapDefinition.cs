using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Environment;

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
	public MapDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Description = string.Empty;
		this.Type = MapType.Overworld;
		this.Atmosphere = AtmosphereProfile.StandardDefault;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="MapDefinition" /> record with a name.
	/// </summary>
	/// <param name="name">The name of the map.</param>
	public MapDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     The unique identifier for the map.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

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
	public ICollection<AmbientValue> BaselineAmbients { get; init; } = [];

	/// <summary>
	///     Navigation property to all chunks persisted for this map.
	/// </summary>
	public virtual ICollection<WorldChunk> Chunks { get; } = new List<WorldChunk>();

	/// <summary>
	///     Transition points (doors, stairs, portals) originating on this map.
	/// </summary>
	public virtual ICollection<MapTransition> Transitions { get; } = new List<MapTransition>();
}
