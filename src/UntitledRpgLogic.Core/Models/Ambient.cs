using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     An ambient is something like Temperature that can be affected by spells.
/// </summary>
public class Ambient
{

	/// <summary>
	///     Gets or sets the unique identifier (PK).
	/// </summary>
	[Key]
	public Ulid Id { get; set; }

	/// <summary>
	/// 	Name of the ambient
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// 	A description of the ambient
	/// </summary>
	public string? Description { get; set; }

}
