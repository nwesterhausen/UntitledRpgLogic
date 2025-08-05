namespace UntitledRpgLogic.LibraryFile;

/// <summary>
/// Represents an exception that is thrown when a file does not conform to the expected URPG file format.
/// </summary>
/// <remarks>This exception is typically used to indicate that a file being processed is invalid or corrupted
/// according to the URPG file format specification. Ensure that the file being provided adheres to the expected format
/// before attempting to process it.</remarks>
public class UrpglibFileFormatException : Exception
{
	/// <summary>
	/// Represents an exception that is thrown when an error occurs related to the URPG file format.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public UrpglibFileFormatException(string message) : base(message) { }
}
