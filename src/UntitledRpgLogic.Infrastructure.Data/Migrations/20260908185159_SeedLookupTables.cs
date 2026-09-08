using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UntitledRpgLogic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedLookupTables : Migration
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
                    { 8, null, "Junk" }
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
                    { 4, null, "Race" },
                    { 5, null, "Class" },
                    { 6, null, "Profession" },
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
                    { 3, null, "Gas" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ability_type_lookup");

            migrationBuilder.DropTable(
                name: "dimension_scale_lookup");

            migrationBuilder.DropTable(
                name: "effect_component_type_lookup");

            migrationBuilder.DropTable(
                name: "effect_type_lookup");

            migrationBuilder.DropTable(
                name: "fracture_type_lookup");

            migrationBuilder.DropTable(
                name: "item_subtype_lookup");

            migrationBuilder.DropTable(
                name: "item_type_lookup");

            migrationBuilder.DropTable(
                name: "mass_scale_lookup");

            migrationBuilder.DropTable(
                name: "message_priority_lookup");

            migrationBuilder.DropTable(
                name: "message_type_lookup");

            migrationBuilder.DropTable(
                name: "quality_lookup");

            migrationBuilder.DropTable(
                name: "requirement_type_lookup");

            migrationBuilder.DropTable(
                name: "scaling_curve_type_lookup");

            migrationBuilder.DropTable(
                name: "stat_variation_lookup");

            migrationBuilder.DropTable(
                name: "state_of_matter_lookup");

            migrationBuilder.DropTable(
                name: "target_type_lookup");

            migrationBuilder.DropTable(
                name: "targeting_type_lookup");
        }
    }
}
