using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Core.Environment;

/// <summary>
///     Defines an owned modification to an environmental ambient condition (e.g., Temperature, Gravity).
/// </summary>
/// <remarks>Owned by <see cref="Effect" /> and serialized as JSON.</remarks>
public record AffectedAmbient : ChangeOptions
{
	public AffectedAmbient()
	{
	}

	public AffectedAmbient(AmbientType ambientType) : this() => this.AmbientType = ambientType;


	/// <summary>
	///     The type of ambient condition being influenced (Temperature, Gravity, Humidity, etc.).
	/// </summary>
	public AmbientType AmbientType { get; init; }

	/// <inheritdoc />
	public override AffectedAmbient Apply(ChangeOptions options)
	{
		var mergedBase = base.Apply(options);

		if (options is AffectedAmbient affectedOptions)
		{
			return (AffectedAmbient)mergedBase with
			{
				AmbientType = affectedOptions.AmbientType == AmbientType.None
					? this.AmbientType
					: affectedOptions.AmbientType
			};
		}

		return (AffectedAmbient)mergedBase;
	}
}
