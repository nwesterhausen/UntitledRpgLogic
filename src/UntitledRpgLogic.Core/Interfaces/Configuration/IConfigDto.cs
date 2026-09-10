namespace UntitledRpgLogic.Core.Interfaces.Configuration;

/// <summary>
///     Enforces that a configuration DTO can deserialize from external TOML and map itself
///     directly to its corresponding domain catalog definition.
/// </summary>
/// <typeparam name="TModel">The domain definition record implementing <see cref="IDefined"/>.</typeparam>
public interface IConfigDto<out TModel> where TModel : class, IDefined
{
	/// <summary>
	///     Transforms this configuration DTO into its domain model representation.
	/// </summary>
	/// <returns>The populated domain model.</returns>
	TModel ToModel();
}
