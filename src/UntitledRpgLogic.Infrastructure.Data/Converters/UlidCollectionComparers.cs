namespace UntitledRpgLogic.Infrastructure.Data.Converters;

/// <summary>
///     Handles collection conversion/comparison for ICollection
/// </summary>
public sealed class UlidCollectionValueComparer : CollectionValueComparer<ICollection<Ulid>, Ulid>
{
	/// <inheritdoc />
	public UlidCollectionValueComparer() : base(c => c.ToList()) { }
}

/// <summary>
///     Handles collection conversion/comparison for IReadonlyCollection
/// </summary>
public sealed class UlidReadOnlyCollectionValueComparer : CollectionValueComparer<IReadOnlyCollection<Ulid>, Ulid>
{
	/// <inheritdoc />
	public UlidReadOnlyCollectionValueComparer() : base(c => c.ToList()) { }
}
