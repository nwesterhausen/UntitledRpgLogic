using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UntitledRpgLogic.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InstancedComplexKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InstancedStats",
                table: "InstancedStats");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InstancedStats");

            migrationBuilder.AddColumn<Guid>(
                name: "EntityId",
                table: "InstancedStats",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstancedStats",
                table: "InstancedStats",
                columns: new[] { "EntityId", "StatId" });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_InstancedStats_Entities_EntityId",
                table: "InstancedStats",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstancedStats_Entities_EntityId",
                table: "InstancedStats");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstancedStats",
                table: "InstancedStats");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "InstancedStats");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InstancedStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstancedStats",
                table: "InstancedStats",
                column: "Id");
        }
    }
}
