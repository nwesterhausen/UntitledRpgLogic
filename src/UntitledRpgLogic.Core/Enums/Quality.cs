namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     The quality of an object. Helps to describe how good or valuable an object is.
/// </summary>
public enum Quality
{
    /// <summary>
    ///     No quality modifier.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Common quality, the most basic and frequently found or created.
    /// </summary>
    Common = 1,

    /// <summary>
    ///     Uncommon quality.
    /// </summary>
    Uncommon = 2,

    /// <summary>
    ///     Rare quality.
    /// </summary>
    Rare = 3,

    /// <summary>
    ///     Epic quality, a step above rare, a rarer-rare.
    /// </summary>
    Epic = 4,

    /// <summary>
    ///     Legendary quality objects are great enough to have a story or legend surrounding them.
    /// </summary>
    Legendary = 5,

    /// <summary>
    ///     Mythic quality objects would be a legendary item for the ages.
    /// </summary>
    Mythic = 6,

    /// <summary>
    ///     Artifact quality objects are considered perfections of their kind.
    /// </summary>
    Artifact = 7,

    /// <summary>
    ///     Unique quality, meaning there is only one of this item in existence, or it is a unique item that cannot be
    ///     replicated.
    /// </summary>
    Unique = 10
}
