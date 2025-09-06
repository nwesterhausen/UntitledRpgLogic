using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Contains the name of an object. Has a singular, a plural and a form when used as an adjective.
/// </summary>
public class Name : IStringSerializable<Name>
{
	/// <summary>
	///     An empty name.
	/// </summary>
	public static readonly Name Empty = new(string.Empty);

	/// <summary>
	///     Constructs a new PluralName object with the given singular, plural and adjective names. If not supplied, the
	///     singular
	///     will be used as the adjective and a best guess will be made for the plural.
	/// </summary>
	/// <param name="singular"></param>
	/// <param name="plural"></param>
	/// <param name="adjective"></param>
	public Name(string singular, string? plural = null, string? adjective = null)
	{
		this.Singular = singular;
		this.Plural = plural ?? BestGuessPlural(singular);
		this.Adjective = adjective ?? singular;
	}

	/// <summary>
	///     The singular name of the object, e.g. "a Sword".
	/// </summary>
	public string Singular { get; init; }

	/// <summary>
	///     The plural name of the object, e.g. "two Swords".
	/// </summary>
	public string Plural { get; init; }

	/// <summary>
	///     The name used as an adjective, e.g. "Sword soup".
	/// </summary>
	public string Adjective { get; init; }

	/// <inheritdoc />
	public string Serialize()
	{
		if (this.Singular.Equals(this.Adjective, StringComparison.Ordinal))
		{
			return $"{this.Singular}:{this.Plural}";
		}

		return $"{this.Singular};{this.Plural};{this.Adjective}";
	}

	/// <inheritdoc />
	public static Name Deserialize(string serialized)
	{
		ArgumentNullException.ThrowIfNull(serialized, nameof(serialized));

		var parts = serialized.Split(';');
		return parts.Length switch
		{
			0 => throw new ArgumentException("Invalid serialized name format."),
			1 => new Name(parts[0]),
			2 => new Name(parts[0], parts[1]),
			_ => new Name(parts[0], parts[1], parts[2])
		};
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

		if (singular.EndsWith('s') || singular.EndsWith('x') || singular.EndsWith('z') ||
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
}
