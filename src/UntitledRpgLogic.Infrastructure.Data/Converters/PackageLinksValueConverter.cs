using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UntitledRpg.LibraryFile;

namespace UntitledRpgLogic.Infrastructure.Data.Converters;

/// <summary>
///     Converts a collection of <see cref="PackageLink" /> to and from a JSON string column.
/// </summary>
public sealed class PackageLinkReadOnlyCollectionConverter : ValueConverter<IReadOnlyCollection<PackageLink>, string>
{
	/// <inheritdoc />
	public PackageLinkReadOnlyCollectionConverter() : base(
		links => JsonSerializer.Serialize(links, UrpglibConstants.DefaultJsonSerializerOptions),
		json => string.IsNullOrWhiteSpace(json)
			? Array.Empty<PackageLink>()
			: JsonSerializer.Deserialize<IReadOnlyCollection<PackageLink>>(json,
				  UrpglibConstants.DefaultJsonSerializerOptions)
			  ?? Array.Empty<PackageLink>())
	{
	}
}

/// <summary>
///     Converts a collection of <see cref="PackageLink" /> to and from a JSON string column.
/// </summary>
public sealed class PackageLinkCollectionConverter : ValueConverter<ICollection<PackageLink>, string>
{
	/// <inheritdoc />
	public PackageLinkCollectionConverter() : base(
		links => JsonSerializer.Serialize(links, UrpglibConstants.DefaultJsonSerializerOptions),
		json => string.IsNullOrWhiteSpace(json)
			? Array.Empty<PackageLink>()
			: JsonSerializer.Deserialize<ICollection<PackageLink>>(json, UrpglibConstants.DefaultJsonSerializerOptions)
			  ?? Array.Empty<PackageLink>())
	{
	}
}

/// <summary>
///     Converts a read-only list of <see cref="PackageLink" /> to and from a JSON string column.
/// </summary>
public sealed class PackageLinkReadOnlyListConverter : ValueConverter<IReadOnlyList<PackageLink>, string>
{
	/// <inheritdoc />
	public PackageLinkReadOnlyListConverter() : base(
		links => JsonSerializer.Serialize(links, UrpglibConstants.DefaultJsonSerializerOptions),
		json => string.IsNullOrWhiteSpace(json)
			? Array.Empty<PackageLink>()
			: JsonSerializer.Deserialize<IReadOnlyList<PackageLink>>(json,
				  UrpglibConstants.DefaultJsonSerializerOptions)
			  ?? Array.Empty<PackageLink>())
	{
	}
}
