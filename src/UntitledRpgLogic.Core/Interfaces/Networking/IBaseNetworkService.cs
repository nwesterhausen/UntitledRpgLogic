namespace UntitledRpgLogic.Core.Interfaces.Networking;

/// <summary>
///     Defines the common functionality for all network ports (client and server).
/// </summary>
public interface IBaseNetworkService : IDisposable
{
	/// <summary>
	///     Gets a value indicating whether the network port is currently running and connected.
	/// </summary>
	public bool IsRunning { get; }
}
