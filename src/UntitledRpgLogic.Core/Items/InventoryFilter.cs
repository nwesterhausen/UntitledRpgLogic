namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Defines filtering constraints for an inventory based on item types and subtypes.
/// </summary>
public record InventoryFilter
{
	/// <summary>
	///     When <see langword="true" />, only matched items are accepted (AllowList / Whitelist).
	///     When <see langword="false" />, matched items are rejected (BlockList / Blacklist).
	/// </summary>
	public bool IsAllowList { get; init; } = true;

	/// <summary>
	///     Broad item classifications covered by this filter rule.
	/// </summary>
	public IReadOnlyCollection<ItemType> ItemTypes { get; init; } = [];

	/// <summary>
	///     Specific item subtypes covered by this filter rule.
	/// </summary>
	public IReadOnlyCollection<ItemSubtype> ItemSubtypes { get; init; } = [];

	/// <summary>
	///     Determines whether an item definition is permitted by this filter.
	/// </summary>
	/// <param name="definition">The item template definition to evaluate.</param>
	/// <returns><see langword="true" /> if the item is accepted; otherwise, <see langword="false" />.</returns>
	public bool IsAllowed(ItemDefinition definition)
	{
		ArgumentNullException.ThrowIfNull(definition);

		var matches = this.ItemTypes.Contains(definition.ItemType)
		              || this.ItemSubtypes.Contains(definition.ItemSubtype);

		return this.IsAllowList ? matches : !matches;
	}
}
