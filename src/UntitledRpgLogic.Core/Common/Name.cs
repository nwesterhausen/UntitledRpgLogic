using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Core.Common;

/// <summary>
///     Contains the name of an object. Has a singular, a plural and a form when used as an adjective.
/// </summary>
/// <remarks>
///     Constructs a new PluralName object with the given singular, plural and adjective names. If not supplied, the
///     singular will be used as the adjective and the best guess will be made for the plural.
/// </remarks>
/// <param name="singular"></param>
/// <param name="plural"></param>
/// <param name="adjective"></param>
public class Name(string singular, string? plural = null, string? adjective = null) : IParsable<Name>
{
	private const char Delim = ';';

	/// <summary>
	///     An empty name.
	/// </summary>
	public static readonly Name Empty = new(string.Empty);

	/// <summary>
	///     The singular name of the object, e.g. "a Sword".
	/// </summary>
	public string Singular { get; init; } = singular;

	/// <summary>
	///     The plural name of the object, e.g. "two Swords".
	/// </summary>
	public string Plural { get; init; } = plural ?? BestGuessPlural(singular);

	/// <summary>
	///     The name used as an adjective, e.g. "Sword soup".
	/// </summary>
	public string Adjective { get; init; } = adjective ?? singular;

	/// <inheritdoc />
	public static Name Parse(string s, IFormatProvider? provider = null) =>
		TryParse(s, provider, out var result)
			? result
			: throw new FormatException($"Invalid name format: '{s}'.");

	/// <inheritdoc />
	public static bool TryParse(
		[NotNullWhen(true)] string? s,
		IFormatProvider? provider,
		[MaybeNullWhen(false)] out Name result)
	{
		result = null;

		if (string.IsNullOrWhiteSpace(s))
		{
			return false;
		}

		var span = s.AsSpan().Trim();
		var firstDelim = span.IndexOf(Delim);

		if (firstDelim < 0)
		{
			result = new Name(span.ToString());
			return true;
		}

		var singular = span[..firstDelim];
		var remainder = span[(firstDelim + 1)..];

		if (singular.IsEmpty || remainder.IsEmpty)
		{
			return false;
		}

		var secondDelim = remainder.IndexOf(Delim);
		if (secondDelim < 0)
		{
			result = new Name(singular.ToString(), remainder.ToString());
			return true;
		}

		var plural = remainder[..secondDelim];
		var adjective = remainder[(secondDelim + 1)..];

		if (plural.IsEmpty || adjective.IsEmpty || adjective.IndexOf(Delim) >= 0)
		{
			return false;
		}

		result = new Name(singular.ToString(), plural.ToString(), adjective.ToString());
		return true;
	}

	/// <inheritdoc />
	public override string ToString()
	{
		// Both Adjective and Plural match defaults, use one part
		if (this.Singular.Equals(this.Adjective, StringComparison.Ordinal) &&
		    this.Plural.Equals(BestGuessPlural(this.Singular), StringComparison.Ordinal))
		{
			return this.Singular;
		}

		// Adjective matches Singular, but Plural is custom, use two parts
		if (this.Singular.Equals(this.Adjective, StringComparison.Ordinal))
		{
			return $"{this.Singular}{Delim}{this.Plural}";
		}

		// Fully custom Adjective requires 3 parts
		return $"{this.Singular}{Delim}{this.Plural}{Delim}{this.Adjective}";
	}

	/// <summary>
	///     Given a name, it will do its best to pluralize it.
	/// </summary>
	/// <param name="singular"></param>
	/// <returns></returns>
	public static string BestGuessPlural(string singular)
	{
		if (string.IsNullOrEmpty(singular))
		{
			return string.Empty;
		}

		if (singular.EndsWith('y') &&
		    !singular.EndsWith("ay", StringComparison.InvariantCultureIgnoreCase) &&
		    !singular.EndsWith("ey", StringComparison.InvariantCultureIgnoreCase) &&
		    !singular.EndsWith("oy", StringComparison.InvariantCultureIgnoreCase) &&
		    !singular.EndsWith("uy", StringComparison.InvariantCultureIgnoreCase))
		{
			return string.Concat(singular.AsSpan(0, singular.Length - 1), "ies");
		}

		if (singular.EndsWith('z') && !singular.EndsWith("zz", StringComparison.InvariantCultureIgnoreCase))
		{
			return singular + "zes";
		}

		if (singular.EndsWith('o') || singular.EndsWith('s') || singular.EndsWith('x') || singular.EndsWith('z') ||
		    singular.EndsWith("ch", StringComparison.InvariantCultureIgnoreCase) ||
		    singular.EndsWith("sh", StringComparison.InvariantCultureIgnoreCase))
		{
			return singular + "es";
		}

		return singular + "s";
	}

	/// <summary>
	///     Get the name of the object, either singular or plural based on the count.
	/// </summary>
	/// <param name="count">number of items</param>
	/// <returns>appropriate name for the count of objects</returns>
	public string GetName(int count = 1) => count == 1 ? this.Singular : this.Plural;

	// Convenient overload without IFormatProvider
	public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Name result) =>
		TryParse(s, null, out result);
}
