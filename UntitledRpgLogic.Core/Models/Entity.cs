using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an entity in the game world.
/// </summary>
public record Entity : IEntity
{
    public Entity(Guid guid)
    {
        // assign primary first
        Guid = guid;
        // derive get-only propertys
        Id = Convert.ToBase64String(guid.ToByteArray());
        ShortGuid = guid.ToString("N")[..8].ToUpperInvariant();
    }

    public Entity() : this(Guid.NewGuid())
    {
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }

    /// <inheritdoc />
    public string PluralName { get; }

    /// <inheritdoc />
    public string NameAsAdjective { get; }
}
