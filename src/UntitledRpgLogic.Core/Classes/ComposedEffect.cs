using UntitledRpgLogic.Core.Interfaces.Effects;
using UntitledRpgLogic.Core.Interfaces.Services;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents a concrete game effect or spell, composed of multiple modular effect components,
///     that can be explicitly activated.
/// </summary>
public class ComposedEffect : IActiveEffect
{
	private readonly List<IEffectComponent> components;
	private readonly IEffectApplicationService effectApplicationService; // Dependency for activating the effect

	/// <summary>
	///     Initializes a new instance of the <see cref="ComposedEffect" /> class.
	/// </summary>
	/// <param name="name">The name of the effect (e.g., "Fireball", "Featherfall").</param>
	/// <param name="components">The collection of micro-effect components that define this effect's behavior.</param>
	/// <param name="effectApplicationService">The service responsible for applying the effect's logic.</param>
	public ComposedEffect(
		string name,
		IEnumerable<IEffectComponent> components,
		IEffectApplicationService effectApplicationService)
	{
		this.Name = new Name(name);
		this.components = [.. components];
		this.effectApplicationService = effectApplicationService;

		// Generate a new GUID for this effect instance
		this.Identifier = Ulid.NewUlid();
	}

	/// <inheritdoc />
	public Ulid Identifier { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public IReadOnlyCollection<IEffectComponent> EffectComponents => this.components.AsReadOnly();

	/// <inheritdoc />
	public void Activate(EffectActivationContext context)
	{
		ArgumentNullException.ThrowIfNull(context, nameof(context));
		// Delegate the actual application logic to the EffectApplicationService
		this.effectApplicationService.ApplyEffect(this, context.Caster, context.Targets);
	}
}
