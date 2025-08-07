using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes.EffectComponents;

/// <summary>
///     Represents a component that defines how an effect targets entities.
/// </summary>
public class TargetingEffectComponent : IEffectComponent
{
	/// <summary>
	///     Initializes a new instance of the <see cref="TargetingEffectComponent" /> class.
	/// </summary>
	/// <param name="name">The name of this targeting component.</param>
	/// <param name="targetType">The type of targeting mechanism.</param>
	/// <param name="range">The range of the effect (if applicable).</param>
	/// <param name="radius">The radius of the effect (if Area of Effect).</param>
	public TargetingEffectComponent(string name, TargetingType targetType, float? range = null, float? radius = null)
	{
		this.Name = new Name(name);
		this.TargetType = targetType;
		this.Range = range;
		this.Radius = radius;

		// Generate a new GUID for this component instance
		this.Identifier = Guid.NewGuid();
		this.Id = Convert.ToBase64String(this.Identifier.ToByteArray());
		this.ShortId = this.Identifier.ToString("N")[..8].ToUpperInvariant();
	}

	/// <summary>
	///     The type of targeting mechanism (e.g., Single, AreaOfEffect, Touch).
	/// </summary>
	public TargetingType TargetType { get; }

	/// <summary>
	///     The range of the effect in current dimension scale (if applicable).
	/// </summary>
	public float? Range { get; }

	/// <summary>
	///     The radius of the effect (if Area of Effect).
	/// </summary>
	public float? Radius { get; }

	/// <inheritdoc />
	public Guid Identifier { get; }

	/// <inheritdoc />
	public string Id { get; }

	/// <inheritdoc />
	public string ShortId { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public EffectComponentType ComponentType => EffectComponentType.Targeting;
}
