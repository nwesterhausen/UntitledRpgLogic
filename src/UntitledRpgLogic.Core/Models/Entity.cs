using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces;

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
	public Entity(Guid identifier)
	{
		// assign primary first
		this.Identifier = identifier;
		// derive get-only properties
		this.Id = Convert.ToBase64String(identifier.ToByteArray());
		this.ShortId = identifier.ToString("N")[..8].ToUpperInvariant();

		// assign name
		this.Name = Name.Empty;
	}

	/// <summary>
	///     Create a new entity with a new <see cref="Identifier" />.
	/// </summary>
	public Entity() : this(Guid.NewGuid())
	{
	}

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public Guid Identifier { get; }

	/// <inheritdoc />
	public string Id { get; }

	/// <inheritdoc />
	public string ShortId { get; }
}
