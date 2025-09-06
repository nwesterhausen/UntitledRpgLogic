using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Minimal material definition for persistence. Instances of items can reference materials by this ULID.
///     Additional physical properties can be modeled later as separate tables or owned types.
/// </summary>
public record MaterialDefinition: IDbEntity<Ulid>
{
	/// <summary>
	///     Constructs a new <see cref="MaterialDefinition" /> with an empty name and default values. (for EF use)
	/// </summary>
	public MaterialDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
	}

	/// <summary>
	///     Constructs a new <see cref="MaterialDefinition" /> with the specified name.
	/// </summary>
	/// <param name="name">The display name for the material.</param>
	public MaterialDefinition(Name name)
	{
		this.Id = Ulid.NewUlid();
		this.Name = name;
	}

	/// <summary>
	///     Primary key for the material definition.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     Display name for the material.
	/// </summary>
	public required Name Name { get; init; }
}
