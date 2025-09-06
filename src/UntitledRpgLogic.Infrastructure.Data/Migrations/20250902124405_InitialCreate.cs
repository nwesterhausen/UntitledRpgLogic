using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UntitledRpgLogic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        ArgumentNullException.ThrowIfNull(migrationBuilder, nameof(migrationBuilder));

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Identifier = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "ItemDefinitions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Quality = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemType = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemSubtype = table.Column<int>(type: "INTEGER", nullable: false),
                    Stackable = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxStack = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxDurability = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialDefinitions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModificationEffects",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
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
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MaxLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    ScalingFactorA = table.Column<float>(type: "REAL", nullable: false),
                    ScalingFactorB = table.Column<float>(type: "REAL", nullable: false),
                    ScalingFactorC = table.Column<float>(type: "REAL", nullable: false),
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
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
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
                name: "ItemInstances",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ItemDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Durability = table.Column<int>(type: "INTEGER", nullable: false),
                    PrimaryMaterialId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CraftedById = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInstances_ItemDefinitions_ItemDefinitionId",
                        column: x => x.ItemDefinitionId,
                        principalTable: "ItemDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemInstances_MaterialDefinitions_PrimaryMaterialId",
                        column: x => x.PrimaryMaterialId,
                        principalTable: "MaterialDefinitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModifierDefinitions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
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
                    ModifierEffectId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    StackEffectId = table.Column<byte[]>(type: "BLOB", nullable: true)
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
                name: "InstancedSkills",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    SkillDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ExperiencePoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstancedSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstancedSkills_SkillDefinitions_SkillDefinitionId",
                        column: x => x.SkillDefinitionId,
                        principalTable: "SkillDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstancedStats",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    StatDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
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
                    DependentStatId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    LinkedStatId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Ratio = table.Column<float>(type: "REAL", nullable: false),
                    StatDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_LinkedStats_StatDefinitions_StatDefinitionId",
                        column: x => x.StatDefinitionId,
                        principalTable: "StatDefinitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntityInventories",
                columns: table => new
                {
                    EntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ItemInstanceId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityInventories", x => new { x.EntityId, x.ItemInstanceId });
                    table.ForeignKey(
                        name: "FK_EntityInventories_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityInventories_ItemInstances_ItemInstanceId",
                        column: x => x.ItemInstanceId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedModifiers",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ModifierDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    EntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Stacks = table.Column<int>(type: "INTEGER", nullable: false),
                    AppliedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppliedModifiers_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedModifiers_ModifierDefinitions_ModifierDefinitionId",
                        column: x => x.ModifierDefinitionId,
                        principalTable: "ModifierDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntitySkills",
                columns: table => new
                {
                    EntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    InstancedSkillId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySkills", x => new { x.EntityId, x.InstancedSkillId });
                    table.ForeignKey(
                        name: "FK_EntitySkills_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntitySkills_InstancedSkills_InstancedSkillId",
                        column: x => x.InstancedSkillId,
                        principalTable: "InstancedSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityStats",
                columns: table => new
                {
                    EntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    InstancedStatId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityStats", x => new { x.EntityId, x.InstancedStatId });
                    table.ForeignKey(
                        name: "FK_EntityStats_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityStats_InstancedStats_InstancedStatId",
                        column: x => x.InstancedStatId,
                        principalTable: "InstancedStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedModifiers_EntityId",
                table: "AppliedModifiers",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedModifiers_ModifierDefinitionId",
                table: "AppliedModifiers",
                column: "ModifierDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityInventories_ItemInstanceId",
                table: "EntityInventories",
                column: "ItemInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySkills_InstancedSkillId",
                table: "EntitySkills",
                column: "InstancedSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityStats_InstancedStatId",
                table: "EntityStats",
                column: "InstancedStatId");

            migrationBuilder.CreateIndex(
                name: "IX_InstancedSkills_SkillDefinitionId",
                table: "InstancedSkills",
                column: "SkillDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstancedStats_StatDefinitionId",
                table: "InstancedStats",
                column: "StatDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInstances_ItemDefinitionId",
                table: "ItemInstances",
                column: "ItemDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInstances_PrimaryMaterialId",
                table: "ItemInstances",
                column: "PrimaryMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedStats_LinkedStatId",
                table: "LinkedStats",
                column: "LinkedStatId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedStats_StatDefinitionId",
                table: "LinkedStats",
                column: "StatDefinitionId");

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
	        ArgumentNullException.ThrowIfNull(migrationBuilder, nameof(migrationBuilder));

            migrationBuilder.DropTable(
                name: "AppliedModifiers");

            migrationBuilder.DropTable(
                name: "EntityInventories");

            migrationBuilder.DropTable(
                name: "EntitySkills");

            migrationBuilder.DropTable(
                name: "EntityStats");

            migrationBuilder.DropTable(
                name: "LinkedStats");

            migrationBuilder.DropTable(
                name: "ModifierDefinitions");

            migrationBuilder.DropTable(
                name: "ItemInstances");

            migrationBuilder.DropTable(
                name: "InstancedSkills");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "InstancedStats");

            migrationBuilder.DropTable(
                name: "ModificationEffects");

            migrationBuilder.DropTable(
                name: "ItemDefinitions");

            migrationBuilder.DropTable(
                name: "MaterialDefinitions");

            migrationBuilder.DropTable(
                name: "SkillDefinitions");

            migrationBuilder.DropTable(
                name: "StatDefinitions");
        }
    }
}
