# Database Initalization

Set up a database connection via the appropriate adapter:

```csharp
// SQLite3 Database (singleplayer or to export perhaps)
services.AddSqlitePersistence(opts =>
{
    opts.ConnectionString = "Data Source=saves/slot1.db";
    // opts.AutoMigrate = true; // The default value for AutoMigrate is true
});

// Dedicated PostgreSQL (multiplayer contexts)
services.AddPostgreSqlPersistence(opts =>
{
    opts.ConnectionString = hostContext.Configuration.GetConnectionString("Postgres")!;
    opts.AutoMigrate = false; // Disable auto-migrate if using external CI/CD migration scripts (optional)
});
```

Regardless of which adapter you add, the code to initialize the database (and "turn on" the unit of work and repository access)
is the same.

```csharp
var initializer = serviceProvider.GetRequiredService<IDatabaseInitializer>();
await initializer.InitializeAsync();
```

If `AutoMigrate` is set to `false`, `InitializeAsync()` exits safely without touching the schema.

### Configuring the HostContext for PostgreSQL

Programmatically setting the HostContext is easily done like so:

```cs
services.AddPostgreSqlPersistence(opts =>
{
    // Hardcoded or dynamically constructed
    opts.ConnectionString = "Host=localhost;Port=5432;Database=my_db;Username=postgres;Password=secret;";
    opts.AutoMigrate = false;
});
```
