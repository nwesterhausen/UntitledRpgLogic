using System.Drawing;
using System.Globalization;
using Tomlet.Exceptions;
using Tomlet.Models;

namespace UntitledRpgLogic.Infrastructure.Configuration.Tomlet;

public static partial class MapperRegistration
{
    /// <summary>
    ///     Serializes a <see cref="Color" /> to a TOML string value.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private static TomlValue SerializeColor(Color color)
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
            if (colorString.StartsWith("#")) colorString = colorString[1..]; // Remove the '#' if present

            if (colorString.Length == 6) colorString = "FF" + colorString; // Add alpha channel if missing

            return Color.FromArgb(int.Parse(colorString, NumberStyles.HexNumber));
        }

        throw new TomlTypeMismatchException(typeof(TomlString), value.GetType(), typeof(Color));
    }
}
