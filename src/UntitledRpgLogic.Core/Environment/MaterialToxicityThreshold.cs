using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.Environment;

/// <summary>
///     Defines dangerous and lethal exposure pressure thresholds for a specific material.
/// </summary>
public class MaterialToxicityThreshold()
{
	/// <summary>Create a toxicity threshold.</summary>
	/// <param name="MaterialId">The <see cref="MaterialDefinition.Id" /> of the toxin.</param>
	/// <param name="DangerousPressure">The partial/hydrostatic pressure in atm where poisoning sets in.</param>
	/// <param name="LethalPressure">The partial/hydrostatic pressure in atm that causes rapid incapacitation or death.</param>
	public MaterialToxicityThreshold(Ulid MaterialId, float DangerousPressure, float LethalPressure) : this()
	{
		this.MaterialId = MaterialId;
		this.DangerousPressure = DangerousPressure;
		this.LethalPressure = LethalPressure;
	}

	/// <summary>The <see cref="MaterialDefinition.Id" /> of the toxin.</summary>
	public Ulid MaterialId { get; init; }

	/// <summary>The partial/hydrostatic pressure in atm where poisoning sets in.</summary>
	public float DangerousPressure { get; init; }

	/// <summary>The partial/hydrostatic pressure in atm that causes rapid incapacitation or death.</summary>
	public float LethalPressure { get; init; }
}
