# Database Initalization

Set up a database connection via the appropriate adapter:

```csharp
// Single-player / Godot client:
services.AddSqlitePersistence(opts =>
{
    opts.ConnectionString = "Data Source=saves/slot1.db";
    // opts.AutoMigrate = true; // The default value for AutoMigrate is true
});

// Dedicated PostgreSQL server:
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
