# **Modding Guide: Creating New Abilities and Effects**

This guide explains how to create new content for Untitled RPG using simple text files. We use the TOML file format for its readability.

## **The Core Concept: Definitions and ULIDs**

Every piece of content in the game—an ability, an effect, an item, a monster—is a "definition" that needs a unique identifier. We use a special type of ID called a **ULID** (Universally Unique Lexicographically Sortable Identifier).

**The most important rule of modding is:**

Every new definition you create in a file MUST have its own, brand-new, globally unique ULID.

You must **never** copy and paste a ULID from another file to create your new content. Re-using a ULID will cause the game to overwrite the original content, breaking your mod and potentially the game itself.

### **Generating ULIDs (The Easy Way)**

Manually creating ULIDs can be a pain. To make this easy, we provide two tools:

1. **The Config Writing Tool:** If you use our official tool to create your config files, it will automatically generate and insert a new, valid ULID for you every time you create a new definition.
2. **The ConfigCheck Tool:** If you prefer to write your files by hand, you can run this command-line tool. It can scan your files and automatically insert new ULIDs into any definitions that are missing one.

## **File Structure: Abilities and Effects**

Abilities (like "Fireball") and Effects (like "Damage" or "Heal") are defined in separate files. This allows many different abilities to re-use the same effect.

### **Example: A Simple "Heal" Effect**

You would create a file like my\_heal\_effect.toml:

```toml
# A unique ID for this effect definition.
Id = "01J7KZM5P7K6N7Q8R9T0V1W2X3" # \<- A new, generated ULID
EffectType = "Heal"
Duration = 0 # 0 means the effect is instant

# This section defines which stats are affected.
[[AffectedStats]]
StatId = "01J7KZMANA6S7P8Q9R0T1V2W3X" # The ULID for the "Health" stat
Amount = 25
IsPercentage = false # This is a flat 25-point heal
```

### **Example: A "Lesser Heal" Ability**

Now, in a separate file like lesser\_heal\_ability.toml, you can create an ability that uses that effect:

```toml
# A unique ID for this ability definition.
Id = "01J7KZN0P1Q2R3S4T5V6W7X8Y9" # \<- A new, generated ULID
Name = "Lesser Heal"
AbilityType = "Spell"
TargetType = "Touch"
AffectsAllies = true
CastTime = 1.5 # Takes 1.5 seconds to cast

# The ULID of the skill this ability belongs to (e.g., "Restoration")
SkillDisciplineId = "01J7KZNABCDEFG0123456789"

# Link to the effect to use on a successful cast.
# This ULID must match the Id from your effect file.
ActiveEffectIds = [ "01J7KZM5P7K6N7Q8R9T0V1W2X3" ]

# This spell costs 10 mana to cast.
[[StatCosts]]
StatId = "01J7KZPQRSTMNA0123456789" # The ULID for the "Mana" stat
Amount = 10
```
