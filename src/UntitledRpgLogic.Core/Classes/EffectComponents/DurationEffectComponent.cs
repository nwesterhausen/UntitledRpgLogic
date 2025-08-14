using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes.EffectComponents;

/// <summary>
///     Represents a component that defines the duration of an effect.
/// </summary>
public class DurationEffectComponent : IEffectComponent
{
	/// <summary>
	///     Initializes a new instance of the <see cref="DurationEffectComponent" /> class.
	/// </summary>
	/// <param name="name">The name of this duration component.</param>
	/// <param name="durationInSeconds">The duration of the effect in seconds. Use -1 for permanent effects.</param>
	public DurationEffectComponent(string name, float durationInSeconds)
	{
		this.Name = new Name(name);
		this.DurationInSeconds = durationInSeconds;

		// Generate a new GUID for this component instance
		this.Identifier = Ulid.NewUlid();
	}

	/// <summary>
	///     The duration of the effect in seconds. If -1, the effect is permanent.
	/// </summary>
	public float DurationInSeconds { get; }

	/// <summary>
	///     Gets a value indicating whether the effect is permanent.
	/// </summary>
	public bool IsPermanent => this.DurationInSeconds < 0;

	/// <inheritdoc />
	public Ulid Identifier { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public EffectComponentType ComponentType => EffectComponentType.Duration;
}
