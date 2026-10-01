using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UntitledRpgLogic.Infrastructure.Data.Converters;

/// <summary>
///     Base comparer providing sequence equality, hash aggregation, and cloning for collection properties.
/// </summary>
public abstract class CollectionValueComparer<TCollection, TElement> : ValueComparer<TCollection>
	where TCollection : IEnumerable<TElement>
{
	/// <inheritdoc />
	protected CollectionValueComparer(Func<TCollection, TCollection> snapshot) : base(
		(c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
		c => c == null
			? 0
			: c.Aggregate(0, (hash, item) => HashCode.Combine(hash, item != null ? item.GetHashCode() : 0)),
		c => c == null ? default! : snapshot(c))
	{
	}
}
