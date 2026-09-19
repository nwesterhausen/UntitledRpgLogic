namespace UntitledRpgLogic.Core.World;

/// <summary>
///     Record for listing a material and the frequency to consider it when generating the world.
/// </summary>
public record MaterialOption : DefinedOption
{
}

/// <summary>
///     Record for listing a magical element and the frequency to consider it when generating the world.
/// </summary>
public record ElementalOption : DefinedOption
{
}

/// <summary>
///     Record for recording a defined ID (for a material, element, item, entity, etc) and frequency of appearance.
/// </summary>
public record DefinedOption
{
	/// <summary>
	///     <c>DefinitionId</c> of the thing.
	/// </summary>
	public required Ulid DefinitionId { get; init; }

	/// <summary>
	///     How frequently to have it appear.
	/// </summary>
	public float Frequency { get; init; } = 1.0f;
}
