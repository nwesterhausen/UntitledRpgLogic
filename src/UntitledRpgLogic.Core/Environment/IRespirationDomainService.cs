using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.Environment;

/// <summary>
///     Pure domain service evaluating gas concentrations, pressure thresholds, and entity respiration states.
/// </summary>
public interface IRespirationDomainService
{
	/// <summary>
	///     Evaluates the current atmospheric conditions against an entity's respiratory profile.
	/// </summary>
	/// <param name="profile">The entity's respiratory traits, requirements, and gas tolerances.</param>
	/// <param name="atmosphere">The local atmospheric gas composition and total pressure.</param>
	/// <returns>The evaluated <see cref="RespirationState" />.</returns>
	public RespirationState EvaluateRespiration(RespiratoryProfile profile, AtmosphereProfile atmosphere);

	/// <summary>
	///     Determines whether an entity is submerged based on elevation and local fluid column depth.
	/// </summary>
	/// <param name="entityElevation">The world elevation of the entity.</param>
	/// <param name="terrainElevation">The terrain floor elevation of the tile cell.</param>
	/// <param name="liquidDepth">The depth of the liquid column above the terrain floor.</param>
	/// <returns>
	///     <see langword="true" /> if the entity elevation is below the liquid surface; otherwise,
	///     <see langword="false" />.
	/// </returns>
	public bool IsSubmerged(short entityElevation, short terrainElevation, ushort liquidDepth);
}
