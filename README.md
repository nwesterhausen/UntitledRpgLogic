# UntitledRpgLogic

Logic used for a WIP game UntitledRpg


# UntitledRpgLogic Engine To-Do List

## Phase 1: Core Architecture & Foundation

- [ ] **Dependency Injection:** Finalize service registration and lifetime management for all core services.
- [ ] **Configuration System:** Implement a system to load and access game-wide settings (e.g., from a JSON file).
- [ ] **Logging Framework:** Integrate a structured logging library (e.g., Serilog) to capture engine events and errors.
- [ ] **Save/Load System:**
    - [ ] Design data transfer objects (DTOs) for game state persistence.
    - [ ] Implement serialization/deserialization logic.
    - [ ] Add support for multiple save slots.
    - [ ] Implement a versioning strategy for save files to handle future updates.
- [ ] **Modding Infrastructure:**
    - [ ] Implement a mod loader that discovers and loads external assemblies.
    - [ ] Create a registration system for mods to add new content (e.g., items, abilities).
    - [ ] Define and document the primary modding interfaces in the `.Core` project.

---

## Phase 2: Core Gameplay Systems

- [ ] **Stat System:**
    - [ ] Define a flexible structure for base stats (e.g., Strength, Health, Mana).
    - [ ] Implement a stat calculation pipeline that accounts for modifiers (e.g., from equipment, status effects).
    - [ ] Add support for derived stats (e.g., Critical Hit Chance derived from Dexterity).
- [ ] **Inventory System:**
    - [ ] Create data structures for managing collections of items.
    - [ ] Implement logic for adding, removing, stacking, and splitting items.
    - [ ] Define base item properties (ID, Name, Description, Icon, Weight).
- [ ] **Equipment System:**
    - [ ] Define character equipment slots (e.g., Head, Weapon, Chest).
    - [ ] Implement logic for equipping and unequipping items.
    - [ ] Integrate with the Stat System to apply and remove stat modifiers from equipment.
- [ ] **Ability & Skill System:**
    - [ ] Design data structures for defining abilities (active and passive).
    - [ ] Implement logic for ability execution, including resource costs (mana/stamina) and cooldowns.
    - [ ] Integrate with the Combat System.
- [ ] **Status Effect System:**
    - [ ] Create a system for applying temporary or permanent effects to entities (e.g., buffs, debuffs).
    - [ ] Implement logic for managing effect duration, stacking rules, and periodic triggers (e.g., poison damage over time).
    - [ ] Integrate with the Stat System to apply stat modifiers.

---

## Phase 3: Content & Interaction Systems

- [ ] **Quest System:**
    - [ ] Design data structures for quests, stages, and objectives.
    - [ ] Implement a quest log for tracking active and completed quests.
    - [ ] Add support for various objective types (e.g., `kill`, `collect`, `goto`).
- [ ] **Dialogue System:**
    - [ ] Create a data-driven system for defining conversations.
    - [ ] Implement support for branching dialogue and player choices.
    - [ ] Integrate with the Quest and Event systems to trigger game state changes from conversations.
- [ ] **Loot System:**
    - [ ] Design and implement loot tables for enemies and containers.
    - [ ] Add support for weighted random drops and conditional loot.
- [ ] **Faction & Reputation System:**
    - [ ] Implement a system to track relationships between different game factions.
    - [ ] Allow player actions to influence reputation with factions.

---

## Phase 4: Gameplay Mechanics & AI

- [ ] **Combat System:**
    - [ ] Define the core combat loop (e.g., turn-based, real-time).
    - [ ] Implement damage calculation formulas that incorporate stats, abilities, and status effects.
    - [ ] Create an action/turn management system.
- [ ] **AI System:**
    - [ ] Implement a basic AI controller for non-player characters (NPCs).
    - [ ] Design and implement AI behaviors (e.g., using Behavior Trees or Finite State Machines).
    - [ ] Create combat-specific AI for target selection and ability usage.
- [ ] **Player Controller:**
    - [ ] Create the central service for processing player commands (e.g., `Move`, `UseAbility`, `Interact`).
    - [ ] This service will act as a primary entry point from the Godot front-end into the logic layer.

---

## Phase 5: Tooling & Documentation

- [ ] **Testing:**
    - [ ] Establish a comprehensive suite of unit tests for all critical logic.
    - [ ] Implement integration tests for systems that work closely together (e.g., Combat and Stats).
- [ ] **Content Pipeline:**
    - [ ] Define the data format (e.g., JSON, YAML) for all game content (items, NPCs, quests).
    - [ ] Implement data loaders that parse content files and populate the game world.
- [ ] **API Documentation (DocFX):**
    - [ ] Ensure all public interfaces and models in `.Core` have complete XML documentation.
    - [ ] Generate and publish the API reference documentation.
- [ ] **Static Documentation (DocFX):**
    - [ ] Write a "Getting Started" guide for modders.
    - [ ] Document the core systems (Inventory, Stats, etc.) from a modder's perspective.
    - [ ] Provide examples of how to create a simple mod.