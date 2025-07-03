using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.Extensions;

/// <summary>
///     Extensions for objects that implement <see cref="IHasName" /> to provide name retrieval functionality.
/// </summary>
public static class NameExtensions
{
    /// <summary>
    ///     Get the name of the object, either singular or plural based on the count.
    /// </summary>
    /// <param name="nameBehavior">the object that implements <see cref="IHasName" /></param>
    /// <param name="count">number of items</param>
    /// <returns>appropriate name for the count of objects</returns>
    public static string GetName(this IHasName nameBehavior, int count = 1)
    {
        return count == 1 ? nameBehavior.SingularName : nameBehavior.PluralName;
    }
}
