using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UntitledRpgLogic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_affected_ambient_effects_effect_id",
                table: "affected_ambient");

            migrationBuilder.DropForeignKey(
                name: "fk_casting_requirement_abilities_ability_id",
                table: "casting_requirement");

            migrationBuilder.DropForeignKey(
                name: "fk_failure_influence_abilities_ability_id",
                table: "failure_influence");

            migrationBuilder.DropForeignKey(
                name: "fk_learning_requirement_abilities_ability_id",
                table: "learning_requirement");

            migrationBuilder.DropForeignKey(
                name: "fk_linked_stats_stat_definitions_dependent_stat_id",
                table: "linked_stats");

            migrationBuilder.DropForeignKey(
                name: "fk_linked_stats_stat_definitions_linked_stat_id",
                table: "linked_stats");

            migrationBuilder.DropForeignKey(
                name: "fk_stat_cost_abilities_ability_id",
                table: "stat_cost");

            migrationBuilder.DropTable(
                name: "affected_stat");

            migrationBuilder.DropPrimaryKey(
                name: "pk_stat_cost",
                table: "stat_cost");

            migrationBuilder.DropPrimaryKey(
                name: "pk_learning_requirement",
                table: "learning_requirement");

            migrationBuilder.DropPrimaryKey(
                name: "pk_failure_influence",
                table: "failure_influence");

            migrationBuilder.DropPrimaryKey(
                name: "pk_casting_requirement",
                table: "casting_requirement");

            migrationBuilder.DropPrimaryKey(
                name: "pk_affected_ambient",
                table: "affected_ambient");

            migrationBuilder.RenameTable(
                name: "stat_cost",
                newName: "ability_stat_costs");

            migrationBuilder.RenameTable(
                name: "learning_requirement",
                newName: "ability_learning_requirements");

            migrationBuilder.RenameTable(
                name: "failure_influence",
                newName: "ability_failure_influences");

            migrationBuilder.RenameTable(
                name: "casting_requirement",
                newName: "ability_casting_requirements");

            migrationBuilder.RenameTable(
                name: "affected_ambient",
                newName: "effect_affected_ambients");

            migrationBuilder.RenameColumn(
                name: "linked_stat_id",
                table: "linked_stats",
                newName: "depends_on_id");

            migrationBuilder.RenameColumn(
                name: "dependent_stat_id",
                table: "linked_stats",
                newName: "stat_id");

            migrationBuilder.RenameIndex(
                name: "ix_linked_stats_linked_stat_id",
                table: "linked_stats",
                newName: "ix_linked_stats_depends_on_id");

            migrationBuilder.RenameIndex(
                name: "ix_stat_cost_ability_id",
                table: "ability_stat_costs",
                newName: "ix_ability_stat_costs_ability_id");

            migrationBuilder.RenameIndex(
                name: "ix_learning_requirement_ability_id",
                table: "ability_learning_requirements",
                newName: "ix_ability_learning_requirements_ability_id");

            migrationBuilder.RenameIndex(
                name: "ix_failure_influence_ability_id",
                table: "ability_failure_influences",
                newName: "ix_ability_failure_influences_ability_id");

            migrationBuilder.RenameIndex(
                name: "ix_casting_requirement_ability_id",
                table: "ability_casting_requirements",
                newName: "ix_ability_casting_requirements_ability_id");

            migrationBuilder.RenameIndex(
                name: "ix_affected_ambient_effect_id",
                table: "effect_affected_ambients",
                newName: "ix_effect_affected_ambients_effect_id");

            migrationBuilder.AddColumn<string>(
                name: "electrical_properties",
                table: "material_definitions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fantastical_properties",
                table: "material_definitions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mechanical_properties",
                table: "material_definitions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thermal_properties",
                table: "material_definitions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "affected_stats",
                table: "entities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "affected_stat_id1",
                table: "ability_stat_costs",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_ability_stat_costs",
                table: "ability_stat_costs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_ability_learning_requirements",
                table: "ability_learning_requirements",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_ability_failure_influences",
                table: "ability_failure_influences",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_ability_casting_requirements",
                table: "ability_casting_requirements",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_effect_affected_ambients",
                table: "effect_affected_ambients",
                column: "id");

            migrationBuilder.CreateTable(
                name: "ambients",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ambients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "effect_affected_stats",
                columns: table => new
                {
                    effect_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    stat_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_change = table.Column<float>(type: "REAL", nullable: false),
                    is_percentage = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_effect_affected_stats", x => new { x.effect_id, x.id });
                    table.ForeignKey(
                        name: "fk_effect_affected_stats_effects_effect_id",
                        column: x => x.effect_id,
                        principalTable: "effects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_effect_affected_stats_stat_definitions_stat_id",
                        column: x => x.stat_id,
                        principalTable: "stat_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "elements",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_elements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "material_state_properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    fantastical_properties_luminosity = table.Column<float>(type: "REAL", nullable: true),
                    material_definition_id = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_material_state_properties", x => x.id);
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

            migrationBuilder.CreateIndex(
                name: "ix_stat_definitions_variation",
                table: "stat_definitions",
                column: "variation");

            migrationBuilder.CreateIndex(
                name: "ix_effects_effect_type",
                table: "effects",
                column: "effect_type");

            migrationBuilder.CreateIndex(
                name: "ix_abilities_ability_type",
                table: "abilities",
                column: "ability_type");

            migrationBuilder.CreateIndex(
                name: "ix_abilities_target_type",
                table: "abilities",
                column: "target_type");

            migrationBuilder.CreateIndex(
                name: "ix_ability_stat_costs_affected_stat_id",
                table: "ability_stat_costs",
                column: "affected_stat_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_stat_costs_affected_stat_id1",
                table: "ability_stat_costs",
                column: "affected_stat_id1");

            migrationBuilder.CreateIndex(
                name: "ix_ability_learning_requirements_required_entity_id",
                table: "ability_learning_requirements",
                column: "required_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_learning_requirements_requirement_type",
                table: "ability_learning_requirements",
                column: "requirement_type");

            migrationBuilder.CreateIndex(
                name: "ix_ability_failure_influences_required_entity_id",
                table: "ability_failure_influences",
                column: "required_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_failure_influences_requirement_type",
                table: "ability_failure_influences",
                column: "requirement_type");

            migrationBuilder.CreateIndex(
                name: "ix_ability_casting_requirements_required_entity_id",
                table: "ability_casting_requirements",
                column: "required_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_ability_casting_requirements_requirement_type",
                table: "ability_casting_requirements",
                column: "requirement_type");

            migrationBuilder.CreateIndex(
                name: "ix_effect_affected_ambients_ambient_id",
                table: "effect_affected_ambients",
                column: "ambient_id");

            migrationBuilder.CreateIndex(
                name: "ix_effect_affected_stats_stat_id",
                table: "effect_affected_stats",
                column: "stat_id");

            migrationBuilder.CreateIndex(
                name: "ix_material_state_properties_material_definition_id",
                table: "material_state_properties",
                column: "material_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_material_state_properties_state",
                table: "material_state_properties",
                column: "state");

            migrationBuilder.AddForeignKey(
                name: "fk_abilities_ability_type_lookup_ability_type",
                table: "abilities",
                column: "ability_type",
                principalTable: "ability_type_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_abilities_target_type_lookup_target_type",
                table: "abilities",
                column: "target_type",
                principalTable: "target_type_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_casting_requirements_abilities_ability_id",
                table: "ability_casting_requirements",
                column: "ability_id",
                principalTable: "abilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_casting_requirements_entities_required_entity_id",
                table: "ability_casting_requirements",
                column: "required_entity_id",
                principalTable: "entities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_casting_requirements_requirement_type_lookup_requirement_type",
                table: "ability_casting_requirements",
                column: "requirement_type",
                principalTable: "requirement_type_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_failure_influences_abilities_ability_id",
                table: "ability_failure_influences",
                column: "ability_id",
                principalTable: "abilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_failure_influences_entities_required_entity_id",
                table: "ability_failure_influences",
                column: "required_entity_id",
                principalTable: "entities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_failure_influences_requirement_type_lookup_requirement_type",
                table: "ability_failure_influences",
                column: "requirement_type",
                principalTable: "requirement_type_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_learning_requirements_abilities_ability_id",
                table: "ability_learning_requirements",
                column: "ability_id",
                principalTable: "abilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_learning_requirements_entities_required_entity_id",
                table: "ability_learning_requirements",
                column: "required_entity_id",
                principalTable: "entities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_learning_requirements_requirement_type_lookup_requirement_type",
                table: "ability_learning_requirements",
                column: "requirement_type",
                principalTable: "requirement_type_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_stat_costs_abilities_ability_id",
                table: "ability_stat_costs",
                column: "ability_id",
                principalTable: "abilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_stat_costs_stat_definitions_affected_stat_id",
                table: "ability_stat_costs",
                column: "affected_stat_id",
                principalTable: "stat_definitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_ability_stat_costs_stat_definitions_affected_stat_id1",
                table: "ability_stat_costs",
                column: "affected_stat_id1",
                principalTable: "stat_definitions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_effect_affected_ambients_ambients_ambient_id",
                table: "effect_affected_ambients",
                column: "ambient_id",
                principalTable: "ambients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_effect_affected_ambients_effects_effect_id",
                table: "effect_affected_ambients",
                column: "effect_id",
                principalTable: "effects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_effects_effect_type_lookup_effect_type",
                table: "effects",
                column: "effect_type",
                principalTable: "effect_type_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_linked_stats_stat_definitions_depends_on_id",
                table: "linked_stats",
                column: "depends_on_id",
                principalTable: "stat_definitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_linked_stats_stat_definitions_stat_id",
                table: "linked_stats",
                column: "stat_id",
                principalTable: "stat_definitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stat_definitions_stat_variation_lookup_variation",
                table: "stat_definitions",
                column: "variation",
                principalTable: "stat_variation_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_abilities_ability_type_lookup_ability_type",
                table: "abilities");

            migrationBuilder.DropForeignKey(
                name: "fk_abilities_target_type_lookup_target_type",
                table: "abilities");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_casting_requirements_abilities_ability_id",
                table: "ability_casting_requirements");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_casting_requirements_entities_required_entity_id",
                table: "ability_casting_requirements");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_casting_requirements_requirement_type_lookup_requirement_type",
                table: "ability_casting_requirements");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_failure_influences_abilities_ability_id",
                table: "ability_failure_influences");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_failure_influences_entities_required_entity_id",
                table: "ability_failure_influences");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_failure_influences_requirement_type_lookup_requirement_type",
                table: "ability_failure_influences");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_learning_requirements_abilities_ability_id",
                table: "ability_learning_requirements");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_learning_requirements_entities_required_entity_id",
                table: "ability_learning_requirements");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_learning_requirements_requirement_type_lookup_requirement_type",
                table: "ability_learning_requirements");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_stat_costs_abilities_ability_id",
                table: "ability_stat_costs");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_stat_costs_stat_definitions_affected_stat_id",
                table: "ability_stat_costs");

            migrationBuilder.DropForeignKey(
                name: "fk_ability_stat_costs_stat_definitions_affected_stat_id1",
                table: "ability_stat_costs");

            migrationBuilder.DropForeignKey(
                name: "fk_effect_affected_ambients_ambients_ambient_id",
                table: "effect_affected_ambients");

            migrationBuilder.DropForeignKey(
                name: "fk_effect_affected_ambients_effects_effect_id",
                table: "effect_affected_ambients");

            migrationBuilder.DropForeignKey(
                name: "fk_effects_effect_type_lookup_effect_type",
                table: "effects");

            migrationBuilder.DropForeignKey(
                name: "fk_linked_stats_stat_definitions_depends_on_id",
                table: "linked_stats");

            migrationBuilder.DropForeignKey(
                name: "fk_linked_stats_stat_definitions_stat_id",
                table: "linked_stats");

            migrationBuilder.DropForeignKey(
                name: "fk_stat_definitions_stat_variation_lookup_variation",
                table: "stat_definitions");

            migrationBuilder.DropTable(
                name: "ambients");

            migrationBuilder.DropTable(
                name: "effect_affected_stats");

            migrationBuilder.DropTable(
                name: "elements");

            migrationBuilder.DropTable(
                name: "material_state_properties");

            migrationBuilder.DropIndex(
                name: "ix_stat_definitions_variation",
                table: "stat_definitions");

            migrationBuilder.DropIndex(
                name: "ix_effects_effect_type",
                table: "effects");

            migrationBuilder.DropIndex(
                name: "ix_abilities_ability_type",
                table: "abilities");

            migrationBuilder.DropIndex(
                name: "ix_abilities_target_type",
                table: "abilities");

            migrationBuilder.DropPrimaryKey(
                name: "pk_effect_affected_ambients",
                table: "effect_affected_ambients");

            migrationBuilder.DropIndex(
                name: "ix_effect_affected_ambients_ambient_id",
                table: "effect_affected_ambients");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ability_stat_costs",
                table: "ability_stat_costs");

            migrationBuilder.DropIndex(
                name: "ix_ability_stat_costs_affected_stat_id",
                table: "ability_stat_costs");

            migrationBuilder.DropIndex(
                name: "ix_ability_stat_costs_affected_stat_id1",
                table: "ability_stat_costs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ability_learning_requirements",
                table: "ability_learning_requirements");

            migrationBuilder.DropIndex(
                name: "ix_ability_learning_requirements_required_entity_id",
                table: "ability_learning_requirements");

            migrationBuilder.DropIndex(
                name: "ix_ability_learning_requirements_requirement_type",
                table: "ability_learning_requirements");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ability_failure_influences",
                table: "ability_failure_influences");

            migrationBuilder.DropIndex(
                name: "ix_ability_failure_influences_required_entity_id",
                table: "ability_failure_influences");

            migrationBuilder.DropIndex(
                name: "ix_ability_failure_influences_requirement_type",
                table: "ability_failure_influences");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ability_casting_requirements",
                table: "ability_casting_requirements");

            migrationBuilder.DropIndex(
                name: "ix_ability_casting_requirements_required_entity_id",
                table: "ability_casting_requirements");

            migrationBuilder.DropIndex(
                name: "ix_ability_casting_requirements_requirement_type",
                table: "ability_casting_requirements");

            migrationBuilder.DropColumn(
                name: "electrical_properties",
                table: "material_definitions");

            migrationBuilder.DropColumn(
                name: "fantastical_properties",
                table: "material_definitions");

            migrationBuilder.DropColumn(
                name: "mechanical_properties",
                table: "material_definitions");

            migrationBuilder.DropColumn(
                name: "thermal_properties",
                table: "material_definitions");

            migrationBuilder.DropColumn(
                name: "affected_stats",
                table: "entities");

            migrationBuilder.DropColumn(
                name: "affected_stat_id1",
                table: "ability_stat_costs");

            migrationBuilder.RenameTable(
                name: "effect_affected_ambients",
                newName: "affected_ambient");

            migrationBuilder.RenameTable(
                name: "ability_stat_costs",
                newName: "stat_cost");

            migrationBuilder.RenameTable(
                name: "ability_learning_requirements",
                newName: "learning_requirement");

            migrationBuilder.RenameTable(
                name: "ability_failure_influences",
                newName: "failure_influence");

            migrationBuilder.RenameTable(
                name: "ability_casting_requirements",
                newName: "casting_requirement");

            migrationBuilder.RenameColumn(
                name: "depends_on_id",
                table: "linked_stats",
                newName: "linked_stat_id");

            migrationBuilder.RenameColumn(
                name: "stat_id",
                table: "linked_stats",
                newName: "dependent_stat_id");

            migrationBuilder.RenameIndex(
                name: "ix_linked_stats_depends_on_id",
                table: "linked_stats",
                newName: "ix_linked_stats_linked_stat_id");

            migrationBuilder.RenameIndex(
                name: "ix_effect_affected_ambients_effect_id",
                table: "affected_ambient",
                newName: "ix_affected_ambient_effect_id");

            migrationBuilder.RenameIndex(
                name: "ix_ability_stat_costs_ability_id",
                table: "stat_cost",
                newName: "ix_stat_cost_ability_id");

            migrationBuilder.RenameIndex(
                name: "ix_ability_learning_requirements_ability_id",
                table: "learning_requirement",
                newName: "ix_learning_requirement_ability_id");

            migrationBuilder.RenameIndex(
                name: "ix_ability_failure_influences_ability_id",
                table: "failure_influence",
                newName: "ix_failure_influence_ability_id");

            migrationBuilder.RenameIndex(
                name: "ix_ability_casting_requirements_ability_id",
                table: "casting_requirement",
                newName: "ix_casting_requirement_ability_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_affected_ambient",
                table: "affected_ambient",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_stat_cost",
                table: "stat_cost",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_learning_requirement",
                table: "learning_requirement",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_failure_influence",
                table: "failure_influence",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_casting_requirement",
                table: "casting_requirement",
                column: "id");

            migrationBuilder.CreateTable(
                name: "affected_stat",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    effect_id = table.Column<byte[]>(type: "BLOB", nullable: false),
                    amount_change = table.Column<float>(type: "REAL", nullable: false),
                    is_percentage = table.Column<bool>(type: "INTEGER", nullable: false),
                    stat_id = table.Column<byte[]>(type: "BLOB", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "ix_affected_stat_effect_id",
                table: "affected_stat",
                column: "effect_id");

            migrationBuilder.AddForeignKey(
                name: "fk_affected_ambient_effects_effect_id",
                table: "affected_ambient",
                column: "effect_id",
                principalTable: "effects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_casting_requirement_abilities_ability_id",
                table: "casting_requirement",
                column: "ability_id",
                principalTable: "abilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_failure_influence_abilities_ability_id",
                table: "failure_influence",
                column: "ability_id",
                principalTable: "abilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_learning_requirement_abilities_ability_id",
                table: "learning_requirement",
                column: "ability_id",
                principalTable: "abilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_linked_stats_stat_definitions_dependent_stat_id",
                table: "linked_stats",
                column: "dependent_stat_id",
                principalTable: "stat_definitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_linked_stats_stat_definitions_linked_stat_id",
                table: "linked_stats",
                column: "linked_stat_id",
                principalTable: "stat_definitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_stat_cost_abilities_ability_id",
                table: "stat_cost",
                column: "ability_id",
                principalTable: "abilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
