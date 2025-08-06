using System.Drawing;
using UntitledRpgLogic.Core.Configuration;

namespace UntitledRpgLogic.Core.Classes;


/// <summary>
/// Represents state-specific properties of a material, such as color.
/// </summary>
public record StateSpecificProperties
{
	/// <summary>
	/// Gets the color of the material in this state.
	/// </summary>
	public Color Color { get; }

	/// <summary>
	/// Creates a <see cref="StateSpecificProperties"/> instance from a configuration object.
	/// </summary>
	/// <param name="config">The configuration object containing state-specific property values.</param>
	/// <returns>A new <see cref="StateSpecificProperties"/> instance.</returns>
	public static StateSpecificProperties FromConfig(StateSpecificPropertiesConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		var color = ColorTranslator.FromHtml(config.Color);
		return new StateSpecificProperties(color);
	}
	private StateSpecificProperties(Color color)
	{
		Color = color;
	}
}
