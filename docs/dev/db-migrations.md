## How to Create a Database Migration

### Step 0: Changes Requiring a Migration

- adding a `DbSet<T>` to `RpgDbContext`
- modifying `OnModelCreating` in `RpgDbContext`
- modifying existing entity classes in `Core`
- changes requiring a new `ValueConverter` to be defined in `RpgDbContext`
- a new Entity will require at least one of the changes above

### Step 1: Prepare Tool

Migrations are made with the EFCore CLI Tool. In the solution root:

```bash
dotnet tool restore
```

This will ensure the `dotnet-ef` program is installed and ready to use.

### Step 2: Run Tool

In the solution root, run this command to create a new migration:

```bash
dotnet ef migrations add <MigrationName> \
  --project src/UntitledRpgLogic.Infrastructure.Data \
  --startup-project src/UntitledRpgLogic.Infrastructure.Data
```

This specifies the `Infrastucture.Data` project to be used by the tool, since any
database relationships or references to the `Core` entities are defined in it.

### Step 3: Apply Migrations to Test Database

`RpgDbContextFactory` is used as a design-time target and it is also in the same
`Infrastructure.Data` project (important for migrations to be written/verified).

```bash
dotnet ef database update \
  --project src/UntitledRpgLogic.Infrastructure.Data \
  --startup-project src/UntitledRpgLogic.Infrastructure.Data
```
