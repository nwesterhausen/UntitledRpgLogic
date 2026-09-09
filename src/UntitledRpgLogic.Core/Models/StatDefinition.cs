using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Database model for a definition of a stat. This is used to define properties of a stat like name, value range
///     and (if its dependent) which stat it depends on. This is not used when a stat belongs to an entity, but rather
///     defines how those stats would behave.
/// </summary>
[Table("stat_definitions")]
public record StatDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="StatDefinition" /> class with default values.
	/// </summary>
	public StatDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Variation = StatVariation.Major;
		this.HasChangeableValue = true;
		this.MinValue = DefaultValues.StatDefaultMinValue;
		this.MaxValue = DefaultValues.StatDefaultMaxValue;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="StatDefinition" /> class with the specified name.
	/// </summary>
	/// <param name="name">The name of the stat.</param>
	[SetsRequiredMembers]
	public StatDefinition(Name name) : this()
	{
		this.Name = name;
		this.Variation = StatVariation.Pseudo;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="StatDefinition" /> class with the specified name.
	/// </summary>
	/// <param name="id">The ULID to identify this stat definition.</param>
	/// <param name="name">The name of the stat.</param>
	[SetsRequiredMembers]
	public StatDefinition(Ulid id, Name name) : this()
	{
		this.Id = id;
		this.Name = name;
		this.Variation = StatVariation.Pseudo;
	}

	/// <summary>
	///     The ULID for the stat. Any instances of this stat refer to this definition via this ID.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The name of the stat. This is used to identify the stat in the game and is used in the UI.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     Whether the stat is able to be directly changed or not.
	/// </summary>
	public bool HasChangeableValue { get; init; }

	/// <summary>
	///     The minimum value for this stat. This is the lowest value the stat can have.
	/// </summary>
	public int MinValue { get; init; }

	/// <summary>
	///     The maximum value for this stat. This is the highest value the stat can have.
	/// </summary>
	public int MaxValue { get; init; }

	/// <summary>
	///     The type of stat this is. This is used to determine how the stat behaves in the game.
	/// </summary>
	public required StatVariation Variation { get; init; }

	/// <summary>
	///     Stats that this stat depends on (if any).
	/// </summary>
	public ICollection<LinkedStats> LinkedStats { get; init; } = [];
}
