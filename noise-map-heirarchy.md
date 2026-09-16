# Noise Map / Map Grid Heirarchy
The dependency chain on the noise maps and map grids. 

## Noise generation settings (world map config)
These are settings that can be configured to influence the world generation.

- noise seed (generated if not supplied of course)
- map width
- map height
- (terrain settings)
	- minimum elevation
	- maximum elevation
	- sea level elevation (typically 0)
	- mountain threshold (elevation to consider terrain a mountain)
	- should surround map with ocean (create an island map)
	- ocean border thickness (how much ocean, if an island)
	- island falloff steepness (how sharp to bring the island into the ocean when generating)
	- override fbm settings (all optional)
		- scale
		- octaves
		- frequency
		- amplitude
		- persistence,
		- lacunarity
	- material settings 
		- defines available minerals/ores
		- defines available bedrock/stone
- (hydrology settings)
	- raindrop cycles
	- base river depth
	- max descent steps
	- river flux threshold
	- river bed erosion (meters)
	- should form inland lakes (rivers can end in lakes inland)
	- maximum lake tiles
	- maximum lake depth
	- material settings 
		- defines freshwater/saltwater materials
- (climate settings)
	- target average equator temperature
	- target average pole temperature
	- poles choice (n+s, n, s)
	- lapse rate per km (how fast the environment cools per km of elevation)
	- override temperature generation factor (frequency)
	- override rainfall generation factor (frequncy)
	- material settings
		- defines rain material
- (fantastical settings)
	- ? settings for mana density
	- ? settings for mana affinity
	- ? settings for alignment
	- ? settings for savagery
	- elemental settings
		- defines avaiable elements for use with mana alignment (and related biomes)
	- special material settings
		- defines additional terrain materials (and when/where to place them)
		- defines additional liquid materials (and when/where to place them)
		- defines additional rain materials (and when/where to place them)

## Generation Pipeline

### Tier 1: Pure noise maps
Generates noise maps based only on a seed and noise generation settings: not influenced by other maps.

- height: determines base height of the map; generates mountains, oceans, and general surface shape of the world
- leyline: determines areas of high and low magical energy for the world; generates a "web" of high energy conduits throughout the world
- alignment: influences the moral alignment of generated flora/fauna/dungeons/etc. created in various areas (ranges from evil to good)
- savagery: influences how savage generated flora/fauna/dungeons/etc. are in the world (ranges from benign to vicious)
- volcanism: determines the mantle forces beneath the crust, more volcanic activity to less

### Teir 1.5
These are slight adjustments to a Tier 1 based on another Tier 1 after all are generated

- (blended heightmap): blends volcanism information into the heightmap (volcanoes, craters and islands); takes into account if it should be an island, and out in the ocean creates trenches and submerged calderas/cones to keep land from forming
- climateTurbulence: influenced by height, leyline and volcanism; provides a map to influence the climate maps and prevent banding but enable leyline influence to come through after application
- basinMask: marks a tile as 'ocean', 'lake', 'river' or 'none' (WaterBodyType). flood fill edge area < sea level with ocean for use in moisture calculation. 

### Tier 2: Influenced noise maps
These depend on one-another or first tier noise maps to generate biased noise maps.

- temperature: biased by height, also takes into consideration desired pole/equator temperatures; generates general temperatures for the world; influenced by temperatureTurbulence to remove banding
- wind: (vectors) very biased by leyline, height and temperature; generates jetstreams and average/typical wind direction. leyline peaks can indicate stronger winds (or perhaps no wind)
- geologyLayers: biased by height and volcanism; generates stratigraphic rock layers
- geologyOres: biased by heightmap, volcanism, and geologyLayers; generates ore and mineral veins found in each grid cell 

### Tier 2.5: 
There are influenced by influenced maps, but still affected by noise (even if its a very small influence). These are not pure calculated layers like Tier 3.

- moisture: influenced by height, temperature, ocean/lake basins, and wind; moisture is moved around by wind, falling off at high elevations, decreasing in cooler temperatures, and recharging over warm water; given very slight variation/noise from turbulence
- rainfall: influenced by the changes in moisture (more rain as moisture decreases, little to no rain at the 'baseline'); temperature would imply liquid rain vs snow vs glacial accumulation
- soilDepth: influenced from height, moisture, geology and rainfall; measurement for "ground level" would be the heightmap elevation + soilDepth elevation
- aquifer: maps the underground water table with its own elevation; broad noise influenced by moisture, rainfall, and geology (sedimentary rock is permeable, igneous is not)
- waterSource: decides if a cell is a water source, candidates have a random chance of being selected; establishes springs of varying types ('natural'/'geothermal'/'arcane') to be used in the liquid simulation. candidates:
	- (waterTable elevation > ground level elevation): default condition for a spring
	- topography (steep slope can intersect an aquifer and provide access for a spring)
	- faults/fractures (high volcanism can force water through cracks/fractures in the ground)
	- geothermal (high volcanism can force water up through the ground because high heat) 
	- arcane (leyline ridge and waterTable near surface): create a magical spring

### Tier 3: Non-noise maps
These only store information about the world grid based on information in lower tiers. These are calculated maps.

1. liquid simulation; runs rainfall simulation and water runoff from springs, creates oceans and lakes
	- (adjusts height and soilDepth): rivers are carved into the soil and even the heightmap during simulation.
	- (adjusts basinMask): update the WaterBodyType: 'ocean','lake','river','spring','none'  ('spring' is only set with waterSource determination)
2. liquid assignment, finalizes liquid material, depth. also assigns molten rock to volcanic interiors
	- liquidDepth: defines a liquidDepth short for each cell of the world grid 
	- liquidMaterial: defines the material of liquids on the map, using a relational dictionary to keep used space efficient
	- rainMaterial: defines the material of liquids for rain (matches 'freshwater' material at this point)
2. biome classification
	- (adjusts soilDepth): slightly adjusts overall soilDepth in certain biomes (removing soil in glaciers, adding soil in forests/rainforests)
	- soilMaterial: defines the mixture of (or singular) material for the soil in each tile. can easily happen alongside the biome classification
3. magical assignment
	- manaDensity: defines how dense mana is in a grid cell, based on biome and leyline
	- manaAlignment: defines which elements are more present in a grid cell, based on biome, savagery, alignment and manaDensity

### Tier 3.5: 
A final pass to allow magical influence on the world.

- (blended liquidMaterial): affects liquid material from alignment, savagery, manaDensity and manaAlignment
- (blended rainMaterial): affects rain material from alignment, savagery, manaDensity and manaAlignment
- (blended geologyOres): affects mineral and ore deposits from manaDensity and manaAlignment
- (biome adjustment): magical biome assignments if required, for perhaps extremely high/low manaDensity or special combinations of biome+alignment

## Collections of Grids
These are "helper" DimensionGrids which have accessors for information from child maps. They do not generate anything new. These are stored in the WorldGenerationContext.

- terrain: terra related maps
	- height
	- soilDepth
	- soilMaterial
	- volcanism
	- geologyLayers
	- geologyOres
- hydrology: liquid related maps
	- aquifer
	- waterSource
	- liquidDepth
	- liquidMaterial
	- rainMaterial
- climate: climate related maps
	- turbulence maps (noise for temperature/wind/rain)
	- wind
	- temperature
	- rainfall
	- biome
- fantastical: fantasy related maps
	- alignment
	- savagery
	- leyline
	- manaDensity
	- manaAlignment
