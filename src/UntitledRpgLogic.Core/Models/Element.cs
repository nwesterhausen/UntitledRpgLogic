using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///		An element in the game (e.g., "Fire", "Time", "Summoning").
/// </summary>
public record Element
{
	/// <summary>
	///		A default constructor for the record.
	/// </summary>
	public Element() { }

	/// <summary>
	/// Constructs an element with specified name.
	/// </summary>
	public Element(string name) => this.Name = name;

	/// <summary>
	/// Constructs an element with specified name and description.
	/// </summary>
	public Element(string name, string description) : this(name) => this.Description = description;

	/// <summary>
	///     The name of the element (e.g., "Fire", "Time", "Summoning"). This is required.
	/// </summary>
	public required string Name { get; init; } = string.Empty;

	/// <summary>
	///     A brief description of the element and its characteristics.
	/// </summary>
	public string Description { get; init; } = string.Empty;

	/// <summary>
	///     The unique identifier for the element. If not provided, a new one will be generated.
	/// </summary>
	[Key]
	public Ulid Id { get; init; } = Ulid.NewUlid();
}
