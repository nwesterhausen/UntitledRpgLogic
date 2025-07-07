using Tomlet.Exceptions;
using Tomlet.Models;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Infrastructure.Configuration.Tomlet;

public static partial class MapperRegistration
{
    /// <summary>
    ///     Serializes a <see cref="DimensionScale" /> to a TOML string value.
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    private static TomlValue SerializeDimensionScale(DimensionScale scale)
    {
        return new TomlString(scale.ToString());
    }

    /// <summary>
    ///     Deserializes a <see cref="DimensionScale" /> from a TOML string value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="TomlTypeMismatchException"></exception>
    private static DimensionScale DeserializeDimensionScale(TomlValue value)
    {
        if (value is TomlString str)
        {
            if (Enum.TryParse(str.Value, true, out DimensionScale scale)) return scale;

            return str.Value.ToLower() switch
            {
                "millimeter" or "millimeters" or "mm" => DimensionScale.mm,
                "centimeter" or "centimeters" or "cm" => DimensionScale.cm,
                "meter" or "meters" or "m" => DimensionScale.m,
                "kilometer" or "kilometers" or "km" => DimensionScale.km,
                _ => throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(DimensionScale))
            };
        }

        throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(DimensionScale));
    }
}
