namespace UntitledRpgLogic.Enums;

/// <summary>
///     Defines the specific type of item, used in conjunction with <see cref="ItemType" />.
/// </summary>
public enum ItemSubtype
{
    /// <summary>
    ///     Represents no specific subtype, or a generic item.
    /// </summary>
    None = 0,

    /// <summary>
    ///     A light, one-handed sword with a short blade, designed for quick thrusts and slashes.
    /// </summary>
    ShortSword = 1,

    /// <summary>
    ///     A small, easily concealable knife with a pointed blade, used for stabbing or as a utility tool.
    /// </summary>
    Dagger = 2,

    /// <summary>
    ///     A small axe designed for one-handed use, suitable for chopping or as a close-quarters weapon.
    /// </summary>
    HandAxe = 3,

    /// <summary>
    ///     A bow with limbs that curve away from the archer, increasing its power and efficiency.
    /// </summary>
    RecurveBow = 4,

    /// <summary>
    ///     A long wooden stick or rod, often used by mages to channel magical energies or as a walking aid.
    /// </summary>
    Staff = 5,

    /// <summary>
    ///     A short, slender rod often imbued with magical properties, used by spellcasters to focus or cast spells.
    /// </summary>
    Wand = 18,

    /// <summary>
    ///     A blunt weapon with a heavy head on a handle, designed to crush or bludgeon.
    /// </summary>
    Mace = 19,

    /// <summary>
    ///     A pole weapon with a pointed tip, designed for thrusting.
    /// </summary>
    Spear = 20,

    /// <summary>
    ///     A mechanical bow that fires bolts, known for its power and ease of use after loading.
    /// </summary>
    Crossbow = 21,

    /// <summary>
    ///     A large bow, typically as tall as the archer, known for its long range and power.
    /// </summary>
    Longbow = 22,

    /// <summary>
    ///     A bow smaller than a longbow, offering quicker handling but generally less range and power.
    /// </summary>
    Shortbow = 23,

    /// <summary>
    ///     A heavier, more damaging version of a hand axe, often requiring more strength to wield effectively.
    /// </summary>
    WarAxe = 24,

    /// <summary>
    ///     A versatile two-handed sword, longer than a short sword, balanced for both slashing and thrusting.
    /// </summary>
    LongSword = 25,

    /// <summary>
    ///     A very large and heavy two-handed sword, designed for powerful sweeping attacks.
    /// </summary>
    GreatSword = 26,

    /// <summary>
    ///     A sword that can be wielded with one or two hands, larger than a longsword but smaller than a greatsword.
    /// </summary>
    BastardSword = 27,

    /// <summary>
    ///     A slender, sharply pointed sword optimized for thrusting attacks.
    /// </summary>
    Rapier = 28,

    /// <summary>
    ///     A Japanese single-edged sword, known for its curved blade and sharpness, designed for cutting.
    /// </summary>
    Katana = 29,

    /// <summary>
    ///     A curved, single-edged sword, often associated with Middle Eastern or cavalry warfare.
    /// </summary>
    Scimitar = 30,

    /// <summary>
    ///     A short, broad, slightly curved sword with a single cutting edge, often associated with sailors.
    /// </summary>
    Cutlass = 31,

    /// <summary>
    ///     A large axe designed for combat, typically wielded with two hands for powerful chopping blows.
    /// </summary>
    BattleAxe = 32,

    /// <summary>
    ///     An exceptionally large and heavy two-handed axe, capable of inflicting massive damage.
    /// </summary>
    GreatAxe = 33,

    /// <summary>
    ///     A large, two-handed hammer designed for crushing blows in combat.
    /// </summary>
    GreatHammer = 34,

    /// <summary>
    ///     A long, sturdy staff, often used by powerful mages or as a formidable two-handed melee weapon.
    /// </summary>
    GreatStaff = 35,

    /// <summary>
    ///     A long pole weapon with a sharply pointed head, designed for mounted combat or for reaching distant foes.
    /// </summary>
    Lance = 36,

    /// <summary>
    ///     A very long spear, typically used by infantry in formation to counter cavalry charges or keep enemies at bay.
    /// </summary>
    Pike = 37,

    /// <summary>
    ///     A light spear designed for throwing.
    /// </summary>
    Javelin = 38,

    /// <summary>
    ///     A two-handed pole weapon with an axe blade and a spike on the end, versatile for chopping, hooking, and thrusting.
    /// </summary>
    Halberd = 39,

    /// <summary>
    ///     A polearm with a large, single-edged blade at the end, used for slashing.
    /// </summary>
    Glaive = 40,

    /// <summary>
    ///     A three-pronged spear, often associated with fishing but also used as a weapon for its versatility and potential
    ///     for multiple wounds.
    /// </summary>
    Trident = 41,

    /// <summary>
    ///     Protective headgear.
    /// </summary>
    Helmet = 6,

    /// <summary>
    ///     Protective armor for the torso.
    /// </summary>
    Chest = 7,

    /// <summary>
    ///     Protective armor for the legs.
    /// </summary>
    Legs = 8,

    /// <summary>
    ///     Protective footwear.
    /// </summary>
    Boots = 9,

    /// <summary>
    ///     Protective handwear.
    /// </summary>
    Gloves = 10,

    /// <summary>
    ///     A piece of jewelry worn on the finger, often enchanted with magical properties.
    /// </summary>
    Ring = 11,

    /// <summary>
    ///     A piece of jewelry worn around the neck, often enchanted with magical properties.
    /// </summary>
    Amulet = 12,

    /// <summary>
    ///     A consumable liquid that provides a temporary effect, such as healing or a stat boost.
    /// </summary>
    Potion = 13,

    /// <summary>
    ///     A consumable parchment inscribed with a spell or magical effect that can be activated once.
    /// </summary>
    Scroll = 14,

    /// <summary>
    ///     Ammunition for bows.
    /// </summary>
    Arrow = 15,

    /// <summary>
    ///     Ammunition for firearms or slings, typically small, round projectiles designed to be fired from a weapon.
    /// </summary>
    Bullet = 16,

    /// <summary>
    ///     A container for holding arrows.
    /// </summary>
    Quiver = 17,

    /// <summary>
    ///     Ammunition for crossbows, typically shorter and thicker than arrows.
    /// </summary>
    Bolt = 42,

    /// <summary>
    ///     A circular band worn on the head, often made of metal or leather, used for protection or decoration.
    /// </summary>
    Circlet = 43,

    /// <summary>
    ///     The most basic sling, a simple weapon that can be used with various projectiles, including stones or lead bullets.
    ///     Slings can be effective in trained hands, offering decent range and damage.
    /// </summary>
    Sling = 44,

    /// <summary>
    ///     Constructed from multiple materials (wood, horn, sinew), these bows offer high power and performance despite being
    ///     relatively compact.
    /// </summary>
    CompositeBow = 45,

    /// <summary>
    ///     An early type of long gun, often considered one of the first handheld firearms with a trigger and shoulder stock.
    /// </summary>
    Arquebus = 46,

    /// <summary>
    /// </summary>
    HandCannon = 47,

    /// <summary>
    /// </summary>
    Pistol = 48,

    /// <summary>
    /// </summary>
    Revolver = 49,

    /// <summary>
    /// </summary>
    Blunderbuss = 50,

    /// <summary>
    /// </summary>
    ClockworkCrossbow = 51,

    /// <summary>
    /// </summary>
    Rifle = 52

    // Todo: add tools, crafting materials, and other item subtypes.
    // Todo: types of resources???
    // Todo: more magic item subtypes? Being magical itself is not a subtype, but rather a property of the item.
}