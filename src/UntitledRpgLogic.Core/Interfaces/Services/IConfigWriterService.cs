using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Interfaces.Services;

/// <summary>
///     Defines a service for writing configuration settings.
/// </summary>
/// <remarks>
///     This provides ways to persist modified or newly created defintions to a configuration file.
/// </remarks>
public interface IConfigWriterService
{
	/// <summary>
	///     Persists a new Ability definition.
	/// </summary>
	/// <param name="ability">The pure Ability domain entity to save.</param>
	public Task SaveAbilityAsync(Ability ability);

	/// <summary>
	///     Persists a new Effect definition.
	/// </summary>
	/// <param name="effect">The pure Effect domain entity to save.</param>
	public Task SaveEffectAsync(Effect effect);

	// Add other methods here for items, etc. as needed
}
