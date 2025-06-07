using UntitledRpgLogic.Enums;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that have a quality. Provides a way to access the quality of the object.
/// </summary>
public interface IHasQuality
{
    /// <summary>
    ///     Describes the quality of the object. This can be used to determine how good or valuable the object is.
    /// </summary>
    Quality Quality { get; }
}