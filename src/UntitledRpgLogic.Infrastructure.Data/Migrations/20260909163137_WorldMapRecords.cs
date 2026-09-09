using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UntitledRpgLogic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorldMapRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "map_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    type = table.Column<byte>(type: "INTEGER", nullable: false),
                    atmosphere = table.Column<string>(type: "TEXT", nullable: false),
                    baseline_ambients = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_map_definitions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "map_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_map_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "map_transitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    source_map_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    source_x = table.Column<float>(type: "REAL", nullable: false),
                    source_y = table.Column<float>(type: "REAL", nullable: false),
                    target_map_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    target_x = table.Column<float>(type: "REAL", nullable: false),
                    target_y = table.Column<float>(type: "REAL", nullable: false),
                    transition_tag = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_map_transitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_map_transitions_map_definitions_source_map_id",
                        column: x => x.source_map_id,
                        principalTable: "map_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_map_transitions_map_definitions_target_map_id",
                        column: x => x.target_map_id,
                        principalTable: "map_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "world_chunks",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    map_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    chunk_x = table.Column<int>(type: "INTEGER", nullable: false),
                    chunk_y = table.Column<int>(type: "INTEGER", nullable: false),
                    material_palette = table.Column<byte[]>(type: "BLOB", nullable: false),
                    compressed_tile_blob = table.Column<byte[]>(type: "BLOB", nullable: false),
                    version = table.Column<uint>(type: "INTEGER", nullable: false),
                    ambient_overrides = table.Column<string>(type: "TEXT", nullable: true),
                    atmosphere_override = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_world_chunks", x => x.id);
                    table.ForeignKey(
                        name: "fk_world_chunks_map_definitions_map_id",
                        column: x => x.map_id,
                        principalTable: "map_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "map_type_lookup",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 0, null, "Overworld" },
                    { 1, null, "Dungeon" },
                    { 2, null, "Cave" },
                    { 3, null, "BuildingInterior" },
                    { 4, null, "PocketDimension" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_map_transitions_source_map_id_source_x_source_y",
                table: "map_transitions",
                columns: new[] { "source_map_id", "source_x", "source_y" });

            migrationBuilder.CreateIndex(
                name: "ix_map_transitions_target_map_id",
                table: "map_transitions",
                column: "target_map_id");

            migrationBuilder.CreateIndex(
                name: "ix_world_chunks_map_id_chunk_x_chunk_y",
                table: "world_chunks",
                columns: new[] { "map_id", "chunk_x", "chunk_y" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "map_transitions");

            migrationBuilder.DropTable(
                name: "map_type_lookup");

            migrationBuilder.DropTable(
                name: "world_chunks");

            migrationBuilder.DropTable(
                name: "map_definitions");
        }
    }
}
