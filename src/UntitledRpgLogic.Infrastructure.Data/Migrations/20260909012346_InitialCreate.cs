using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UntitledRpgLogic.Infrastructure.Data.Migrations
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
                name: "ambients",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    ambient_type = table.Column<int>(type: "INTEGER", nullable: false),
                    value = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ambients", x => x.id);
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
                name: "effect_component_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_effect_component_type_lookup", x => x.id);
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
                name: "entities",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    affected_stats = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entities", x => x.id);
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
                name: "target_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 127, nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_target_type_lookup", x => x.id);
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
                name: "abilities",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    ability_type = table.Column<int>(type: "INTEGER", nullable: false),
                    target_type = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "fk_abilities_target_type_lookup_target_type",
                        column: x => x.target_type,
                        principalTable: "target_type_lookup",
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
                    entity_inventory_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    item_definition_id1 = table.Column<byte[]>(type: "BLOB", nullable: true)
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
                        name: "fk_item_instances_item_definitions_item_definition_id1",
                        column: x => x.item_definition_id1,
                        principalTable: "item_definitions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_item_instances_material_definitions_primary_material_id",
                        column: x => x.primary_material_id,
                        principalTable: "material_definitions",
                        principalColumn: "id");
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
                name: "ability_casting_requirements",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    requirement_type = table.Column<int>(type: "INTEGER", nullable: false),
                    required_entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_needed = table.Column<float>(type: "REAL", nullable: false),
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false)
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
                name: "ability_failure_influences",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    requirement_type = table.Column<int>(type: "INTEGER", nullable: false),
                    required_entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_always_succeed = table.Column<float>(type: "REAL", nullable: false),
                    influence_scale = table.Column<float>(type: "REAL", nullable: false),
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false)
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
                        name: "fk_ability_failure_influences_entities_required_entity_id",
                        column: x => x.required_entity_id,
                        principalTable: "entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                table: "effect_component_type_lookup",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Dimensions" },
                    { 2, null, "Physics" },
                    { 3, null, "Elemental" },
                    { 4, null, "Duration" },
                    { 5, null, "Targeting" },
                    { 6, null, "StatModification" },
                    { 7, null, "Summoning" }
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
                table: "target_type_lookup",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 0, null, "None" },
                    { 1, null, "Aoe" },
                    { 2, null, "Projectile" },
                    { 3, null, "Touch" },
                    { 4, null, "Self" }
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

            migrationBuilder.CreateIndex(
                name: "ix_abilities_ability_type",
                table: "abilities",
                column: "ability_type");

            migrationBuilder.CreateIndex(
                name: "ix_abilities_skill_discipline_id",
                table: "abilities",
                column: "skill_discipline_id");

            migrationBuilder.CreateIndex(
                name: "ix_abilities_target_type",
                table: "abilities",
                column: "target_type");

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
                name: "ix_ability_failure_influences_required_entity_id",
                table: "ability_failure_influences",
                column: "required_entity_id");

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
                name: "ix_item_instances_entity_inventory_id",
                table: "item_instances",
                column: "entity_inventory_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_instances_item_definition_id",
                table: "item_instances",
                column: "item_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_instances_item_definition_id1",
                table: "item_instances",
                column: "item_definition_id1");

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
                name: "ix_stat_definitions_variation",
                table: "stat_definitions",
                column: "variation");
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
                name: "ambients");

            migrationBuilder.DropTable(
                name: "applied_modifiers");

            migrationBuilder.DropTable(
                name: "dimension_scale_lookup");

            migrationBuilder.DropTable(
                name: "effect_component_type_lookup");

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
                name: "item_subtype_lookup");

            migrationBuilder.DropTable(
                name: "item_type_lookup");

            migrationBuilder.DropTable(
                name: "linked_stats");

            migrationBuilder.DropTable(
                name: "log_entries");

            migrationBuilder.DropTable(
                name: "mass_scale_lookup");

            migrationBuilder.DropTable(
                name: "material_state_properties");

            migrationBuilder.DropTable(
                name: "message_priority_lookup");

            migrationBuilder.DropTable(
                name: "message_type_lookup");

            migrationBuilder.DropTable(
                name: "quality_lookup");

            migrationBuilder.DropTable(
                name: "scaling_curve_type_lookup");

            migrationBuilder.DropTable(
                name: "targeting_type_lookup");

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
                name: "entity_inventories");

            migrationBuilder.DropTable(
                name: "item_definitions");

            migrationBuilder.DropTable(
                name: "material_definitions");

            migrationBuilder.DropTable(
                name: "state_of_matter_lookup");

            migrationBuilder.DropTable(
                name: "effect_type_lookup");

            migrationBuilder.DropTable(
                name: "ability_type_lookup");

            migrationBuilder.DropTable(
                name: "target_type_lookup");

            migrationBuilder.DropTable(
                name: "modification_effects");

            migrationBuilder.DropTable(
                name: "skill_definitions");

            migrationBuilder.DropTable(
                name: "stat_definitions");

            migrationBuilder.DropTable(
                name: "entities");

            migrationBuilder.DropTable(
                name: "stat_variation_lookup");
        }
    }
}
