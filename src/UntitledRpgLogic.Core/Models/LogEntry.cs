using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents a single structured log entry stored in the database.
/// </summary>
[Table("log_entries")]
public record LogEntry : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new default instance of the <see cref="LogEntry" /> record for EF Core materialization.
	/// </summary>
	[SetsRequiredMembers]
	public LogEntry()
	{
		this.Id = Ulid.NewUlid();
		this.Timestamp = DateTimeOffset.UtcNow;
		this.Message = string.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="LogEntry" /> record with core log details.
	/// </summary>
	[SetsRequiredMembers]
	public LogEntry(int level, int eventId, string message, string? category = null, string? parameters = null) : this()
	{
		ArgumentNullException.ThrowIfNull(message);

		this.Level = level;
		this.EventId = eventId;
		this.Message = message;
		this.Category = category;
		this.Parameters = parameters;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="LogEntry" /> record with entity tracking.
	/// </summary>
	[SetsRequiredMembers]
	public LogEntry(int level, int eventId, string message, string? category, Ulid? entityId, string? parameters = null)
		: this(level, eventId, message, category, parameters)
	{
		this.EntityId = entityId;
	}

	/// <summary>
	///     The unique identifier for the log entry.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The UTC timestamp when the log entry was created.
	/// </summary>
	public DateTimeOffset Timestamp { get; init; }

	/// <summary>
	///     The severity level (e.g., Trace, Debug, Information, Warning, Error, Critical) as an integer.
	/// </summary>
	public int Level { get; init; }

	/// <summary>
	///     The structured event identifier associated with the message.
	/// </summary>
	public int EventId { get; init; }

	/// <summary>
	///     Optional foreign identifier of an associated entity (e.g., player, NPC, or item).
	/// </summary>
	public Ulid? EntityId { get; init; }

	/// <summary>
	///     Navigation property to the owning entity.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public Entity? Entity { get; init; }

	/// <summary>
	///     The log message template with placeholder tokens.
	/// </summary>
	[MaxLength(2048)]
	public required string Message { get; init; }

	/// <summary>
	///     A serialized JSON string containing the arguments used to format the message.
	/// </summary>
	public string? Parameters { get; init; }

	/// <summary>
	///     The category name of the source logger (e.g., "StatService", "LevelingService").
	/// </summary>
	[MaxLength(256)]
	public string? Category { get; init; }
}
