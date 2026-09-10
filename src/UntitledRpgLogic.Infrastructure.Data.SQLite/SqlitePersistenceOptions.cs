using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Infrastructure.Data.SQLite;

/// <summary>
/// 	Database configuration for the SQLite database.
/// </summary>
public record SqlitePersistenceOptions : PersistenceOptions
{
	/// <inheritdoc />
	public SqlitePersistenceOptions()
	{
		ConnectionString = "Data Source=urpg.db";
	}
}
