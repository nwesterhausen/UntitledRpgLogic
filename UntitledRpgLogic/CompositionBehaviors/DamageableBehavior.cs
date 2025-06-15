using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <summary>
///     A generic behavior that allows a stat to be damageable.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DamageableBehavior<T> : IDamageable
    where T : IStat
{
    /// <summary>
    ///     The stat that is being damaged.
    /// </summary>
    private readonly T _stat;

    /// <summary>
    ///     Internal field to store the current damage applied to the stat in points.
    /// </summary>
    private int _currentDamage;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DamageableBehavior{T}" /> class with the specified stat.
    /// </summary>
    /// <param name="stat"></param>
    public DamageableBehavior(T stat)
    {
        _stat = stat;
    }

    /// <inheritdoc />
    public int CurrentDamage
    {
        get => _currentDamage;
        private set
        {
            if (_currentDamage == value) return;
            int previousDamage = _currentDamage;
            if (value < 0)
            {
#if DEBUG
                throw new ArgumentOutOfRangeException(nameof(value), "Damage cannot be negative.");
#endif
                value = 0;
            }

            _currentDamage = value;

            // Clamp the current damage within the stats minimum and maximum values
            if (_currentDamage > _stat.MaxValue)
                _currentDamage = _stat.MaxValue;
            else if (_currentDamage < _stat.MinValue) _currentDamage = _stat.MinValue;

            // update the percentage damage whenever the current damage changes
            CurrentPercentageDamage = _stat.MaxValue > 0 ? CurrentDamage / (float)_stat.MaxValue * 100 : 0;
            // raise the DamageTaken event with the amount of damage taken
            DamageTaken?.Invoke(this, _currentDamage - previousDamage);
        }
    }

    /// <inheritdoc />
    public float CurrentPercentageDamage { get; private set; }


    /// <inheritdoc />
    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;

        // Apply the damage to the current damage
        CurrentDamage += amount;
    }

    /// <inheritdoc />
    public event EventHandler<int>? DamageTaken;
}
