using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Contracts;
using UntitledRpgLogic.Networking;
using UntitledRpgLogic.Extensions.Logging;
using UntitledRpgLogic.UnitTests.TestInfrastructure;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.UnitTests.Networking;

[TestClass]
[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
public sealed class RiptideNetworkingAdapterBehaviorTests
{
	[TestMethod]
	public void ConstructingService_Logs_NetworkServiceStarted()
	{
		using var provider = new CapturingLoggerProvider();
		var services = new ServiceCollection();
		services.AddLogging(b => b.AddProvider(provider));
		services.AddNetworkingAdapter();

		using var sp = services.BuildServiceProvider();
		// Resolving triggers singleton construction and logging
		_ = sp.GetRequiredService<INetworkingService>();

		var loggedStart = provider.Entries.Any(e =>
			e.Level == LogLevel.Information &&
			e.EventId.Id == EventIds.NetworkServiceStarted &&
			string.Equals(e.Message, "Network service started.", StringComparison.Ordinal)
		);
		Assert.IsTrue(loggedStart, "Expected NetworkServiceStarted log entry on construction.");
	}

	[TestMethod]
	public void Methods_DoNotThrow_When_NotStarted()
	{
		var services = new ServiceCollection();
		services.AddLogging();
		services.AddNetworkingAdapter();
		using var sp = services.BuildServiceProvider();
		var svc = sp.GetRequiredService<INetworkingService>();

		// Should be no-ops without server/client
		// Just ensure they do not throw exceptions
		try
		{
			svc.PollEvents();
			svc.Broadcast(Array.Empty<byte>(), isReliable: true);
			svc.SendTo(Guid.NewGuid(), Array.Empty<byte>(), isReliable: false);
			svc.Disconnect(Guid.NewGuid());
		}
		catch (Exception ex)
		{
			Assert.Fail($"Methods should not throw when not started. Threw: {ex}");
		}
	}
}
