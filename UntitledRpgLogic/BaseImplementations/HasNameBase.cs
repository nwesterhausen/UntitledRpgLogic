using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Provides a base implementation for classes that have a name.
///     Prefer to use this instead of implementing <see cref="IHasName" /> directly.
/// </summary>
public abstract class HasNameBase : IHasName
{
    /// <summary>
    ///     Maximum length of supported name
    /// </summary>
    private const int MaxNameLength = 255;

    /// <summary>
    ///     Name of the object. Must not be null or longer than what's allowed by <see cref="MaxNameLength" />.
    /// </summary>
    private string _name = string.Empty;

    /// <summary>
    ///     The name of the object. If setting a new name, it must not be null or longer than the maximum length defined by
    ///     <see cref="MaxNameLength" />.
    /// </summary>
    /// <exception cref="ArgumentNullException">Attempted to set a null value to the name</exception>
    /// <exception cref="ArgumentException">Attempted to set a name that was too long</exception>
    public string Name
    {
        get => _name;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Name cannot be null.");
            if (value.Length > MaxNameLength)
                throw new ArgumentException($"Name cannot be longer than {MaxNameLength} characters.", nameof(value));
            _name = value;
        }
    }
}