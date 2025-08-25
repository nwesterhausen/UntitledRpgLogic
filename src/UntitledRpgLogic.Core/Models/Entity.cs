using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces.Entities;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an entity in the game world.
/// </summary>
public record Entity : IEntity
{
	/// <summary>
	///     Creates an instance of <see cref="Entity" /> using the provided <see cref="Identifier" />.
	/// </summary>
	/// <param name="identifier"></param>
	public Entity(Ulid identifier)
	{
		// assign primary first
		this.Identifier = identifier;

		// assign name
		this.Name = Name.Empty;
	}

	/// <inheritdoc />
	public Name Name { get; init; }

	/// <inheritdoc />
	[Key]
	public Ulid Identifier { get; init; } = Ulid.NewUlid();
}
