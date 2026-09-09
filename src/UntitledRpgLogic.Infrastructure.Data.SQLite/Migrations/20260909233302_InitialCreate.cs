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
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ability_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ambient_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ambient_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dimension_scale_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dimension_scale_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "effect_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_effect_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "elements",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_elements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "entity_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    classification = table.Column<int>(type: "INTEGER", nullable: false),
                    base_level = table.Column<int>(type: "INTEGER", nullable: false),
                    base_health = table.Column<float>(type: "REAL", nullable: false),
                    base_mana = table.Column<float>(type: "REAL", nullable: false),
                    base_stamina = table.Column<float>(type: "REAL", nullable: false),
                    innate_skill_definition_ids = table.Column<byte[]>(type: "BLOB", nullable: false),
                    starting_item_definition_ids = table.Column<byte[]>(type: "BLOB", nullable: false),
                    respiratory_profile = table.Column<string>(type: "TEXT", nullable: true),
                    starting_stats = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_definitions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fracture_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fracture_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "item_subtype_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_subtype_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "item_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_type_lookup", x => x.id);
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
                name: "mass_scale_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mass_scale_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "material_slot_lookup",
                columns: table => new
                {
                    id = table.Column<byte>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_material_slot_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message_priority_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_priority_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modification_effects",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    flat_amount = table.Column<int>(type: "INTEGER", nullable: false),
                    percentage = table.Column<float>(type: "REAL", nullable: false),
                    percentage_of_max = table.Column<float>(type: "REAL", nullable: false),
                    positive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modification_effects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "quality_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quality_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "requirement_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requirement_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scaling_curve_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scaling_curve_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stat_variation_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stat_variation_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "state_of_matter_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_state_of_matter_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "targeting_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_targeting_type_lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "map_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    type = table.Column<int>(type: "INTEGER", nullable: false),
                    atmosphere = table.Column<string>(type: "TEXT", nullable: false),
                    baseline_ambients = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_map_definitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_map_definitions_map_type_lookup_type",
                        column: x => x.type,
                        principalTable: "map_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "modifier_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    is_permanent = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_positive = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_additive = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_multiplicative = table.Column<bool>(type: "INTEGER", nullable: false),
                    scales_on_base_value = table.Column<bool>(type: "INTEGER", nullable: false),
                    max_stacks = table.Column<int>(type: "INTEGER", nullable: false),
                    duration = table.Column<float>(type: "REAL", nullable: false),
                    lose_all_stacks_on_expiration = table.Column<bool>(type: "INTEGER", nullable: false),
                    priority = table.Column<int>(type: "INTEGER", nullable: false),
                    modifier_effect_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    stack_effect_id = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modifier_definitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_modifier_definitions_modification_effects_modifier_effect_id",
                        column: x => x.modifier_effect_id,
                        principalTable: "modification_effects",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_modifier_definitions_modification_effects_stack_effect_id",
                        column: x => x.stack_effect_id,
                        principalTable: "modification_effects",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "skill_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    max_level = table.Column<int>(type: "INTEGER", nullable: false),
                    scaling_factor_a = table.Column<float>(type: "REAL", nullable: false),
                    scaling_factor_b = table.Column<float>(type: "REAL", nullable: false),
                    scaling_factor_c = table.Column<float>(type: "REAL", nullable: false),
                    points_for_first_level = table.Column<int>(type: "INTEGER", nullable: false),
                    scaling_curve = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_skill_definitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_skill_definitions_scaling_curve_type_lookup_scaling_curve",
                        column: x => x.scaling_curve,
                        principalTable: "scaling_curve_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stat_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    has_changeable_value = table.Column<bool>(type: "INTEGER", nullable: false),
                    min_value = table.Column<int>(type: "INTEGER", nullable: false),
                    max_value = table.Column<int>(type: "INTEGER", nullable: false),
                    variation = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stat_definitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_stat_definitions_stat_variation_lookup_variation",
                        column: x => x.variation,
                        principalTable: "stat_variation_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "material_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    flags = table.Column<int>(type: "INTEGER", nullable: false),
                    default_state = table.Column<int>(type: "INTEGER", nullable: false),
                    electrical_properties = table.Column<string>(type: "TEXT", nullable: true),
                    fantastical_properties = table.Column<string>(type: "TEXT", nullable: true),
                    mechanical_properties = table.Column<string>(type: "TEXT", nullable: true),
                    smelt_yields = table.Column<string>(type: "TEXT", nullable: true),
                    thermal_properties = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_material_definitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_material_definitions_state_of_matter_lookup_default_state",
                        column: x => x.default_state,
                        principalTable: "state_of_matter_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entities",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    definition_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    map_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    position_x = table.Column<float>(type: "REAL", nullable: true),
                    position_y = table.Column<float>(type: "REAL", nullable: true),
                    rotation_yaw = table.Column<float>(type: "REAL", nullable: true),
                    position_map_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    affected_stats = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entities", x => x.id);
                    table.ForeignKey(
                        name: "fk_entities_entity_definitions_definition_id",
                        column: x => x.definition_id,
                        principalTable: "entity_definitions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_entities_map_definitions_position_map_id",
                        column: x => x.position_map_id,
                        principalTable: "map_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.CreateTable(
                name: "abilities",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    ability_type = table.Column<int>(type: "INTEGER", nullable: false),
                    targeting_type = table.Column<int>(type: "INTEGER", nullable: false),
                    affects_caster = table.Column<bool>(type: "INTEGER", nullable: false),
                    affects_allies = table.Column<bool>(type: "INTEGER", nullable: false),
                    number_of_targets = table.Column<int>(type: "INTEGER", nullable: false),
                    cast_time = table.Column<float>(type: "REAL", nullable: false),
                    skill_discipline_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abilities", x => x.id);
                    table.ForeignKey(
                        name: "fk_abilities_ability_type_lookup_ability_type",
                        column: x => x.ability_type,
                        principalTable: "ability_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_abilities_skill_definitions_skill_discipline_id",
                        column: x => x.skill_discipline_id,
                        principalTable: "skill_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_abilities_targeting_type_lookup_targeting_type",
                        column: x => x.targeting_type,
                        principalTable: "targeting_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "instanced_skills",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    skill_definition_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    experience_points = table.Column<int>(type: "INTEGER", nullable: false),
                    level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instanced_skills", x => x.id);
                    table.ForeignKey(
                        name: "fk_instanced_skills_skill_definitions_skill_definition_id",
                        column: x => x.skill_definition_id,
                        principalTable: "skill_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instanced_stats",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    stat_definition_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    base_value = table.Column<int>(type: "INTEGER", nullable: false),
                    apparent_value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instanced_stats", x => x.id);
                    table.ForeignKey(
                        name: "fk_instanced_stats_stat_definitions_stat_definition_id",
                        column: x => x.stat_definition_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "linked_stats",
                columns: table => new
                {
                    stat_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    depends_on_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ratio = table.Column<float>(type: "REAL", nullable: false),
                    stat_definition_id = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_linked_stats", x => new { x.stat_id, x.depends_on_id });
                    table.ForeignKey(
                        name: "fk_linked_stats_stat_definitions_depends_on_id",
                        column: x => x.depends_on_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_linked_stats_stat_definitions_stat_definition_id",
                        column: x => x.stat_definition_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_linked_stats_stat_definitions_stat_id",
                        column: x => x.stat_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "material_state_properties",
                columns: table => new
                {
                    material_definition_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    color = table.Column<string>(type: "TEXT", nullable: false),
                    state = table.Column<int>(type: "INTEGER", nullable: false),
                    mechanical_properties_density = table.Column<float>(type: "REAL", nullable: true),
                    mechanical_properties_hardness = table.Column<float>(type: "REAL", nullable: true),
                    mechanical_properties_toughness = table.Column<float>(type: "REAL", nullable: true),
                    mechanical_properties_stiffness = table.Column<float>(type: "REAL", nullable: true),
                    mechanical_properties_malleability = table.Column<float>(type: "REAL", nullable: true),
                    mechanical_properties_viscosity = table.Column<float>(type: "REAL", nullable: true),
                    mechanical_properties_surface_tension = table.Column<float>(type: "REAL", nullable: true),
                    mechanical_properties_adhesion = table.Column<float>(type: "REAL", nullable: true),
                    thermal_properties_melting_point = table.Column<float>(type: "REAL", nullable: true),
                    thermal_properties_boiling_point = table.Column<float>(type: "REAL", nullable: true),
                    thermal_properties_ignition_temperature = table.Column<float>(type: "REAL", nullable: true),
                    thermal_properties_thermal_conductivity = table.Column<float>(type: "REAL", nullable: true),
                    electrical_properties_conductivity = table.Column<float>(type: "REAL", nullable: true),
                    fantastical_properties_aetherial_conductivity = table.Column<float>(type: "REAL", nullable: true),
                    fantastical_properties_elemental_attunement = table.Column<string>(type: "TEXT", nullable: true),
                    fantastical_properties_mana_capacity = table.Column<float>(type: "REAL", nullable: true),
                    fantastical_properties_purity = table.Column<float>(type: "REAL", nullable: true),
                    fantastical_properties_luminosity = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_material_state_properties", x => new { x.material_definition_id, x.id });
                    table.ForeignKey(
                        name: "fk_material_state_properties_material_definitions_material_definition_id",
                        column: x => x.material_definition_id,
                        principalTable: "material_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_material_state_properties_state_of_matter_lookup_state",
                        column: x => x.state,
                        principalTable: "state_of_matter_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applied_modifiers",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    modifier_definition_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    stacks = table.Column<int>(type: "INTEGER", nullable: false),
                    applied_at = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    expires_at = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applied_modifiers", x => x.id);
                    table.ForeignKey(
                        name: "fk_applied_modifiers_entities_entity_id",
                        column: x => x.entity_id,
                        principalTable: "entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_applied_modifiers_modifier_definitions_modifier_definition_id",
                        column: x => x.modifier_definition_id,
                        principalTable: "modifier_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "effects",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    effect_type = table.Column<int>(type: "INTEGER", nullable: false),
                    duration = table.Column<float>(type: "REAL", nullable: false),
                    tick_interval = table.Column<float>(type: "REAL", nullable: false),
                    base_damage = table.Column<float>(type: "REAL", nullable: true),
                    damage_type = table.Column<int>(type: "INTEGER", nullable: true),
                    ignores_armor = table.Column<bool>(type: "INTEGER", nullable: true),
                    delay = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    base_heal_amount = table.Column<float>(type: "REAL", nullable: true),
                    can_overheal = table.Column<bool>(type: "INTEGER", nullable: true),
                    summon_entity_template_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    affected_ambients = table.Column<string>(type: "TEXT", nullable: true),
                    affected_stats = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_effects", x => x.id);
                    table.ForeignKey(
                        name: "fk_effects_effect_type_lookup_effect_type",
                        column: x => x.effect_type,
                        principalTable: "effect_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_effects_entities_summon_entity_template_id",
                        column: x => x.summon_entity_template_id,
                        principalTable: "entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_inventories",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_inventories", x => x.id);
                    table.ForeignKey(
                        name: "fk_entity_inventories_entities_entity_id",
                        column: x => x.entity_id,
                        principalTable: "entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    item_type = table.Column<int>(type: "INTEGER", nullable: false),
                    item_subtype = table.Column<int>(type: "INTEGER", nullable: false),
                    base_quality = table.Column<int>(type: "INTEGER", nullable: false),
                    max_stack_size = table.Column<int>(type: "INTEGER", nullable: false),
                    base_durability = table.Column<float>(type: "REAL", nullable: false),
                    weight = table.Column<float>(type: "REAL", nullable: false),
                    base_value = table.Column<int>(type: "INTEGER", nullable: false),
                    creator_entity_id = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_definitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_item_definitions_entities_creator_entity_id",
                        column: x => x.creator_entity_id,
                        principalTable: "entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_item_definitions_item_subtype_lookup_item_subtype",
                        column: x => x.item_subtype,
                        principalTable: "item_subtype_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_item_definitions_item_type_lookup_item_type",
                        column: x => x.item_type,
                        principalTable: "item_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_item_definitions_quality_lookup_base_quality",
                        column: x => x.base_quality,
                        principalTable: "quality_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "log_entries",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    event_id = table.Column<int>(type: "INTEGER", nullable: false),
                    entity_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    message = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
                    parameters = table.Column<string>(type: "TEXT", nullable: true),
                    category = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_log_entries_entities_entity_id",
                        column: x => x.entity_id,
                        principalTable: "entities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ability_casting_requirements",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    requirement_type = table.Column<int>(type: "INTEGER", nullable: false),
                    required_entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_needed = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_casting_requirements", x => x.id);
                    table.ForeignKey(
                        name: "fk_ability_casting_requirements_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ability_casting_requirements_requirement_type_lookup_requirement_type",
                        column: x => x.requirement_type,
                        principalTable: "requirement_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ability_failure_influences",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    amount_always_succeed = table.Column<float>(type: "REAL", nullable: false),
                    influence_scale = table.Column<float>(type: "REAL", nullable: false),
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    requirement_type = table.Column<int>(type: "INTEGER", nullable: false),
                    required_entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_needed = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_failure_influences", x => x.id);
                    table.ForeignKey(
                        name: "fk_ability_failure_influences_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ability_failure_influences_requirement_type_lookup_requirement_type",
                        column: x => x.requirement_type,
                        principalTable: "requirement_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ability_learning_requirements",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    requirement_type = table.Column<int>(type: "INTEGER", nullable: false),
                    required_entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_needed = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_learning_requirements", x => x.id);
                    table.ForeignKey(
                        name: "fk_ability_learning_requirements_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ability_learning_requirements_requirement_type_lookup_requirement_type",
                        column: x => x.requirement_type,
                        principalTable: "requirement_type_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ability_stat_costs",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    stat_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount = table.Column<float>(type: "REAL", nullable: false),
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ability_stat_costs", x => x.id);
                    table.ForeignKey(
                        name: "fk_ability_stat_costs_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ability_stat_costs_stat_definitions_stat_id",
                        column: x => x.stat_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entity_skills",
                columns: table => new
                {
                    entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    instanced_skill_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_skills", x => new { x.entity_id, x.instanced_skill_id });
                    table.ForeignKey(
                        name: "fk_entity_skills_entities_entity_id",
                        column: x => x.entity_id,
                        principalTable: "entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_entity_skills_instanced_skills_instanced_skill_id",
                        column: x => x.instanced_skill_id,
                        principalTable: "instanced_skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_stats",
                columns: table => new
                {
                    entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    instanced_stat_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_stats", x => new { x.entity_id, x.instanced_stat_id });
                    table.ForeignKey(
                        name: "fk_entity_stats_entities_entity_id",
                        column: x => x.entity_id,
                        principalTable: "entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_entity_stats_instanced_stats_instanced_stat_id",
                        column: x => x.instanced_stat_id,
                        principalTable: "instanced_stats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ability_active_effects",
                columns: table => new
                {
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    effect_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ability_active_effects", x => new { x.ability_id, x.effect_id });
                    table.ForeignKey(
                        name: "fk_ability_active_effects_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ability_active_effects_effects_effect_id",
                        column: x => x.effect_id,
                        principalTable: "effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ability_failure_effects",
                columns: table => new
                {
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    effect_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ability_failure_effects", x => new { x.ability_id, x.effect_id });
                    table.ForeignKey(
                        name: "fk_ability_failure_effects_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ability_failure_effects_effects_effect_id",
                        column: x => x.effect_id,
                        principalTable: "effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_definition_materials",
                columns: table => new
                {
                    item_definition_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    slot = table.Column<byte>(type: "INTEGER", nullable: false),
                    material_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    proportion = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_definition_materials", x => new { x.item_definition_id, x.id });
                    table.ForeignKey(
                        name: "fk_item_definition_materials_item_definitions_item_definition_id",
                        column: x => x.item_definition_id,
                        principalTable: "item_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_item_definition_materials_material_definitions_material_id",
                        column: x => x.material_id,
                        principalTable: "material_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_item_definition_materials_material_slot_lookup_slot",
                        column: x => x.slot,
                        principalTable: "material_slot_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "item_instances",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    item_definition_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    durability = table.Column<int>(type: "INTEGER", nullable: false),
                    primary_material_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    crafted_by_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    entity_inventory_id = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_instances", x => x.id);
                    table.ForeignKey(
                        name: "fk_item_instances_entity_inventories_entity_inventory_id",
                        column: x => x.entity_inventory_id,
                        principalTable: "entity_inventories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_item_instances_item_definitions_item_definition_id",
                        column: x => x.item_definition_id,
                        principalTable: "item_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_item_instances_material_definitions_primary_material_id",
                        column: x => x.primary_material_id,
                        principalTable: "material_definitions",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "ability_type_lookup",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Spell" },
                    { 2, null, "ActiveAbility" },
                    { 3, null, "PassiveAbility" }
                });

            migrationBuilder.InsertData(
                table: "ambient_type_lookup",
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 10, null, "Mg" },
                    { 11, null, "G" },
                    { 12, null, "Kg" }
                });

            migrationBuilder.InsertData(
                table: "material_slot_lookup",
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 0, null, "Normal" },
                    { 1, null, "Important" },
                    { 2, null, "Crucial" }
                });

            migrationBuilder.InsertData(
                table: "message_type_lookup",
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Linear" },
                    { 2, null, "Exponential" },
                    { 3, null, "Polynomial" }
                });

            migrationBuilder.InsertData(
                table: "stat_variation_lookup",
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "description", "name" },
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
                columns: new[] { "id", "has_changeable_value", "max_value", "min_value", "name", "variation" },
                values: new object[] { new byte[] { 1, 160, 135, 52, 154, 28, 237, 148, 44, 15, 243, 169, 111, 68, 183, 169 }, true, 2147483647, 0, "Level:Levels", 1 });

            migrationBuilder.CreateIndex(
                name: "ix_abilities_ability_type",
                table: "abilities",
                column: "ability_type");

            migrationBuilder.CreateIndex(
                name: "ix_abilities_skill_discipline_id",
                table: "abilities",
                column: "skill_discipline_id");

            migrationBuilder.CreateIndex(
                name: "ix_abilities_targeting_type",
                table: "abilities",
                column: "targeting_type");

            migrationBuilder.CreateIndex(
                name: "ix_ability_active_effects_effect_id",
                table: "ability_active_effects",
                column: "effect_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_casting_requirements_ability_id",
                table: "ability_casting_requirements",
                column: "ability_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_casting_requirements_requirement_type",
                table: "ability_casting_requirements",
                column: "requirement_type");

            migrationBuilder.CreateIndex(
                name: "ix_ability_failure_effects_effect_id",
                table: "ability_failure_effects",
                column: "effect_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_failure_influences_ability_id",
                table: "ability_failure_influences",
                column: "ability_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_failure_influences_requirement_type",
                table: "ability_failure_influences",
                column: "requirement_type");

            migrationBuilder.CreateIndex(
                name: "ix_ability_learning_requirements_ability_id",
                table: "ability_learning_requirements",
                column: "ability_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_learning_requirements_requirement_type",
                table: "ability_learning_requirements",
                column: "requirement_type");

            migrationBuilder.CreateIndex(
                name: "ix_ability_stat_costs_ability_id",
                table: "ability_stat_costs",
                column: "ability_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_stat_costs_stat_id",
                table: "ability_stat_costs",
                column: "stat_id");

            migrationBuilder.CreateIndex(
                name: "ix_applied_modifiers_entity_id",
                table: "applied_modifiers",
                column: "entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_applied_modifiers_modifier_definition_id",
                table: "applied_modifiers",
                column: "modifier_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_effects_effect_type",
                table: "effects",
                column: "effect_type");

            migrationBuilder.CreateIndex(
                name: "ix_effects_summon_entity_template_id",
                table: "effects",
                column: "summon_entity_template_id");

            migrationBuilder.CreateIndex(
                name: "ix_entities_definition_id",
                table: "entities",
                column: "definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_entities_map_id",
                table: "entities",
                column: "map_id");

            migrationBuilder.CreateIndex(
                name: "ix_entities_position_map_id",
                table: "entities",
                column: "position_map_id");

            migrationBuilder.CreateIndex(
                name: "ix_entity_inventories_entity_id",
                table: "entity_inventories",
                column: "entity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_entity_skills_instanced_skill_id",
                table: "entity_skills",
                column: "instanced_skill_id");

            migrationBuilder.CreateIndex(
                name: "ix_entity_stats_instanced_stat_id",
                table: "entity_stats",
                column: "instanced_stat_id");

            migrationBuilder.CreateIndex(
                name: "ix_instanced_skills_skill_definition_id",
                table: "instanced_skills",
                column: "skill_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_instanced_stats_stat_definition_id",
                table: "instanced_stats",
                column: "stat_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_definition_materials_material_id",
                table: "item_definition_materials",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_definition_materials_slot",
                table: "item_definition_materials",
                column: "slot");

            migrationBuilder.CreateIndex(
                name: "ix_item_definitions_base_quality",
                table: "item_definitions",
                column: "base_quality");

            migrationBuilder.CreateIndex(
                name: "ix_item_definitions_creator_entity_id",
                table: "item_definitions",
                column: "creator_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_definitions_item_subtype",
                table: "item_definitions",
                column: "item_subtype");

            migrationBuilder.CreateIndex(
                name: "ix_item_definitions_item_type",
                table: "item_definitions",
                column: "item_type");

            migrationBuilder.CreateIndex(
                name: "ix_item_instances_entity_inventory_id",
                table: "item_instances",
                column: "entity_inventory_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_instances_item_definition_id",
                table: "item_instances",
                column: "item_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_instances_primary_material_id",
                table: "item_instances",
                column: "primary_material_id");

            migrationBuilder.CreateIndex(
                name: "ix_linked_stats_depends_on_id",
                table: "linked_stats",
                column: "depends_on_id");

            migrationBuilder.CreateIndex(
                name: "ix_linked_stats_stat_definition_id",
                table: "linked_stats",
                column: "stat_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_log_entries_entity_id",
                table: "log_entries",
                column: "entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_map_definitions_type",
                table: "map_definitions",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_map_transitions_source_map_id_source_x_source_y",
                table: "map_transitions",
                columns: new[] { "source_map_id", "source_x", "source_y" });

            migrationBuilder.CreateIndex(
                name: "ix_map_transitions_target_map_id",
                table: "map_transitions",
                column: "target_map_id");

            migrationBuilder.CreateIndex(
                name: "ix_material_definitions_default_state",
                table: "material_definitions",
                column: "default_state");

            migrationBuilder.CreateIndex(
                name: "ix_material_state_properties_state",
                table: "material_state_properties",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "ix_modifier_definitions_modifier_effect_id",
                table: "modifier_definitions",
                column: "modifier_effect_id");

            migrationBuilder.CreateIndex(
                name: "ix_modifier_definitions_stack_effect_id",
                table: "modifier_definitions",
                column: "stack_effect_id");

            migrationBuilder.CreateIndex(
                name: "ix_skill_definitions_scaling_curve",
                table: "skill_definitions",
                column: "scaling_curve");

            migrationBuilder.CreateIndex(
                name: "ix_stat_definitions_variation",
                table: "stat_definitions",
                column: "variation");

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
