namespace UntitledRpgLogic.Core.Interfaces.Common;

/// <summary>
///     Interface for classes that have a tooltip string. Provides a way to access the tooltip string of the object.
/// </summary>
public interface IHasTooltipString
{
	/// <summary>
	///     Gets the tooltip string of the object. Should be short and concise, ideally one line.
	/// </summary>
	public string TooltipString { get; }

	/// <summary>
	///     Get a longer description for the tooltip, which can be used to provide more detailed information about the object.
	/// </summary>
	public string TooltipDescription { get; }
}
