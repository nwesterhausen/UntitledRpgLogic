using Tomlet.Exceptions;
using Tomlet.Models;

namespace UntitledRpgLogic.Infrastructure.Configuration.Tomlet;

public static partial class MapperRegistration
{
	/// <summary>
	///     Serializes a <see cref="Guid" /> to a TOML string value.
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	private static TomlValue SerializeGuid(Guid t) => new TomlString(t.ToString());

	/// <summary>
	///     Deserializes a <see cref="Guid" /> from a TOML string value.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	/// <exception cref="TomlException"></exception>
	private static Guid DeserializeGuid(TomlValue value)
	{
		if (value is TomlString str)
		{
			return Guid.Parse(str.Value);
		}

		throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(Guid));
	}
}
