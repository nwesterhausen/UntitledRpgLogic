namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     A configuration used to describe the author of a configuration module.
/// </summary>
public record AuthorConfig
{
	/// <summary>
	///     The unique identifier for the author.
	/// </summary>
	public required Ulid Id { get; init; }

	/// <summary>
	///     The name of the author.
	/// </summary>
	public required string AuthorName { get; init; }

	/// <summary>
	///     The author's website.
	/// </summary>
	public string Website { get; init; } = string.Empty;
}
