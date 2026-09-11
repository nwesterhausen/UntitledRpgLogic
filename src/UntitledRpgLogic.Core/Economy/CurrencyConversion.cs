namespace UntitledRpgLogic.Core.Economy;

/// <summary>
/// 	Describes the conversion ratios of a pair of currencies.
/// </summary>
/// <remarks>
/// 	<see cref="CurrencyBId" /> is worth <see cref="Ratio" /> of <see cref="CurrencyAId" />
/// </remarks>
public record CurrencyConversion
{

	/// <summary>
	/// 	Amount of <see cref="CurrencyAId" /> that is worth 1 of <see cref="CurrencyBId" />
	/// </summary>
	public float Ratio { get; init; }

	/// <summary>
	/// 	<see cref="CurrencyDefinition.Id" /> of currency "A"
	/// </summary>
	public Ulid CurrencyAId { get; init; }

	/// <summary>
	/// 	<see cref="CurrencyDefinition.Id" /> of currency "B"
	/// </summary>
	public Ulid CurrencyBId { get; init; }
}
