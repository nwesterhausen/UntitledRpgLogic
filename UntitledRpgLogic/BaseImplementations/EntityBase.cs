using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Represents the base class for all entities in the RPG system using composition.
/// </summary>
public abstract class EntityBase : IEntity
{
    /// <summary>
    ///     Supports the GUID behavior for the entity, allowing it to have a unique identifier.
    /// </summary>
    private readonly HasGuidBase _guidBehavior = new GuidBehavior();

    /// <summary>
    ///     Supports the name behavior for the entity, allowing it to have a name.
    /// </summary>
    private IHasName _nameBehavior = new NameBehavior();

    /// <summary>
    ///     Creates a new instance of <see cref="EntityBase" /> with a new unique identifier.
    /// </summary>
    /// <param name="guid">Optionally provide a unique identifier to use.</param>
    protected EntityBase(Guid? guid = null)
    {
        if (guid != null) _guidBehavior.SetId(guid.Value);
    }

    /// <summary>
    ///     Gets or sets the name of the entity.
    /// </summary>
    public string Name
    {
        get => _nameBehavior.Name;
        internal set => _nameBehavior = new NameBehavior(value);
    }

    /// <inheritdoc />
    public Guid Guid => _guidBehavior.Guid;

    /// <inheritdoc />
    public string Id => _guidBehavior.Id;

    /// <inheritdoc />
    public string ShortGuid => _guidBehavior.ShortGuid;
}