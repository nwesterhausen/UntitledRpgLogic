using System.Drawing;
using System.Globalization;
using Tomlet;
using Tomlet.Exceptions;
using Tomlet.Models;

namespace UntitledRpgLogic.Configuration;

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
            var colorString = str.Value.Trim();
            if (colorString.StartsWith("#")) colorString = colorString.Substring(1); // Remove the '#' if present

            if (colorString.Length == 6) colorString = "FF" + colorString; // Add alpha channel if missing

            return Color.FromArgb(int.Parse(colorString, NumberStyles.HexNumber));
        }

        throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(Color));
    }
}