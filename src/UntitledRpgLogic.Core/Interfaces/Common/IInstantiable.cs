namespace UntitledRpgLogic.Core.Interfaces.Common;

/// <summary>
/// Defines a factory for creating an instance of a specific type.
/// </summary>
/// <typeparam name="T">The type of object that this factory creates.</typeparam>
public interface IInstantiable<out T>
{
	/// <summary>
	/// Creates a new instance of type <see cref="T"/>.
	/// </summary>
	/// <returns>A new instance of <see cref="T"/>.</returns>
	public T CreateInstance();
}
