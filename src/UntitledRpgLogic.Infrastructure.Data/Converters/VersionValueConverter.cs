using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Version = UntitledRpg.LibraryFile.Version;

namespace UntitledRpgLogic.Infrastructure.Data.Converters;

/// <summary>
///     A value converter that tells Entity Framework Core how to store <see cref="Version" /> properties in a database.
///     It serializes the <see cref="Version" /> object to a single string for storage and deserializes it back when
///     reading.
/// </summary>
public class VersionValueConverter : ValueConverter<Version, string>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="VersionValueConverter" /> class.
	/// </summary>
	public VersionValueConverter() : base(
		version => version.ToString(),
		str => new Version(str))
	{
	}
}
