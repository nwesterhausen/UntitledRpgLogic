namespace UntitledRpgLogic.Core.Data;

/// <summary>
///     Represents a summary of how many of a specific content type are in a package.
/// </summary>
public record ContentSummaryEntry : IStringSerializable<ContentSummaryEntry>
{
	/// <summary>
	///     Create a new content summary entry
	/// </summary>
	/// <param name="Category">type of definition</param>
	/// <param name="Count">amount (defaults to 0)</param>
	public ContentSummaryEntry(string Category, uint Count = 0)
	{
		this.Category = Category;
		this.Count = Count;
	}

	/// <summary>
	///     The type of definition file
	/// </summary>
	public string Category { get; init; }

	/// <summary>
	///     The amount of definitions
	/// </summary>
	public uint Count { get; set; }

	/// <inheritdoc />
	public string Serialize() => $"{this.Category}:{this.Count}";

	/// <inheritdoc />
	public static bool TryDeserialize(string? serialized, out ContentSummaryEntry result)
	{
		result = default!;

		if (string.IsNullOrWhiteSpace(serialized))
		{
			return false;
		}

		var span = serialized.AsSpan();
		var separatorIndex = span.LastIndexOf(':');

		// Ensure separator exists and isn't at the very start or end
		if (separatorIndex <= 0 || separatorIndex == span.Length - 1)
		{
			return false;
		}

		var categorySpan = span[..separatorIndex];
		var countSpan = span[(separatorIndex + 1)..];

		if (!uint.TryParse(countSpan, out var count))
		{
			return false;
		}

		result = new ContentSummaryEntry(categorySpan.ToString(), count);
		return true;
	}

	public void Deconstruct(out string Category, out uint Count)
	{
		Category = this.Category;
		Count = this.Count;
	}
}
