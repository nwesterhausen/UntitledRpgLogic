using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an entity's stat, defined by a StatDefinition, with a base value and an apparent value.
/// </summary>
public class InstancedStat: IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedSkill" /> class with default values.
	///     This constructor sets the stat instance ID to a new <see cref="Ulid" />, initializes base and apparent value to 0,
	///     and sets the stat definition ID to an empty <see cref="Ulid" />.
	/// </summary>
	public InstancedStat()
	{
		this.Id = Ulid.NewUlid();
		this.StatDefinitionId = Ulid.Empty;
		this.BaseValue = 0;
		this.ApparentValue = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedStat" /> class with the specified stat definition ID.
	///     This constructor sets the stat instance ID to a new <see cref="Ulid" />, initializes base and apparent value to 0,
	///     and links the stat instance to the provided stat definition.
	/// </summary>
	/// <param name="statDefinitionId">The unique identifier of the stat definition this instance is based on.</param>
	public InstancedStat(Ulid statDefinitionId)
	{
		this.Id = Ulid.NewUlid();
		this.StatDefinitionId = statDefinitionId;
		this.BaseValue = 0;
		this.ApparentValue = 0;
	}

	/// <summary>
	///     The unique identifier for the instanced stat. This is used to identify the stat in the database and in the game.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     The unique identifier for the stat definition that this instanced stat is based on. This links the instanced stat
	///     to its definition.
	/// </summary>
	public Ulid StatDefinitionId { get; init; }

	/// <summary>
	///     The base value of the stat, which is the raw value before any modifications or effects are applied.
	/// </summary>
	public int BaseValue { get; init; }

	/// <summary>
	///     The apparent value of the stat, which may differ from the base value due to modifications, buffs, or debuffs.
	/// </summary>
	public int ApparentValue { get; init; }

	/// <summary>
	///     The stat definition that this instanced stat is based on. This provides the properties and behavior of the stat.
	/// </summary>
	[ForeignKey(nameof(StatDefinitionId))]
	public StatDefinition? StatDefinition { get; init; }
}
