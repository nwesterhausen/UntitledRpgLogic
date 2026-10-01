# Proposed World Gen Usage

World map generation takes into account it is for a "world" or "game file", so it can assume that modules have already been loaded to provide biomes, elements, materials and entities. It's also fair for a single "game file" to have multiple generated maps.

Something that may be neat is to generate 2 or 4 'half maps' (only one pole) and stitch them together into something larger. 

## A 'primary' database
Create a main DB context used by the "game system." Knows a list of available modules that could be used. Knows saved world-gen parameters or other settings relevant to "setting up" a game. 

When you chose to start a game, firstly you have to select which modules to use, and they will get loaded into a per-game-save ('file') database. Then you have an option to 

## Non-game Context: "Testing Arena"

Provides a 'debug' interface:

- allow creation/inspection of items from any material
- create spells/entities/item blueprints
- summon entities, inspect stats,etc.
- manually paint maps or create building interiors
- save created buildings/areas that can be randomly placed in a world
- allow creating new biomes, materials, elements, skills, stats
- exporter to enable exporting new things in urpglib module
- world generation -
	- edit more settings than normal
	- able to save settings into a profile for re-use in any game file
	- allows to generate world maps and see all layers
	- choose to save a map and play on it

## Game Context: "New Game"

Choose an existing map or make a new one. 

Choose to keep or re-gen a map until you like one.

Create a character. 

Enter the game.