namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Defines the specific type of item, used in conjunction with <see cref="ItemType" />.
/// </summary>
public enum ItemSubtype
{
	/// <summary>
	///     Represents no specific subtype, or a generic item.
	/// </summary>
	None = 0,

	#region Weapon Subtypes: 1000 - 1999

	// Daggers & Small Blades (1000 - 1019)
	/// <summary>
	///     A small, easily concealable knife with a pointed blade, used for stabbing or as a utility tool.
	/// </summary>
	Dagger = 1000,

	/// <summary>
	///     A slender, needle-like dagger designed purely for thrusting through gaps in heavy armor.
	/// </summary>
	Stiletto = 1001,

	/// <summary>
	///     A curved, forward-angled short blade optimized for powerful, chopping slashes.
	/// </summary>
	Kukri = 1002,

	/// <summary>
	///     A dagger with an asymmetrical, wavy blade, often associated with ceremonies or poisons.
	/// </summary>
	Kris = 1003,

	/// <summary>
	///     A short, heavy thrusting dagger traditionally wielded in the off-hand alongside a rapier.
	/// </summary>
	MainGauche = 1004,

	// One-Handed & Short Swords (1020 - 1039)
	/// <summary>
	///     A light, one-handed sword with a short blade, designed for quick thrusts and slashes.
	/// </summary>
	ShortSword = 1020,

	/// <summary>
	///     A double-edged short sword with a wide blade, traditionally used by disciplined infantry formations.
	/// </summary>
	Gladius = 1021,

	/// <summary>
	///     A slender, sharply pointed sword optimized for thrusting attacks.
	/// </summary>
	Rapier = 1022,

	/// <summary>
	///     A light, thrusting sword with a flexible rectangular or triangular blade, used for precise parries and strikes.
	/// </summary>
	Estoc = 1023,

	/// <summary>
	///     A single-edged curved sword, typically with a basket hilt, favored by cavalry and duelists.
	/// </summary>
	Saber = 1024,

	/// <summary>
	///     A short, broad, slightly curved sword with a single cutting edge, often associated with sailors.
	/// </summary>
	Cutlass = 1025,

	/// <summary>
	///     A wide, cleaver-like single-edged sword optimized for heavy cutting and chopping strokes.
	/// </summary>
	Falchion = 1026,

	/// <summary>
	///     A versatile one-handed cutting sword with a broad blade and a protective crossguard or basket hilt.
	/// </summary>
	Broadsword = 1027,

	// Curved & Eastern Swords (1040 - 1059)
	/// <summary>
	///     A curved, single-edged sword, often associated with Middle Eastern or cavalry warfare.
	/// </summary>
	Scimitar = 1040,

	/// <summary>
	///     A Japanese single-edged sword, known for its curved blade and sharpness, designed for cutting.
	/// </summary>
	Katana = 1041,

	/// <summary>
	///     A Japanese short sword carried alongside a katana, ideal for close-quarters fighting.
	/// </summary>
	Wakizashi = 1042,

	/// <summary>
	///     A double-edged, straight Chinese sword known for fluid, precise martial techniques.
	/// </summary>
	Jian = 1043,

	/// <summary>
	///     A forward-curving, single-edged sword with a flared tip, designed for cleaving strikes.
	/// </summary>
	Kopis = 1044,

	// Long & Two-Handed Swords (1060 - 1079)
	/// <summary>
	///     A versatile two-handed sword, longer than a short sword, balanced for both slashing and thrusting.
	/// </summary>
	LongSword = 1060,

	/// <summary>
	///     A sword that can be wielded with one or two hands, larger than a longsword but smaller than a greatsword.
	/// </summary>
	BastardSword = 1061,

	/// <summary>
	///     A very large and heavy two-handed sword, designed for powerful sweeping attacks.
	/// </summary>
	GreatSword = 1062,

	/// <summary>
	///     A Scottish variant of the two-handed greatsword, often fitted with crossguards terminating in quatrefoils.
	/// </summary>
	Claymore = 1063,

	/// <summary>
	///     A massive, two-handed sword featuring parrying hooks above the ricasso, used to break pike lines.
	/// </summary>
	Zweihander = 1064,

	/// <summary>
	///     A massive, elongated Japanese two-handed curved sword carried across the back or into open field battles.
	/// </summary>
	Odachi = 1065,

	// Axes & Bludgeons (1080 - 1099)
	/// <summary>
	///     A small axe designed for one-handed use, suitable for chopping or as a close-quarters weapon.
	/// </summary>
	HandAxe = 1080,

