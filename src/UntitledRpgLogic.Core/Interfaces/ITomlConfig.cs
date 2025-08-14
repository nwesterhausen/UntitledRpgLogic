using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface to simply add type safety to reading TOML configuration files.
/// </summary>
public interface ITomlConfig : IHasIdentifier
{
	/// <summary>
	///     Gets the type of the object model represented by this configuration.
	///     This field is used to discriminate between different configuration types
	///     when loading generic TOML files.
	/// </summary>
	public ConfigType ConfigType { get; }
}
