namespace UntitledRpgLogic.LibraryFile;

/// <summary>
/// Represents an exception that is thrown when a version mismatch is detected in a URPG file.
/// </summary>
/// <remarks>This exception is typically thrown when the version of a URPG file being processed does not match the
/// expected version. It indicates that the file format may be incompatible with the current implementation.</remarks>
public class UrpglibVersionMismatchException : UrpglibFileFormatException
{
	/// <summary>
	/// Represents an exception that is thrown when there is a version mismatch in the URPG schema versions between the file and the library.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public UrpglibVersionMismatchException(string message) : base(message) { }
}
