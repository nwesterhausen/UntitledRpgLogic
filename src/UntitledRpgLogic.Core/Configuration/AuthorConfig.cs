using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

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
	public Guid AuthorId { get; set; } = Guid.NewGuid();

	/// <inheritdoc />
	public ConfigType ConfigType => ConfigType.Author;
}
