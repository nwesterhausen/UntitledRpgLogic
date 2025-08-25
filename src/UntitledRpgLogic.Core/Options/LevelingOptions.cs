using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Common;

namespace UntitledRpgLogic.Core.Options;

/// <summary>
///     Options for configuring <see cref="IHasLeveling" />-derived objects.
/// </summary>
public record LevelingOptions
{
	/// <summary>
	///     The maximum level this object can achieve.
	/// </summary>
	public int? MaxLevel { get; init; }

	/// <summary>
	///     The primary scaling factor (A) for level progression.
	///     <para>
	///         <b>Influence:</b> This factor is typically used as a base multiplier or exponent in the experience formula.
	///         Increasing <c>ScalingFactorA</c> generally makes each level require more experience, steepening the curve.
	///     </para>
	/// </summary>
	public float? ScalingFactorA { get; init; }

	/// <summary>
	///     How many points to reach level 1 from level 0. Typically, this is 1, but can be adjusted for different scaling
	///     behaviors.
	/// </summary>
	public int? PointsForFirstLevel { get; init; }

	/// <summary>
	///     The secondary scaling factor (B) for level progression.
	///     <para>
	///         <b>Influence:</b> This factor is usually applied as an additive or multiplicative modifier to the experience
	///         formula.
	///         Increasing <c>ScalingFactorB</c> will further increase the experience required per level, often as a percentage
	///         or offset.
	///     </para>
	/// </summary>
	public float? ScalingFactorB { get; init; }

	/// <summary>
	///     The tertiary scaling factor (C) for level progression.
	///     <para>
	///         <b>Influence:</b> This factor is commonly used as an exponent or offset in polynomial or logarithmic scaling.
	///         Adjusting <c>ScalingFactorC</c> changes the curvature of the experience curve, especially for parabolic or
	///         logarithmic scaling types.
	///     </para>
	/// </summary>
	public float? ScalingFactorC { get; init; }

	/// <summary>
	///     The type of scaling curve to use for experience requirements (e.g., linear, parabolic, logarithmic).
	/// </summary>
	public ScalingCurveType? ScalingCurve { get; init; }
}
