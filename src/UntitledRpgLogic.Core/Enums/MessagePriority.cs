namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines the processing priority of a network message.
/// </summary>
public enum MessagePriority
{
	/// <summary>
	///     Normal priority. Processed only if there is spare time in the frame.
	///     (e.g., cosmetic changes, player emotes).
	/// </summary>
	Normal,

	/// <summary>
	///     Important priority. Processed after Crucial messages, if the frame budget allows.
	///     (e.g., other player movements, animations).
	/// </summary>
	Important,

	/// <summary>
	///     Crucial priority. Always processed immediately at the start of the frame.
	///     (e.g., player taking damage, using an ability, receiving a vital quest item).
	/// </summary>
	Crucial
}
