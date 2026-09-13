namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Options available when applying damage to a stat.
/// </summary>
public record StatChangeOptions
{
	/// <summary>
	///     A flat amount of damage to apply to the stat.
	/// </summary>
	public int? FlatChange { get; init; }

	/// <summary>
	///     A percentage of the stat's current value to apply as damage.
	/// </summary>
	public float? PercentageChange { get; init; }

	/// <summary>
	///     A percentage of the stat's maximum value to apply as damage.
	/// </summary>
	public float? PercentageChangeOfMax { get; init; }

	/// <summary>
	///     Whether this change affects the stat in a positive or negative direction.
	/// </summary>
	public bool IsPositive { get; init; }

	/// <summary>
	///     Apply the given options to this record, returning a new record instance.
	/// </summary>
	/// <param name="options">The options to apply</param>
	/// <returns>A record with any applicable fields overwritten by <paramref name="options" /></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public virtual StatChangeOptions Apply(StatChangeOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);

		return this with
		{
			FlatChange = options.FlatChange ?? this.FlatChange,
			PercentageChange = options.PercentageChange ?? this.PercentageChange,
			PercentageChangeOfMax = options.PercentageChangeOfMax ?? this.PercentageChangeOfMax,
			IsPositive = options.IsPositive || this.IsPositive
		};
	}
}
