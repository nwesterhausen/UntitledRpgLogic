namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
/// </summary>
public interface IEffect : IHasGuid, IHasName
{
    /// <summary>
    ///     An immutable collection of the effect components that make up this effect.
    /// </summary>
    IReadOnlyCollection<IEffectComponent> Components { get; }

    /// <summary>
    ///     Retrieves an effect component of a specific type from this effect.
    /// </summary>
    /// <typeparam name="T">The type of the effect component to retrieve.</typeparam>
    /// <returns>The first component of the specified type, or null if not found.</returns>
    T? GetComponent<T>() where T : class, IEffectComponent
    {
        return GetComponents<T>().FirstOrDefault();
    }

    /// <summary>
    ///     Retrieves all effect components of a specific type from this effect.
    /// </summary>
    /// <typeparam name="T">The type of the effect components to retrieve.</typeparam>
    /// <returns>A collection of components of the specified type.</returns>
    IEnumerable<T> GetComponents<T>() where T : class, IEffectComponent
    {
        return Components.Where(x => x.GetType() == typeof(T)).Cast<T>();
    }

    /// <summary>
    ///     Retrieves an effect component by its GUID from this effect.
    /// </summary>
    /// <param name="guid">the guid for the effect to get</param>
    /// <returns>The component with the specific GUID or null if not found.</returns>
    IEffectComponent? GetComponent(Guid guid)
    {
        return Components.FirstOrDefault(x => x.Guid == guid);
    }

    /// <summary>
    ///     Checks if this effect has a component with a specific GUID.
    /// </summary>
    /// <param name="guid">the GUID of the effect</param>
    /// <returns>True if the component is part of this effect, false otherwise.</returns>
    bool HasComponent(Guid guid)
    {
        return Components.Any(x => x.Guid == guid);
    }
}
