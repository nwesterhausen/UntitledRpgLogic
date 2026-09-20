using Tomlyn.Serialization;
using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Infrastructure.Configuration.Serialization;

/// <summary>
///     A converter to convert <see cref="Name" /> to <see cref="string" /> and vice versa.
/// </summary>
public sealed class TomlNameConverter : TomlConverter<Name>
{
	/// <inheritdoc />
	public override Name? Read(TomlReader reader)
	{
		ArgumentNullException.ThrowIfNull(reader);

		var nameStr = reader.GetString();
		return Name.Deserialize(nameStr);
	}

	/// <inheritdoc />
	public override void Write(TomlWriter writer, Name value)
	{
		ArgumentNullException.ThrowIfNull(writer);
		ArgumentNullException.ThrowIfNull(value);

		writer.WriteStringValue(value.Serialize());
	}
}
