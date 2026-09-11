namespace UntitledRpgLogic.Core.Materials;

/// <summary>
///     Value object detailing electrical and magnetic conductivity traits for materials.
/// </summary>
/// <remarks>Owned by <see cref="MaterialDefinition" /> and serialized as JSON.</remarks>
public record ElectricalProperties
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ElectricalProperties" /> record with baseline defaults.
	/// </summary>
	public ElectricalProperties()
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ElectricalProperties" /> record with a specific conductivity measure.
	/// </summary>
	/// <param name="conductivity">Relative conductivity of the material (0.0 = perfect insulator, 1.0+ = high conductor).</param>
	public ElectricalProperties(float conductivity) => this.Conductivity = conductivity;

	/// <summary>
	///     A relative measure of how well the material conducts electrical current.
	///     0 indicates an absolute insulator; values above 1.0 represent high-efficiency conductors.
	/// </summary>
	public float Conductivity { get; init; } = 0.25f;
}
