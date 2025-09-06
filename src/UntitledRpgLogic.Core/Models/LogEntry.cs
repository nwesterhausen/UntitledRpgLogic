using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents a single log entry to be stored in the database.
/// </summary>
public record LogEntry
{
  /// <summary>
  ///     Parameterless constructor for EF Core.
  /// </summary>
  [SetsRequiredMembers]
  private LogEntry()
  {
    this.Id = Ulid.NewUlid();
    this.Timestamp = DateTimeOffset.UtcNow;
    this.Message = string.Empty;
  }

  /// <summary>
  ///     Initializes a new instance of the <see cref="LogEntry" /> class.
  /// </summary>
  [SetsRequiredMembers]
  public LogEntry(int level, int eventId, string message, string? category = null, string? parameters = null)
  {
	  this.Id = Ulid.NewUlid();
	  this.Timestamp = DateTimeOffset.UtcNow;
	  this.Level = level;
	  this.EventId = eventId;
	  this.Message = message;
	  this.Category = category;
	  this.Parameters = parameters;
  }

  /// <summary>
  ///     Initializes a new instance of the <see cref="LogEntry" /> class.
  /// </summary>
  [SetsRequiredMembers]
  public LogEntry(int level, int eventId, string message, string? category = null, Ulid? entityId = null, string? parameters = null)
  {
	  this.Id = Ulid.NewUlid();
	  this.Timestamp = DateTimeOffset.UtcNow;
	  this.Level = level;
	  this.EventId = eventId;
	  this.Message = message;
	  this.Category = category;
	  this.EntityId = entityId;
	  this.Parameters = parameters;
  }

  /// <summary>
  ///     The unique identifier for the log entry.
  /// </summary>
  [Key]
  public Ulid Id { get; init; }

  /// <summary>
  ///     The timestamp of when the log entry was created.
  /// </summary>
  public DateTimeOffset Timestamp { get; init; }

  /// <summary>
  ///     The severity level of the log entry (e.g., Information, Warning, Error) as its integer representation.
  /// </summary>
  public int Level { get; init; }

  /// <summary>
  ///     The event ID associated with the log entry, for structured logging.
  /// </summary>
  public int EventId { get; init; }

  /// <summary>
  ///	The optional ID of the entity related to this log entry (e.g., a player, an mob, and item).
  /// </summary>
  public Ulid? EntityId { get; init; }

  /// <summary>
  ///     The log message with placeholders (the message template).
  /// </summary>
  public required string Message { get; init; }

  /// <summary>
  ///     A JSON blob representing the parameters used to format the message.
  /// </summary>
  public string? Parameters { get; init; }

  /// <summary>
  ///     The category of the log, typically the name of the logger (e.g., "StatService").
  /// </summary>
  public string? Category { get; init; }
}