	/// <summary>
	///     A heavier, more damaging version of a hand axe, often requiring more strength to wield effectively.
	/// </summary>
	WarAxe = 1081,

	/// <summary>
	///     A large axe designed for combat, typically wielded with two hands for powerful chopping blows.
	/// </summary>
	BattleAxe = 1082,

	/// <summary>
	///     An exceptionally large and heavy two-handed axe, capable of inflicting massive damage.
	/// </summary>
	GreatAxe = 1083,

	/// <summary>
	///     A blunt weapon with a heavy head on a handle, designed to crush or bludgeon.
	/// </summary>
	Mace = 1084,

	/// <summary>
	///     A spiked metal head attached to a wooden or metal shaft, designed to pierce armor plates.
	/// </summary>
	Morningstar = 1085,

	/// <summary>
	///     A weapon consisting of a striking head attached to a handle via a flexible chain.
	/// </summary>
	Flail = 1086,

	/// <summary>
	///     A combat hammer with a blunt head on one side and a piercing spike on the reverse.
	/// </summary>
	Warhammer = 1087,

	/// <summary>
	///     A large, two-handed hammer designed for crushing blows in combat.
	/// </summary>
	GreatHammer = 1088,

	// Polearms & Spears (1100 - 1119)
	/// <summary>
	///     A pole weapon with a pointed tip, designed for thrusting.
	/// </summary>
	Spear = 1100,

	/// <summary>
	///     A very long spear, typically used by infantry in formation to counter cavalry charges or keep enemies at bay.
	/// </summary>
	Pike = 1101,

	/// <summary>
	///     A long pole weapon with a sharply pointed head, designed for mounted combat or for reaching distant foes.
	/// </summary>
	Lance = 1102,

	/// <summary>
	///     A light spear designed for throwing.
	/// </summary>
	Javelin = 1103,

	/// <summary>
	///     A two-handed pole weapon with an axe blade and a spike on the end, versatile for chopping, hooking, and thrusting.
	/// </summary>
	Halberd = 1104,

	/// <summary>
	///     A polearm with a large, single-edged blade at the end, used for slashing.
	/// </summary>
	Glaive = 1105,

	/// <summary>
	///     A three-pronged spear, often associated with fishing but also used as a weapon for its versatility and potential
	///     for multiple wounds.
	/// </summary>
	Trident = 1106,

	// Staves & Focuses (1120 - 1139)
	/// <summary>
	///     A short, slender rod often imbued with magical properties, used by spellcasters to focus or cast spells.
	/// </summary>
	Wand = 1120,

	/// <summary>
	///     A long wooden stick or rod, often used by mages to channel magical energies or as a walking aid.
	/// </summary>
	Staff = 1121,

	/// <summary>
	///     A long, sturdy staff, often used by powerful mages or as a formidable two-handed melee weapon.
	/// </summary>
	GreatStaff = 1122,

	// Bows & Crossbows (1140 - 1159)
	/// <summary>
	///     A bow smaller than a longbow, offering quicker handling but generally less range and power.
	/// </summary>
	Shortbow = 1140,

	/// <summary>
	///     A large bow, typically as tall as the archer, known for its long range and power.
	/// </summary>
	Longbow = 1141,

	/// <summary>
	///     A bow with limbs that curve away from the archer, increasing its power and efficiency.
	/// </summary>
	RecurveBow = 1142,

	/// <summary>
	///     Constructed from multiple materials (wood, horn, sinew), these bows offer high power and performance despite being
	///     relatively compact.
	/// </summary>
	CompositeBow = 1143,

	/// <summary>
	///     A mechanical bow that fires bolts, known for its power and ease of use after loading.
	/// </summary>
	Crossbow = 1144,

	/// <summary>
	///     A mechanical crossbow featuring a clockwork self-drawing or multi-shot mechanism.
	/// </summary>
	ClockworkCrossbow = 1145,

	/// <summary>
	///     The most basic sling, a simple weapon that can be used with various projectiles, including stones or lead bullets.
	///     Slings can be effective in trained hands, offering decent range and damage.
	/// </summary>
	Sling = 1146,

	// Firearms (1160 - 1179)
	/// <summary>
	///     An early smoothbore firearm, small enough to be operated by hand or mounted on a pole.
	/// </summary>
	HandCannon = 1160,

	/// <summary>
	///     An early type of long gun, often considered one of the first handheld firearms with a trigger and shoulder stock.
	/// </summary>
	Arquebus = 1161,

