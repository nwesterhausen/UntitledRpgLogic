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
                name: "effects",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    effect_type = table.Column<int>(type: "INTEGER", nullable: false),
                    duration = table.Column<float>(type: "REAL", nullable: false),
                    damage_type_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    base_amount = table.Column<float>(type: "REAL", nullable: true),
                    entity_to_summon_id = table.Column<byte[]>(type: "BLOB", nullable: true),
                    quantity = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_effects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "entities",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "item_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    quality = table.Column<int>(type: "INTEGER", nullable: false),
                    item_type = table.Column<int>(type: "INTEGER", nullable: false),
                    item_subtype = table.Column<int>(type: "INTEGER", nullable: false),
                    stackable = table.Column<bool>(type: "INTEGER", nullable: false),
                    max_stack = table.Column<int>(type: "INTEGER", nullable: false),
                    max_durability = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_definitions", x => x.id);
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
                    message = table.Column<string>(type: "TEXT", nullable: false),
                    parameters = table.Column<string>(type: "TEXT", nullable: true),
                    category = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_entries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "material_definitions",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_material_definitions", x => x.id);
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
                });

            migrationBuilder.CreateTable(
                name: "affected_ambient",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    effect_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ambient_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_change = table.Column<float>(type: "REAL", nullable: false),
                    is_percentage = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_affected_ambient", x => x.id);
                    table.ForeignKey(
                        name: "fk_affected_ambient_effects_effect_id",
                        column: x => x.effect_id,
                        principalTable: "effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "affected_stat",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    effect_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    stat_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_change = table.Column<float>(type: "REAL", nullable: false),
                    is_percentage = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_affected_stat", x => x.id);
                    table.ForeignKey(
                        name: "fk_affected_stat_effects_effect_id",
                        column: x => x.effect_id,
                        principalTable: "effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_inventories",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    entity_id = table.Column<byte[]>(type: "BLOB", nullable: false)
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
                        name: "fk_abilities_skill_definitions_skill_discipline_id",
                        column: x => x.skill_discipline_id,
                        principalTable: "skill_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    dependent_stat_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    linked_stat_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ratio = table.Column<float>(type: "REAL", nullable: false),
                    stat_definition_id = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_linked_stats", x => new { x.dependent_stat_id, x.linked_stat_id });
                    table.ForeignKey(
                        name: "fk_linked_stats_stat_definitions_dependent_stat_id",
                        column: x => x.dependent_stat_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_linked_stats_stat_definitions_linked_stat_id",
                        column: x => x.linked_stat_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_linked_stats_stat_definitions_stat_definition_id",
                        column: x => x.stat_definition_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id");
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
                    entity_inventory_id = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "ability_active_effects",
                columns: table => new
                {
                    abilities_using_as_active_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    active_effects_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ability_active_effects", x => new { x.abilities_using_as_active_id, x.active_effects_id });
                    table.ForeignKey(
                        name: "fk_ability_active_effects_abilities_abilities_using_as_active_id",
                        column: x => x.abilities_using_as_active_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ability_active_effects_effects_active_effects_id",
                        column: x => x.active_effects_id,
                        principalTable: "effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ability_failure_effects",
                columns: table => new
                {
                    abilities_using_as_failure_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    failure_effects_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ability_failure_effects", x => new { x.abilities_using_as_failure_id, x.failure_effects_id });
                    table.ForeignKey(
                        name: "fk_ability_failure_effects_abilities_abilities_using_as_failure_id",
                        column: x => x.abilities_using_as_failure_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ability_failure_effects_effects_failure_effects_id",
                        column: x => x.failure_effects_id,
                        principalTable: "effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "casting_requirement",
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
                    table.PrimaryKey("pk_casting_requirement", x => x.id);
                    table.ForeignKey(
                        name: "fk_casting_requirement_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "failure_influence",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    requirement_type = table.Column<int>(type: "INTEGER", nullable: false),
                    required_entity_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_always_succeed = table.Column<float>(type: "REAL", nullable: false),
                    influence_scale = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_failure_influence", x => x.id);
                    table.ForeignKey(
                        name: "fk_failure_influence_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learning_requirement",
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
                    table.PrimaryKey("pk_learning_requirement", x => x.id);
                    table.ForeignKey(
                        name: "fk_learning_requirement_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stat_cost",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ability_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    affected_stat_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stat_cost", x => x.id);
                    table.ForeignKey(
                        name: "fk_stat_cost_abilities_ability_id",
                        column: x => x.ability_id,
                        principalTable: "abilities",
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

            migrationBuilder.CreateIndex(
                name: "ix_abilities_skill_discipline_id",
                table: "abilities",
                column: "skill_discipline_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_active_effects_active_effects_id",
                table: "ability_active_effects",
                column: "active_effects_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_failure_effects_failure_effects_id",
                table: "ability_failure_effects",
                column: "failure_effects_id");

            migrationBuilder.CreateIndex(
                name: "ix_affected_ambient_effect_id",
                table: "affected_ambient",
                column: "effect_id");

            migrationBuilder.CreateIndex(
                name: "ix_affected_stat_effect_id",
                table: "affected_stat",
                column: "effect_id");

            migrationBuilder.CreateIndex(
                name: "ix_applied_modifiers_entity_id",
                table: "applied_modifiers",
                column: "entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_applied_modifiers_modifier_definition_id",
                table: "applied_modifiers",
                column: "modifier_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_casting_requirement_ability_id",
                table: "casting_requirement",
                column: "ability_id");

            migrationBuilder.CreateIndex(
                name: "ix_entity_inventories_entity_id",
                table: "entity_inventories",
                column: "entity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_entity_skills_entity_id",
                table: "entity_skills",
                column: "entity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_entity_skills_instanced_skill_id",
                table: "entity_skills",
                column: "instanced_skill_id");

            migrationBuilder.CreateIndex(
                name: "ix_entity_stats_entity_id",
                table: "entity_stats",
                column: "entity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_entity_stats_instanced_stat_id",
                table: "entity_stats",
                column: "instanced_stat_id");

            migrationBuilder.CreateIndex(
                name: "ix_failure_influence_ability_id",
                table: "failure_influence",
                column: "ability_id");

            migrationBuilder.CreateIndex(
                name: "ix_instanced_skills_skill_definition_id",
                table: "instanced_skills",
                column: "skill_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_instanced_stats_stat_definition_id",
                table: "instanced_stats",
                column: "stat_definition_id");

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
                name: "ix_learning_requirement_ability_id",
                table: "learning_requirement",
                column: "ability_id");

            migrationBuilder.CreateIndex(
                name: "ix_linked_stats_linked_stat_id",
                table: "linked_stats",
                column: "linked_stat_id");

            migrationBuilder.CreateIndex(
                name: "ix_linked_stats_stat_definition_id",
                table: "linked_stats",
                column: "stat_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_modifier_definitions_modifier_effect_id",
                table: "modifier_definitions",
                column: "modifier_effect_id");

            migrationBuilder.CreateIndex(
                name: "ix_modifier_definitions_stack_effect_id",
                table: "modifier_definitions",
                column: "stack_effect_id");

            migrationBuilder.CreateIndex(
                name: "ix_stat_cost_ability_id",
                table: "stat_cost",
                column: "ability_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
	        ArgumentNullException.ThrowIfNull(migrationBuilder, nameof(migrationBuilder));

            migrationBuilder.DropTable(
                name: "ability_active_effects");

            migrationBuilder.DropTable(
                name: "ability_failure_effects");

            migrationBuilder.DropTable(
                name: "affected_ambient");

            migrationBuilder.DropTable(
                name: "affected_stat");

            migrationBuilder.DropTable(
                name: "applied_modifiers");

            migrationBuilder.DropTable(
                name: "casting_requirement");

            migrationBuilder.DropTable(
                name: "entity_skills");

            migrationBuilder.DropTable(
                name: "entity_stats");

            migrationBuilder.DropTable(
                name: "failure_influence");

            migrationBuilder.DropTable(
                name: "item_instances");

            migrationBuilder.DropTable(
                name: "learning_requirement");

            migrationBuilder.DropTable(
                name: "linked_stats");

            migrationBuilder.DropTable(
                name: "log_entries");

            migrationBuilder.DropTable(
                name: "stat_cost");

            migrationBuilder.DropTable(
                name: "effects");

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
                name: "abilities");

            migrationBuilder.DropTable(
                name: "modification_effects");

            migrationBuilder.DropTable(
                name: "stat_definitions");

            migrationBuilder.DropTable(
                name: "entities");

            migrationBuilder.DropTable(
                name: "skill_definitions");
        }
    }
}
