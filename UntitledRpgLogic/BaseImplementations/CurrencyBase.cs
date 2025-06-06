using UntitledRpgLogic.Classes;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <inheritdoc />
public abstract class CurrencyBase : ICurrency
{
    /// <summary>
    ///     Internal field to store the amount of this currency.
    /// </summary>
    private int _amount;

    /// <summary>
    ///     Constructor for the CurrencyBase class.
    /// </summary>
    /// <param name="options"></param>
    protected CurrencyBase(CurrencyOptions options)
    {
        PluralName = options.PluralName == null
            ? new PluralName(options.Name)
            : new PluralName(options.Name, options.PluralName);

        Value = options.Value;

        Material = options.Material;
    }

    /// <inheritdoc />
    public PluralName PluralName { get; }

    /// <inheritdoc />
    public long Value { get; }

    /// <inheritdoc />
    public long GetTotalValue()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string TooltipString => $"{Amount} {PluralName.GetName(Amount)}";

    /// <inheritdoc />
    public string TooltipDescription => TooltipString;

    /// <inheritdoc />
    public int Amount
    {
        get => _amount;
        private set
        {
            if (_amount == value) return;
            if (value < 0)
            {
#if DEBUG
                throw new ArgumentOutOfRangeException(nameof(value), "Amount cannot be negative.");
#endif
                value = 0;
            }

            _amount = value;
        }
    }

    /// <inheritdoc />
    public IMaterial Material { get; }

    /// <inheritdoc />
    public long Add(long value)
    {
        switch (value)
        {
            case < 0:
                return Subtract(-value);
            case 0:
                return 0;
        }

        // how many currency is in this value
        var canAdd = value / Value;
        // if we cannot add any, return the value
        if (canAdd <= 0) return value;

        var remainingValue = value;
        while (canAdd > 0)
        {
            // add one to the amount
            Amount++;
            // subtract the value of this currency from the value
            remainingValue -= Value;
            // decrease how many we can add
            canAdd--;
        }

        return remainingValue;
    }

    /// <inheritdoc />
    public long Subtract(long value)
    {
        switch (value)
        {
            case < 0:
                return Add(-value);
            case 0:
                return 0;
        }

        // how many currency is in this value
        var canSubtract = value / Value;
        // if we cannot subtract any, return the value
        if (canSubtract <= 0) return value;

        var remainingValue = value;
        var workingAmount = Amount;
        // if we can subtract more than we have, subtract all we have
        while (canSubtract > 0)
        {
            if (workingAmount <= 0) break;

            // subtract one from the amount
            workingAmount--;
            // subtract the value of this currency from the remaining value
            remainingValue -= Value;
            // decrease how many we can subtract
            canSubtract--;
        }

        if (remainingValue < 0)
        {
#if DEBUG
                throw new InvalidOperationException("Remaining value cannot be negative after subtraction. Tried to subtract {0} from {1}", value, GetTotalValue());
#endif
            // If we somehow ended up with a negative remaining value, just return the original value and make not changes
            return value;
        }

        // return any remaining value that could not be subtracted (I think this only returns 0, but just in case)
        Amount = workingAmount;
        return remainingValue;
    }


    /// <inheritdoc />
    string IHasName.Name => PluralName.Singular;
}