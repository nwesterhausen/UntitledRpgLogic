namespace UntitledRpgLogic.Core.Data;

/// <summary>
/// 	Options related to setting up the database connection.
/// </summary>
public record PersistenceOptions
{
	/// <summary>
	/// 	The connection string for the database connection.
	/// </summary>
	public string ConnectionString { get; set; } = string.Empty;

	/// <summary>
	/// 	Whether to perform the migration to setup the database.
	/// </summary>
	public bool AutoMigrate { get; set; } = true;
}
