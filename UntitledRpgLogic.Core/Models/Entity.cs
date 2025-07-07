using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an entity in the game world.
/// </summary>
public record Entity : IEntity
{
    /// <summary>
    ///     Creates an instance of <see cref="Entity" /> using the provided <see cref="Guid" />.
    /// </summary>
    /// <param name="guid"></param>
    public Entity(Guid guid)
    {
        // assign primary first
        Guid = guid;
        // derive get-only propertys
        Id = Convert.ToBase64String(guid.ToByteArray());
        ShortGuid = guid.ToString("N")[..8].ToUpperInvariant();

        // assign name
        Name = Name.Empty;
    }

    /// <summary>
    ///     Create a new entity with a new <see cref="Guid" />.
    /// </summary>
    public Entity() : this(Guid.NewGuid())
    {
    }

    /// <inheritdoc />
    public Name Name { get; }

    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }
}
