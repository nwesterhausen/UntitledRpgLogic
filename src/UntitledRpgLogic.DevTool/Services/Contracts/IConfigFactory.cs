using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.DevTool.Services.Contracts;

/// <summary>
/// Provides a mechanism to create instances of configuration objects.
/// </summary>
public interface IConfigFactory
{
  /// <summary>
  /// Creates a new instance of an <see cref="ITomlConfig"/> based on the specified <see cref="ConfigType"/>.
  /// </summary>
  /// <param name="configType">The type of configuration to create.</param>
  /// <returns>A new instance of the corresponding configuration class.</returns>
  public ITomlConfig Create(ConfigType configType);
}
