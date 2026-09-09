using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines an owned modification to an environmental ambient condition (e.g., Temperature, Gravity).
/// </summary>
/// <remarks>Owned by <see cref="Effect" /> and serialized as JSON.</remarks>
public record AffectedAmbient
{
	/// <summary>
    ///     The type of ambient condition being influenced (Temperature, Gravity, Humidity, etc.).
    /// </summary>
    public AmbientType AmbientType { get; init; }

	/// <summary>
	///     The magnitude of the change.
	/// </summary>
	public float AmountChange { get; init; }

	/// <summary>
	///     Indicates whether AmountChange is a percentage modifier (true) or a flat offset (false).
	/// </summary>
	public bool IsPercentage { get; init; }
}
