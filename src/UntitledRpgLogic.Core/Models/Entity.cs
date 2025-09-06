using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Core.Interfaces.Entities;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an entity in the game world.
/// </summary>
public record Entity : IEntity
{
	/// <summary>
	///     Creates an instance of <see cref="Entity" /> using the provided <see cref="Id" />.
	/// </summary>
	/// <param name="identifier"></param>
	public Entity(Ulid identifier)
	{
		// assign primary first
		this.Id = identifier;

		// assign name
		this.Name = Name.Empty;
	}

	/// <summary>
	///     Creates an instance of <see cref="Entity" /> using the provided <see cref="Name" />.
	/// </summary>
	/// <param name="name"></param>
	public Entity(Name name)
	{
		// assign primary first
		this.Id = Ulid.NewUlid();

		// assign name
		this.Name = name;
	}

	/// <summary>
	///     Creates an instance of <see cref="Entity" /> with a new <see cref="Id" /> and an empty <see cref="Name" />.
	/// </summary>
	public Entity()
	{
		// assign primary first
		this.Id = Ulid.NewUlid();

		// assign name
		this.Name = Name.Empty;
	}

	/// <inheritdoc />
	public Name Name { get; init; }

	/// <inheritdoc />
	[Key]
	public Ulid Id { get; init; } = Ulid.NewUlid();

	/// <summary>
	///     Gets or sets the entity's inventory.
	/// </summary>
	public virtual EntityInventory? Inventory { get; set; }

	/// <summary>
	///     Gets or sets the entity's collection of skills.
	/// </summary>
	public virtual EntitySkills? Skills { get; set; }

	/// <summary>
	///     Gets or sets the entity's collection of stats.
	/// </summary>
	public virtual EntityStats? Stats { get; set; }
}
