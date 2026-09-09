using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Root database catalog model representing an ambient world property that can be measured,
///     simulated, or affected by abilities and environmental events.
/// </summary>
[Table("ambients")]
public record Ambient : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="Ambient" /> record with default values (for EF Core).
	/// </summary>
	public Ambient()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.AmbientType = AmbientType.None;
		this.Value = 0f;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Ambient" /> record with a designated name.
	/// </summary>
	/// <param name="name">The display name of the ambient condition.</param>
	public Ambient(Name name) : this() => this.Name = name;

	/// <summary>
	///     The unique identifier for the ambient definition.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The display name of the ambient metric (e.g., "Ambient Temperature", "Atmospheric Pressure").
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     The categorical type of ambient condition.
	/// </summary>
	public required AmbientType AmbientType { get; init; }

	/// <summary>
	///     The current baseline or ambient value in world units.
	/// </summary>
	public float Value { get; set; }
}
