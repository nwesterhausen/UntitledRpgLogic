using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Core.Elements;

/// <summary>
///     Root database catalog model representing an elemental discipline or force in the RPG logic
///     (e.g., Fire, Cold, Lightning, Aether).
/// </summary>
[Table("elements")]
public record Element : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="Element" /> record with default values for EF Core.
	/// </summary>
	public Element()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Description = string.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Element" /> record with a designated name.
	/// </summary>
	/// <param name="name">The name of the element.</param>
	public Element(Name name) : this() => this.Name = name;

	/// <summary>
	///     Initializes a new instance of the <see cref="Element" /> record with a designated name and description.
	/// </summary>
	/// <param name="name">The name of the element.</param>
	/// <param name="description">Flavor text detailing the element's planar origin or behavior.</param>
	public Element(Name name, string description) : this(name) => this.Description = description;

	/// <summary>
	///     The unique catalog identifier for the element. Can be assigned explicitly when loading from config archives.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The display name of the element.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     A brief description or lore snippet regarding the element.
	/// </summary>
	[MaxLength(1024)]
	public string Description { get; init; }
}
