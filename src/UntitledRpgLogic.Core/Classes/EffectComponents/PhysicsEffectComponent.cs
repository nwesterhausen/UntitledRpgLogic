using System.Numerics;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes.EffectComponents;

/// <summary>
///     Represents a component that defines physics-related properties of an effect.
/// </summary>
public class PhysicsEffectComponent : IEffectComponent
{
	/// <summary>
	///     Initializes a new instance of the <see cref="PhysicsEffectComponent" /> class.
	/// </summary>
	/// <param name="name">The name of this physics component.</param>
	/// <param name="initialVelocity">The initial velocity of the effect.</param>
	/// <param name="acceleration">The acceleration of the effect.</param>
	/// <param name="mass">The mass of the effect, which can influence momentum and interaction with other physics objects.</param>
	public PhysicsEffectComponent(string name, Vector3 initialVelocity, Vector3 acceleration, float mass)
	{
		this.Name = new Name(name);
		this.InitialVelocity = initialVelocity;
		this.Acceleration = acceleration;
		this.Mass = mass;

		// Generate a new GUID for this component instance
		this.Guid = Guid.NewGuid();
		this.Id = Convert.ToBase64String(this.Guid.ToByteArray());
		this.ShortGuid = this.Guid.ToString("N")[..8].ToUpperInvariant();
	}

	/// <summary>
	///     The initial velocity of the effect (e.g., direction and speed).
	/// </summary>
	public Vector3 InitialVelocity { get; }

	/// <summary>
	///     The acceleration applied to the effect.
	/// </summary>
	public Vector3 Acceleration { get; }

	/// <summary>
	///     The mass of the effect.
	/// </summary>
	public float Mass { get; }

	/// <inheritdoc />
	public Guid Guid { get; }

	/// <inheritdoc />
	public string Id { get; }

	/// <inheritdoc />
	public string ShortGuid { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public EffectComponentType ComponentType => EffectComponentType.Physics;
}
