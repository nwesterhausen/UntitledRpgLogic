using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Contracts;

namespace UntitledRpgLogic.Networking;

/// <summary>
///		Service registration for the networking adapter.
/// </summary>
public static class NetworkingServiceRegistration
{
	/// <summary>
	///		Adds the Riptide networking adapter as a singleton service.
	/// </summary>
	/// <param name="services">The service collection to add the service to.</param>
	/// <returns>The service collection so that additional calls can be chained.</returns>
	public static IServiceCollection AddNetworkingAdapter(this IServiceCollection services)
	{
		// This registers our Riptide adapter as a singleton, providing an
		// INetworkingService to any class that asks for it.
		_ = services.AddSingleton<INetworkingService, RiptideNetworkingAdapter>();

		return services;
	}
}
