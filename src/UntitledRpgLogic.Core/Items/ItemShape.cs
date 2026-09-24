using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Item shapes describe the crafted pieces of an item. These allow for flexibility in materials used to make
///     any item that defines them.
/// </summary>
[Table("item_shapes")]
public record ItemShape : IDefined
{
	/// <summary>
	///     The approximate dimensions of the <see cref="ItemShape" />. Used for volume and weight calculations. Affects
	///     properties of the completed item via volume and weight.
	/// </summary>
	public Dimensions Dimensions { get; init; } = new();

	/// <inheritdoc />
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; } = Ulid.NewUlid();

	/// <inheritdoc />
	public Name Name { get; init; } = Name.Empty;

	/// <inheritdoc />
	[MaxLength(1024)]
	public string Description { get; init; } = string.Empty;
}
