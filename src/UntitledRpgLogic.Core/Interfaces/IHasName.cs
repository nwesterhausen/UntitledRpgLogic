using UntitledRpgLogic.Core.Classes;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for classes that have a name. Provides a way to access the name of the object.
/// </summary>
public interface IHasName
{
	/// <summary>
	///     The name of the object.
	/// </summary>
	public Name Name { get; }

	/// <summary>
	///     The singular name of the object, which is the same as the Name property.
	/// </summary>
	public string SingularName => this.Name.Singular;

	/// <summary>
	///     A plural name of the object.
	/// </summary>
	public string PluralName => this.Name.Plural;

	/// <summary>
	///     The name of the object if it was used as an adjective. Most objects will use the singular name as the adjective,
	///     but some may have a different form (e.g. "Sword" Soup vs "Swordy" Soup).
	/// </summary>
	public string NameAsAdjective => this.Name.Adjective;

	/// <summary>
	///     Get the appropriate name for the given quantity.
	/// </summary>
	/// <param name="quantity">amount of an object</param>
	/// <returns>a singular or plural version of the name</returns>
	public string NameForQuantity(int quantity) => this.Name.GetName(quantity);
}
