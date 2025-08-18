using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Contracts;
using UntitledRpgLogic.Networking;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.UnitTests.Networking;

[TestClass]
[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
public sealed class NetworkingServiceRegistrationTests
{
	[TestMethod]
	public void AddNetworkingAdapter_Registers_INetworkingService_AsSingleton()
	{
		var services = new ServiceCollection();
		services.AddLogging();
		services.AddNetworkingAdapter();

		using var provider = services.BuildServiceProvider();

		var instance1 = provider.GetRequiredService<INetworkingService>();
		var instance2 = provider.GetRequiredService<INetworkingService>();

		Assert.IsNotNull(instance1);
		Assert.IsTrue(object.ReferenceEquals(instance1, instance2), "Expected INetworkingService to be registered as singleton.");
	}
}
