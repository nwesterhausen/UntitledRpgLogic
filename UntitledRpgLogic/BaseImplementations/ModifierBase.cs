using UntitledRpgLogic.Extensions;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <inheritdoc />
public abstract class ModifierBase : IModifier
{
    /// <summary>
    ///     Internal reference value for the duration, used to reset the duration when refreshing.
    /// </summary>
    private readonly float _durationReferenceValue;

    /// <summary>
    ///     Internal field to track the current number of stacks for this modification.
    /// </summary>
    private int _currentStacks;

    /// <summary>
    ///     Internal field to track the duration in seconds for this modification.
    /// </summary>
    private float _duration;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ModifierBase" /> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure this modification.</param>
    protected ModifierBase(ModifiableOptions options)
    {
        IsPermanent = options.IsPermanent ?? true;
        IsPositive = options.IsPositive ?? true;
        IsAdditive = options.IsAdditive ?? false;
        IsPercentage = options.IsPercentage ?? false;
        ScalesOnBaseValue = options.ScalesOnBaseValue ?? false;
        Amount = options.Amount ?? 0f;
        MaxStacks = options.MaxStacks ?? 1;
        CurrentStacks = options.CurrentStacks ?? 0;
        StackEffect = options.StackEffect;
        Duration = options.Duration ?? 0f;
        LoseAllStacksOnExpiration = options.LoseAllStacksOnExpiration ?? false;
        ModificationPriority = options.ModificationPriority ?? 0;

        RecalculateEffectiveAmount();
        _durationReferenceValue = Duration;

#if DEBUG
        if (MaxStacks < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(MaxStacks), "MaxStacks must be at least 1.");
        }

        if (MaxStacks > 1 && StackEffect == null)
        {
            throw new ArgumentNullException(nameof(StackEffect), "StackEffect must be defined if MaxStacks is greater than 1.");
        }
        
        if (!IsPermanent && Duration < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Duration), "Duration must be -1 for permanent modifications or a positive value for temporary modifications.");
        }
#endif
    }

    /// <inheritdoc />
    public bool IsPermanent { get; }

    /// <inheritdoc />
    public bool IsPositive { get; }

    /// <inheritdoc />
    public bool IsAdditive { get; }

    /// <inheritdoc />
    public bool IsPercentage { get; }

    /// <inheritdoc />
    public bool ScalesOnBaseValue { get; }

    /// <inheritdoc />
    public float Amount { get; }

    /// <summary>
    ///     The effective amount of the modification after considering stacks and duration.
    /// </summary>
    public float EffectiveAmount { get; private set; }

    /// <inheritdoc />
    public string Display => this.ToDisplay();

    /// <inheritdoc />
    public int MaxStacks { get; }

    /// <inheritdoc />
    public int CurrentStacks
    {
        get => _currentStacks;
        private set
        {
            if (_currentStacks == value) return;
            var oldStacks = _currentStacks;
            if (value < 0)
            {
#if DEBUG
                throw new ArgumentOutOfRangeException(nameof(value), "CurrentStacks cannot be negative.");
#endif
                value = 0;
            }

            if (value > MaxStacks)
            {
#if DEBUG
                throw new ArgumentOutOfRangeException(nameof(value), $"CurrentStacks cannot exceed MaxStacks ({MaxStacks}).");
#endif
                value = MaxStacks;
            }

            _currentStacks = value;

            RecalculateEffectiveAmount();

            if (_currentStacks <= 0)
                // If stacks reach zero, trigger the expiration event
                ModificationExpired?.Invoke(this, EventArgs.Empty);

            StacksChanged?.Invoke(this, _currentStacks - oldStacks);
        }
    }

    /// <inheritdoc />
    public void AddStack(int amount = 1)
    {
        CurrentStacks += amount;
    }

    /// <inheritdoc />
    public void RemoveStack(int amount = 1)
    {
        CurrentStacks -= amount;
    }

    /// <inheritdoc />
    public ModificationStackEffect? StackEffect { get; }

    /// <inheritdoc />
    public float Duration
    {
        get => _duration;
        private set
        {
            if (Math.Abs(_duration - value) < 0.0001f) return;
            _duration = value < 0 ? 0 : value;
            DurationChanged?.Invoke(this, _duration);
        }
    }

    /// <inheritdoc />
    public bool LoseAllStacksOnExpiration { get; }

    /// <inheritdoc />
    public void ProcessDeltaTime(float deltaTime)
    {
        if (Duration <= 0 || IsPermanent) return; // No processing needed for permanent or non-temporary modifications

        Duration -= deltaTime;
        if (Duration < 0) Duration = 0;
    }

    /// <inheritdoc />
    public int ModificationPriority { get; }

    /// <inheritdoc />
    public int ApplyModification(int baseValue, int currentValue)
    {
        if (ScalesOnBaseValue)
            return currentValue + (IsPercentage
                ? (int)(baseValue * EffectiveAmount)
                : (int)EffectiveAmount);

        return currentValue + (IsPercentage
            ? (int)(currentValue * EffectiveAmount)
            : (int)EffectiveAmount);
    }

    /// <inheritdoc />
    public void RefreshDuration()
    {
        Duration = _durationReferenceValue;
    }

    /// <inheritdoc />
    public event EventHandler? ModificationExpired;

    /// <inheritdoc />
    public event EventHandler<int>? StacksChanged;

    /// <inheritdoc />
    public event EventHandler<float>? DurationChanged;

    /// <inheritdoc />
    public event EventHandler<int>? ModificationApplied;

    /// <summary>
    ///     Recalculates the effective amount of this modification based on the current stacks, amount and duration.
    /// </summary>
    private void RecalculateEffectiveAmount()
    {
        // placeholder calculation. Really is affected by stacks, the stack effect, and all that.
        EffectiveAmount = CurrentStacks * Amount;
    }
}