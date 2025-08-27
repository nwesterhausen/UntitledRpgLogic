using Tomlet.Exceptions;
using Tomlet.Models;

namespace UntitledRpgLogic.Infrastructure.Configuration.Tomlet;

public static partial class MapperRegistration
{
	/// <summary>
	///     Serializes a <see cref="Ulid" /> to a TOML string value.
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	private static TomlValue SerializeUlid(Ulid t) => new TomlString(t.ToString());

	/// <summary>
	///     Deserializes a <see cref="Ulid" /> from a TOML string value.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	/// <exception cref="TomlException"></exception>
	private static Ulid DeserializeUlid(TomlValue value)
	{
		if (value is TomlString str)
		{
			if (Ulid.TryParse(str.Value, out var ulid))
			{
				return ulid;
			}
		}

		throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(Ulid));
	}
}
