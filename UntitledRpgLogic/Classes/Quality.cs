namespace UntitledRpgLogic.Classes;

/// <summary>
///     The quality of an object. Helps to describe how good or valuable an object is.
/// </summary>
public enum Quality
{
    /// <summary>
    ///     No quality modifier.
    /// </summary>
    None,

    /// <summary>
    ///     Common quality, the most basic and frequently found or created.
    /// </summary>
    Common,

    /// <summary>
    ///     Uncommon quality.
    /// </summary>
    Uncommon,

    /// <summary>
    ///     Rare quality.
    /// </summary>
    Rare,

    /// <summary>
    ///     Epic quality, a step above rare, a rarer-rare.
    /// </summary>
    Epic,

    /// <summary>
    ///     Legendary quality objects are great enough to have a story or legend surrounding them.
    /// </summary>
    Legendary,

    /// <summary>
    ///     Mythic quality objects would be a legendary item for the ages.
    /// </summary>
    Mythic,

    /// <summary>
    ///     Artifact quality objects are considered perfections of their kind.
    /// </summary>
    Artifact,

    /// <summary>
    ///     Unique quality, meaning there is only one of this item in existence, or it is a unique item that cannot be
    ///     replicated.
    /// </summary>
    Unique
}