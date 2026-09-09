using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UntitledRpgLogic.Infrastructure.Data.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ability_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ambient_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ambient_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dimension_scale_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dimension_scale_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "effect_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_effect_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "elements",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_elements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entity_definitions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Classification = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseHealth = table.Column<float>(type: "REAL", nullable: false),
                    BaseMana = table.Column<float>(type: "REAL", nullable: false),
                    BaseStamina = table.Column<float>(type: "REAL", nullable: false),
                    InnateSkillDefinitionIds = table.Column<byte[]>(type: "BLOB", nullable: false),
                    StartingItemDefinitionIds = table.Column<byte[]>(type: "BLOB", nullable: false),
                    RespiratoryProfile = table.Column<string>(type: "TEXT", nullable: true),
                    StartingStats = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_definitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fracture_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fracture_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "item_subtype_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_subtype_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "item_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "map_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mass_scale_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mass_scale_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "material_slot_lookup",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material_slot_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "message_priority_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message_priority_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "message_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modification_effects",
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
                    table.PrimaryKey("PK_modification_effects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "quality_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quality_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requirement_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requirement_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "scaling_curve_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scaling_curve_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stat_variation_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stat_variation_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "state_of_matter_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state_of_matter_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "targeting_type_lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_targeting_type_lookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "map_definitions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Atmosphere = table.Column<string>(type: "TEXT", nullable: false),
                    BaselineAmbients = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_map_definitions_map_type_lookup_Type",
                        column: x => x.Type,
                        principalTable: "map_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "modifier_definitions",
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
                    table.PrimaryKey("PK_modifier_definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_modifier_definitions_modification_effects_ModifierEffectId",
                        column: x => x.ModifierEffectId,
                        principalTable: "modification_effects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_modifier_definitions_modification_effects_StackEffectId",
                        column: x => x.StackEffectId,
                        principalTable: "modification_effects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "skill_definitions",
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
                    table.PrimaryKey("PK_skill_definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_skill_definitions_scaling_curve_type_lookup_ScalingCurve",
                        column: x => x.ScalingCurve,
                        principalTable: "scaling_curve_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stat_definitions",
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
                    table.PrimaryKey("PK_stat_definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stat_definitions_stat_variation_lookup_Variation",
                        column: x => x.Variation,
                        principalTable: "stat_variation_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "material_definitions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    Flags = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultState = table.Column<int>(type: "INTEGER", nullable: false),
                    ElectricalProperties = table.Column<string>(type: "TEXT", nullable: true),
                    FantasticalProperties = table.Column<string>(type: "TEXT", nullable: true),
                    MechanicalProperties = table.Column<string>(type: "TEXT", nullable: true),
                    SmeltYields = table.Column<string>(type: "TEXT", nullable: true),
                    ThermalProperties = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material_definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_material_definitions_state_of_matter_lookup_DefaultState",
                        column: x => x.DefaultState,
                        principalTable: "state_of_matter_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entities",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DefinitionId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    map_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    position_x = table.Column<float>(type: "REAL", nullable: true),
                    position_y = table.Column<float>(type: "REAL", nullable: true),
                    rotation_yaw = table.Column<float>(type: "REAL", nullable: true),
                    Position_MapId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    AffectedStats = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entities_entity_definitions_DefinitionId",
                        column: x => x.DefinitionId,
                        principalTable: "entity_definitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_entities_map_definitions_Position_MapId",
                        column: x => x.Position_MapId,
                        principalTable: "map_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "map_transitions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    SourceMapId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    SourceX = table.Column<float>(type: "REAL", nullable: false),
                    SourceY = table.Column<float>(type: "REAL", nullable: false),
                    TargetMapId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    TargetX = table.Column<float>(type: "REAL", nullable: false),
                    TargetY = table.Column<float>(type: "REAL", nullable: false),
                    TransitionTag = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_transitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_map_transitions_map_definitions_SourceMapId",
                        column: x => x.SourceMapId,
                        principalTable: "map_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_map_transitions_map_definitions_TargetMapId",
                        column: x => x.TargetMapId,
                        principalTable: "map_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "world_chunks",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    MapId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ChunkX = table.Column<int>(type: "INTEGER", nullable: false),
                    ChunkY = table.Column<int>(type: "INTEGER", nullable: false),
                    MaterialPalette = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CompressedTileBlob = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Version = table.Column<uint>(type: "INTEGER", nullable: false),
                    AmbientOverrides = table.Column<string>(type: "TEXT", nullable: true),
                    AtmosphereOverride = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_world_chunks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_world_chunks_map_definitions_MapId",
                        column: x => x.MapId,
                        principalTable: "map_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abilities",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AbilityType = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetingType = table.Column<int>(type: "INTEGER", nullable: false),
                    AffectsCaster = table.Column<bool>(type: "INTEGER", nullable: false),
                    AffectsAllies = table.Column<bool>(type: "INTEGER", nullable: false),
                    NumberOfTargets = table.Column<int>(type: "INTEGER", nullable: false),
                    CastTime = table.Column<float>(type: "REAL", nullable: false),
                    SkillDisciplineId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abilities_ability_type_lookup_AbilityType",
                        column: x => x.AbilityType,
                        principalTable: "ability_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abilities_skill_definitions_SkillDisciplineId",
                        column: x => x.SkillDisciplineId,
                        principalTable: "skill_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_abilities_targeting_type_lookup_TargetingType",
                        column: x => x.TargetingType,
                        principalTable: "targeting_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "instanced_skills",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    SkillDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ExperiencePoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instanced_skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_instanced_skills_skill_definitions_SkillDefinitionId",
                        column: x => x.SkillDefinitionId,
                        principalTable: "skill_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instanced_stats",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    StatDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    BaseValue = table.Column<int>(type: "INTEGER", nullable: false),
                    ApparentValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instanced_stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_instanced_stats_stat_definitions_StatDefinitionId",
                        column: x => x.StatDefinitionId,
                        principalTable: "stat_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "linked_stats",
                columns: table => new
                {
                    StatId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    DependsOnId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Ratio = table.Column<float>(type: "REAL", nullable: false),
                    StatDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linked_stats", x => new { x.StatId, x.DependsOnId });
                    table.ForeignKey(
                        name: "FK_linked_stats_stat_definitions_DependsOnId",
                        column: x => x.DependsOnId,
                        principalTable: "stat_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_linked_stats_stat_definitions_StatDefinitionId",
                        column: x => x.StatDefinitionId,
                        principalTable: "stat_definitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_linked_stats_stat_definitions_StatId",
                        column: x => x.StatId,
                        principalTable: "stat_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "material_state_properties",
                columns: table => new
                {
                    MaterialDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    MechanicalProperties_Density = table.Column<float>(type: "REAL", nullable: true),
                    MechanicalProperties_Hardness = table.Column<float>(type: "REAL", nullable: true),
                    MechanicalProperties_Toughness = table.Column<float>(type: "REAL", nullable: true),
                    MechanicalProperties_Stiffness = table.Column<float>(type: "REAL", nullable: true),
                    MechanicalProperties_Malleability = table.Column<float>(type: "REAL", nullable: true),
                    MechanicalProperties_Viscosity = table.Column<float>(type: "REAL", nullable: true),
                    MechanicalProperties_SurfaceTension = table.Column<float>(type: "REAL", nullable: true),
                    MechanicalProperties_Adhesion = table.Column<float>(type: "REAL", nullable: true),
                    ThermalProperties_MeltingPoint = table.Column<float>(type: "REAL", nullable: true),
                    ThermalProperties_BoilingPoint = table.Column<float>(type: "REAL", nullable: true),
                    ThermalProperties_IgnitionTemperature = table.Column<float>(type: "REAL", nullable: true),
                    ThermalProperties_ThermalConductivity = table.Column<float>(type: "REAL", nullable: true),
                    ElectricalProperties_Conductivity = table.Column<float>(type: "REAL", nullable: true),
                    FantasticalProperties_AetherialConductivity = table.Column<float>(type: "REAL", nullable: true),
                    FantasticalProperties_ElementalAttunement = table.Column<string>(type: "TEXT", nullable: true),
                    FantasticalProperties_ManaCapacity = table.Column<float>(type: "REAL", nullable: true),
                    FantasticalProperties_Purity = table.Column<float>(type: "REAL", nullable: true),
                    FantasticalProperties_Luminosity = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material_state_properties", x => new { x.MaterialDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_material_state_properties_material_definitions_MaterialDefinitionId",
                        column: x => x.MaterialDefinitionId,
                        principalTable: "material_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_material_state_properties_state_of_matter_lookup_State",
                        column: x => x.State,
                        principalTable: "state_of_matter_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applied_modifiers",
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
                    table.PrimaryKey("PK_applied_modifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_applied_modifiers_entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_applied_modifiers_modifier_definitions_ModifierDefinitionId",
                        column: x => x.ModifierDefinitionId,
                        principalTable: "modifier_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "effects",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    EffectType = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<float>(type: "REAL", nullable: false),
                    TickInterval = table.Column<float>(type: "REAL", nullable: false),
                    BaseDamage = table.Column<float>(type: "REAL", nullable: true),
                    DamageType = table.Column<int>(type: "INTEGER", nullable: true),
                    IgnoresArmor = table.Column<bool>(type: "INTEGER", nullable: true),
                    Delay = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    BaseHealAmount = table.Column<float>(type: "REAL", nullable: true),
                    CanOverheal = table.Column<bool>(type: "INTEGER", nullable: true),
                    SummonEntityTemplateId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    AffectedAmbients = table.Column<string>(type: "TEXT", nullable: true),
                    AffectedStats = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_effects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_effects_effect_type_lookup_EffectType",
                        column: x => x.EffectType,
                        principalTable: "effect_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_effects_entities_SummonEntityTemplateId",
                        column: x => x.SummonEntityTemplateId,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_inventories",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    EntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entity_inventories_entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_definitions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    ItemType = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemSubtype = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseQuality = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxStackSize = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseDurability = table.Column<float>(type: "REAL", nullable: false),
                    Weight = table.Column<float>(type: "REAL", nullable: false),
                    BaseValue = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatorEntityId = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_item_definitions_entities_CreatorEntityId",
                        column: x => x.CreatorEntityId,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_item_definitions_item_subtype_lookup_ItemSubtype",
                        column: x => x.ItemSubtype,
                        principalTable: "item_subtype_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_item_definitions_item_type_lookup_ItemType",
                        column: x => x.ItemType,
                        principalTable: "item_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_item_definitions_quality_lookup_BaseQuality",
                        column: x => x.BaseQuality,
                        principalTable: "quality_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "log_entries",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Message = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
                    Parameters = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_log_entries_entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "entities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ability_casting_requirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AbilityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    RequirementType = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredEntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    AmountNeeded = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_casting_requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ability_casting_requirements_abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ability_casting_requirements_requirement_type_lookup_RequirementType",
                        column: x => x.RequirementType,
                        principalTable: "requirement_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ability_failure_influences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AmountAlwaysSucceed = table.Column<float>(type: "REAL", nullable: false),
                    InfluenceScale = table.Column<float>(type: "REAL", nullable: false),
                    AbilityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    RequirementType = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredEntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    AmountNeeded = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_failure_influences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ability_failure_influences_abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ability_failure_influences_requirement_type_lookup_RequirementType",
                        column: x => x.RequirementType,
                        principalTable: "requirement_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ability_learning_requirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AbilityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    RequirementType = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredEntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    AmountNeeded = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_learning_requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ability_learning_requirements_abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ability_learning_requirements_requirement_type_lookup_RequirementType",
                        column: x => x.RequirementType,
                        principalTable: "requirement_type_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ability_stat_costs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Amount = table.Column<float>(type: "REAL", nullable: false),
                    AbilityId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_stat_costs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ability_stat_costs_abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ability_stat_costs_stat_definitions_StatId",
                        column: x => x.StatId,
                        principalTable: "stat_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entity_skills",
                columns: table => new
                {
                    EntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    InstancedSkillId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_skills", x => new { x.EntityId, x.InstancedSkillId });
                    table.ForeignKey(
                        name: "FK_entity_skills_entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entity_skills_instanced_skills_InstancedSkillId",
                        column: x => x.InstancedSkillId,
                        principalTable: "instanced_skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_stats",
                columns: table => new
                {
                    EntityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    InstancedStatId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_stats", x => new { x.EntityId, x.InstancedStatId });
                    table.ForeignKey(
                        name: "FK_entity_stats_entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entity_stats_instanced_stats_InstancedStatId",
                        column: x => x.InstancedStatId,
                        principalTable: "instanced_stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ability_active_effects",
                columns: table => new
                {
                    AbilityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    EffectId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_active_effects", x => new { x.AbilityId, x.EffectId });
                    table.ForeignKey(
                        name: "FK_ability_active_effects_abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ability_active_effects_effects_EffectId",
                        column: x => x.EffectId,
                        principalTable: "effects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ability_failure_effects",
                columns: table => new
                {
                    AbilityId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    EffectId = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_failure_effects", x => new { x.AbilityId, x.EffectId });
                    table.ForeignKey(
                        name: "FK_ability_failure_effects_abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ability_failure_effects_effects_EffectId",
                        column: x => x.EffectId,
                        principalTable: "effects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_definition_materials",
                columns: table => new
                {
                    ItemDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Slot = table.Column<byte>(type: "INTEGER", nullable: false),
                    MaterialId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Proportion = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_definition_materials", x => new { x.ItemDefinitionId, x.Id });
                    table.ForeignKey(
                        name: "FK_item_definition_materials_item_definitions_ItemDefinitionId",
                        column: x => x.ItemDefinitionId,
                        principalTable: "item_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_definition_materials_material_definitions_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "material_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_item_definition_materials_material_slot_lookup_Slot",
                        column: x => x.Slot,
                        principalTable: "material_slot_lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "item_instances",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ItemDefinitionId = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Durability = table.Column<int>(type: "INTEGER", nullable: false),
                    PrimaryMaterialId = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CraftedById = table.Column<byte[]>(type: "BLOB", nullable: true),
                    EntityInventoryId = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_instances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_item_instances_entity_inventories_EntityInventoryId",
                        column: x => x.EntityInventoryId,
                        principalTable: "entity_inventories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_item_instances_item_definitions_ItemDefinitionId",
                        column: x => x.ItemDefinitionId,
                        principalTable: "item_definitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_instances_material_definitions_PrimaryMaterialId",
                        column: x => x.PrimaryMaterialId,
                        principalTable: "material_definitions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ability_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Spell" },
                    { 2, null, "ActiveAbility" },
                    { 3, null, "PassiveAbility" }
                });

            migrationBuilder.InsertData(
                table: "ambient_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Humidity" },
                    { 2, null, "Precipitation" },
                    { 3, null, "Drainage" },
                    { 4, null, "Temperature" },
                    { 10, null, "Gravity" },
                    { 11, null, "WindSpeed" },
                    { 12, null, "WindDirection" },
                    { 20, null, "SunlightExposure" },
                    { 21, null, "AmbientLightLevel" },
                    { 22, null, "AcousticNoise" },
                    { 23, null, "Vibration" },
                    { 30, null, "ManaDensity" },
                    { 31, null, "ElementalAttunement" }
                });

            migrationBuilder.InsertData(
                table: "dimension_scale_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 5, null, "Mm" },
                    { 6, null, "Cm" },
                    { 7, null, "M" },
                    { 8, null, "Km" }
                });

            migrationBuilder.InsertData(
                table: "effect_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Summon" },
                    { 2, null, "Enchant" },
                    { 3, null, "Charm" },
                    { 4, null, "Heal" },
                    { 5, null, "Damage" },
                    { 6, null, "Buff" },
                    { 7, null, "Debuff" },
                    { 8, null, "Elemental" },
                    { 9, null, "Movement" },
                    { 10, null, "Transformation" }
                });

            migrationBuilder.InsertData(
                table: "fracture_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Brittle" },
                    { 2, null, "Malleable" },
                    { 3, null, "Ductile" },
                    { 4, null, "Sectile" },
                    { 5, null, "Flexible" },
                    { 6, null, "Elastic" }
                });

            migrationBuilder.InsertData(
                table: "item_subtype_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "ShortSword" },
                    { 2, null, "Dagger" },
                    { 3, null, "HandAxe" },
                    { 4, null, "RecurveBow" },
                    { 5, null, "Staff" },
                    { 6, null, "Helmet" },
                    { 7, null, "Chest" },
                    { 8, null, "Legs" },
                    { 9, null, "Boots" },
                    { 10, null, "Gloves" },
                    { 11, null, "Ring" },
                    { 12, null, "Amulet" },
                    { 13, null, "Potion" },
                    { 14, null, "Scroll" },
                    { 15, null, "Arrow" },
                    { 16, null, "Bullet" },
                    { 17, null, "Quiver" },
                    { 18, null, "Wand" },
                    { 19, null, "Mace" },
                    { 20, null, "Spear" },
                    { 21, null, "Crossbow" },
                    { 22, null, "Longbow" },
                    { 23, null, "Shortbow" },
                    { 24, null, "WarAxe" },
                    { 25, null, "LongSword" },
                    { 26, null, "GreatSword" },
                    { 27, null, "BastardSword" },
                    { 28, null, "Rapier" },
                    { 29, null, "Katana" },
                    { 30, null, "Scimitar" },
                    { 31, null, "Cutlass" },
                    { 32, null, "BattleAxe" },
                    { 33, null, "GreatAxe" },
                    { 34, null, "GreatHammer" },
                    { 35, null, "GreatStaff" },
                    { 36, null, "Lance" },
                    { 37, null, "Pike" },
                    { 38, null, "Javelin" },
                    { 39, null, "Halberd" },
                    { 40, null, "Glaive" },
                    { 41, null, "Trident" },
                    { 42, null, "Bolt" },
                    { 43, null, "Circlet" },
                    { 44, null, "Sling" },
                    { 45, null, "CompositeBow" },
                    { 46, null, "Arquebus" },
                    { 47, null, "HandCannon" },
                    { 48, null, "Pistol" },
                    { 49, null, "Revolver" },
                    { 50, null, "Blunderbuss" },
                    { 51, null, "ClockworkCrossbow" },
                    { 52, null, "Rifle" }
                });

            migrationBuilder.InsertData(
                table: "item_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Weapon" },
                    { 2, null, "Armor" },
                    { 3, null, "Shield" },
                    { 4, null, "Apparel" },
                    { 5, null, "Consumable" },
                    { 6, null, "Ammunition" },
                    { 7, null, "Tool" },
                    { 8, null, "Junk" },
                    { 9, null, "Miscellaneous" }
                });

            migrationBuilder.InsertData(
                table: "map_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "Overworld" },
                    { 1, null, "Dungeon" },
                    { 2, null, "Cave" },
                    { 3, null, "BuildingInterior" },
                    { 4, null, "PocketDimension" }
                });

            migrationBuilder.InsertData(
                table: "mass_scale_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 10, null, "Mg" },
                    { 11, null, "G" },
                    { 12, null, "Kg" }
                });

            migrationBuilder.InsertData(
                table: "material_slot_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { (byte)0, null, "Primary" },
                    { (byte)1, null, "Secondary" },
                    { (byte)2, null, "Tertiary" },
                    { (byte)3, null, "Gemstone" },
                    { (byte)4, null, "Coating" }
                });

            migrationBuilder.InsertData(
                table: "message_priority_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "Normal" },
                    { 1, null, "Important" },
                    { 2, null, "Crucial" }
                });

            migrationBuilder.InsertData(
                table: "message_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Ack" },
                    { 2, null, "AuthRequest" },
                    { 3, null, "AuthResponse" },
                    { 4, null, "ClientReady" },
                    { 5, null, "Disconnect" },
                    { 100, null, "EntityStateUpdate" },
                    { 101, null, "SpawnEntity" },
                    { 102, null, "DespawnEntity" },
                    { 103, null, "StatUpdate" },
                    { 200, null, "UseAbility" },
                    { 201, null, "Interact" },
                    { 300, null, "MoveItem" },
                    { 301, null, "EquipItem" },
                    { 302, null, "UnequipItem" },
                    { 303, null, "LootItem" },
                    { 400, null, "GlobalChat" },
                    { 401, null, "Whisper" },
                    { 402, null, "PartyChat" },
                    { 403, null, "GuildChat" },
                    { 404, null, "PartyInvite" },
                    { 500, null, "TimeUpdate" },
                    { 501, null, "WeatherUpdate" }
                });

            migrationBuilder.InsertData(
                table: "quality_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Common" },
                    { 2, null, "Uncommon" },
                    { 3, null, "Rare" },
                    { 4, null, "Epic" },
                    { 5, null, "Legendary" },
                    { 6, null, "Mythic" },
                    { 7, null, "Artifact" },
                    { 10, null, "Unique" }
                });

            migrationBuilder.InsertData(
                table: "requirement_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Stat" },
                    { 2, null, "SkillLevel" },
                    { 3, null, "PlayerLevel" },
                    { 4, null, "RaceLevel" },
                    { 5, null, "ClassLevel" },
                    { 6, null, "ProfessionLevel" },
                    { 7, null, "OngoingSpell" }
                });

            migrationBuilder.InsertData(
                table: "scaling_curve_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Linear" },
                    { 2, null, "Exponential" },
                    { 3, null, "Polynomial" }
                });

            migrationBuilder.InsertData(
                table: "stat_variation_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Pseudo" },
                    { 3, null, "Complex" },
                    { 5, null, "Major" },
                    { 6, null, "Minor" }
                });

            migrationBuilder.InsertData(
                table: "state_of_matter_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Solid" },
                    { 2, null, "Liquid" },
                    { 3, null, "Gas" },
                    { 4, null, "Plasma" }
                });

            migrationBuilder.InsertData(
                table: "targeting_type_lookup",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "SingleTarget" },
                    { 2, null, "MultipleTarget" },
                    { 3, null, "AreaOfEffect" },
                    { 4, null, "Self" },
                    { 5, null, "Touch" }
                });

            migrationBuilder.InsertData(
                table: "stat_definitions",
                columns: new[] { "Id", "HasChangeableValue", "MaxValue", "MinValue", "Name", "Variation" },
                values: new object[] { new byte[] { 1, 160, 135, 52, 154, 28, 237, 148, 44, 15, 243, 169, 111, 68, 183, 169 }, true, 2147483647, 0, "Level:Levels", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_abilities_AbilityType",
                table: "abilities",
                column: "AbilityType");

            migrationBuilder.CreateIndex(
                name: "IX_abilities_SkillDisciplineId",
                table: "abilities",
                column: "SkillDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_abilities_TargetingType",
                table: "abilities",
                column: "TargetingType");

            migrationBuilder.CreateIndex(
                name: "IX_ability_active_effects_EffectId",
                table: "ability_active_effects",
                column: "EffectId");

            migrationBuilder.CreateIndex(
                name: "IX_ability_casting_requirements_AbilityId",
                table: "ability_casting_requirements",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ability_casting_requirements_RequirementType",
                table: "ability_casting_requirements",
                column: "RequirementType");

            migrationBuilder.CreateIndex(
                name: "IX_ability_failure_effects_EffectId",
                table: "ability_failure_effects",
                column: "EffectId");

            migrationBuilder.CreateIndex(
                name: "IX_ability_failure_influences_AbilityId",
                table: "ability_failure_influences",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ability_failure_influences_RequirementType",
                table: "ability_failure_influences",
                column: "RequirementType");

            migrationBuilder.CreateIndex(
                name: "IX_ability_learning_requirements_AbilityId",
                table: "ability_learning_requirements",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ability_learning_requirements_RequirementType",
                table: "ability_learning_requirements",
                column: "RequirementType");

            migrationBuilder.CreateIndex(
                name: "IX_ability_stat_costs_AbilityId",
                table: "ability_stat_costs",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ability_stat_costs_StatId",
                table: "ability_stat_costs",
                column: "StatId");

            migrationBuilder.CreateIndex(
                name: "IX_applied_modifiers_EntityId",
                table: "applied_modifiers",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_applied_modifiers_ModifierDefinitionId",
                table: "applied_modifiers",
                column: "ModifierDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_effects_EffectType",
                table: "effects",
                column: "EffectType");

            migrationBuilder.CreateIndex(
                name: "IX_effects_SummonEntityTemplateId",
                table: "effects",
                column: "SummonEntityTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_entities_DefinitionId",
                table: "entities",
                column: "DefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_entities_map_id",
                table: "entities",
                column: "map_id");

            migrationBuilder.CreateIndex(
                name: "IX_entities_Position_MapId",
                table: "entities",
                column: "Position_MapId");

            migrationBuilder.CreateIndex(
                name: "IX_entity_inventories_EntityId",
                table: "entity_inventories",
                column: "EntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_entity_skills_InstancedSkillId",
                table: "entity_skills",
                column: "InstancedSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_entity_stats_InstancedStatId",
                table: "entity_stats",
                column: "InstancedStatId");

            migrationBuilder.CreateIndex(
                name: "IX_instanced_skills_SkillDefinitionId",
                table: "instanced_skills",
                column: "SkillDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_instanced_stats_StatDefinitionId",
                table: "instanced_stats",
                column: "StatDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_item_definition_materials_MaterialId",
                table: "item_definition_materials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_item_definition_materials_Slot",
                table: "item_definition_materials",
                column: "Slot");

            migrationBuilder.CreateIndex(
                name: "IX_item_definitions_BaseQuality",
                table: "item_definitions",
                column: "BaseQuality");

            migrationBuilder.CreateIndex(
                name: "IX_item_definitions_CreatorEntityId",
                table: "item_definitions",
                column: "CreatorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_item_definitions_ItemSubtype",
                table: "item_definitions",
                column: "ItemSubtype");

            migrationBuilder.CreateIndex(
                name: "IX_item_definitions_ItemType",
                table: "item_definitions",
                column: "ItemType");

            migrationBuilder.CreateIndex(
                name: "IX_item_instances_EntityInventoryId",
                table: "item_instances",
                column: "EntityInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_item_instances_ItemDefinitionId",
                table: "item_instances",
                column: "ItemDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_item_instances_PrimaryMaterialId",
                table: "item_instances",
                column: "PrimaryMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_linked_stats_DependsOnId",
                table: "linked_stats",
                column: "DependsOnId");

            migrationBuilder.CreateIndex(
                name: "IX_linked_stats_StatDefinitionId",
                table: "linked_stats",
                column: "StatDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_log_entries_EntityId",
                table: "log_entries",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_map_definitions_Type",
                table: "map_definitions",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_map_transitions_SourceMapId_SourceX_SourceY",
                table: "map_transitions",
                columns: new[] { "SourceMapId", "SourceX", "SourceY" });

            migrationBuilder.CreateIndex(
                name: "IX_map_transitions_TargetMapId",
                table: "map_transitions",
                column: "TargetMapId");

            migrationBuilder.CreateIndex(
                name: "IX_material_definitions_DefaultState",
                table: "material_definitions",
                column: "DefaultState");

            migrationBuilder.CreateIndex(
                name: "IX_material_state_properties_State",
                table: "material_state_properties",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_modifier_definitions_ModifierEffectId",
                table: "modifier_definitions",
                column: "ModifierEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_modifier_definitions_StackEffectId",
                table: "modifier_definitions",
                column: "StackEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_skill_definitions_ScalingCurve",
                table: "skill_definitions",
                column: "ScalingCurve");

            migrationBuilder.CreateIndex(
                name: "IX_stat_definitions_Variation",
                table: "stat_definitions",
                column: "Variation");

            migrationBuilder.CreateIndex(
                name: "IX_world_chunks_MapId_ChunkX_ChunkY",
                table: "world_chunks",
                columns: new[] { "MapId", "ChunkX", "ChunkY" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ability_active_effects");

            migrationBuilder.DropTable(
                name: "ability_casting_requirements");

            migrationBuilder.DropTable(
                name: "ability_failure_effects");

            migrationBuilder.DropTable(
                name: "ability_failure_influences");

            migrationBuilder.DropTable(
                name: "ability_learning_requirements");

            migrationBuilder.DropTable(
                name: "ability_stat_costs");

            migrationBuilder.DropTable(
                name: "ambient_type_lookup");

            migrationBuilder.DropTable(
                name: "applied_modifiers");

            migrationBuilder.DropTable(
                name: "dimension_scale_lookup");

            migrationBuilder.DropTable(
                name: "elements");

            migrationBuilder.DropTable(
                name: "entity_skills");

            migrationBuilder.DropTable(
                name: "entity_stats");

            migrationBuilder.DropTable(
                name: "fracture_type_lookup");

            migrationBuilder.DropTable(
                name: "item_definition_materials");

            migrationBuilder.DropTable(
                name: "item_instances");

            migrationBuilder.DropTable(
                name: "linked_stats");

            migrationBuilder.DropTable(
                name: "log_entries");

            migrationBuilder.DropTable(
                name: "map_transitions");

            migrationBuilder.DropTable(
                name: "mass_scale_lookup");

            migrationBuilder.DropTable(
                name: "material_state_properties");

            migrationBuilder.DropTable(
                name: "message_priority_lookup");

            migrationBuilder.DropTable(
                name: "message_type_lookup");

            migrationBuilder.DropTable(
                name: "world_chunks");

            migrationBuilder.DropTable(
                name: "effects");

            migrationBuilder.DropTable(
                name: "requirement_type_lookup");

            migrationBuilder.DropTable(
                name: "abilities");

            migrationBuilder.DropTable(
                name: "modifier_definitions");

            migrationBuilder.DropTable(
                name: "instanced_skills");

            migrationBuilder.DropTable(
                name: "instanced_stats");

            migrationBuilder.DropTable(
                name: "material_slot_lookup");

            migrationBuilder.DropTable(
                name: "entity_inventories");

            migrationBuilder.DropTable(
                name: "item_definitions");

            migrationBuilder.DropTable(
                name: "material_definitions");

            migrationBuilder.DropTable(
                name: "effect_type_lookup");

            migrationBuilder.DropTable(
                name: "ability_type_lookup");

            migrationBuilder.DropTable(
                name: "targeting_type_lookup");

            migrationBuilder.DropTable(
                name: "modification_effects");

            migrationBuilder.DropTable(
                name: "skill_definitions");

            migrationBuilder.DropTable(
                name: "stat_definitions");

            migrationBuilder.DropTable(
                name: "entities");

            migrationBuilder.DropTable(
                name: "item_subtype_lookup");

            migrationBuilder.DropTable(
                name: "item_type_lookup");

            migrationBuilder.DropTable(
                name: "quality_lookup");

            migrationBuilder.DropTable(
                name: "state_of_matter_lookup");

            migrationBuilder.DropTable(
                name: "scaling_curve_type_lookup");

            migrationBuilder.DropTable(
                name: "stat_variation_lookup");

            migrationBuilder.DropTable(
                name: "entity_definitions");

            migrationBuilder.DropTable(
                name: "map_definitions");

            migrationBuilder.DropTable(
                name: "map_type_lookup");
        }
    }
}
