namespace UntitledRpgLogic.Stat;

public abstract partial class StatBase
{
    /// <summary>
    ///     Compares the value of this stat to an integer.
    /// </summary>
    /// <param name="other">The integer value to compare to.</param>
    /// <returns>An integer indicating the relative order.</returns>
    public int CompareTo(int other)
    {
        if (Value == other) return 0;

        if (Value < other) return -1;

        return 1;
    }

    /// <summary>
    ///     Compares this stat to another stat.
    /// </summary>
    /// <param name="other">The other stat to compare to.</param>
    /// <returns>An integer indicating the relative order.</returns>
    public int CompareTo(StatBase? other)
    {
        if (ReferenceEquals(this, other)) return 0;

        if (other is null) return 1;

        var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
        if (nameComparison != 0) return nameComparison;

        var valueComparison = Value.CompareTo(other.Value);
        if (valueComparison != 0) return valueComparison;

        var maxValueComparison = MaxValue.CompareTo(other.MaxValue);
        if (maxValueComparison != 0) return maxValueComparison;

        return MinValue.CompareTo(other.MinValue);
    }

    /// <summary>
    ///     Explicitly converts a stat to its integer value.
    /// </summary>
    /// <param name="stat">The stat to convert.</param>
    public static explicit operator int(StatBase stat)
    {
        return stat.Value;
    }

    /// <summary>
    ///     Explicitly converts a stat to its string representation.
    /// </summary>
    /// <param name="stat">The stat to convert.</param>
    public static explicit operator string(StatBase stat)
    {
        if (stat.MinValue == STAT_DEFAULT_MIN_VALUE)
            return
                $"{stat.Variation} {stat.Name}: {stat.Value} / {stat.MaxValue} ({stat.Value / (float)stat.MaxValue:F2 * 100}";

        return
            $"{stat.Variation} {stat.Name}: {stat.Value} / {stat.MaxValue} with {stat.MinValue} minimum ({stat.EffectiveValue:F2})%";
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return (string)this;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        if (ReferenceEquals(obj, null)) return false;

        if (obj.GetType() != GetType()) return false;

        var other = (StatBase)obj;
        return Variation == other.Variation &&
               Name == other.Name &&
               MaxValue == other.MaxValue &&
               MinValue == other.MinValue;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + Variation.GetHashCode();
            hash = hash * 23 + (Name?.GetHashCode() ?? 0);
            hash = hash * 23 + MaxValue.GetHashCode();
            hash = hash * 23 + MinValue.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    ///     Determines whether two stats are equal.
    /// </summary>
    public static bool operator ==(StatBase left, StatBase right)
    {
        if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);

        return left.Equals(right);
    }

    /// <summary>
    ///     Determines whether two stats are not equal.
    /// </summary>
    public static bool operator !=(StatBase left, StatBase right)
    {
        return !(left == right);
    }

    /// <summary>
    ///     Determines whether one stat is less than another.
    /// </summary>
    public static bool operator <(StatBase left, StatBase right)
    {
        return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
    }

    /// <summary>
    ///     Determines whether one stat is less than or equal to another.
    /// </summary>
    public static bool operator <=(StatBase left, StatBase right)
    {
        return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
    }

    /// <summary>
    ///     Determines whether one stat is greater than another.
    /// </summary>
    public static bool operator >(StatBase left, StatBase right)
    {
        return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
    }

    /// <summary>
    ///     Determines whether one stat is greater than or equal to another.
    /// </summary>
    public static bool operator >=(StatBase left, StatBase right)
    {
        return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
    }
}