using UntitledRpgLogic.CompositionBehaviors;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Represents the base class for all entities in the RPG system using composition.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    ///     Supports the GUID behavior for the entity, allowing it to have a unique identifier.
    /// </summary>
    private readonly HasGuidBase _guidBehavior = new GuidBehavior();

    /// <summary>
    ///     Supports the name behavior for the entity, allowing it to have a name.
    /// </summary>
    private readonly HasMonoNameBase _monoNameBehavior = new MonoNameBehavior();

    /// <summary>
    ///     Creates a new instance of <see cref="EntityBase" /> with a new unique identifier.
    /// </summary>
    /// <param name="id">Optionally provide a unique identifier to use.</param>
    protected EntityBase(Guid? id = null)
    {
        if (id != null) _guidBehavior.SetId(id.Value);
    }

    /// <summary>
    ///     Gets or sets the name of the entity.
    /// </summary>
    public string Name
    {
        get => _monoNameBehavior.Name;
        internal set => _monoNameBehavior.Name = value;
    }

    /// <summary>
    ///     Gets the unique identifier of the entity.
    /// </summary>
    public Guid Id => _guidBehavior.Id;
}