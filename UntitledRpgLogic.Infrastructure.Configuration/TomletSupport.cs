using System.Drawing;
using System.Globalization;

namespace UntitledRpgLogic.Infrastructure.Configuration;

/// <summary>
///     Tomlet support for custom types that are not natively supported by Tomlet.
/// </summary>
public static class TomletSupport
{
    /// <summary>
    ///     Register the custom mappers to handle types we use that Tomlet does not support by default, such as
    ///     <see cref="Guid" />.
    /// </summary>
    public static void RegisterMappers()
    {
        // Register custom mappers for types that Tomlet does not support by default.
        TomletMain.RegisterMapper(SerializeGuid, DeserializeGuid);
        TomletMain.RegisterMapper(SerializeColor, DeserializeColor);
        TomletMain.RegisterMapper(SerializeDimensionScale, DeserializeDimensionScale);
        TomletMain.RegisterMapper(SerializeMassScale, DeserializeMassScale);
    }

    /// <summary>
    ///     Serializes a <see cref="Guid" /> to a TOML string value.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    private static TomlValue? SerializeGuid(Guid t)
    {
        return new TomlString(t.ToString());
    }

    /// <summary>
    ///     Deserializes a <see cref="Guid" /> from a TOML string value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="TomlException"></exception>
    private static Guid DeserializeGuid(TomlValue value)
    {
        if (value is TomlString str) return Guid.Parse(str.Value);

        throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(Guid));
    }

    /// <summary>
    ///     Serializes a <see cref="Color" /> to a TOML string value.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private static TomlValue? SerializeColor(Color color)
    {
        return new TomlString(color.ToArgb().ToString("X8"));
    }

    /// <summary>
    ///     Deserializes a <see cref="Color" /> from a TOML string value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="TomlTypeMismatchException"></exception>
    private static Color DeserializeColor(TomlValue value)
    {
        if (value is TomlString str)
        {
            string colorString = str.Value.Trim();
            if (colorString.StartsWith("#")) colorString = colorString.Substring(1); // Remove the '#' if present

            if (colorString.Length == 6) colorString = "FF" + colorString; // Add alpha channel if missing

            return Color.FromArgb(int.Parse(colorString, NumberStyles.HexNumber));
        }

        throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(Color));
    }

    /// <summary>
    ///     Serializes a <see cref="DimensionScale" /> to a TOML string value.
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    private static TomlValue? SerializeDimensionScale(DimensionScale scale)
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

    /// <summary>
    ///     Serializes a <see cref="MassScale" /> to a TOML string value.
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    private static TomlValue? SerializeMassScale(MassScale scale)
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
