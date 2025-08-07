using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.DevTool.Services.Contracts;

namespace UntitledRpgLogic.DevTool.Services;

/// <inheritdoc />
public class ConfigFactory : IConfigFactory
{
	/// <inheritdoc />
	public ITomlConfig Create(ConfigType configType) =>
		configType switch
		{
			ConfigType.ModuleInfo => new ModuleInfoConfig { Name = "New Module" },
			ConfigType.Stat => new StatDataConfig { Name = "New Stat" },
			ConfigType.Skill => new SkillDataConfig { Name = "New Skill" },
			ConfigType.Item => new ItemDataConfig { Name = "New Item", ItemType = ItemType.Junk },
			ConfigType.Material => new MaterialDataConfig { Name = "New Material" },
			ConfigType.MagicType => new MagicTypeConfig(),
			_ => throw new ArgumentOutOfRangeException(nameof(configType), $"Unsupported config type: {configType}")
		};
}
