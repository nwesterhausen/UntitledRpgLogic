using UntitledRpgLogic.LibraryFile;

namespace UntitledRpgLogic.Core.Data.Urpglib;

/// <summary>
///     Defines a service responsible for validating, unpacking, and ingesting .urpglib content packages into the
///     persistent catalog.
/// </summary>
public interface IPackageLoaderService
{
	/// <summary>
	///     Reads a .urpglib package from disk, validates compatibility, and persists all contained definitions into the
	///     database.
	/// </summary>
	/// <param name="filePath">Absolute or relative path to the .urpglib archive.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A summary of the definitions loaded and persisted.</returns>
	public Task<PackageManifest> IngestPackageAsync(string filePath, CancellationToken cancellationToken = default);

	/// <summary>
	///     Streams and ingests a .urpglib package from an arbitrary binary stream.
	/// </summary>
	/// <param name="packageStream">Readable stream containing the raw .urpglib data.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A summary of the definitions loaded and persisted.</returns>
	public Task<PackageManifest>
		IngestPackageAsync(Stream packageStream, CancellationToken cancellationToken = default);
}
