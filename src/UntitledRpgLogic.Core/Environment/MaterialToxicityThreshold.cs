using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Core.Environment;

/// <summary>
///     Defines dangerous and lethal exposure pressure thresholds for a specific material.
/// </summary>
/// <param name="MaterialId">The <see cref="MaterialDefinition.Id" /> of the toxin.</param>
/// <param name="DangerousPressure">The partial/hydrostatic pressure in atm where poisoning sets in.</param>
/// <param name="LethalPressure">The partial/hydrostatic pressure in atm that causes rapid incapacitation or death.</param>
public record MaterialToxicityThreshold(Ulid MaterialId, float DangerousPressure, float LethalPressure);
