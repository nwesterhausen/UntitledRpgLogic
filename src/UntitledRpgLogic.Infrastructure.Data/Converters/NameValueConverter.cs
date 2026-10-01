using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Infrastructure.Data.Converters;

/// <summary>
///     A value converter that tells Entity Framework Core how to store <see cref="Name" /> properties in a database.
///     It serializes the <see cref="Name" /> object to a single string for storage and deserializes it back when reading.
/// </summary>
public class NameValueConverter : ValueConverter<Name, string>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="NameValueConverter" /> class.
	/// </summary>
	public NameValueConverter() : base(
		name => name.ToString(),
		str => Name.Parse(str, CultureInfo.InvariantCulture))
	{
	}
}
