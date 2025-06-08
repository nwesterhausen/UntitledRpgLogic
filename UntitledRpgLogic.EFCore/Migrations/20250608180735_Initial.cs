using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UntitledRpgLogic.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstancedSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SkillDefinitionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExperiencePoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstancedSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModificationEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FlatAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    Percentage = table.Column<float>(type: "REAL", nullable: false),
                    PercentageOfMax = table.Column<float>(type: "REAL", nullable: false),
                    Positive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationEffects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MaxLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    LevelScalingA = table.Column<float>(type: "REAL", nullable: false),
                    LevelScalingB = table.Column<float>(type: "REAL", nullable: false),
                    LevelScalingC = table.Column<float>(type: "REAL", nullable: false),
                    PointsForFirstLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    ScalingCurve = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HasChangeableValue = table.Column<bool>(type: "INTEGER", nullable: false),
                    MinValue = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxValue = table.Column<int>(type: "INTEGER", nullable: false),
                    Variation = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntitySkills",
                columns: table => new
                {
                    EntityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    InstancedSkillId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySkills", x => new { x.EntityId, x.InstancedSkillId });
                    table.ForeignKey(
                        name: "FK_EntitySkills_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntitySkills_InstancedSkills_InstancedSkillId",
                        column: x => x.InstancedSkillId,
                        principalTable: "InstancedSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModifierDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsPermanent = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPositive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAdditive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsMultiplicative = table.Column<bool>(type: "INTEGER", nullable: false),
                    ScalesOnBaseValue = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxStacks = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<float>(type: "REAL", nullable: false),
                    LoseAllStacksOnExpiration = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    ModifierEffectId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StackEffectId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifierDefinitions_ModificationEffects_ModifierEffectId",
                        column: x => x.ModifierEffectId,
                        principalTable: "ModificationEffects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModifierDefinitions_ModificationEffects_StackEffectId",
                        column: x => x.StackEffectId,
                        principalTable: "ModificationEffects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstancedStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StatDefinitionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerEntityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BaseValue = table.Column<int>(type: "INTEGER", nullable: false),
                    ApparentValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstancedStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstancedStats_StatDefinitions_StatDefinitionId",
                        column: x => x.StatDefinitionId,
                        principalTable: "StatDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkedStats",
                columns: table => new
                {
                    DependentStatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LinkedStatId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedStats", x => new { x.DependentStatId, x.LinkedStatId });
                    table.ForeignKey(
                        name: "FK_LinkedStats_StatDefinitions_DependentStatId",
                        column: x => x.DependentStatId,
                        principalTable: "StatDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkedStats_StatDefinitions_LinkedStatId",
                        column: x => x.LinkedStatId,
                        principalTable: "StatDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityStats",
                columns: table => new
                {
                    EntityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    InstancedStatId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityStats", x => new { x.EntityId, x.InstancedStatId });
                    table.ForeignKey(
                        name: "FK_EntityStats_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityStats_InstancedStats_InstancedStatId",
                        column: x => x.InstancedStatId,
                        principalTable: "InstancedStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntitySkills_InstancedSkillId",
                table: "EntitySkills",
                column: "InstancedSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityStats_InstancedStatId",
                table: "EntityStats",
                column: "InstancedStatId");

            migrationBuilder.CreateIndex(
                name: "IX_InstancedStats_StatDefinitionId",
                table: "InstancedStats",
                column: "StatDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedStats_LinkedStatId",
                table: "LinkedStats",
                column: "LinkedStatId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierDefinitions_ModifierEffectId",
                table: "ModifierDefinitions",
                column: "ModifierEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierDefinitions_StackEffectId",
                table: "ModifierDefinitions",
                column: "StackEffectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntitySkills");

            migrationBuilder.DropTable(
                name: "EntityStats");

            migrationBuilder.DropTable(
                name: "LinkedStats");

            migrationBuilder.DropTable(
                name: "ModifierDefinitions");

            migrationBuilder.DropTable(
                name: "SkillDefinitions");

            migrationBuilder.DropTable(
                name: "InstancedSkills");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "InstancedStats");

            migrationBuilder.DropTable(
                name: "ModificationEffects");

            migrationBuilder.DropTable(
                name: "StatDefinitions");
        }
    }
}
