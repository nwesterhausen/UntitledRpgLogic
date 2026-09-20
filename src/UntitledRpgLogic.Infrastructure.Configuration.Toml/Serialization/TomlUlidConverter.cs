using Tomlyn;
using Tomlyn.Serialization;

namespace UntitledRpgLogic.Infrastructure.Configuration.Serialization;

/// <summary>
///
/// </summary>
public sealed class TomlUlidConverter : TomlConverter<Ulid>
{
	/// <inheritdoc />
	public override Ulid Read(TomlReader reader)
	{
		ArgumentNullException.ThrowIfNull(reader);

		var text = reader.GetString();
		if (string.IsNullOrWhiteSpace(text))
		{
			return default;
		}

		if (Ulid.TryParse(text, out var id))
		{
			return id;
		}

		throw new TomlException($"Unable to parse '{text}' as a valid ULID at column {reader.Column}.");
	}

	/// <inheritdoc />
	public override void Write(TomlWriter writer, Ulid value)
	{
		ArgumentNullException.ThrowIfNull(writer);

		writer.WriteStringValue(value.ToString());
	}
}
