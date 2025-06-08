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
        MaxStacks = options.MaxStacks ?? 1;
        CurrentStacks = options.CurrentStacks ?? 0;
        Duration = options.Duration ?? 0f;
        LoseAllStacksOnExpiration = options.LoseAllStacksOnExpiration ?? false;
        Priority = options.ModificationPriority ?? 0;

        _durationReferenceValue = Duration;

#if DEBUG
        if (MaxStacks < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(MaxStacks), "MaxStacks must be at least 1.");
        }

        if (MaxStacks > 1 && StackEffects == null)
        {
            throw new ArgumentNullException(nameof(StackEffects), "StackEffect must be defined if MaxStacks is greater than 1.");
        }
        
        if (!IsPermanent && Duration < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Duration), "Duration must be -1 for permanent modifications or a positive value for temporary modifications.");
        }
#endif
    }

    /// <inheritdoc />
    public bool IsPermanent { get; }

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
            if (IsPermanent && _currentStacks == 1 && value <= 1)
                // Permanent modifiers can't go below 1 stack.
                return;

            // Short-circuit if the value hasn't changed
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

            if (_currentStacks <= 0 && !IsPermanent)
                // If stacks reach zero, trigger the expiration event
                ModificationExpired?.Invoke(this, EventArgs.Empty);

            StacksChanged?.Invoke(this, _currentStacks - oldStacks);
        }
    }

    /// <inheritdoc />
    public IModifierEffect? ModificationEffect { get; } = null;

    /// <inheritdoc />
    public IEnumerable<IModifierEffect>? StackEffects { get; } = null;

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
        // No processing needed for permanent modifiers at 1 stack. Or if duration is zero or less.
        if (Duration <= 0 || (IsPermanent && MaxStacks == 1)) return;

        Duration -= deltaTime;
    }

    /// <inheritdoc />
    public int Priority { get; }

    /// <inheritdoc />
    public int ApplyModification(int baseValue, int currentValue, int maxValue)
    {
        var updatedValue = currentValue;
        if (ModificationEffect != null)
            updatedValue = ModificationEffect.ApplyEffect(baseValue, currentValue, maxValue);

        if (CurrentStacks > 0 && StackEffects != null)
            foreach (var effect in StackEffects.OrderBy(e => e.Priority))
                updatedValue = effect.ApplyEffect(baseValue, updatedValue, maxValue);

        EffectiveAmount = updatedValue - baseValue;
        return updatedValue;
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
}