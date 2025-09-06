using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
///     A configuration used to describe the author of a configuration module.
/// </summary>
public record AuthorConfig : ITomlConfig
{
	/// <summary>
	///     The author's website.
	/// </summary>
	public required string Website { get; set; }

	/// <summary>
	///     The name of the author.
	/// </summary>
	public required string AuthorName { get; set; }

	/// <summary>
	///     The unique identifier for the author.
	/// </summary>
	public required Ulid Id { get; set; }

	/// <inheritdoc />
	public ConfigType ConfigType => ConfigType.Author;
}
