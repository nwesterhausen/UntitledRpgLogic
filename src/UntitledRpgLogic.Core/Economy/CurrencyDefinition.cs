using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.Economy;

/// <summary>
/// 	Model for defining currency data (amount, type, material)
/// </summary>
public record CurrencyDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     The unique catalog identifier for the currency. Can be assigned explicitly when loading from config archives.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	/// 	The name of the currency
	/// </summary>
	public Name Name { get; init; } = Name.Empty;

	/// <summary>
	/// 	The Id of the material this currency is made of (if tangible)
	/// </summary>
	public Ulid? MaterialId { get; init; }

	/// <summary>
	///     The material that this currency is made of. Used for immersive purposes, such as displaying it or attributing
	///     weight to it.
	/// </summary>
	[ForeignKey(nameof(MaterialId))]
	public MaterialDefinition? Material { get; init; }
}
