using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UntitledRpgLogic.Core.Classes;

namespace UntitledRpgLogic.Infrastructure.Data.ValueConverters;

/// <summary>
///     A value converter that tells Entity Framework Core how to store <see cref="Name" /> properties in a database.
///     It serializes the <see cref="Name" /> object to a single string for storage and deserializes it back when reading.
/// </summary>
public class NameToSimpleStringConverter : ValueConverter<Name, string>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="NameToSimpleStringConverter" /> class.
	/// </summary>
	public NameToSimpleStringConverter() : base(
		name => name.Serialize(),
		str => Name.Deserialize(str))
	{
	}
}
