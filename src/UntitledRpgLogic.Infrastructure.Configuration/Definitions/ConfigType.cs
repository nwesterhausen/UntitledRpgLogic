namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Specifies the type of configuration file.
/// </summary>
/// <remarks>
///     The <see cref="ConfigType" /> must exist in every TOML config file to identify
///     the type of configuration being defined. Without this property, the configuration
///    file cannot be processed.
/// </remarks>
public enum ConfigType
{
  /// <summary>
  ///     Default / Unknown configuration type.
  /// </summary>
  Unknown,

  /// <summary>
  ///     Configuration that describes the author of a configuration module.
  /// </summary>
  Author,

  /// <summary>
  ///     Configuration that describes a module, which can contain multiple configurations.
  /// </summary>
  ModuleInfo,

  /// <summary>
  ///     Configuration for an Item defintion.
  /// </summary>
  Item,

  /// <summary>
  ///     Configuration for a Material definition.
  /// </summary>
  Material,

  /// <summary>
  ///     Configuration for a Skill defintion.
  /// </summary>
  Skill,

  /// <summary>
  ///     Configuration for a Stat defintion.
  /// </summary>
  Stat,

  /// <summary>
  ///     Configuration for an Ability definition: Spells, Passsive and Active abilities are all defined here.
  /// </summary>
  Ability,

  /// <summary>
  ///    Configuration for an Effect of an ability.
  /// </summary>
  Effect,

  /// <summary>
  /// Configuration for an Element, such as Fire, Water, Earth, etc. These are used in skills to have
  /// a governing element and in materials to define elemental attunement. Damage can also be of an
  /// elemental type and resistances can be defined against elements.
  /// </summary>
  Element,
}
