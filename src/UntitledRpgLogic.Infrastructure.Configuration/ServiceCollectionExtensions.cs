using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Infrastructure.Configuration.Toml;

namespace UntitledRpgLogic.Infrastructure.Configuration;

/// <summary>
///     Provides extension methods for registering configuration infrastructure services in a
///     <see
///         cref="IServiceCollection" />
///     .
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	///     Adds configuration infrastructure services to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to which the configuration services are added.</param>
	/// <returns>The same <see cref="IServiceCollection" /> instance so that additional calls can be chained.</returns>
	public static IServiceCollection AddConfigurationInfrastructure(this IServiceCollection services)
	{
		// Try adding the default handlers
		services.TryAddEnumerable(
			ServiceDescriptor.Singleton<IDefinitionSerializationService, TomlDefinitionSerializationService>()
		);

		// Add any other infrastructure-specific registrations here (e.g., file system access for config)
		return services;
	}
}
