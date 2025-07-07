using Tomlet.Exceptions;
using Tomlet.Models;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Infrastructure.Configuration.Tomlet;

public static partial class MapperRegistration
{
    /// <summary>
    ///     Serializes a <see cref="MassScale" /> to a TOML string value.
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    private static TomlValue SerializeMassScale(MassScale scale)
    {
        return new TomlString(scale.ToString());
    }

    /// <summary>
    ///     Deserializes a <see cref="MassScale" /> from a TOML string value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="TomlTypeMismatchException"></exception>
    private static MassScale DeserializeMassScale(TomlValue value)
    {
        if (value is TomlString str)
        {
            if (Enum.TryParse(str.Value, true, out MassScale scale)) return scale;

            return str.Value.ToLower() switch
            {
                "milligram" or "milligrams" or "mg" => MassScale.mg,
                "gram" or "grams" or "g" => MassScale.g,
                "kilogram" or "kilograms" or "kg" => MassScale.kg,
                _ => throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(MassScale))
            };
        }

        throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(MassScale));
    }
}