	/// <summary>
	///     A short, large-caliber firearm with a flared muzzle, designed for short-range spread damage.
	/// </summary>
	Blunderbuss = 1162,

	/// <summary>
	///     A compact firearm designed for one-handed shooting.
	/// </summary>
	Pistol = 1163,

	/// <summary>
	///     A repeating handgun that houses cartridges in a revolving cylinder.
	/// </summary>
	Revolver = 1164,

	/// <summary>
	///     A shoulder-fired firearm with a rifled barrel designed for accurate, longer-range combat.
	/// </summary>
	Rifle = 1165,

	#endregion

	#region Armor Subtypes: 2000 - 2999

	/// <summary>
	///     Protective headgear covering the skull and face.
	/// </summary>
	Helmet = 2000,

	/// <summary>
	///     A circular band worn on the head, often made of metal or leather, used for protection or decoration.
	/// </summary>
	Circlet = 2010,

	/// <summary>
	///     A fabric, leather, or mail hood worn over the head and neck.
	/// </summary>
	Hood = 2020,

	/// <summary>
	///     Protective armor for the torso.
	/// </summary>
	Chest = 2030,

	/// <summary>
	///     Shoulder guards or pauldrons providing deflection against downward strikes.
	/// </summary>
	Shoulders = 2040,

	/// <summary>
	///     Protective arm guards or bracers.
	/// </summary>
	Bracers = 2050,

	/// <summary>
	///     Protective handwear.
	/// </summary>
	Gloves = 2060,

	/// <summary>
	///     A protective belt or girdle worn around the waist.
	/// </summary>
	Belt = 2070,

	/// <summary>
	///     Protective armor for the legs.
	/// </summary>
	Legs = 2080,

	/// <summary>
	///     Protective footwear.
	/// </summary>
	Boots = 2090,

	#endregion

	#region Shield Subtypes: 3000 - 3999

	/// <summary>
	///     A small, light shield strapped to the arm or gripped in hand, ideal for deflecting quick strikes.
	/// </summary>
	Buckler = 3000,

	/// <summary>
	///     A medium circular shield, versatile for blocking blows and counter-bashing.
	/// </summary>
	RoundShield = 3010,

	/// <summary>
	///     A triangular or heater-shaped shield offering good upper body and torso coverage.
	/// </summary>
	HeaterShield = 3020,

	/// <summary>
	///     An elongated, teardrop-shaped shield originally used by cavalry to protect the rider's legs.
	/// </summary>
	KiteShield = 3030,

	/// <summary>
	///     A massive rectangular shield providing near-total coverage, often plantable in the ground.
	/// </summary>
	TowerShield = 3040,

	#endregion

	#region Apparel & Accessory Subtypes: 4000 - 4999

	/// <summary>
	///     A loose outer garment extending from neck to ankle, worn by mages and civilians.
	/// </summary>
	Robe = 4000,

	/// <summary>
	///     A sleeveless outer garment fastened at the neck, draped over the shoulders.
	/// </summary>
	Cloak = 4010,

	/// <summary>
	///     A piece of jewelry worn on the finger, often enchanted with magical properties.
	/// </summary>
	Ring = 4020,

	/// <summary>
	///     A piece of jewelry worn around the neck, often enchanted with magical properties.
	/// </summary>
	Amulet = 4030,

	/// <summary>
	///     A pendant, brooch, or charm believed to hold defensive or luck-altering wards.
	/// </summary>
	Talisman = 4040,

	#endregion

	#region Consumable Subtypes: 5000 - 5999

	/// <summary>
	///     A consumable liquid that provides a temporary effect, such as healing or a stat boost.
	/// </summary>
	Potion = 5000,

	/// <summary>
	///     A concentrated medicinal or magical liquid applied or consumed in drops.
	/// </summary>
	Elixir = 5010,

	/// <summary>
	///     A consumable parchment inscribed with a spell or magical effect that can be activated once.
	/// </summary>
	Scroll = 5020,

	/// <summary>
	///     A prepared meal or provision consumed to replenish energy and vitality over time.
	/// </summary>
	Food = 5030,

	/// <summary>
	///     A beverage consumed for hydration, energy, or mild status effects.
	/// </summary>
	Drink = 5040,

	/// <summary>
	///     A plant or botanical resource that can be ingested directly or used in alchemy.
	/// </summary>
	Herb = 5050,

	/// <summary>
	///     A harmful concoction applied to weapons or introduced to enemies to inflict debilitating effects.
	/// </summary>
	Poison = 5060,

	/// <summary>
	///     An ore of a material which can be processed.
	/// </summary>
	Ore = 5070,

