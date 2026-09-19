namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Defines the ore deposits that can be found on a specific tile of the world.
/// </summary>
public record OreDepositDefinition
{
	/// <summary>
	///     Reference integer id for referring to this deposit.
	/// </summary>
	public ushort DepositId { get; init; }

	/// <summary>
	///     The primary ore found at a given tile.
	/// </summary>
	public Ulid PrimaryMaterialId { get; init; }

	/// <summary>
	///     The frequency of finding the primary ore here.
	/// </summary>
	/// <remarks>
	///     <c>1.0f</c> corresponds to a "normal" distribution.
	/// </remarks>
	public float PrimaryChance { get; init; } = 1.0f;

	/// <summary>
	///     (Optional) The secondary ore found at a given tile.
	/// </summary>
	public Ulid? SecondaryMaterialId { get; init; }

	/// <summary>
	///     The frequency of finding the secondary ore here.
	/// </summary>
	public float SecondaryChance { get; init; }

	/// <summary>
	///     (Optional) The tertiary ore found at a given tile.
	/// </summary>
	public Ulid? TertiaryMaterialId { get; init; }

	/// <summary>
	///     The frequency of finding the tertiary ore here.
	/// </summary>
	public float TertiaryChance { get; init; }
}
