namespace UntitledRpgLogic.Enums;

/// <summary>
///     Represents detail about what kind of stat this is.
/// </summary>
public enum StatVariation
{
    /// <summary>
    ///     Major stats are the primary stat which likely influence many other things. Players should have some agency
    ///     over these stats.
    /// </summary>
    Major = 5,

    /// <summary>
    ///     Minor stats derive value from one or more major stats
    /// </summary>
    Minor = 6,

    /// <summary>
    ///     Represents a fake stat or a stat which is completely contrived
    /// </summary>
    Pseudo = 0,

    /// <summary>
    ///     Represents a stat which is a complex calculation or derived from multiple other stats.
    /// </summary>
    Complex = 3
}