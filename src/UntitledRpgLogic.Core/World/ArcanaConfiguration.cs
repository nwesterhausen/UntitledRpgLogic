namespace UntitledRpgLogic.Core.World;

/// <summary>
/// </summary>
public record ArcanaConfiguration
{
	/// <summary>
	///     The target amount of mana density overall (as an average). <c>0.5f</c> is the default value.
	/// </summary>
	public float MeanManaDensity { get; init; } = 0.5f;

	/// <summary>
	///     Whether to allow a neutral or no-specific-element affinity to be assigned to a given mana zone.
	/// </summary>
	public bool AllowNeutralAffinity { get; init; }

	/// <summary>
	///     Amount of areas to heavily lean into one end of the alignment (good/evil) scale.
	/// </summary>
	public float AlignmentInfluence { get; init; } = 0.5f;

	/// <summary>
	///     Weight of assigning all alignment values to <c>good</c>. Default is <c>1</c>.
	/// </summary>
	public int GoodAlignmentWeight { get; init; } = 1;

	/// <summary>
	///     Weight of assigning all alignment values to <c>evil</c>. Default is <c>1</c>.
	/// </summary>
	public int EvilAlignmentWeight { get; init; } = 1;

	/// <summary>
	///     Amount of areas to heavily lean into one end of the savagery (benign/vicious) scale.
	/// </summary>
	public float SavageryInfluence { get; init; } = 0.01f;

	/// <summary>
	///     Weight of assigning all savagery values to <c>benign</c>. Default is <c>1</c>.
	/// </summary>
	public int BenignSavageryWeight { get; init; } = 1;

	/// <summary>
	///     Weight of assigning all savagery values to <c>vicious</c>. Default is <c>1</c>.
	/// </summary>
	public int ViciousSavageryWeight { get; init; } = 1;

	/// <summary>
	///     List of available elements to be considered when assigning areas of elemental affinity during world gen.
	/// </summary>
	public IReadOnlyList<ElementalOption> AvailableElements { get; init; } = [];
}
