namespace UntitledRpgLogic.Core.Interfaces.Data;

/// <summary>
///     Database connection provider which allows for changing database based on which world is loaded.
///     This interface and its implementation manage which database file is currently active.
///     <list type="bullet">
///         <item>
///             It knows the location of your main-ref.db (assumed to be res://main-ref.db, adjust if needed) and the
///             directory for player worlds (user://worlds/).
///         </item>
///         <item>CreateNewWorldDb(string worldName): Copies main-ref.db to a new file like user://worlds/worldName.db.</item>
///         <item>
///             SetActiveWorldDb(string worldName): Sets the connection string to point to the specified player's world
///             database.
///         </item>
///         <item>SetActiveMainReferenceDb(): Sets the connection string to point to the main-ref.db.</item>
///         <item>GetCurrentConnectionString(): Provides the connection string for the currently active database.</item>
///     </list>
/// </summary>
/// <remarks>
///     <para>
///         Services.ConfigureServices:
///         <list>
///             <item>IWorldDbConnectionProvider is registered as a singleton.</item>
///             <item>
///                 AddDbContext{RpgDbContext} is now configured with a factory function (serviceProvider, options) =>
///                 .... This function retrieves the IWorldDbConnectionProvider and uses its GetCurrentConnectionString()
///                 to configure the DbContextOptions for RpgDbContext each time an RpgDbContext instance is created by the
///                 DI container.
///             </item>
///         </list>
///     </para>
///     Workflow:
///     <para>
///         Game Start: Services initializes, WorldDbConnectionProvider is created, and by default, it might point to
///         main-ref.db.
///         Create New World (e.g., "MyAdventure"):
///         <code>
/// var dbProvider = Services.ServiceProvider.GetRequiredService{IWorldDbConnectionProvider}();
/// if (dbProvider.CreateNewWorldDb("MyAdventure"))
/// {
///     dbProvider.SetActiveWorldDb("MyAdventure");
///     // Now, any RpgDbContext resolved will use "user://worlds/MyAdventure.db"
/// }
/// </code>
///     </para>
///     <para>
///         Load Existing World (e.g., "MySavedGame"):
///         <code>
/// var dbProvider = Services.ServiceProvider.GetRequiredService{IWorldDbConnectionProvider}();
/// dbProvider.SetActiveWorldDb("MySavedGame");
/// // Now, any RpgDbContext resolved will use "user://worlds/MySavedGame.db"
/// Accessing Data:
/// // After setting the active world (or main reference)
/// using (var context = Services.ServiceProvider.GetRequiredService{RpgDbContext}())
/// {
///     // context is now connected to the database specified by IWorldDbConnectionProvider
///     // Perform database operations...
///     // context.Players.Add(newPlayer);
///     // context.SaveChanges();
/// }
/// </code>
///     </para>
///     <para>
///         Accessing Main Reference Data:
///         <code>
/// var dbProvider = Services.ServiceProvider.GetRequiredService{IWorldDbConnectionProvider}();
/// dbProvider.SetActiveMainReferenceDb();
/// using (var context = Services.ServiceProvider.GetRequiredService{RpgDbContext}())
/// {
///     // context is now connected to "res://main-ref.db"
///     // Read game template data, etc.
/// }
/// </code>
///     </para>
///     This setup allows you to dynamically switch the database connection for RpgDbContext based on game state, keeping
///     your main reference data separate from player-specific world data. Remember to place main-ref.db in your project's
///     res:// directory (or adjust the path in WorldDbConnectionProvider) and ensure it's exported with your game. Player
///     world databases will be created in the user://worlds/ directory.
/// </remarks>
public interface IWorldDbConnectionProvider
{
	/// <summary>
	///     Get the current connection string
	/// </summary>
	/// <returns>current connection string</returns>
	public string GetCurrentConnectionString();

	/// <summary>
	///     Sets the active world to load/create by name
	/// </summary>
	/// <param name="worldName">name of the world</param>
	public void SetActiveWorldDb(string worldName);

	/// <summary>
	///     Sets an active "main reference" database which is an immutable base for the world files.
	/// </summary>
	public void SetActiveMainReferenceDb();

	/// <summary>
	///     Creates a new world database based on the main reference.
	/// </summary>
	/// <param name="worldName">The name of the world to create.</param>
	/// <returns>True if the world database was created successfully, false otherwise.</returns>
	public bool CreateNewWorldDb(string worldName);

	/// <summary>
	///     Get the path to the main reference db
	/// </summary>
	/// <returns>The file path to the main reference database.</returns>
	public string GetMainReferenceDbPath();

	/// <summary>
	///     Get the path to the world save database by name
	/// </summary>
	/// <param name="worldName">The name of the world.</param>
	/// <returns>The file path to the specified world's database.</returns>
	public string GetWorldDbPath(string worldName);
}
