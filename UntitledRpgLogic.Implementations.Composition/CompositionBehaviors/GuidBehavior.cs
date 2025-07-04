namespace UntitledRpgLogic.CompositionBehaviors;

/// <inheritdoc />
public class GuidBehavior : IHasGuid
{
    /// <summary>
    ///     Internal field to store the unique identifier for this object.
    /// </summary>
    private Guid _guid;

    /// <summary>
    ///     Create a new instance for tracking a unique identifier for an object.
    /// </summary>
    /// <param name="guid"></param>
    public GuidBehavior(Guid? guid = null)
    {
        Guid = guid ?? Guid.NewGuid();
    }

    /// <inheritdoc />
    public Guid Guid
    {
        get => _guid;
        private set
        {
            _guid = value;
            Id = Convert.ToBase64String(_guid.ToByteArray());
            ShortGuid = _guid.ToString("N")[..8].ToUpperInvariant();
        }
    }

    /// <inheritdoc />
    public string Id { get; private set; } = string.Empty;

    /// <inheritdoc />
    public string ShortGuid { get; private set; } = string.Empty;

    /// <summary>
    ///     Overwrite the unique identifier for this object.
    /// </summary>
    /// <param name="guid">guid to set for this object</param>
    protected internal void SetId(Guid guid)
    {
        Guid = guid;
    }
}
