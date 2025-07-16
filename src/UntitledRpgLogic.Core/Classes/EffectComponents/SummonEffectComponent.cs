using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes.EffectComponents;

/// <summary>
///     Represents a component that defines properties for summoning effects.
///     This component uses MagicType to categorize its specific magic school.
/// </summary>
public class SummonEffectComponent : IEffectComponent
{
	/// <summary>
	///     Initializes a new instance of the <see cref="SummonEffectComponent" /> class.
	/// </summary>
	/// <param name="name">The name of this summoning component.</param>
	/// <param name="creatureTypeToSummon">The type or identifier of the creature to be summoned.</param>
	/// <param name="numberOfCreatures">The number of creatures to summon.</param>
	/// <param name="durationInSeconds">How long the summoned creatures persist (use -1 for permanent).</param>
	public SummonEffectComponent(string name, string creatureTypeToSummon, int numberOfCreatures,
		float durationInSeconds)
	{
		this.Name = new Name(name);
		this.CreatureTypeToSummon = creatureTypeToSummon;
		this.NumberOfCreatures = numberOfCreatures;
		this.DurationInSeconds = durationInSeconds;

		// Ensure this component is specifically for Summoning magic.
		this.MagicType = MagicType.Summoning;

		// Generate a new GUID for this component instance
		this.Guid = Guid.NewGuid();
		this.Id = Convert.ToBase64String(this.Guid.ToByteArray());
		this.ShortGuid = this.Guid.ToString("N")[..8].ToUpperInvariant();
	}

	/// <summary>
	///     The type of magic this component belongs to (should be MagicType.Summoning).
	/// </summary>
	public MagicType MagicType { get; }

	/// <summary>
	///     The identifier for the type of creature that will be summoned.
	/// </summary>
	public string CreatureTypeToSummon { get; }

	/// <summary>
	///     The number of creatures to be summoned.
	/// </summary>
	public int NumberOfCreatures { get; }

	/// <summary>
	///     The duration in seconds that the summoned creatures will persist.
	///     Use -1 for permanent summons.
	/// </summary>
	public float DurationInSeconds { get; }

	/// <inheritdoc />
	public Guid Guid { get; }

	/// <inheritdoc />
	public string Id { get; }

	/// <inheritdoc />
	public string ShortGuid { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public EffectComponentType ComponentType => EffectComponentType.Summoning; // New component type
}
