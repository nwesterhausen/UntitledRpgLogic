namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     An object which can be instantiated in the game world. This is used to track objects that are created, such as
///     skills, stats, items, etc.
/// </summary>
public interface IInstantiable
{
	/// <summary>
	///     The instance ID of the created object. If not an instance, set this to be <see cref="Guid.Empty" />.
	/// </summary>
	public Guid InstanceId { get; init; }
}
