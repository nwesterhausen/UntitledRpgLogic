using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Minimal material definition for persistence. Instances of items can reference materials by this ULID.
///     Additional physical properties can be modeled later as separate tables or owned types.
/// </summary>
public record MaterialDefinition
{
	/// <summary>
	///     Primary key (ULID) for the material definition.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     Display name for the material.
	/// </summary>
	public required string Name { get; init; }
}
