namespace UntitledRpgLogic.Core.Data.Urpglib;

/// <summary>
///     Enforces that a configuration DTO can deserialize from external TOML and map itself
///     directly to its corresponding domain catalog definition.
/// </summary>
/// <typeparam name="TModel">The domain definition record implementing <see cref="IDefined" />.</typeparam>
/// <typeparam name="TSelf">The DTO definition used when doing (de)serialization</typeparam>
public interface IConfigDto<out TSelf, TModel>
	where TSelf : IConfigDto<TSelf, TModel>
	where TModel : class, IDefined
{
	/// <summary>
	///     Transforms this configuration DTO into its domain model representation.
	/// </summary>
	/// <returns>The populated domain model.</returns>
	public TModel ToModel();

	/// <summary>
	///     Constructs the configuration DTO from an existing domain model representation.
	/// </summary>
	public static abstract TSelf FromModel(TModel model);
}
