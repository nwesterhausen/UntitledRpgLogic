using UntitledRpgLogic.BaseImplementations;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <summary>
///     A generic behavior that allows a stat to be damageable.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DamageableBehavior<T> : DamageableBase<T>
    where T : StatBase
{
    /// <summary>
    ///     Constructs a new instance of the <see cref="DamageableBehavior{T}" /> class with the specified stat.
    /// </summary>
    /// <param name="stat"></param>
    public DamageableBehavior(T stat) : base(stat)
    {
    }
}