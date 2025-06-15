namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Used to mark a Stat as being able to take damage.
/// </summary>
public interface IDamageable
{
    /// <summary>
    ///     The current damage applied to the stat in points.
    /// </summary>
    public int CurrentDamage { get; }

    /// <summary>
    ///     The current damage applied to the stat as a percentage of its maximum value.
    /// </summary>
    public float CurrentPercentageDamage { get; }

    /// <summary>
    ///     Apply damage to the stat, reducing its value by the specified amount.
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount);

    /// <summary>
    ///     Event that is raised when damage is applied to the stat. The event should notify of the total amount of damage
    ///     taken.
    /// </summary>
    event EventHandler<int> DamageTaken;
}
