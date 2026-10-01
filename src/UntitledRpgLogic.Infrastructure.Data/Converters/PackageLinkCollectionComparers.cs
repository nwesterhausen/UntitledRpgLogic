using UntitledRpg.LibraryFile;

namespace UntitledRpgLogic.Infrastructure.Data.Converters;

/// <summary>
///     Provides sequence-based equality and snapshotting for change-tracking <see cref="ICollection{PackageLink}" />.
/// </summary>
public sealed class PackageLinkCollectionComparer : CollectionValueComparer<ICollection<PackageLink>, PackageLink>
{
	/// <inheritdoc />
	public PackageLinkCollectionComparer() : base(c => c.ToList()) { }
}

/// <summary>
///     Provides sequence-based equality and snapshotting for change-tracking <see cref="IReadOnlyList{PackageLink}" />.
/// </summary>
public sealed class
	PackageLinkReadOnlyCollectionComparer : CollectionValueComparer<IReadOnlyCollection<PackageLink>, PackageLink>
{
	/// <inheritdoc />
	public PackageLinkReadOnlyCollectionComparer() : base(c => c.ToList()) { }
}

/// <summary>
///     Provides sequence-based equality and snapshotting for change-tracking <see cref="IReadOnlyList{PackageLink}" />.
/// </summary>
public sealed class PackageLinkReadOnlyListComparer : CollectionValueComparer<IReadOnlyList<PackageLink>, PackageLink>
{
	/// <inheritdoc />
	public PackageLinkReadOnlyListComparer() : base(c => c.ToList()) { }
}
