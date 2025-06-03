using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <inheritdoc />
public abstract class DamageableBase<T> : IDamageable<T>
    where T : StatBase
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
    ///     Initializes a new instance of the <see cref="DamageableBase{T}" /> class with the specified stat.
    /// </summary>
    /// <param name="stat"></param>
    protected DamageableBase(T stat)
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
            var previousDamage = _currentDamage;
            if (value < 0)
            {
#if DEBUG
                throw new ArgumentOutOfRangeException(nameof(value), "Damage cannot be negative.");
#endif
                value = 0;
            }

            _currentDamage = value;
            // update the percentage damage whenever the current damage changes
            CurrentPercentageDamage = _stat.MaxValue > 0 ? CurrentDamage / (float)_stat.MaxValue * 100 : 0;
            // raise the DamageTaken event with the amount of damage taken
            DamageTaken?.Invoke(this, _currentDamage - previousDamage);
        }
    }

    /// <inheritdoc />
    public float CurrentPercentageDamage { get; private set; }


    /// <inheritdoc />
    public void TakeDamage(DamageOptions damageOptions)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public event EventHandler<int>? DamageTaken;
}