	/// <summary>
	///     An uncut gemstone which holds limited use/value until cut/polished/processed.
	/// </summary>
	RawGemstone = 5071,

	/// <summary>
	///     A cut gemstone holds value and can be used as a trade good or further processed for enchanting/armor enhancement.
	/// </summary>
	CutGemstone = 5072,

	#endregion

	#region Ammunition Subtypes: 6000 - 6999

	/// <summary>
	///     Ammunition for bows.
	/// </summary>
	Arrow = 6000,

	/// <summary>
	///     Ammunition for crossbows, typically shorter and thicker than arrows.
	/// </summary>
	Bolt = 6010,

	/// <summary>
	///     Ammunition for firearms or slings, typically small, round projectiles designed to be fired from a weapon.
	/// </summary>
	Bullet = 6020,

	/// <summary>
	///     Spherical stones or shaped lead pellets intended for use in a sling.
	/// </summary>
	SlingBullet = 6030,

	/// <summary>
	///     A container for holding arrows.
	/// </summary>
	Quiver = 6040,

	#endregion

	#region Tool Subtypes: 7000 - 7999

	/// <summary>
	///     A heavy pick used for extracting ores, gems, and stone from mineral veins.
	/// </summary>
	MiningPick = 7000,

	/// <summary>
	///     A heavy-duty axe optimized for felling trees and chopping raw lumber.
	/// </summary>
	WoodcuttingAxe = 7010,

	/// <summary>
	///     A rod with line and hook, used to catch fish and aquatic creatures.
	/// </summary>
	FishingRod = 7020,

	/// <summary>
	///     A specialized hammer used at an anvil for shaping metal ingots and crafting arms.
	/// </summary>
	BlacksmithHammer = 7030,

	/// <summary>
	///     A mortar, pestle, and vial kit used for brewing potions and refining botanical reagents.
	/// </summary>
	AlchemyKit = 7040,

	/// <summary>
	///     A set of delicate tension wrenches and picks used to bypass physical locks.
	/// </summary>
	Lockpick = 7050,

	/// <summary>
	///     A handheld illumination device used to navigate dark underground areas.
	/// </summary>
	Torch = 7060,

	#endregion

	#region Junk Subtypes: 8000 - 8999

	/// <summary>
	///     Shattered pottery, broken dishware, or useless fragments of earthenware.
	/// </summary>
	BrokenPottery = 8000,

	/// <summary>
	///     A worthless shard of crushed glass or mirror.
	/// </summary>
	GlassShard = 8010,

	/// <summary>
	///     Porous, unrefined chunk of slag, worthless ore waste, or a simple decorative pebble.
	/// </summary>
	Rock = 8020,

	/// <summary>
	///     Old, soiled rags or tattered scraps of fabric unsuitable for mending.
	/// </summary>
	TatteredCloth = 8030,

	/// <summary>
	///     Decayed organic waste or spoiled food fit only for disposal.
	/// </summary>
	RottenRemains = 8040,

	/// <summary>
	///     Scrap iron or unidentifiable metal shavings too oxidized to smith.
	/// </summary>
	RustedScrap = 8050,

	#endregion

	#region Miscellaneous Subtypes: 9000 - 9899

	/// <summary>
	///     A metal or carved key used to open chests, locked doors, or containers.
	/// </summary>
	Key = 9000,

	/// <summary>
	///     A written document, journal entry, parchment, or map conveying lore or narrative clues.
	/// </summary>
	Document = 9010,

	/// <summary>
	///     An item tied directly to progression, rituals, or narrative milestones.
	/// </summary>
	QuestItem = 9020,

	/// <summary>
	///     A trophy, pelt, or biological specimen gathered from a defeated creature.
	/// </summary>
	MonsterPart = 9030,

	/// <summary>
	///     A decorative or historic relic kept for display, lore, or high-value bartering.
	/// </summary>
	Curio = 9050,

	#endregion

	#region Currency Subtypes: 9900 - 9999

	/// <summary>
	///     Currency in the shape of small tokens used for trade.
	/// </summary>
	Coin = 9900,

	/// <summary>
	///     A substantial block of <see cref="ItemSubtype.Coin" /> material used for its value in trade.
	/// </summary>
	Bullion = 9901,

	/// <summary>
	///     A written or printed note that is worth money in the context of an economy.
	/// </summary>
	Banknote = 9902,

	/// <summary>
	///     A token representing value in the context of an economy.
	/// </summary>
	TradeToken = 9903,

	#endregion
}
