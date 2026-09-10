#!/bin/sh
# Helper script to reset migrations during the early development (frequent changes to models and relations). It's better to
# just make a new InitialCreate migration than create a bunch of migrations before the library is even in use.

# POSTGRESQL connection string for design_time database (used for creating migration)
export URPG_PG_CONNECTION_STRING="Host=172.22.122.14;Port=5432;Database=urpg_dev;Username=urpg_user;Password=urpg_password"

# Remove existing migration artifacts
rm -rf src/UntitledRpgLogic.Infrastructure.Data.SQLite/Migrations
rm -rf src/UntitledRpgLogic.Infrastructure.Data.PostgreSQL/Migrations

# Generate for SQLite
dotnet ef migrations add InitialCreate \
  --project src/UntitledRpgLogic.Infrastructure.Data.SQLite \
  --startup-project src/UntitledRpgLogic.Infrastructure.Data.SQLite

# Generate for PostgreSQL
dotnet ef migrations add InitialCreate \
  --project src/UntitledRpgLogic.Infrastructure.Data.PostgreSQL \
  --startup-project src/UntitledRpgLogic.Infrastructure.Data.PostgreSQL
