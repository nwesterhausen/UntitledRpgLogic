# UntitledRpgLogic

Game logic and data management layer for UntitledRpg, a moddable RPG framework with a Godot front-end (UntitledRpg - not yet public).

## **UntitledRpgLogic Engine To-Do List**

### **Phase 1: Core Architecture & Foundation**

* [x] **Configuration System:** Implement a system to load and access game-wide settings from TOML files.
	* *Status: The TomlConfigHandler and associated configuration models in .Core and .Infrastructure.Configuration are complete.*
* [ ] **Dependency Injection:** Finalize service registration and lifetime management for all core services.
	* *Status: Service collection extensions exist, but this will be an ongoing task as new services are added.*
* [ ] **Logging Framework:** Fully integrate the structured logging library from UntitledRpgLogic.Extensions.Logging across all services to
  capture engine events and errors.
	* *Status: The framework is in place; widespread implementation is the next step.*
* [ ] **Data Persistence System:**
	* [ ] Design the database schema and corresponding data models to represent the full game state (entities, inventory, world data, etc.).
	* [ ] Implement a data access layer (DAL) to abstract database operations, likely within UntitledRpgLogic.Infrastructure.Data.
	* [ ] Create concrete data provider implementations for SQLite (for single-player/hosted games) and PostgreSQL (for dedicated servers).
	* [ ] Implement a schema migration strategy to manage database updates as the data models evolve.
* [ ] **Modding Infrastructure:**
	* [x] Implement a data-driven content loader for TOML files.
	* [ ] Implement a mod loader that discovers and loads external .urpglib packages or raw mod directories.
	* [ ] Create a central registration system for mods to add or patch content definitions (items, stats, etc.).
	* [ ] Define and document the primary modding interfaces in the .Core project.

### **Phase 2: Core Gameplay Systems**

* [ ] **Stat System:**
	* [x] Define data structures for base and derived stats (StatDefinition, InstancedStat).
	* [ ] Finalize the stat calculation pipeline to correctly apply all modifiers from equipment, abilities, and status effects.
	* [ ] Implement logic for temporary stat modifications.
	* *Status: Data models and the StatService are well-defined. The core calculation logic is the main remaining task.*
* [ ] **Inventory System:**
	* [x] Create data structures for managing collections of items (ItemDefinition, ItemInstance).
	* [ ] Implement robust logic for adding, removing, stacking, and splitting items within an inventory.
	* [ ] Define base item properties and behaviors.
	* *Status: Core models and the ItemStorageService are implemented. Advanced functionality is the next step.*
* [ ] **Equipment System:**
	* [ ] Define character equipment slots (e.g., Head, Weapon, Chest).
	* [ ] Implement logic for equipping and unequipping items.
	* [ ] Integrate with the Stat System to apply and remove stat modifiers from equipped items.
* [ ] **Ability & Skill System:**
	* [x] Design data structures for defining abilities and skills (SkillDefinition).
	* [ ] Implement logic for ability execution, including resource costs (mana/stamina) and cooldowns.
	* [ ] Integrate with the Combat System.
	* *Status: Data models are defined. The execution logic is the next major step.*
* [ ] **Status Effect System:**
	* [x] Create a system for applying effects to entities using ComposedEffect and IEffectComponent.
	* [ ] Implement logic for managing effect duration, stacking rules, and periodic triggers (e.g., damage over time).
	* [ ] Integrate with the Stat System to apply stat modifiers from effects.
	* *Status: The underlying effect architecture is strong. The focus now is on the management of applied, timed effects.*

### **Phase 3: Content & Interaction Systems**

* [ ] **Quest System:**
	* [ ] Design data structures for quests, stages, and objectives.
	* [ ] Implement a quest log for tracking active and completed quests.
	* [ ] Add support for various objective types (e.g., kill, collect, goto).
* [ ] **Dialogue System:**
	* [ ] Create a data-driven system for defining conversations (likely in TOML).
	* [ ] Implement support for branching dialogue and player choices.
	* [ ] Integrate with the Quest and Event systems to trigger game state changes from conversations.
* [ ] **Loot System:**
	* [ ] Design and implement loot tables for enemies and containers.
	* [ ] Add support for weighted random drops and conditional loot.
* [ ] **Faction & Reputation System:**
	* [ ] Implement a system to track relationships between different game factions.
	* [ ] Allow player actions to influence reputation with factions.

### **Phase 4: Gameplay Mechanics & AI**

* [ ] **Combat System:**
	* [ ] Define the core combat loop (e.g., turn-based, real-time with pause).
	* [ ] Implement damage calculation formulas, consuming the IDamageCalculator service.
	* [ ] Create an action/turn management system.
* [ ] **AI System:**
	* [ ] Implement a basic AI controller for non-player characters (NPCs).
	* [ ] Design and implement AI behaviors (e.g., using Behavior Trees or Finite State Machines).
	* [ ] Create combat-specific AI for target selection and ability usage.
* [ ] **Player Controller:**
	* [ ] Create the central service for processing player commands (e.g., Move, UseAbility, Interact). This will act as a primary entry point
	  from the Godot front-end into the logic layer.

### **Phase 5: Tooling & Documentation**

* [ ] **Testing:**
	* [ ] Establish a comprehensive suite of unit tests for all critical logic (services, calculations, etc.).
	* [ ] Implement integration tests for systems that work closely together (e.g., Combat and Stats).
	* *Status: Test projects are set up but currently contain placeholders.*
* [ ] **Content Pipeline:**
	* [x] Define the data format (TOML) for all game content.
	* [ ] Finalize the schemas for all content types (items, NPCs, quests) and document them.
	* *Status: The TOML loading mechanism is complete. The focus is now on schema definition.*
* [ ] **API Documentation (DocFX):**
	* [ ] Ensure all public interfaces, models, and methods have complete XML documentation.
	* [ ] Regularly generate and publish the API reference documentation.
	* *Status: The DocFX project and CI workflow are fully configured.*
* [ ] **Static Documentation (DocFX):**
	* [ ] Write a "Getting Started" guide for modders.
	* [ ] Document the core systems (Inventory, Stats, etc.) from a modder's perspective, explaining the TOML schemas.
	* [ ] Provide examples of how to create a simple content mod.
	* *Status: The DocFX project and CI workflow are fully configured.*
