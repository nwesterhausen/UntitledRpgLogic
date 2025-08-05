using System.Text;
using System.Text.Json;

namespace UntitledRpgLogic.LibraryFile;

/// <summary>
///		Reads .urpglib files, providing access to the header, manifest, and payload stream.
/// </summary>
public static class UrpglibReader
{
	/// <summary>
	/// Reads the header and manifest of a .urpglib file, providing a package object for further processing.
	/// </summary>
	/// <param name="filePath">The path to the .urpglib file.</param>
	/// <param name="readPayloadIntoMemory">If true, the entire payload is read into a MemoryStream. If false, the underlying FileStream is used, which is more memory-efficient for large files.</param>
	/// <returns>A UrpgPackage object for accessing the file's contents.</returns>
	public static async Task<UrpglibPackage> ReadAsync(string filePath, bool readPayloadIntoMemory = false)
	{
		var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		using var reader = new BinaryReader(fileStream, Encoding.UTF8, leaveOpen: true);

		// 1. Read and Validate Header
		var magic = reader.ReadBytes(UrpglibConstants.MagicBytes.Length);
		if (!magic.SequenceEqual(UrpglibConstants.MagicBytes))
		{
			await fileStream.DisposeAsync().ConfigureAwait(false);
			throw new UrpglibFileFormatException("Invalid file signature. This is not a valid .urpglib file.");
		}

		var header = new UrpglibHeader
		{
			HeaderSchemaVersion = reader.ReadByte(),
			ManifestSchemaVersion = reader.ReadUInt16(),
			PayloadCompression = (PayloadCompressionType)reader.ReadByte(),
			PayloadSchemaVersion = reader.ReadUInt16(),
			ManifestLength = reader.ReadUInt32()
		};

		// --- Compatibility Checks ---
		if (header.HeaderSchemaVersion > UrpglibConstants.CurrentHeaderSchemaVersion)
		{
			await fileStream.DisposeAsync().ConfigureAwait(false);
			throw new UrpglibVersionMismatchException($"File header version ({header.HeaderSchemaVersion}) is newer than supported version ({UrpglibConstants.CurrentHeaderSchemaVersion}). Please update the application.");
		}

		// 2. Read and Deserialize Manifest
		var manifestJsonBytes = reader.ReadBytes((int)header.ManifestLength);
		var manifest = JsonSerializer.Deserialize<PackageManifest>(manifestJsonBytes);
		if (manifest == null)
		{
			await fileStream.DisposeAsync().ConfigureAwait(false);
			throw new UrpglibFileFormatException("Failed to deserialize package manifest.");
		}

		// 3. Prepare Payload Stream
		Stream payloadStream;
		if (readPayloadIntoMemory)
		{
			// Copy the rest of the file (the payload) into a memory stream
			var remainingBytes = reader.ReadBytes((int)(fileStream.Length - fileStream.Position));
			payloadStream = new MemoryStream(remainingBytes);
			await fileStream.DisposeAsync().ConfigureAwait(false); // We have the whole file in memory now
		}
		else
		{
			// The payload is the rest of the file stream. We leave the stream open.
			// The UrpgPackage.Dispose() method will be responsible for closing it.
			payloadStream = fileStream;
		}

		return new UrpglibPackage(header, manifest, payloadStream);
	}
}
