
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Infrastructure.Data.LookupEntities;
/// <summary>
/// Provides a reference table in the databse for the `EffectType` enum.
/// </summary>
public class EffectTypeLookup
{

	///<summary>The specific enum for this entry</summary>
	public EffectType Id { get; set; }

	///<summary>The name of the enum</summary>
	public string Name { get; set; } = string.Empty;

	///<summary>An optional description. Possibly provide something to describe it in-game</summary>
	public string? Description { get; set; }
}
