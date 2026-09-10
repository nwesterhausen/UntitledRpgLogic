using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Interfaces.Configuration;

/// <summary>
///     Identifies a foundational game content archetype or catalog definition (e.g., Stats, Skills, Items, Materials)
///     that can be authored externally in configuration packages and persisted into the database.
/// </summary>
public interface IDefined : IDbEntity<Ulid>
{
	/// <summary>
	///     The localized display name of the definition.
	/// </summary>
	Name Name { get; }

	/// <summary>
	///     A descriptive summary or lore overview of this content definition.
	/// </summary>
	string Description { get; }
}
